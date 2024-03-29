using Game2048.Managers.Enums;
using Game2048.Managers.EventHandlers;
using Game2048.Processors;
using System;

namespace Game2048.Managers;

/// <summary>
/// Represents a set of methods for handling an active play and also a
/// set of events and properties that contain information about an active play.
/// </summary>
public interface IPlayInstanceManager
{
    /// <summary>
    /// The play processor object that stores all playdata
    /// </summary>
    IPlayProcessor Processor { get; }

    /// <summary>
    /// Triggers events before a play starts.
    /// </summary>
    void Start();

    /// <summary>
    /// Triggers events after a pley ends.
    /// </summary>
    void End();

    /// <summary>
    /// Handles a game input.
    /// </summary>
    /// <param name="input">The input as a <see cref="GameInput"/> enum.</param>
    /// <returns>The result of an input as an <see cref="InputResult"/>.</returns>
    InputResult HandleInput(GameInput input);
    /// <summary>
    /// The ID of the play instance.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// The current score of the play.
    /// </summary>
    int PlayerScore { get; }

    /// <summary>
    /// The player name of the current play.
    /// </summary>
    string PlayerName { get; set; }

    /// <summary>
    /// An event that triggers when a play is started.
    /// </summary>
    event EventHandler<PlayStartedEventArgs>? PlayStarted;

    /// <summary>
    /// An event that triggers when a move happens.
    /// </summary>
    event EventHandler<MoveHappenedEventArgs>? MoveHappened;

    /// <summary>
    /// An event that triggers when an undo happens.
    /// </summary>
    event EventHandler<UndoHappenedEventArgs>? UndoHappened;

    /// <summary>
    /// An event that triggers when an error happens.
    /// </summary>
    event EventHandler<ErrorHappenedEventArgs>? ErrorHappened;

    /// <summary>
    /// An event that triggers when a misc event happens. For misc event types, see
    /// <seealso cref="Enums.MiscEvent"/>
    /// </summary>
    event EventHandler<MiscEventHappenedEventArgs>? MiscEventHappened;

    /// <summary>
    /// An event that triggers when the player changes their name.
    /// </summary>
    event EventHandler<PlayerNameChangedEventArgs>? PlayerNameChanged;

    /// <summary>
    /// An event that triggers after an input was processed. Triggers after one ore more of the following events have been triggered:<br/>
    /// - <see cref="MoveHappened"/><br/>
    /// - <see cref="UndoHappened"/><br/>
    /// - <see cref="ErrorHappened"/><br/>
    /// - <see cref="MiscEventHappened"/>
    /// </summary>
    event EventHandler? InputProcessingFinished;

    /// <summary>
    /// An event that triggers when a play ends.
    /// </summary>
    event EventHandler? PlayEnded;
}