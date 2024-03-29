﻿namespace Game2048.Processors.Enums;

/// <summary>
/// An enum that represents the result of a movement on the grid.
/// </summary>
public enum PostMoveResult
{
    NoError,
    NotGameEndingError,
    GameOverError,
    Unknown
}
