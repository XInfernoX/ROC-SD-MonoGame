using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CSharpAdvanced.CSharpAdvanced.StateGameRefactored
{
    public class Gate : GameObject
    {
        //Fields
        private StateGame _game;
        private Player _player;
        private string _sceneName;
        private SpriteFont _font;
        private Vector2 _playerOffset;

        private bool _drawHint = false;

        //Constructor
        public Gate(Vector2 pPosition, Texture2D pTexture, StateGame pGame, Player pPlayer, string pSceneName, Vector2 pPlayerOffset, SpriteFont pFont) : base(pPosition, pTexture)
        {
            _game = pGame;
            _player = pPlayer;
            _sceneName = pSceneName;
            _playerOffset = pPlayerOffset;

            _font = pFont;
        }
        //Methods
        public override void Update(GameTime pGameTime)
        {
            if (Collision(_player))
            {
                _drawHint = true;

                KeyboardState state = Keyboard.GetState();
                if (state.IsKeyDown(Keys.Space))
                {
                    _player.Position += _playerOffset;
                    _game.ChangeSceneTo(_sceneName);
                }
            }
        }

        public override void Draw(SpriteBatch pSpriteBatch)
        {
            if (_active)
            {
                pSpriteBatch.Draw(_texture, _position, Color.White);

                if (_drawHint)
                    pSpriteBatch.DrawString(_font, "Press space to enter the gate", _position, Color.White);
            }
        }
    }
}