using Godot;

namespace Main
{
    public class JavaScriptInterface : Node
    {
        private JavaScriptObject window { get; set; }
        public override void _Ready()
        {
            base._Ready();
            window = JavaScript.GetInterface("window");
        }
        public string GetUserName()
        {
            return window.Call("getParams").ToString();
        }
    }
}