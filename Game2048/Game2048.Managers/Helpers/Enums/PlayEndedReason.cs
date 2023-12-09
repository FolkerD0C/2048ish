namespace Game2048.Managers.Enums;

/// <summary>
/// An enum that represents the reason of the ending of the current play.
/// </summary>
public enum PlayEndedReason
{
    GameOver,
    ExitPlay,
    QuitGame,
    PlayNotInitialized,
    Unknown
}