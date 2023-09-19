using Godot;
using Attributes;

namespace Main.UI.Scores
{
    public class LabelComponent : MarginContainer
    {
        [Node(nameof(Label))]
        public Label text { get; private set; }
        public override void _Ready()
        {
            base._Ready();
            this.WireNodes();
        }
    }
}
