namespace Game2048.Classes;

class GridInstance
{
    int [,] grid;
    public int[,] Grid
    {
        get
        {
            return grid;
        }
        protected set
        {
            grid = value;
        }
    }

    int score;
    public int Score
    {
        get
        {
            return score;
        }
        protected set
        {
            if (value == score)
            {
                ScoreQueue.Enqueue(null);
            }
            else
            {
                ScoreQueue.Enqueue(value);
            }
            score = value;
        }
    }

    Queue<(int Vertical, int Horizontal, int Value)> MoveQueue;

    Queue<int?> ScoreQueue;

    public event Action Reached2048;

    public event Action<Queue<(int Vertical, int Horizontal, int Value)>, Queue<int?>> UpdateHappened;

    public GridInstance()
    {
        grid = new int[4, 4];
        MoveQueue = new Queue<(int Vertical, int Horizontal, int Value)>();
        ScoreQueue = new Queue<int?>();
    }

    public GridInstance(int score) : this()
    {
        this.score = score;
    }

    GridInstance CopyGrid()
    {
        GridInstance copycat = new GridInstance(Score);
        for (int i = 0; i < copycat.Grid.GetLength(0); i++)
        {
            for (int j = 0; j < copycat.Grid.GetLength(1); j++)
            {
                copycat.Grid[i, j] = this.Grid[i, j];
            }
        }
        return copycat;
    }

    public void UpdateField(int vertical, int horizontal, int value)
    {
        Grid[vertical, horizontal] = value;
    }

    void SetField(int vertical, int horizontal, int value)
    {
        Grid[vertical, horizontal] = value;
        MoveQueue.Enqueue((vertical, horizontal, value));
        if (value == 2048)
        {
            Reached2048?.Invoke();
        }
    }

    public void CheckIfCanMove()
    {
        int canMove = 4;
        foreach (MoveDirection direction in Enum.GetValues(typeof(MoveDirection)))
        {
            GridInstance copycat = CopyGrid();
            try
            {
                copycat.SimulateMotion(direction, false);
            }
            catch (CannotMoveException)
            {
                canMove -= 1;
            }
        }
        if (canMove <= 0)
        {
            throw new GridStuckException();
        }
    }

    public GridInstance Move(MoveDirection? direction,
            Action<Queue<(int Vertical, int Horizontal, int Value)>, Queue<int?>> updateInstance,
            Action? reach2048Func)
    {
        GridInstance copycat = CopyGrid();
        if (reach2048Func != null)
        {
            copycat.Reached2048 += reach2048Func;
        }
        copycat.UpdateHappened += updateInstance;
        return copycat.SimulateMotion(direction, true);
    }

    GridInstance SimulateMotion(MoveDirection? direction, bool updating)
    {
        switch (direction)
        {
            case MoveDirection.Up:
                {
                    MoveUp();
                    break;
                }
            case MoveDirection.Down:
                {
                    MoveDown();
                    break;
                }
            case MoveDirection.Left:
                {
                    MoveLeft();
                    break;
                }
            case MoveDirection.Right:
                {
                    MoveRight();
                    break;
                }
        }
        if (MoveQueue.Count == 0)
        {
            throw new CannotMoveException();
        }
        if (updating)
        {
            UpdateHappened?.Invoke(MoveQueue, ScoreQueue);
            MoveQueue = new Queue<(int Vertical, int Horizontal, int Value)>();
            ScoreQueue = new Queue<int?>();
        }
        return this;
    }

    void MoveDown()
    {
        for (int i = 0; i < Grid.GetLength(1); i++)
        {
            for (int j = 2; j > -1; j--)
            {
                int k = j;
                while (k < 3 && ZeroLogic(k, i, k + 1, i))
                {
                    int numberToMove = Grid[k, i];
                    SetField(k, i, 0);
                    Score += 0;
                    SetField(k + 1, i, numberToMove);
                    Score += 0;
                    k++;
                }
            }
            for (int j = 2; j > -1; j--)
            {
                if (AdditionLogic(j, i, j + 1, i))
                {
                    int numberToMove = Grid[j, i];
                    SetField(j, i, 0);
                    Score += 0;
                    SetField(j + 1, i, numberToMove * 2);
                    Score += numberToMove * 2;
                    j--;
                }
            }
            for (int j = 1; j > -1; j--)
            {
                int k = j;
                while (k < 2 && ZeroLogic(k, i, k + 1, i))
                {
                    int numberToMove = Grid[k, i];
                    SetField(k, i, 0);
                    SetField(k + 1, i, numberToMove);
                    k++;
                }
            }
        }
    }

    void MoveUp()
    {
        for (int i = 0; i < Grid.GetLength(1); i++)
        {
            for (int j = 1; j < Grid.GetLength(0); j++)
            {
                int k = j;
                while (k > 0 && ZeroLogic(k, i, k - 1, i))
                {
                    int numberToMove = Grid[k, i];
                    SetField(k, i, 0);
                    Score += 0;
                    SetField(k - 1, i, numberToMove);
                    Score += 0;
                    k--;
                }
            }
            for (int j = 1; j < Grid.GetLength(0); j++)
            {
                if (AdditionLogic(j, i, j - 1, i))
                {
                    int numberToMove = Grid[j, i];
                    SetField(j, i, 0);
                    Score += 0;
                    SetField(j - 1, i, numberToMove * 2);
                    Score += numberToMove * 2;
                    j++;
                }
            }
            for (int j = 2; j < Grid.GetLength(0); j++)
            {
                int k = j;
                while (k > 1 && ZeroLogic(k, i, k - 1, i))
                {
                    int numberToMove = Grid[k, i];
                    SetField(k, i, 0);
                    SetField(k - 1, i, numberToMove);
                    k--;
                }
            }
        }
    }

    void MoveLeft()
    {
        for (int i = 0; i < Grid.GetLength(0); i++)
        {
            for (int j = 1; j < Grid.GetLength(1); j++)
            {
                int k = j;
                while (k > 0 && ZeroLogic(i, k, i, k - 1))
                {
                    int numberToMove = Grid[i, k];
                    SetField(i, k, 0);
                    Score += 0;
                    SetField(i, k - 1, numberToMove);
                    Score += 0;
                    k--;
                }
            }
            for (int j = 1; j < Grid.GetLength(1); j++)
            {
                if (AdditionLogic(i, j, i, j - 1))
                {
                    int numberToMove = Grid[i, j];
                    SetField(i, j, 0);
                    Score += 0;
                    SetField(i, j - 1, numberToMove * 2);
                    Score += numberToMove * 2;
                    j++;
                }
            }
            for (int j = 2; j < Grid.GetLength(1); j++)
            {
                int k = j;
                while (k > 1 && ZeroLogic(i, k, i, k - 1))
                {
                    int numberToMove = Grid[i, k];
                    SetField(i, k, 0);
                    SetField(i, k - 1, numberToMove);
                    k--;
                }
            }
        }
    }

    void MoveRight()
    {
        for (int i = 0; i < Grid.GetLength(0); i++)
        {
            for (int j = 2; j > -1; j--)
            {
                int k = j;
                while (k < 3 && ZeroLogic(i, k, i, k + 1))
                {
                    int numberToMove = Grid[i, k];
                    SetField(i, k, 0);
                    Score += 0;
                    SetField(i, k + 1, numberToMove);
                    Score += 0;
                    k++;
                }
            }
            for (int j = 2; j > -1; j--)
            {
                if (AdditionLogic(i, j, i, j + 1))
                {
                    int numberToMove = Grid[i, j];
                    SetField(i, j, 0);
                    Score += 0;
                    SetField(i, j + 1, numberToMove * 2);
                    Score += numberToMove * 2;
                    j--;
                }
            }
            for (int j = 1; j > -1; j--)
            {
                int k = j;
                while (k < 2 && ZeroLogic(i, k, i, k + 1))
                {
                    int numberToMove = Grid[i, k];
                    SetField(i, k, 0);
                    SetField(i, k + 1, numberToMove);
                    k++;
                }
            }
        }
    }

    bool AdditionLogic(int currentVerticalPosition, int currentHorizontalPosition,
            int nextVerticalPosition, int nextHorizontalPosition)
    {
        return Grid[currentVerticalPosition, currentHorizontalPosition] != 0 &&
            Grid[nextVerticalPosition, nextHorizontalPosition] ==
            Grid[currentVerticalPosition, currentHorizontalPosition];
    }

    bool ZeroLogic(int currentVerticalPosition, int currentHorizontalPosition,
            int nextVerticalPosition, int nextHorizontalPosition)
    {
        return Grid[nextVerticalPosition, nextHorizontalPosition] == 0 &&
            Grid[currentVerticalPosition, currentHorizontalPosition] != 0;
    }
}