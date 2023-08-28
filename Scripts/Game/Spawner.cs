using Godot;
using System;

public class Spawner : Node
{
    [Export]
    private PackedScene StartScene { get; set; }
    [Export]
    private Random random {  get; set; }
    private ZoneStorage Zones { get; set; }
    private string LastZoneName { get; set; }
    public Zone lastScene { private set; get; }
    public override void _Ready()
    {
        base._Ready();
        random = new Random();
        Zones = GetNode<ZoneStorage>($"../../{nameof(ZoneStorage)}");
        LastZoneName = "Tunnel";
        lastScene = StartScene.Instance<Zone>();
        lastScene.init();
        lastScene.Position = Vector2.Zero;
        AddChild(lastScene);
        for (int i = 0; i < 2; i++)
        {
            AddZone();
        }
    }
    private void AddZone()
    {
        string name = "";
        while (true)
        {
            string s = Zones.ZonesNames[random.Next(Zones.ZonesNames.Length)];
            if (s != LastZoneName)
            {
                name = s;
                break;
            }
        }
        LastZoneName = name;
        Zone zone = Zones.Scenes[name][lastScene.Connection][random.Next(Zones.Scenes[name][lastScene.Connection].Length)].Instance<Zone>();
        zone.init();
        zone.Position = lastScene.EndPosition.GlobalPosition - (zone.StartPosition.Position - zone.Position) - new Vector2(10, 0);
        zone.Connect(nameof(Zone.ZoneDeleted), this, nameof(OnZoneDeleted));
        AddChild(zone);
        lastScene = zone;
    }
    private void OnZoneDeleted()
    {
        AddZone();
    }
}
