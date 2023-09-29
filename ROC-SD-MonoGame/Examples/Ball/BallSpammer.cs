using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace ROC_SD_MonoGame.Examples.Ball
{
    public class BallSpammer : Game
    {
        //Fields - MonoGame
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Viewport _viewport;
        private Texture2D _ballTexture;
        private Vector2 _centerPosition;


        //Ball
        private List<Ball> _listOfBalls = new List<Ball>();

        //Constructor
        public BallSpammer()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;

            _graphics.ApplyChanges();



            base.Initialize();
        }

        protected override void LoadContent()
        {
            Console.WriteLine("LoadContent");

            base.LoadContent();

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _viewport = GraphicsDevice.Viewport;
            _centerPosition = new Vector2(_viewport.Width * 0.5f, _viewport.Height * 0.5f);

            _ballTexture = Content.Load<Texture2D>("ball");

            _listOfBalls.Add(new Ball(_viewport, _ballTexture, _centerPosition));
        }

        protected override void Update(GameTime pGameTime)
        {
            base.Update(pGameTime);

            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Space))
            {
                _listOfBalls.Add(new Ball(_viewport, _ballTexture, _centerPosition));
            }

            for (int i = 0; i < _listOfBalls.Count; i++)
            {
                _listOfBalls[i].UpdateBall(pGameTime);
            }
        }

        protected override void Draw(GameTime pGameTime)
        {
            base.Draw(pGameTime);

            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            for (int i = 0; i < _listOfBalls.Count; i++)
            {
                _listOfBalls[i].DrawBall(_spriteBatch);
            }

            _spriteBatch.End();
        }
    }
}
