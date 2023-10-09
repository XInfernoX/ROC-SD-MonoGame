using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ROC_SD_MonoGame.Examples.Ball
{
    public class BallGame : Game
    {
        //Fields - MonoGame
        protected GraphicsDeviceManager _graphics;
        protected SpriteBatch _spriteBatch;
        protected Viewport _viewport;
        protected Texture2D _ballTexture;
        protected Texture2D _cannonTexture;
        protected Vector2 _centerPosition;

        //Balls
        protected List<Ball> _listOfBalls = new List<Ball>();

        //Constructor
        public BallGame()
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
            base.LoadContent();

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _viewport = GraphicsDevice.Viewport;
            _centerPosition = new Vector2(_viewport.Width * 0.5f, _viewport.Height * 0.5f);

            _ballTexture = Content.Load<Texture2D>("cannonBallSmall");
            _cannonTexture = Content.Load<Texture2D>("cannonSmall");
        }

        protected override void Update(GameTime pGameTime)
        {
            base.Update(pGameTime);

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

        public void CreateBall(Vector2 pPosition, Vector2 pVelocity)
        {
            _listOfBalls.Add(new Ball(_viewport, _ballTexture, pPosition, pVelocity));
        }
    }
}