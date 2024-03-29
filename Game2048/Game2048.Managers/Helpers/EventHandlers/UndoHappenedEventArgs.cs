using _2048ish.Base.Models;
using System;

namespace Game2048.Managers.EventHandlers;

/// <summary>
/// A class used for event handling that stores information about an undo happened on the playing grid.
/// </summary>
public class UndoHappenedEventArgs : EventArgs
{
    /// <summary>
    /// The new state of the playing grid.
    /// </summary>
    public GameState State { get; }

    /// <summary>
    /// Creates a new instance of the <see cref="UndoHappenedEventArgs"/> class.
    /// </summary>
    /// <param name="state">The new state of the playing grid.</param>
    public UndoHappenedEventArgs(GameState state)
    {
        State = state;
    }
}