using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CSharpAdvanced.CSharpAdvanced.StateDesignPattern.Assignment1
{
    public class Shield : GameObject
    {
        private Player _player;

        public Shield(Vector2 pPosition, Player pPlayer) : base(pPosition)
        {
            _player = pPlayer;
        }

        public override void LoadContent(ContentManager pContent)
        {
            Texture = pContent.Load<Texture2D>("Shield");
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
