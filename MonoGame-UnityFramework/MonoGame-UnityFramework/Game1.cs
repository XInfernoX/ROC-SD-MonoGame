using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGame_UnityFramework
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Vector2 _playerPosition;
        private Vector2 _mousePosition;
        private Vector2 _controllerPosition;

        private Texture2D _playerTile;
        private Texture2D _mouseTile;
        private Texture2D _controllerTile;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _playerPosition = new Vector2(_graphics.GraphicsDevice.Viewport.Width / 2 - 64,
                                  _graphics.GraphicsDevice.Viewport.Height / 2 - 64);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _playerTile = Content.Load<Texture2D>("tile_0000");
            _mouseTile = Content.Load<Texture2D>("tile_0044");
            _controllerTile = Content.Load<Texture2D>("tile_0111");
        
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            CalculatePlayerPosition();
            CalculateMousePosition();
            CalculateControllerPosition();

            base.Update(gameTime);
        }

        private void CalculatePlayerPosition()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Right))
                _playerPosition.X += 10;
            if (keyboardState.IsKeyDown(Keys.Left))
                _playerPosition.X -= 10;
            if (keyboardState.IsKeyDown(Keys.Up))
                _playerPosition.Y -= 10;
            if (keyboardState.IsKeyDown(Keys.Down))
                _playerPosition.Y += 10;
        }

        private void CalculateMousePosition()
        {
            MouseState mouseState = Mouse.GetState();

            _mousePosition.X = mouseState.X;
            _mousePosition.Y = mouseState.Y;
        }

        private void CalculateControllerPosition()
        {
            // Check the device for Player One
            GamePadCapabilities capabilities = GamePad.GetCapabilities(PlayerIndex.One);

            // If there a controller attached, handle it
            if (capabilities.IsConnected)
            {
                // Get the current state of Controller1
                GamePadState state = GamePad.GetState(PlayerIndex.One);
                // You can check explicitly if a gamepad has support for a certain feature
                if (capabilities.HasLeftXThumbStick)
                {
                    // Check teh direction in X axis of left analog stick
                    if (state.ThumbSticks.Left.X < -0.5f)
                        _controllerPosition.X -= 10.0f;
                    if (state.ThumbSticks.Left.X > 0.5f)
                        _controllerPosition.X += 10.0f;
                    if (state.ThumbSticks.Left.Y < -0.5f)
                        _controllerPosition.Y -= 10.0f;
                    if (state.ThumbSticks.Left.Y > 0.5f)
                        _controllerPosition.Y += 10.0f;
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);

            _spriteBatch.Begin();
            _spriteBatch.Draw(_playerTile, _playerPosition, Color.White);
            _spriteBatch.Draw(_mouseTile, _mousePosition, Color.White);
            _spriteBatch.Draw(_controllerTile, _controllerPosition, Color.White);

            //_spriteBatch.Draw()
            _spriteBatch.End();
        }
    }
}
