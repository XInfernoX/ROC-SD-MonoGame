using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ROC_SD_MonoGame.CSharpIntermediate.Asteroids
{
    public class Asteroid : GameObject
    {
        //Fields
        private Random _random;
        private Rectangle[] _sourceRectangles;
        private Rectangle _currentSourceRectangle;

        //Utility
        private int _sliceWidth;
        private int _sliceHeight;

        private float _radius;
        private int _asteroidSize;

        private Vector2 _direction;
        private float _speed;

        private float _rotationSpeed;

        //Debug
        private Color _color = Color.White;

        //Properties
        public float Radius => _radius;
        public int AsteroidSize => _asteroidSize;

        //Constructor

        public Asteroid(Vector2 pPosition, int pAsteroidSize) : base(pPosition, pAsteroidSize / 2.0f)
        {
            _asteroidSize = pAsteroidSize;

            _random = new Random();

            _rotationSpeed = (float)_random.NextDouble() * (1.0f / pAsteroidSize) * 180;
            _speed = (float)_random.NextDouble() * (1.0f / pAsteroidSize) * 5;

            _direction = DegreesToVector(_random.Next(0, 360));
        }

        public override void LoadContent(ContentManager pContent, Viewport pViewport)
        {
            base.LoadContent(pContent, pViewport);

            _texture = pContent.Load<Texture2D>("Asteroids");

            int width = 8;
            int height = 8;

            //Hardcoded to the Asteroids spritesheet
            _sliceWidth = _texture.Width / width;
            _sliceHeight = _texture.Height / height;

            _radius = (_sliceWidth + _sliceHeight) / 4;
            _radius *= _asteroidSize / 2.0f;

            _collider = new Rectangle((int)_position.X, (int)_position.Y, _sliceWidth, _sliceHeight);

            _sourceRectangles = new Rectangle[width * height];
            for (int y = 0, i = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++, i++)
                {
                    _sourceRectangles[i] = new Rectangle(x * _sliceWidth, y * _sliceHeight, _sliceWidth, _sliceHeight);
                }
            }

            _currentSourceRectangle = _sourceRectangles[_random.Next(_sourceRectangles.Length)];
        }

        public override void UpdateGameObject(GameTime pTime)
        {
            //Rotate
            _rotation += _rotationSpeed * (float)pTime.ElapsedGameTime.TotalSeconds;

            //Move
            _position += (float)pTime.ElapsedGameTime.TotalSeconds * _speed * _direction;

            //Debug
            MouseState state = Mouse.GetState();
            Vector2 mousePos = state.Position.ToVector2();

            float mouseDistance = (mousePos - Position).Length();

            if (mouseDistance < Radius)
                _color = Color.Red;
            else
                _color = Color.White;
        }

        public override void DrawGameObject(SpriteBatch pSpriteBatch)
        {
            if (_active)
            {
                Vector2 scaledOrigin = new Vector2(_sliceWidth * _origin.X, _sliceHeight * _origin.Y);
                float radians = MathHelper.ToRadians(_rotation);
                pSpriteBatch.Draw(_texture, _position, _currentSourceRectangle, _color, radians, scaledOrigin, _scale, SpriteEffects.None, 0);
            }
        }

        public override void Dispose()
        {
        }
    }
}
