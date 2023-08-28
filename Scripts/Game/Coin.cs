using Godot;
using System;

public class Coin : Area2D
{
    [Signal]
    public delegate void Gathered();
    private AnimatedSprite Animations { get; set; }
    public override void _Ready()
    {
        base._Ready();
        Animations = GetNode<AnimatedSprite>(nameof(AnimatedSprite));
    }
    private void OnBodyEntered(Node body)
    {
        if (body is Player)
        {
            EmitSignal(nameof(Gathered));
            Animations.Play("Gathered");
        }
    }
    private void OnAnimationFinished()
    {
        if (Animations.Animation == "Gathered")
        {
            QueueFree();
        }
    }


}
