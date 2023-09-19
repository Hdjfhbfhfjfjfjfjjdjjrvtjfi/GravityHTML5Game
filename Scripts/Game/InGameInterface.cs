using Godot;
using Attributes;

namespace Main.Game
{
    public class InGameInterface : Node
    {
        [Signal]
        public delegate void PlayerKilled(int score);
        [Node(nameof(Label))]
        private Label scoreLabel { set; get; }
        private int scoreNumber { set; get; } = 0;
        public override void _Ready()
        {
            base._Ready();
            this.WireNodes();
            scoreLabel.Text = scoreNumber.ToString();
        }
        private void OnTimeout()
        {
            scoreNumber += 1;
            scoreLabel.Text = scoreNumber.ToString();
        }
        private void OnPlayerKilled()
        {
            EmitSignal(nameof(PlayerKilled), scoreNumber);
        }
    }
}
