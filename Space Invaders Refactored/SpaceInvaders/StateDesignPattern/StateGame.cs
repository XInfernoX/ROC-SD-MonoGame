using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SpaceInvaders.Refactor;
using SpaceInvaders.Refactor.Core;

namespace SpaceInvaders.StateDesignPattern
{
    public class StateGame : RefactoredGameBase
    {
        public StateGame()
        {

        }

        protected override void LoadContent()
        {
            base.LoadContent();

            Viewport viewport = GraphicsDevice.Viewport;

            //Text
            SpriteFont font = Content.Load<SpriteFont>("Arial");
            Text textComponent = new Text(font, "Test", new Vector2(0,0), new Vector2(0.5f, 0.5f), Color.White);

            GameObject text = new GameObject(this, "Text", new Vector2(viewport.Width / 2, viewport.Height / 2), textComponent);
            AddGameObject(text);


            //Button
            Texture2D buttonTexture = Content.Load<Texture2D>("tile_0009");

            Button keyButton = new Button(buttonTexture, Color.White, Color.Aquamarine);
            GameObject button = new GameObject(this, "Key", new Vector2(viewport.Width / 2, viewport.Height / 2), new Vector2(0f, 0f), 0, new Vector2(10, 10), keyButton);
            AddGameObject(button);
        }
    }
}
