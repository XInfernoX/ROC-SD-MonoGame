using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace ROC_SD_MonoGame.Examples.MouseExamples
{
    public class MouseStick : Game
    {
        //Fields - MonoGame
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private GameObject _gameObject;

        //Constructor
        public MouseStick()
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


            Texture2D texture = Content.Load<Texture2D>("cannonBallSmall");

            _gameObject = new GameObject(Vector2.Zero, texture);
        }

        protected override void Update(GameTime pGameTime)
        {
            base.Update(pGameTime);

            MouseState mouseState = Mouse.GetState();

            _gameObject.Position = mouseState.Position.ToVector2();
        }

        protected override void Draw(GameTime pGameTime)
        {
            base.Draw(pGameTime);

            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _gameObject.Draw(_spriteBatch);

            _spriteBatch.End();
        }
    }
}
