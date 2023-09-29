using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace ROC_SD_MonoGame.Examples.Player
{
    public class SimpleMovement : Game
    {
        //Fields - MonoGame
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Viewport _viewport;

        //SpaceShip
        private Texture2D _spaceShipTexture;
        private float _spaceShipScale = 0.05f;

        private Vector2 _spaceShipPosition;
        private Vector2 _spaceShipOrigin;

        private float _spaceShipSpeed = 200;

        //Constructor
        public SimpleMovement()
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
            _spriteBatch.Draw(_spaceShipTexture, _spaceShipPosition, null, Color.White, 0, _spaceShipOrigin, _spaceShipScale, SpriteEffects.None, 0);

            _spriteBatch.End();
        }

        private void UpdateSpaceShip(GameTime pGameTime)
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
                Vector2 playerTranslation = playerInput * _spaceShipSpeed * (float)pGameTime.ElapsedGameTime.TotalSeconds;
                _spaceShipPosition += playerTranslation;
            }

            KeepPlayerOnScreen();
        }

        //Methods
       
        private void KeepPlayerOnScreen()
        {
            if (_spaceShipPosition.X < 0)
                _spaceShipPosition.X += _viewport.Width;

            if (_spaceShipPosition.Y < 0)
                _spaceShipPosition.Y += _viewport.Height;

            _spaceShipPosition.X %= _viewport.Width;
            _spaceShipPosition.Y %= _viewport.Height;
        }
    }
}
