using Godot;
using System.Linq;
using Attributes;
using PlayerNamespace = Main.Game.Player;

namespace Main.Game.Zones
{
    public class HidingZonePart : Area2D
    {
        [Node(nameof(Tween))]
        private Tween tweener { get; set; }
        [Node(nameof(Sprite))]
        private Sprite image { get; set; }
        private Color startSpriteImageModulate { get; set; }
        private Color finalSpriteImageModulate { get; set; }
        public override void _Ready()
        {
            base._Ready();
            this.WireNodes();
            startSpriteImageModulate = image.Modulate;
            finalSpriteImageModulate = new Color(startSpriteImageModulate.r, startSpriteImageModulate.g, startSpriteImageModulate.b, 0f);
        }
        private void OnBodyEntered(Node body)
        {
            if (body is PlayerNamespace.Player)
            {
                StartInterpolating(startSpriteImageModulate, finalSpriteImageModulate);
            }
        }
        private void OnBodyExited(Node body)
        {
            if (body is PlayerNamespace.Player)
            {
                StartInterpolating(finalSpriteImageModulate, startSpriteImageModulate);
            }
        }
        private void StartInterpolating(Color startVal, Color finalVal)
        {
            tweener.InterpolateProperty(image, "modulate", startVal, finalVal, 1);
            tweener.Start();
        }
    }
}
