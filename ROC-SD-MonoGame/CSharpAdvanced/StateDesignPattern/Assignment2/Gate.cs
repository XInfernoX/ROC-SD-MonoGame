using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CSharpAdvanced.CSharpAdvanced.StateDesignPattern.Assignment2
{
    public class Gate : GameObject
    {
        private Player _player;
        private Game1 _game;
        private GameState _state;

        private Gate _connectedGate;

        public Gate(Vector2 pPosition, Player pPlayer, Game1 pGame, GameState pState) : base(pPosition)
        {
            _player = pPlayer;
            _game = pGame;
            _state = pState;
        }

        public override void LoadContent(ContentManager pContent)
        {
            Texture = pContent.Load<Texture2D>("Gate");
        }

        public void SetConnectedGate(Gate pConnectedGate)
        {
            _connectedGate = pConnectedGate;
        }

        public override void Update(GameTime pTime)
        {
            if (Collision(_player))
            {
                _game.SetGameState(_state);
                _player.Position = _connectedGate.Position + new Vector2(0, 100);
            }
        }

        public override void Dispose()
        {
            base.Dispose();

            _player = null;
            _game = null;
        }
    }
}
