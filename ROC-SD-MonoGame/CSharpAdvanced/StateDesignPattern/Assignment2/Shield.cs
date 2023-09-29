using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ROC_SD_MonoGame.CSharpAdvanced.StateDesignPattern.Assignment2
{
    public class Shield : GameObject
    {
        private Player _player;

        public Shield(Vector2 pPosition, Player pPlayer) : base(pPosition)
        {
            _player = pPlayer;
        }

        public Shield(Vector2 pPosition, Texture2D pTexture, Player pPlayer) : base(pPosition, pTexture)
        {
            _player = pPlayer;
        }

        public override void Update(GameTime pTime)
        {
            if (_active && Collision(_player))
            {
                _player.AddShield();
                _active = false;
            }
        }
    }
}
