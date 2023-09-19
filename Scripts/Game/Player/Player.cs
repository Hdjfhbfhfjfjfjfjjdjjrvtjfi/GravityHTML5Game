using Godot;
using Attributes;

namespace Main.Game.Player
{
    public class Player : KinematicBody2D
    {
        [Signal]
        public delegate void Killed();
        [Node(nameof(animations))]
        private AnimatedSprite animations { get; set; }
        private PlayerGravityState gravityState { get; set; } = PlayerGravityState.Down;
        public PlayerState state { get; private set; } = PlayerState.NotOnGround;
        [Export]
        private int gravityStrenght { get; set; }
        public override void _Ready()
        {
            base._Ready();
            this.WireNodes();
        }
        public override void _PhysicsProcess(float delta)
        {
            base._PhysicsProcess(delta);
            Vector2 velocity = new Vector2(0, gravityStrenght) * delta * (int)gravityState;
            KinematicCollision2D collision = MoveAndCollide(velocity);
            if (collision != null)
            {
                if (state == PlayerState.NotOnGround)
                {
                    state = PlayerState.OnGround;
                    animations.Animation = "Running";
                }
            }
            else
            {
                if (state == PlayerState.OnGround)
                {
                    state = PlayerState.NotOnGround;
                    animations.Animation = "Falling";
                }
            }
        }
        private void OnGravityChanged()
        {
            Scale = new Vector2(Scale.x, -Scale.y);
            state = PlayerState.NotOnGround;
            gravityState = gravityState == PlayerGravityState.Down ? PlayerGravityState.Up : PlayerGravityState.Down;
            state = PlayerState.NotOnGround;
            animations.Animation = "Falling";
        }
        private void OnHitBoxEntered(Node body)
        {
            if (body is TileMap || body is Area2D)
            {
                EmitSignal(nameof(Killed));
            }
        }
    }
}
