using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ComponentDesignPattern.Assignment5
{
    public class Game1 : Game
    {
        //Fields
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private List<GameObject> _gameObjects = new List<GameObject>();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Viewport viewport = GraphicsDevice.Viewport;

            AnimatedSpriteRenderer animatedSpriteRenderer = new AnimatedSpriteRenderer(Content.Load<Texture2D>("Megaman2"), 5, 2, 12f);
            _gameObjects.Add(new GameObject(this, "MegaMan", new Vector2(viewport.Width / 2.0f, viewport.Height / 2.0f), animatedSpriteRenderer));

            AwakeGameObjects();
            StartGameObjects();
        }

        private void AwakeGameObjects()
        {
            for (int i = 0; i < _gameObjects.Count; i++)
            {
                _gameObjects[i].AwakeComponents();
            }
        }

        private void StartGameObjects()
        {
            for (int i = 0; i < _gameObjects.Count; i++)
            {
                _gameObjects[i].StartComponents();
            }
        }

        protected override void Update(GameTime pGameTime)
        {
            for (int i = 0; i < _gameObjects.Count; i++)
            {
                _gameObjects[i].Update(pGameTime);
            }
        }

        protected override void Draw(GameTime pGameTime)
        {
            base.Draw(pGameTime);

            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(SpriteSortMode.BackToFront);

            for (int i = 0; i < _gameObjects.Count; i++)
            {
                _gameObjects[i].Draw(_spriteBatch);
            }

            _spriteBatch.End();
        }
    }
}