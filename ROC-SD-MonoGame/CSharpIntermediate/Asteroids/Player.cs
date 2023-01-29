using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CSharpIntermediate.Asteroids
{
    public class Player : GameObject
    {
        //Fields
        private Game1 _game;
        private Viewport _viewport;

        private Vector2 _velocity = Vector2.Zero;

        private float _speed = 3;
        private float _targetRotation = 0;
        private float _rotationSpeed = 2;

        //Constructor
        public Player(Game1 pGame, Vector2 pStartPosition) : base(pStartPosition, 0.05f)
        {
            _game = pGame;
        }

        public override void LoadContent(ContentManager pContent, Viewport pViewport)
        {
            Texture = pContent.Load<Texture2D>("SpaceShip");
            _viewport = pViewport;
        }

        //Methods
        public override void UpdateGameObject(GameTime pGameTime)
        {
            base.UpdateGameObject(pGameTime);

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

                _game.AddLaser(spawnLocation, 300, _rotation);
            }
        }
    }
}
