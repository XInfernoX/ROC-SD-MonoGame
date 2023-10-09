using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ROC_SD_MonoGame.Examples.Ball
{
    public class BallCannon : BallGame
    {
        //Cannon
        private Cannon _cannon;

        protected override void LoadContent()
        {
            Console.WriteLine("LoadContent");

            base.LoadContent();

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _viewport = GraphicsDevice.Viewport;
            _centerPosition = new Vector2(_viewport.Width * 0.5f, _viewport.Height * 0.5f);

            _ballTexture = Content.Load<Texture2D>("cannonBallSmall");
            Texture2D cannonTexture = Content.Load<Texture2D>("cannonSmall");
            
            //_listOfBalls.Add(new Ball(_viewport, _ballTexture, _centerPosition));

            _cannon = new Cannon(this, cannonTexture, _centerPosition, 2);
        }

        protected override void Update(GameTime pGameTime)
        {
            base.Update(pGameTime);

            _cannon.UpdateCannon(pGameTime);
        }

        protected override void Draw(GameTime pGameTime)
        {
            base.Draw(pGameTime);

            _spriteBatch.Begin();
            _cannon.DrawCannon(_spriteBatch);
            _spriteBatch.End();
        }
    }
}
