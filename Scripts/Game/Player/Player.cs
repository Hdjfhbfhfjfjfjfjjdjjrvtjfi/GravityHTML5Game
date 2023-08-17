using Godot;
using System.Net.NetworkInformation;

public class Player : KinematicBody2D
{
    [Signal]
    public delegate void Killed();
    [Export]
    private int GravityStrenght { get; set; }
    private PlayerGravityState GravityState { get; set; }
    public PlayerState State { get; private set; }
    private AnimatedSprite Animations { get; set; }
    public override void _Ready()
    {
        base._Ready();
        GravityState = PlayerGravityState.Down;
        State = PlayerState.NotOnGround;
        Animations = GetNode<AnimatedSprite>(nameof(Animations));
    }
    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
        Vector2 velocity = new Vector2(0, GravityStrenght) * delta * (int)GravityState;
        KinematicCollision2D collision = MoveAndCollide(velocity);   
        if (collision != null)
        {
            State = PlayerState.OnGround;
            Animations.Animation = "Running";
        }
    }
    private void OnGravityChanged()
    {
        Scale = new Vector2(Scale.x, -Scale.y);
        State = PlayerState.NotOnGround;
        GravityState = GravityState == PlayerGravityState.Down ? PlayerGravityState.Up : PlayerGravityState.Down;
        State = PlayerState.NotOnGround;
        Animations.Animation = "Falling";
    }
    private void OnHitBoxEntered(Node body)
    {
        if (body is TileMap || body is Area2D)
        {
            EmitSignal(nameof(Killed));
        }
    }
}
