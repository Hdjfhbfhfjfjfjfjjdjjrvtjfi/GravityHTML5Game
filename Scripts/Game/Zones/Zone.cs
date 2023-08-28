using Godot;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Security.Cryptography;

public class Zone : Node2D
{
    [Signal]
    public delegate void ZoneDeleted();
    // False is down, true is up
    [Export]
    public bool Connection { private set; get; }
    public Position2D StartPosition { private set; get; }
    public Position2D EndPosition { private set; get; }
    public void init()
    {
        StartPosition = GetNode<Position2D>(nameof(StartPosition));
        EndPosition = GetNode<Position2D>(nameof(EndPosition));
    }
    public override void _Process(float delta)
    {
        base._Process(delta);
        if (EndPosition.GlobalPosition.x <= -10)
        {
            EmitSignal(nameof(ZoneDeleted));
            QueueFree();
        }
    }
    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
        Position -= new Vector2(200 , 0) * delta;
    }
}
