using Godot;
using Attributes;
using PlayerNamespace = Main.Game.Player;

namespace Main.Game
{
    public class Coin : Area2D
    {
        [Signal]
        public delegate void Gathered();
        [Node(nameof(AnimatedSprite))]
        private AnimatedSprite animations { get; set; }
        public override void _Ready()
        {
            base._Ready();
            this.WireNodes();
        }
        private void OnBodyEntered(Node body)
        {
            if (body is PlayerNamespace.Player)
            {
                EmitSignal(nameof(Gathered));
                animations.Play("Gathered");
            }
        }
        private void OnAnimationFinished()
        {
            if (animations.Animation == "Gathered")
            {
                QueueFree();
            }
        }
    }
}
