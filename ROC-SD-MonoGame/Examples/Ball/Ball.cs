using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ROC_SD_MonoGame.Examples.Ball
{
    public class Ball
    {
        //Ball
        private Viewport _viewPort;

        private Texture2D _texture;
        private Vector2 _position;

        private Vector2 _velocity;
        private Vector2 _origin;

        private float _speed = 200;

        private Random random = new Random();

        public Ball(Viewport pViewPort,Texture2D pTexture, Vector2 pPosition)
        {
            _viewPort = pViewPort;
            _texture = pTexture;
            _position = pPosition;

            _origin = new Vector2(pTexture.Width * 0.5f, pTexture.Height * 0.5f);

            int randomDegree = random.Next(0, 360);
            _velocity.X = MathF.Cos(randomDegree);
            _velocity.Y = MathF.Sin(randomDegree);

            _velocity.Normalize();
        }

        public Ball(Viewport pViewPort, Texture2D pTexture, Vector2 pPosition, Vector2 pVelocity)
        {
            _viewPort = pViewPort;
            _texture = pTexture;
            _position = pPosition;

            _origin = new Vector2(pTexture.Width * 0.5f, pTexture.Height * 0.5f);

            _velocity = pVelocity;
            _velocity.Normalize();
        }




        public void UpdateBall(GameTime pGameTime)
        {
            BounceBall();

            _position += _velocity * (float)pGameTime.ElapsedGameTime.TotalSeconds * _speed;
        }

        private void BounceBall()
        {
            if (_position.X - _origin.X < 0 || _position.X + _origin.X > _viewPort.Width)
                _velocity.X *= -1;

            if (_position.Y - _origin.Y < 0 || _position.Y + _origin.Y > _viewPort.Height)
                _velocity.Y *= -1;
        }




        public void DrawBall(SpriteBatch pSpriteBatch)
        {
            pSpriteBatch.Draw(_texture, _position, null, Color.White, 0, _origin, Vector2.One, SpriteEffects.None, 0);
        }

    }
}