using Godot;
using System;

public class JavaScriptInterface : Node
{
    private JavaScriptObject Window { get; set; }
    public override void _Ready()
    {
        base._Ready();
        Window = JavaScript.GetInterface("window");
    }
    public string GetUserName()
    {
        return (string)Window.Call("getParams");
    }
}
