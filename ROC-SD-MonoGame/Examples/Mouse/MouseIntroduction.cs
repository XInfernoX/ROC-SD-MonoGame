using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceInvadersRefactored.GamePlay;

namespace ROC_SD_MonoGame.Examples.MouseExamples
{
    public class MouseIntroduction : Game
    {
        //Fields - MonoGame
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        private Texture2D _texture;
        private Vector2 _position;
        private Rectangle _rectangle;
        private Color _color = Color.White;

        //Constructor
        public MouseIntroduction()
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

            Viewport viewPort = GraphicsDevice.Viewport;

            _position = new Vector2(viewPort.Width * 0.5f, viewPort.Height * 0.5f);
            _texture = Content.Load<Texture2D>("WhiteBox");

            _rectangle = _texture.Bounds;
            _rectangle.X = (int)_position.X;
            _rectangle.Y = (int)_position.Y;
        }

        protected override void Update(GameTime pGameTime)
        {
            base.Update(pGameTime);

            MouseState mouseState = Mouse.GetState();

            _color = _rectangle.Contains(mouseState.Position) ? Color.Red : Color.White;
        }

        protected override void Draw(GameTime pGameTime)
        {
            base.Draw(pGameTime);

            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.Draw(_texture, _position, _color);

            _spriteBatch.End();
        }
    }
}
