using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace ROC_SD_MonoGame.Examples.Player
{
    public class SpaceMovement : Game
    {
        //Fields - MonoGame
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Viewport _viewport;

        //SpaceShip
        private Texture2D _spaceShipTexture;
        private float _spaceShipScale = 0.05f;

        private Vector2 _spaceShipPosition;
        private Vector2 _spaceShipVelocity;

        private float _spaceShipRotation;
        private float _spaceShipRotationSpeed = 180;
        private Vector2 _spaceShipOrigin;

        private float _spaceShipSpeed = 3;

        //Constructor
        public SpaceMovement()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1000;
            _graphics.PreferredBackBufferHeight = 1000;

            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Console.WriteLine("LoadContent");

            base.LoadContent();

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _viewport = GraphicsDevice.Viewport;


            _spaceShipTexture = Content.Load<Texture2D>("SpaceShip");
            _spaceShipOrigin = new Vector2(_spaceShipTexture.Width * 0.5f, _spaceShipTexture.Height * 0.5f);
        }

        protected override void Update(GameTime pGameTime)
        {
            base.Update(pGameTime);

            UpdateSpaceShip(pGameTime);
        }

        protected override void Draw(GameTime pGameTime)
        {
            base.Draw(pGameTime);

            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            //_spriteBatch.Draw(_spaceShipTexture, _spaceShipPosition, Color.White);
            _spriteBatch.Draw(_spaceShipTexture, _spaceShipPosition, null, Color.White, MathHelper.ToRadians(_spaceShipRotation), _spaceShipOrigin, _spaceShipScale, SpriteEffects.None, 0);


            _spriteBatch.End();
        }

        private void UpdateSpaceShip(GameTime pGameTime)
        {
            UpdateRotation(pGameTime);
            UpdateVelocity(pGameTime);

            _spaceShipPosition += _spaceShipVelocity;

            KeepPlayerOnScreen();
        }

        //Methods
        private void UpdateVelocity(GameTime pGameTime)
        {
            KeyboardState keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.W))
            {
                Vector2 acceleration = DegreesToVector(_spaceShipRotation);
                acceleration *= (float)(pGameTime.ElapsedGameTime.TotalSeconds * _spaceShipSpeed);

                _spaceShipVelocity += acceleration;
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

            _spaceShipRotation += input * _spaceShipRotationSpeed * (float)pGameTime.ElapsedGameTime.TotalSeconds;
        }
        private void KeepPlayerOnScreen()
        {
            if (_spaceShipPosition.X < 0)
                _spaceShipPosition.X += _viewport.Width;

            if (_spaceShipPosition.Y < 0)
                _spaceShipPosition.Y += _viewport.Height;

            _spaceShipPosition.X %= _viewport.Width;
            _spaceShipPosition.Y %= _viewport.Height;
        }

        private Vector2 DegreesToVector(float pDegrees)
        {
            float radians = MathHelper.ToRadians(pDegrees);

            return new Vector2(MathF.Sin(radians), -MathF.Cos(radians));
        }
    }
}
