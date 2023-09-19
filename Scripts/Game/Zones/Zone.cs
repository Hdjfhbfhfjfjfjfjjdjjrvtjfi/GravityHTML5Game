using Attributes;
using Godot;

namespace Main.Game.Zones
{
    public class Zone : Node2D
    {
        [Signal]
        public delegate void ZoneDeleted();
        // False is down, true is up
        [Export]
        public bool connection { private set; get; }
        [Node(nameof(startPosition))]
        public Position2D startPosition { private set; get; }
        [Node(nameof(endPosition))]
        public Position2D endPosition { private set; get; }
        public void init()
        {
            this.WireNodes();  
        }
        public override void _Process(float delta)
        {
            base._Process(delta);
            if (endPosition.GlobalPosition.x <= -10)
            {
                EmitSignal(nameof(ZoneDeleted));
                QueueFree();
            }
        }
        public override void _PhysicsProcess(float delta)
        {
            base._PhysicsProcess(delta);
            Position -= new Vector2(200, 0) * delta;
        }
    }
}
