using Godot;
using System;
using System.Drawing;

public class Zone : Node2D
{
    public Position2D EndPosition { private set; get; }
    public override void _Ready()
    {
        base._Ready();
        EndPosition = GetNode<Position2D>(nameof(Position2D));
    }
}
