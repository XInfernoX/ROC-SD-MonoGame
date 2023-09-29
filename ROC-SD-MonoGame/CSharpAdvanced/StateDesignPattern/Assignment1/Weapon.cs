using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ROC_SD_MonoGame.CSharpAdvanced.StateDesignPattern.Assignment1
{
    public class Weapon : GameObject
    {
        private Player _player;

        public Weapon(Vector2 pPosition, Player pPlayer) : base(pPosition)
        {
            _player = pPlayer;
        }

        public override void LoadContent(ContentManager pContent)
        {
            Texture = pContent.Load<Texture2D>("Weapon");
        }

        public override void Update(GameTime pTime)
        {
            if (_active && Collision(_player))
            {
                Console.WriteLine("Weapon collides with Player");
                _player.AddWeapon();

                _active = false;
            }
        }
    }
}
