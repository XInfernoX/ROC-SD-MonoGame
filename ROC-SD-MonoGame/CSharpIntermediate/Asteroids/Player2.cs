using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ROC_SD_MonoGame.CSharpIntermediate.Asteroids
{
    public class Player2 : GameObject
    {
        //Fields
        private Game1 _game;
        private Viewport _viewport;

        private Vector2 _velocity = Vector2.Zero;

        private float _speed = 3.0f;
        private float _rotationSpeed = 180;

        private float _laserCooldown = 0.5f;
        private float _lastLaserShotFired;

        //Constructor
        public Player2(Game1 pGame, Vector2 pStartPosition) : base(pStartPosition, 0.05f)
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

            UpdateRotation(pGameTime);
            UpdateVelocity(pGameTime);

            _position += _velocity;

            KeepPlayerOnScreen();
            CheckForLaserShot(pGameTime);
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

        private void CheckForLaserShot(GameTime pGameTime)
        {
            KeyboardState keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.Space) && pGameTime.TotalGameTime.TotalSeconds > _lastLaserShotFired)
            {
                _lastLaserShotFired = (float)pGameTime.TotalGameTime.TotalSeconds + _laserCooldown;
                Vector2 forward = DegreesToVector(_rotation);
                Vector2 spawnLocation = _position + _texture.Width / 2 * _scale * forward;

                _game.AddLaser(spawnLocation, 300, _rotation);
            }
        }
    }
}

