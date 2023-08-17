using Godot;
using System;

public class InGameInterface : Node
{
    [Signal]
    public delegate void PlayerKilled(int score);
    private Label ScoreLabel { set; get; }
    private int ScoreNumber { set; get; }
    public override void _Ready()
    {
        base._Ready();
        ScoreNumber = 0;
        ScoreLabel = GetNode<Label>("Label");
        ScoreLabel.Text = ScoreNumber.ToString();
    }
    private void OnTimeout()
    {
        ScoreNumber += 1;
        ScoreLabel.Text = ScoreNumber.ToString();
    }
    private void OnPlayerKilled()
    {
        EmitSignal(nameof(PlayerKilled), ScoreNumber);
    }
}
