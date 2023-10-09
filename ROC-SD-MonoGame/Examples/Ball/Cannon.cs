using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ROC_SD_MonoGame.Examples.Ball
{
    public class Cannon
    {
        private BallGame _ballGame;

        private Texture2D _texture;
        private Vector2 _position;

        private float _rotation;
        private Vector2 _origin;


        private float _fireRate;
        private float _time;


        public Cannon(BallGame pBallGame, Texture2D pTexture, Vector2 pPosition, float pFireRate)
        {
            _ballGame = pBallGame;

            _texture = pTexture;
            _position = pPosition;
            _origin = new Vector2(pTexture.Width * 0.5f, pTexture.Height * 0.5f);
            _fireRate = pFireRate;

            _time = 1.0f / pFireRate;
        }

        public void UpdateCannon(GameTime pGameTime)
        {
            RotateTowardsMouse();

            _time += (float)pGameTime.ElapsedGameTime.TotalSeconds;

            if (_time > 1.0f / _fireRate)
            {
                _time -= 1.0f / _fireRate;

                Vector2 fireDirection = Vector2.Zero;
                fireDirection.X = MathF.Cos(MathHelper.ToRadians(_rotation));
                fireDirection.Y = MathF.Sin(MathHelper.ToRadians(_rotation));

                _ballGame.CreateBall(_position + fireDirection * 100, fireDirection * 200);
            }
        }

        private void RotateTowardsMouse()
        {
            MouseState mouseState = Mouse.GetState();

            Vector2 mouseDirection = (mouseState.Position.ToVector2() - _position);

            if (mouseDirection != Vector2.Zero)
            {
                mouseDirection.Normalize();
                _rotation = MathHelper.ToDegrees(MathF.Atan2(mouseDirection.Y, mouseDirection.X));
            }
        }

        public void DrawCannon(SpriteBatch pSpriteBatch)
        {
            pSpriteBatch.Draw(_texture, _position, null, Color.White, MathHelper.ToRadians(_rotation), _origin, Vector2.One, SpriteEffects.None, 0);
        }
    }
}