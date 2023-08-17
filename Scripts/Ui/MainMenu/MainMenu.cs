using Godot;
using System;

public class MainMenu : Node
{
    [Signal]
    public delegate void StartPressed();
    [Signal]
    public delegate void ScoresPressed();
    private void OnStartPressed()
    {
        EmitSignal(nameof(StartPressed));
        QueueFree();
    }
    private void OnScoresPressed()
    {
        EmitSignal(nameof(ScoresPressed));
        QueueFree();
    }
}
