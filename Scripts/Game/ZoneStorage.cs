using Godot;
using Godot.Collections;

public class ZoneStorage : Node
{
    [Export]
    public Dictionary<string, Dictionary<bool, PackedScene[]>> Scenes { get; private set; }
    public string[] ZonesNames { get; private set; }
    public override void _Ready()
    {
        base._Ready();
        ZonesNames = new string[Scenes.Keys.Count];
        int i = 0;
        foreach (string name in Scenes.Keys)
        {
            ZonesNames[i] = name;
            i++;
        }
    }
}