using Godot;
using Godot.Collections;
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
            zonesNames = new ZoneNames[scenes.Keys.Count];
            int i = 0;
            foreach (ZoneNames name in scenes.Keys)
            {
                zonesNames[i] = name;
                i++;
            }
        }
    }
}