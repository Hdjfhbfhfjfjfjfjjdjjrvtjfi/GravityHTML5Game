using Godot;
using System;

public class IntermediateZone : Zone
{
    [Export]
    private int TunnelSize { get; set; }
    public override void _Ready()
    {
        base._Ready();
    }
}
