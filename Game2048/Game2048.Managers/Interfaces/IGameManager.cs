using _2048ish.Base.Models;
using Game2048.Managers.Models;
using System;
using System.Collections.Generic;

namespace Game2048.Managers;

/// <summary>
/// Represents a high level manager for the game. Handles playing, saving, loading, high scores and gets the game description.
/// </summary>
public interface IGameManager
{
    /// <summary>
    /// Gets an <see cref="IPlayInstanceManager"/> that can be used for a new game.
    /// </summary>
    /// <param name="gameConfiguration">The configuration to use when starting and playing the game.</param>
    /// <returns>An <see cref="IPlayInstanceManager"/> suitable for a new game.</returns>
    public IPlayInstanceManager NewGame(NewGameConfiguration gameConfiguration);

    /// <summary>
    /// Gets an <see cref="IPlayInstanceManager"/> that can be used for a loaded game.
    /// </summary>
    /// <param name="saveGameName">The name of the saved game.</param>
    /// <returns>An <see cref="IPlayInstanceManager"/> suitable for a loaded game.</returns>
    public IPlayInstanceManager LoadGame(string saveGameName);

    /// <summary>
    /// Gets the name of all saved games.
    /// </summary>
    /// <returns>An <see cref="IEnumerable{string}"/> that contains the name of all saved games.</returns>
    public IEnumerable<string> GetSavedGames();

    /// <summary>
    /// Performs a save action on play specified by <paramref name="playId"/>.
    /// </summary>
    /// <param name="playId">The ID of the play to save.</param>
    /// <returns>The result of the save action as a <see cref="SaveResult"/>.</returns>
    public SaveResult SaveGame(Guid playId);

    /// <summary>
    /// Gets all high scores.
    /// </summary>
    /// <returns>An <see cref="IList{IHighscore}"/> that contains all high scores.</returns>
    public List<Highscore> GetHighscores();

    /// <summary>
    /// Adds a new high scores to the existing ones. Migth delete another if there are too many.
    /// </summary>
    /// <param name="playerName">The name of the player who set a high score.</param>
    /// <param name="score">The score that has been set by <paramref name="playerName"/>.</param>
    public void AddHighscore(string playerName, int score);

    /// <summary>
    /// Gets the description of the game.
    /// </summary>
    /// <returns>The description of the game as a <see cref="string"/>.</returns>
    public string GetGameDescription();
}