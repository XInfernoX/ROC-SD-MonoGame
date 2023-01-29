using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Asteroids
{
    public class Asteroid : GameObject
    {
        private Rectangle[] _sourceRectangles;

        private Rectangle _slice;
        private int _sliceIndex;

        public Asteroid(Texture2D pTexture, int pWidth, int pHeight)
        {
            _texture = pTexture;
            //Collider?

            int sliceCount = pWidth * pHeight;
            int sliceWidth = pTexture.Width / pWidth;
            int sliceHeight = pTexture.Height / pHeight;
            _collider = new Rectangle((int)_position.X, (int)_position.Y, sliceWidth, sliceHeight);

            _sourceRectangles = new Rectangle[sliceCount];
            for (int y = 0, i = 0; y < pHeight; y++)
            {
                for (int x = 0; x < pWidth; x++, i++)
                {
                    _sourceRectangles[i] = new Rectangle(x * sliceWidth, y * sliceHeight, sliceWidth, sliceHeight);
                }
            }

            Random random = new Random();
            _sliceIndex = random.Next(sliceCount);
            _slice = _sourceRectangles[_sliceIndex];
        }

        public override void Draw(SpriteBatch pSpriteBatch)
        {
            if (_active)
            {
                Vector2 scaledOrigin = new Vector2(_texture.Width * _origin.X, _texture.Height * _origin.Y);
                float radians = MathHelper.ToRadians(_rotation);
                pSpriteBatch.Draw(_texture, _position, _slice, Color.White, radians, scaledOrigin, _scale, SpriteEffects.None, 0);
            }
        }
    }


    public class Player : GameObject
    {
        //Fields
        private Vector2 _velocity = Vector2.Zero;

        private float _speed = 3;
        private float _targetRotation = 0;
        private float _rotationSpeed = 2;

        private Game1 _game;
        private Viewport _viewport;

        //Constructor
        public Player(Game1 pGame, Vector2 pStartPosition, Viewport pViewport) : base(pStartPosition)
        {
            _game = pGame;
            _viewport = pViewport;
        }

        public override void LoadContent(ContentManager pContent, Viewport pViewport)
        {
            Texture = pContent.Load<Texture2D>("player");
        }

        //Methods
        public override void Update(GameTime pGameTime)
        {
            base.Update(pGameTime);

            UpdateVelocity(pGameTime);
            RotateTowards(pGameTime);

            _position += _velocity;

            KeepPlayerOnScreen();

            CheckForLaserShot();
        }

        //Methods

        private void UpdateVelocity(GameTime pGameTime)
        {
            if (GetRotationInput(out Vector2 pInput))
            {
                _targetRotation = (float)Math.Atan2(pInput.Y, pInput.X) + MathHelper.PiOver2;

                Vector2 acceleration = DegreesToVector(_rotation);
                acceleration *= (float)(pGameTime.ElapsedGameTime.TotalSeconds * _speed);

                _velocity += acceleration;
            }
        }
        private bool GetRotationInput(out Vector2 input)
        {
            KeyboardState keyboard = Keyboard.GetState();

            input = Vector2.Zero;

            if (keyboard.IsKeyDown(Keys.W))
                input.Y--;
            if (keyboard.IsKeyDown(Keys.A))
                input.X--;
            if (keyboard.IsKeyDown(Keys.S))
                input.Y++;
            if (keyboard.IsKeyDown(Keys.D))
                input.X++;

            if (input != Vector2.Zero)
            {
                input.Normalize();
                return true;
            }

            return false;
        }
        private void RotateTowards(GameTime pGameTime)
        {
        https://math.stackexchange.com/questions/110080/shortest-way-to-achieve-target-angle
            float C = _rotation;
            float T = _targetRotation;

            float A = Math.Abs(T - C);
            float B = Math.Abs(T - C + MathHelper.TwoPi);
            float Y = Math.Abs(T - C - MathHelper.TwoPi);


            //Find smallest value between A,B and Y
            float min = A;
            if (A <= B && A <= Y)
                min = T - C;

            if (B <= A && B <= Y)
                min = T - C + MathHelper.TwoPi;

            if (Y <= A && Y <= B)
                min = T - C - MathHelper.TwoPi;

            _rotation = MathHelper.Lerp(_rotation, _rotation + min, (float)pGameTime.ElapsedGameTime.TotalSeconds * _rotationSpeed);
        }
        private void KeepPlayerOnScreen()
        {
            if (_position.X < 0)
                _position.X += _viewport.Width;

            if (_position.Y < 0)
                _position.Y += _viewport.Height;

            _position.X %= _viewport.Width;
            _position.Y %= _viewport.Height;
        }

        private void CheckForLaserShot()
        {
            KeyboardState keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.Space))
            {
                Vector2 forward = DegreesToVector(_rotation);
                Vector2 spawnLocation = _position + (forward * _texture.Width / 2);

                _game.AddLaser(spawnLocation, _rotation);
            }
        }
    }
}
