using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ROC_SD_MonoGame.CSharpAdvanced.StateDesignPattern.Assignment2
{
    public class Gate : GameObject
    {
        private Player _player;
        private Game1 _game;
        private GameState _state;

        private Gate _connectedGate;

        public Gate(Vector2 pPosition, Texture2D pTexture, Player pPlayer, Game1 pGame, GameState pState) : base(pPosition, pTexture)
        {
            _player = pPlayer;
            _game = pGame;
            _state = pState;
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
    }
}
