using Godot;
using Godot.Collections;
using System.Linq;
using Main.Game.Zones;

namespace Main.Game
{
    public class ZoneStorage : Node
    {
        [Export]
        public Dictionary<ZoneNames, Dictionary<bool, PackedScene[]>> scenes { get; private set; }
        public ZoneNames[] zonesNames { get; private set; }
        public override void _Ready()
        {
            base._Ready();
            zonesNames = (from key in scenes.Keys
                          select key).ToArray();
        }
    }
}