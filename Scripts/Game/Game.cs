using Godot;
using Attributes;
using PlayerNamespace = Main.Game.Player;

namespace Main.Game
{
    public class Game : Node
    {
        [Signal]
        public delegate void GameEnded(int score);
        [Node(nameof(Player))]
        public PlayerNamespace.Player player { get; private set; }
        public override void _Ready()
        {
            base._Ready();
            this.WireNodes();
        }
        private void OnPlayerKilled(int score)
        {
            EmitSignal(nameof(GameEnded), score);
            QueueFree();
        }
    }
}
