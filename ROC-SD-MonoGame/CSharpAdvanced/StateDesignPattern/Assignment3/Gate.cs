using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ROC_SD_MonoGame.CSharpAdvanced.StateDesignPattern.Assignment3
{
    public class Gate : GameObject
    {
        private Player _player;
        private Game1 _game;

        private SceneBase _scene;
        private string _sceneName;

        private Gate _connectedGate;

        public Gate ConnectedGate
        {
            get => _connectedGate;
            set=> _connectedGate = value;
        }

        public Gate(Vector2 pPosition, Player pPlayer, Game1 pGame, string pSceneName) : base(pPosition)
        {
            _player = pPlayer;
            _game = pGame;
            _sceneName = pSceneName;
        }

        public Gate(Vector2 pPosition, Player pPlayer, Game1 pGame, SceneBase pScene) : base(pPosition)
        {
            _player = pPlayer;
            _game = pGame;

            _scene = pScene;
        }


        public override void LoadContent(ContentManager pContent, Viewport pViewPort)
        {
            Texture = pContent.Load<Texture2D>("Gate");
        }

        public override void Update(GameTime pTime)
        {
            if (Collision(_player))
            {
                if (_scene != null)
                    _game.ChangeSceneTo(_scene);
                else if (_sceneName != null)
                    _game.ChangeSceneTo(_sceneName);

                if (_connectedGate != null)
                    _player.Position = _connectedGate.Position + new Vector2(0, 100);
            }
        }
    }
}
