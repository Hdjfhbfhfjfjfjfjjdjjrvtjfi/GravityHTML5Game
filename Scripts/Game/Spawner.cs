using Godot;
using Godot.Collections;
using System;
using System.Net.NetworkInformation;

public class Spawner : Node
{
    [Export]
    private PackedScene StartScene { get; set; }
    [Export]
    private Dictionary<bool, PackedScene[]> Scenes { get; set; }
    private Dictionary<bool, int> ScenesLength { get; set; }
    private Random random {  get; set; }
    public Zone lastScene { private set; get; }
    public override void _Ready()
    {
        base._Ready();
        random = new Random();
        ScenesLength = new Dictionary<bool, int>
        {
            { true, Scenes[true].Length },
            { false, Scenes[false].Length }
        };
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
        Zone zone = Scenes[lastScene.Connection][random.Next(ScenesLength[lastScene.Connection])].Instance<Zone>();
        zone.init();
        zone.Position = lastScene.EndPosition.GlobalPosition - (zone.StartPosition.Position - zone.Position) - new Vector2(10, 0)      ;
        zone.Connect(nameof(Zone.ZoneDeleted), this, nameof(OnZoneDeleted));
        AddChild(zone);
        lastScene = zone;
    }
    private void OnZoneDeleted()
    {
        AddZone();
    }
}
