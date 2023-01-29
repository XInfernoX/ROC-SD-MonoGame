using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Asteroids
{
    public class Player2 : GameObject
    {
        //Fields
        private Vector2 _velocity = Vector2.Zero;

        private float _speed = 3;
        private float _rotationSpeed = 2;

        private Game1 _game;
        private Viewport _viewport;

        //Constructor
        public Player2(Game1 pGame, Vector2 pStartPosition, Viewport pViewport) : base(pStartPosition)
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

            UpdateRotation(pGameTime);
            UpdateVelocity(pGameTime);

            _position += _velocity;

            KeepPlayerOnScreen();
            CheckForLaserShot();
        }

        //Methods

        private void UpdateVelocity(GameTime pGameTime)
        {
            KeyboardState keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.W))
            {
                Vector2 acceleration = DegreesToVector(_rotation);
                acceleration *= (float)(pGameTime.ElapsedGameTime.TotalSeconds * _speed);

                _velocity += acceleration;
            }
        }
        private void UpdateRotation(GameTime pGameTime)
        {
            KeyboardState keyboard = Keyboard.GetState();

            float input = 0;

            if (keyboard.IsKeyDown(Keys.A))
                input--;
            if (keyboard.IsKeyDown(Keys.D))
                input++;

            _rotation += input * _rotationSpeed * (float)pGameTime.ElapsedGameTime.TotalSeconds;
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

