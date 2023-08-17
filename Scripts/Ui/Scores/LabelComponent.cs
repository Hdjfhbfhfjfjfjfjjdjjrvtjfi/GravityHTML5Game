using Godot;
using System;

public class LabelComponent : MarginContainer
{
    public Label Text { get; private set; }
    public override void _Ready()
    {
        base._Ready();
        Text = GetNode<Label>("Label");
    }
}
