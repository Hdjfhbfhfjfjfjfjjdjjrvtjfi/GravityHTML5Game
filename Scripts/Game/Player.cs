using Godot;
using System;
using System.Runtime.CompilerServices;

public class Player : RigidBody2D
{
    private void OnGroundEntered()
    {
        GD.Print(1);
    }
}
