using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using SpaceInvaders.Refactor.Core;

namespace SpaceInvaders.Refactor
{
    public abstract class RefactoredGameBase : Game
    {
        //Fields
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private readonly List<GameObject> _gameObjects;

        //Constructor
        protected RefactoredGameBase()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _gameObjects = new List<GameObject>();
        }

        protected override void LoadContent()
        {
            //Basic graphical initialization
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            base.LoadContent();
        }

        public void AddGameObject(GameObject pNewGameObject)
        {
            _gameObjects.Add(pNewGameObject);
        }
        public void RemoveGameObject(GameObject pGameObject)
        {
            _gameObjects.Remove(pGameObject);
        }

        protected override void Update(GameTime pGameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            UpdateAllGameObjects(pGameTime);
            LateUpdateAllGameObjects(pGameTime);
            CollisionCheckAllGameObjects(pGameTime);

            base.Update(pGameTime);
        }

        protected void UpdateAllGameObjects(GameTime pGameTime)
        {
            for (int i = 0; i < _gameObjects.Count; i++)
            {
                _gameObjects[i].Update(pGameTime);
            }
        }
        protected void LateUpdateAllGameObjects(GameTime pGameTime)
        {
            for (int i = 0; i < _gameObjects.Count; i++)
            {
                _gameObjects[i].LateUpdate(pGameTime);
            }
        }

        protected void CollisionCheckAllGameObjects(GameTime pGameTime)
        {
            for (int outerI = 0; outerI < _gameObjects.Count - 1; outerI++)
            {
                GameObject outerGameObject = _gameObjects[outerI];

                for (int innerI = outerI + 1; innerI < _gameObjects.Count; innerI++)
                {
                    GameObject innerGameObject = _gameObjects[innerI];
                    outerGameObject.CollisionCheck(innerGameObject);
                }
            }
        }

        protected override void Draw(GameTime pGameTime)
        {
            DrawAllGameObjects();

            base.Draw(pGameTime);
        }
        protected void DrawAllGameObjects()
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(sortMode: SpriteSortMode.BackToFront);

            for (int i = 0; i < _gameObjects.Count; i++)
            {
                _gameObjects[i].Draw(_spriteBatch);
            }

            _spriteBatch.End();
        }
    }
}