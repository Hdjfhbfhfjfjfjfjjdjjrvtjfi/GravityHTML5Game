using Godot;
using System;

public class Wall : Sprite
{
    [Signal]
    private delegate void PlayerOnGround();

    private void OnBodyEntered(Node body)
    {
        try
        {
            Player player = (Player)body;
            EmitSignal(nameof(Wall.PlayerOnGround));
        }
        catch { }

    }

}
