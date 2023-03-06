using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ROC_SD_MonoGame.CSharpAdvanced.StateDesignPattern.Assignment3
{
    public class Pickup : GameObject
    {
        private Player _player;

        public Pickup(Player pPlayer)
        {
            _player = pPlayer;
        }

        public Pickup(Vector2 pPosition, Texture2D pTexture, Player pPlayer) : base(pPosition, pTexture)
        {
            _player = pPlayer;
        }

        public override void Update(GameTime pGameTime)
        {
            if (Collision(_player))
            {
                OnPlayerCollision(_player);
                Active = false;
            }
        }

        protected virtual void OnPlayerCollision(Player pPlayer)
        {
        }
    }
}