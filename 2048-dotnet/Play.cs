namespace Game2048;

class Play
{
    Display display;

    Repository repository;

    public Play()
    {
        Dictionary<int, (ConsoleColor Fg, ConsoleColor Bg)> colorSet = new Dictionary<int, (ConsoleColor Fg, ConsoleColor Bg)>();
        ConsoleColor[] fgColors = new ConsoleColor[]
        {
            ConsoleColor.Green, ConsoleColor.Blue, ConsoleColor.Red,
            ConsoleColor.Green, ConsoleColor.Blue, ConsoleColor.Red,
            ConsoleColor.Green, ConsoleColor.Blue, ConsoleColor.Red,
            ConsoleColor.Black, ConsoleColor.Black, ConsoleColor.Black,
            ConsoleColor.White, ConsoleColor.White, ConsoleColor.White,
            ConsoleColor.White, ConsoleColor.White
        };
        ConsoleColor[] bgColors = new ConsoleColor[]
        {
            ConsoleColor.Gray, ConsoleColor.Gray, ConsoleColor.Gray,
            ConsoleColor.White, ConsoleColor.White, ConsoleColor.White,
            ConsoleColor.DarkGray, ConsoleColor.DarkGray, ConsoleColor.DarkGray,
            ConsoleColor.Green, ConsoleColor.Blue, ConsoleColor.Red,
            ConsoleColor.Green, ConsoleColor.Blue, ConsoleColor.Red,
            ConsoleColor.Red, ConsoleColor.Red
        };
        colorSet.Add(0, (ConsoleColor.White, ConsoleColor.Black));
        for (int i = 0; i < fgColors.Length; i++)
        {
            colorSet.Add((int)Math.Pow(2, i + 1), (fgColors[i], bgColors[i]));
        }
        display = new Display(colorSet);
        display.InitializeDisplay();
        repository = new Repository(display);
    }

    bool HandleInput()
    {
        MoveDirection? input = null;
        switch(Console.ReadKey().Key)
        {
            case ConsoleKey.W: case ConsoleKey.UpArrow:
                {
                    input = MoveDirection.Up;
                    break;
                }
            case ConsoleKey.S: case ConsoleKey.DownArrow:
                {
                    input = MoveDirection.Down;
                    break;
                }
            case ConsoleKey.A: case ConsoleKey.LeftArrow:
                {
                    input = MoveDirection.Left;
                    break;
                }
            case ConsoleKey.D: case ConsoleKey.RightArrow:
                {
                    input = MoveDirection.Right;
                    break;
                }
            case ConsoleKey.Backspace:
                {
                    repository.Undo();
                    break;
                }
            case ConsoleKey.Escape:
                {
                    break;
                }
        }
        if (input == null)
        {
            return false;
        }
        try
        {
            repository.Move(input, display);
        }
        catch (CannotMoveException)
        {
            return false;
        }
        return true;
    }

    void GameOver()
    {

    }

    void GameWon()
    {

    }

    public void Run()
    {
        while (true)
        {
            if (HandleInput())
            {
                repository.AddNewTile();
            }
        }
    }

    void Save()
    {

    }
}
