using System;
using System.Collections.Generic;
using Game2048.Backend.Helpers.Enums;
using Game2048.Backend.Helpers.EventHandlers;
using Game2048.Backend.Models;

namespace Game2048.Backend.Repository;

public interface IGameRepository
{
    public int RemainingLives { get; }
    public int RemainingUndos { get; }
    public int HighestNumber { get; }
    public int GridWidth { get; }
    public int GridHeight { get; }
    public string PlayerName { get; }
    public int Goal { get; }
    public IList<int>? AcceptedSpawnables { get; }
    public LinkedList<IGamePosition> UndoChain { get; }
    public IGamePosition MoveGrid(MoveDirection input);
    public IGamePosition Undo();
    public void SetPlayerName(string playerName);
}