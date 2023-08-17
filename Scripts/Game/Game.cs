using Godot;
using System;

public class Game : Node
{
    [Signal]
    public delegate void GameEnded(int score);
    public Player player { get; private set; }
    public override void _Ready()
    {
        base._Ready();
        player = GetNode<Player>("Player");
    }
    private void OnPlayerKilled(int score)
    {
        EmitSignal(nameof(GameEnded), score);
        QueueFree();
    }
}
