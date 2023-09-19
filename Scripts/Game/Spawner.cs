using Godot;
using System;
using Attributes;
using Main.Game.Zones;

namespace Main.Game
{
    public class Spawner : Node
    {
        [Export]
        private PackedScene startScene { get; set; }
        [Node($"../../{nameof(ZoneStorage)}")]
        private ZoneStorage zoneStorage { get; set; }
        public Zone lastScene { private set; get; }
        private Random random { get; set; } = new Random();
        private ZoneNames lastZoneName { get; set; } = ZoneNames.Tunnel;
        public override void _Ready()
        {
            base._Ready();
            this.WireNodes();
            lastScene = startScene.Instance<Zone>();
            lastScene.init();
            lastScene.Position = Vector2.Zero;
            AddChild(lastScene);
            for (int i = 0; i < 2; i++)
            {
                AddZone();
            }
        }
        private void OnZoneDeleted()
        {
            AddZone();
        }
        private void AddZone()
        {
            ZoneNames name;
            while (true)
            {
                ZoneNames s = zoneStorage.zonesNames[random.Next(zoneStorage.zonesNames.Length)];
                if (s != lastZoneName)
                {
                    name = s;
                    break;
                }
            }
            lastZoneName = name;
            Zone zone = zoneStorage.scenes[name][lastScene.connection][random.Next(zoneStorage.scenes[name][lastScene.connection].Length)].Instance<Zone>();
            zone.init();
            zone.Position = lastScene.endPosition.GlobalPosition - (zone.startPosition.Position - zone.Position) - new Vector2(10, 0);
            zone.Connect(nameof(Zone.ZoneDeleted), this, nameof(OnZoneDeleted));
            AddChild(zone);
            lastScene = zone;
        }
    }
}
