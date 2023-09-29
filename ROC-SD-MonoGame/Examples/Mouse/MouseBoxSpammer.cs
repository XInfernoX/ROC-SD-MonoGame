using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceInvadersRefactored.GamePlay;

namespace ROC_SD_MonoGame.Examples.MouseExamples
{
    public class MouseBoxSpammer : Game
    {
        //Fields - MonoGame
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Viewport _viewport;
        private Random _random;

        private Texture2D _boxTexture;
        private MouseBox _mouseBox;


        //Constructor
        public MouseBoxSpammer()
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
            _random = new Random();
            _viewport = GraphicsDevice.Viewport;

            _boxTexture = Content.Load<Texture2D>("WhiteBox");

            CreateMouseBox();
        }

        protected override void Update(GameTime pGameTime)
        {
            base.Update(pGameTime);

            _mouseBox.UpdateMouseBox(pGameTime);

            if (_mouseBox.IsMouseOverBox(Mouse.GetState()))
            {
                CreateMouseBox();
            }
        }

        private void CreateMouseBox()
        {
            int randomX = _random.Next((int)(_viewport.Width * 0.9f));
            int randomY = _random.Next((int)(_viewport.Height * 0.9f));

            Vector2 pos = new Vector2(randomX, randomY);

            _mouseBox = new MouseBox(_boxTexture, pos, Color.White);
        }

        protected override void Draw(GameTime pGameTime)
        {
            base.Draw(pGameTime);

            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _mouseBox.DrawMouseBox(_spriteBatch);

            _spriteBatch.End();
        }
    }
}
