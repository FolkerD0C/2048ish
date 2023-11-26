using ConsoleClient.Display;
using Game2048.Shared.EventHandlers;
using System;

namespace ConsoleClient.AppUI.Play;

public interface IGameDisplay : IOverLay
{
    void OnPlayStarted(object? sender, PlayStartedEventArgs args);
    void OnMoveHappened(object? sender, MoveHappenedEventArgs args);
    void OnUndoHappened(object? sender, UndoHappenedEventArgs args);
    void OnErrorHappened(object? sender, ErrorHappenedEventArgs args);
    void MiscEventHappenedDispatcher(object? sender, MiscEventHappenedEventArgs args);
    void OnPlayerNameChanged(object? sender, PlayerNameChangedEventArgs args);
    void OnPlayEnded(object? sender, EventArgs args);
}