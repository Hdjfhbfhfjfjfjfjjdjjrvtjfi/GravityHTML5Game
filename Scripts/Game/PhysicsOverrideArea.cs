using Godot;
using System;

public class PhysicsOverrideArea : Area2D
{
    public bool IsPlayerOnGround { get; set; }
    public override void _Ready()
    {
        base._Ready();
        IsPlayerOnGround = false;
    }
    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        if (@event is InputEventMouseButton && @event.IsPressed() && IsPlayerOnGround)
        {
            GravityVec = new Vector2(-GravityVec.x, 0);
            IsPlayerOnGround = false;
        }
    }
    private void OnPlayerOnGround()
    {
        IsPlayerOnGround = true;
    }
}
