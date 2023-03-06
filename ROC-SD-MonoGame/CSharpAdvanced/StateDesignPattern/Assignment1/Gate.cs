using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CSharpAdvanced.CSharpAdvanced.StateDesignPattern.Assignment1
{
    public class Gate : GameObject
    {
        private Player _player;
        private Game1 _game;

        public Gate(Vector2 pPosition, Player pPlayer, Game1 pGame) : base(pPosition)
        {
            _player = pPlayer;
            _game = pGame;
        }

        public override void LoadContent(ContentManager pContent)
        {
            Texture = pContent.Load<Texture2D>("Gate");
        }

        public override void Update(GameTime pTime)
        {
            if (Collision(_player))
            {
                _game.Exit();
            }
        }
    }
}
