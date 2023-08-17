using Godot;
using System;

public class Scores : Node
{
    [Signal]
    public delegate void ExitPressed();
    private string[] scoreArray { get; set; }
    public void init(string[,] scores)
    {
        scoreArray = new string[5];
        string text = null;
        for (int i = 0; i < 15; i++)
        {
            switch (i % 3)
            {
                case 0:
                    {
                        text = scores.GetValue(i).ToString();
                        break;
                    }
                case 1:
                    {
                        text += scores.GetValue(i).ToString();
                        break;
                    }
                case 2:
                    {
                        text += scores.GetValue(i).ToString();
                        scoreArray[i / 3] = text;
                        break;
                    }
            }
        }
    }
    public override void _Ready()
    {
        base._Ready();
        Node labelContainer = GetNode<Node>($"{nameof(VBoxContainer)}/{nameof(VBoxContainer)}/{nameof(VBoxContainer)}");
        for (int i = 1; i < 4; i++)
        {
            LabelComponent label = labelContainer.GetNode<LabelComponent>(i.ToString());
            label.Text.Text = scoreArray[i - 1];
        }
    }
    private void OnExitPressed()
    {
        EmitSignal(nameof(ExitPressed));
        QueueFree();
    }
}
