using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ROC_SD_MonoGame.Examples.Ball
{
    public class CannonPlacer : BallGame
    {
        //Cannons
        private Texture2D _cannonTexture;
        private List<Cannon> _cannons = new List<Cannon>();

        private MouseState _previousMouseState;

        //Constructor
        protected override void LoadContent()
        {
            base.LoadContent();

            _cannonTexture = Content.Load<Texture2D>("cannonSmall");
        }

        protected override void Update(GameTime pGameTime)
        {
            base.Update(pGameTime);

            for (int i = 0; i < _cannons.Count; i++)
            {
                _cannons[i].UpdateCannon(pGameTime);
            }

            PlaceCannon();
        }

        private void PlaceCannon()
        {
            MouseState mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (_previousMouseState.LeftButton != mouseState.LeftButton)
                {
                    Console.WriteLine("Adding a new cannon!");
                    _cannons.Add(new Cannon(this, _cannonTexture, mouseState.Position.ToVector2(), 2));
                }
            }

            _previousMouseState = mouseState;
        }

        protected override void Draw(GameTime pGameTime)
        {
            base.Draw(pGameTime);

            _spriteBatch.Begin();

            for (int i = 0; i < _cannons.Count; i++)
            {
                _cannons[i].DrawCannon(_spriteBatch);
            }

            _spriteBatch.End();
        }
    }
}
