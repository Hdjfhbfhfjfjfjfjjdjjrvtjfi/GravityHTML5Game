using Godot;
using Main.UI.MainMenu;
using Main.UI.Scores;
using GameNamespace = Main.Game;
using Attributes;   

namespace Main 
{ 
    public class Main : Node
    {
        [Signal]
        public delegate void GameEnded(int score);
        [Export]
        private PackedScene mainMenuScene { get; set; }
        [Export]
        private PackedScene scoreScene { get; set; }
        [Export]
        private PackedScene gameScene { get; set; }
        [Node(nameof(JavaScriptInterface))]
        private JavaScriptInterface JSInterface { get; set; }
        [Node(nameof(HttpClient))]
        private HttpClient httpClient { get; set; }
        public override void _Ready()
        {
            base._Ready();
            this.WireNodes();
            CreateMainMenu();
        }
        private void OnScoresPressed()
        {
            httpClient.GetScore();
        }
        private void OnDataRecieved(string[,] scores)
        {
            var scorePage = scoreScene.Instance<Scores>();
            scorePage.init(scores);
            scorePage.Connect(nameof(Scores.ExitPressed), this, nameof(OnExitPressed));
            AddChild(scorePage);

        }
        private void OnStartPressed()
        {
            GameNamespace.Game game = gameScene.Instance<GameNamespace.Game>();
            game.Connect(nameof(GameNamespace.Game.GameEnded), this, nameof(OnGameEnded));
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
            MainMenu menu = mainMenuScene.Instance<MainMenu>();
            menu.Connect(nameof(MainMenu.ScoresPressed), this, nameof(OnScoresPressed));
            menu.Connect(nameof(MainMenu.StartPressed), this, nameof(OnStartPressed));
            AddChild(menu);
        }
    }
}