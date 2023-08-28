using Godot;
using System;

public class HidingZonePart : Area2D
{
    private Tween Tweener { get; set; }
    private Sprite Image { get; set; }
    private Color StartSpriteImageModulate { get; set; }
    private Color FinalSpriteImageModulate { get; set; }
    public override void _Ready()
    {
        base._Ready();
        Image = GetNode<Sprite>(nameof(Sprite));
        StartSpriteImageModulate = Image.Modulate;
        FinalSpriteImageModulate = new Color(StartSpriteImageModulate.r, StartSpriteImageModulate.g, StartSpriteImageModulate.b, 0f);
        Tweener = GetNode<Tween>(nameof(Tween));
    }
    private void StartInterpolating(Color startVal, Color finalVal)
    {
        Tweener.InterpolateProperty(Image, "modulate", startVal, finalVal, 1);
        Tweener.Start();
    }
    private void OnBodyEntered(Node body)
    {
        if (body is Player)
        {
            StartInterpolating(StartSpriteImageModulate, FinalSpriteImageModulate);
        }
    }
    private void OnBodyExited(Node body)
    {
        if (body is Player)
        {
            StartInterpolating(FinalSpriteImageModulate, StartSpriteImageModulate);
        }
    }
}
