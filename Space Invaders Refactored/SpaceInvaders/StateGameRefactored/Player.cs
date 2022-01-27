using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace StateGameRefactored1
{
    public class Player : GameObject
    {
        private int _speed;

        private GameObject _shield;
        private GameObject _weapon;

        private Texture2D _shieldTexture;
        private Texture2D _weaponTexture;
        private Texture2D _weaponShieldTexture;

        public Player(Vector2 pPosition, Texture2D pTexture, int pSpeed, 
            GameObject pShield, GameObject pWeapon, Texture2D pShieldTexture, Texture2D pWeaponTexture, Texture2D pWeaponShieldTexture) 
            : base(pPosition, pTexture)
        {
            _speed = pSpeed;

            _shield = pShield;
            _weapon = pWeapon;

            _shieldTexture = pShieldTexture;
            _weaponTexture = pWeaponTexture;
            _weaponShieldTexture = pWeaponShieldTexture;
        }

        public override void Update(GameTime pGameTime)
        {
            Movement();
            WeaponCollisionCheck();
            ShieldCollisionCheck();
        }

        private void Movement()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            Vector2 playerInput = Vector2.Zero;
            if (keyboardState.IsKeyDown(Keys.A))
                playerInput.X--;
            if (keyboardState.IsKeyDown(Keys.D))
                playerInput.X++;
            if (keyboardState.IsKeyDown(Keys.W))
                playerInput.Y--;
            if (keyboardState.IsKeyDown(Keys.S))
                playerInput.Y++;

            if (playerInput != Vector2.Zero)
            {
                playerInput.Normalize();
                Vector2 playerTranslation = playerInput * _speed;
                Position += playerTranslation;
            }
        }

        private void WeaponCollisionCheck()
        {
            Console.WriteLine($"WeaponCollisionCheck, playerCollider:{_collider}, weaponCollider:{_weapon.Collider}");
            if (Collision(_weapon))
            {
                Console.WriteLine("Contains with _weapon");
                _weapon.Active = false;

                if (_shield.Active)
                {
                    Texture = _weaponTexture;
                }
                else
                {
                    Texture = _weaponShieldTexture;
                }
            }
        }

        private void ShieldCollisionCheck()
        {
            if (Collision(_shield))
            {
                Console.WriteLine("Contains with _shield");
                _shield.Active = false;

                if (_weapon.Active)
                {
                    Texture = _shieldTexture;
                }
                else
                {
                    Texture = _weaponShieldTexture;
                }
            }
        }
    }
}