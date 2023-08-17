using Godot;
using System;

public class TouchTracker : Area2D
{
    [Signal]
    public delegate void GravityChanged();
    public override void _Ready()
    {
        base._Ready();
    }
    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        bool CanGhangeGravity = @event is InputEventMouseButton && @event.IsPressed() && GetParent<Game>().player.State is PlayerState.OnGround;
        if (CanGhangeGravity)
        {
            EmitSignal(nameof(GravityChanged));
        }
    }
}
