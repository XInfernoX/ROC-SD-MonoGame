using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ROC_SD_MonoGame.Examples.Ball
{
    public class Cannon
    {
        private BallCannon _ballCannon;

        private Texture2D _texture;
        private Vector2 _position;

        private float _rotation;
        private Vector2 _origin;

        public Cannon(BallCannon pBallCannon, Texture2D pTexture, Vector2 pPosition)
        {
            _ballCannon = pBallCannon;

            _texture = pTexture;
            _position = pPosition;
            _origin = new Vector2(pTexture.Width * 0.5f, pTexture.Height * 0.5f);
        }

        public void UpdateCannon(GameTime pGameTime)
        {
            RotateTowardsMouse();

            KeyboardState keyBoardState = Keyboard.GetState();

            if(keyBoardState.IsKeyDown(Keys.Space))
                _ballCannon.CreateBall(_rotation);
        }

        private void RotateTowardsMouse()
        {
            MouseState mouseState = Mouse.GetState();

            Vector2 mouseDirection = (mouseState.Position.ToVector2() - _position);
            mouseDirection.Normalize();

            _rotation = MathHelper.ToDegrees(MathF.Atan2(mouseDirection.Y, mouseDirection.X));
        }

        public void DrawCannon(SpriteBatch pSpriteBatch)
        {
            pSpriteBatch.Draw(_texture, _position, null, Color.White, MathHelper.ToRadians(_rotation), _origin, Vector2.One, SpriteEffects.None, 0);
        }
    }
}