using Godot;
using System;

public class Main : Node
{
    [Signal]
    public delegate void GameEnded(int score);
    [Export]
    private PackedScene MainMenuScene { get; set; }
    [Export]
    private PackedScene ScoreScene { get; set; }
    [Export]
    private PackedScene GameScene { get; set; }
    private JavaScriptInterface JSInterface { get; set; }
    private HttpClient Client { get; set; }
    public override void _Ready()
    {
        base._Ready();
        Client = GetNode<HttpClient>(nameof(HttpClient));
        JSInterface = GetNode<JavaScriptInterface>(nameof(JavaScriptInterface));
        CreateMainMenu();
    }
    private void OnScoresPressed()
    {
        Client.GetScore();
    }
    private void OnDataRecieved(string[,] scores)
    {
        var scorePage = ScoreScene.Instance<Scores>();
        scorePage.init(scores);
        scorePage.Connect(nameof(Scores.ExitPressed), this, nameof(OnExitPressed));
        AddChild(scorePage);

    }
    private void OnStartPressed()
    {
        Game game = GameScene.Instance<Game>();
        game.Connect(nameof(Game.GameEnded), this, nameof(OnGameEnded));
        AddChild(game);
    }
    private void OnExitPressed()
    {
        CreateMainMenu();
    }
    private void OnGameEnded(int score)
    {
        EmitSignal(nameof(GameEnded), JSInterface.GetUserName(), score);
        CreateMainMenu();
    }
    private void CreateMainMenu()
    {
        MainMenu menu = MainMenuScene.Instance<MainMenu>();
        menu.Connect(nameof(MainMenu.ScoresPressed), this, nameof(OnScoresPressed));
        menu.Connect(nameof(MainMenu.StartPressed), this, nameof(OnStartPressed));
        AddChild(menu);
    }
}
