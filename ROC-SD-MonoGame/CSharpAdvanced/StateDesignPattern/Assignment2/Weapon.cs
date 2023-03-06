using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace CSharpAdvanced.CSharpAdvanced.StateDesignPattern.Assignment2
{
    public class Weapon : GameObject
    {
        private Player _player;

        public Weapon(Vector2 pPosition, Texture2D pTexture, Player pPlayer) : base(pPosition, pTexture)
        {
            _player = pPlayer;
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
