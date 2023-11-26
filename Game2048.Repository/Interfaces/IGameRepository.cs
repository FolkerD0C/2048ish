using Game2048.Repository.Enums;
using Game2048.Repository.EventHandlers;
using Game2048.Shared.Enums;
using Game2048.Shared.Models;
using System;
using System.Collections.Generic;

namespace Game2048.Repository;

public interface IGameRepository : ISerializable
{
    public int RemainingLives { get; }
    public int RemainingUndos { get; }
    public int HighestNumber { get; }
    public int GridWidth { get; }
    public int GridHeight { get; }
    public string PlayerName { get; set; }
    public int Goal { get; }
    public IList<int>? AcceptedSpawnables { get; }
    IGameState CurrentGameState { get; }
    public LinkedList<IGameState> UndoChain { get; }
    public string MoveResultErrorMessage { get; }
    int GetScore();
    public MoveResult MoveGrid(MoveDirection input);
    public IGameState? Undo();
    public event EventHandler<GameRepositoryEventHappenedEventArgs>? GameRepositoryEventHappened;
}