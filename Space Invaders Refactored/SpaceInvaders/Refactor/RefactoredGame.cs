using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.Refactor.Core;
using SpaceInvaders.Refactor.Core.Components;
using SpaceInvaders.Refactor.GamePlay;

namespace SpaceInvaders.Refactor
{
    public class RefactoredGame : Game
    {
        //Fields
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private readonly List<GameObject> _gameObjects;
        private GameObject _background;
        private GameObject _player;
        private GameObject _alienWave1;

        //Constructor
        public RefactoredGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _gameObjects = new List<GameObject>();
        }

        protected override void Initialize()
        {
            Console.WriteLine("Initialize");

            base.Initialize();
        }

        protected override void LoadContent()
        {
            //Basic graphical initialization
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Viewport viewport = GraphicsDevice.Viewport;

            //Background setup
            SpriteRenderer backgroundRenderer = new SpriteRenderer("background", Content, Color.White, 1.0f);
            _background = new GameObject(this, "background", Vector2.Zero, Vector2.Zero, backgroundRenderer);
            AddGameObject(_background);


            //Player setup
            Player playerBehaviour = new Player();
            SpriteRenderer playerRenderer = new SpriteRenderer("player", Content, Color.White, 0.5f);
            Collider playerCollider = new Collider(playerRenderer);
            PlayerMovement playerMovement = new PlayerMovement(5, viewport, playerRenderer.Width);

            Texture2D playerLaserTexture = Content.Load<Texture2D>("laser2");
            PlayerLaserShooter playerLaserShooter = new PlayerLaserShooter(playerLaserTexture, 0.5f);
            Vector2 playerSpawnPosition = new Vector2(viewport.Width / 2, viewport.Height - playerRenderer.Height);
            _player = new GameObject(this, "player", playerSpawnPosition, new Vector2(0.5f, 0.5f), 0, Vector2.One, playerBehaviour, playerCollider, playerRenderer, playerMovement, playerLaserShooter);
            AddGameObject(_player);


            //AlienWave1 setup
            Texture2D alienTexture = Content.Load<Texture2D>("alien1");
            Texture2D alienLaserTexture = Content.Load<Texture2D>("laser1");
            AlienWaveBehaviour alienWaveBehaviour = new AlienWaveBehaviour(this, alienTexture, alienLaserTexture, viewport, 3, 10, 1.5f);
            _alienWave1 = new GameObject(this, "Alien Wave 1", alienWaveBehaviour);
            AddGameObject(_alienWave1);
        }

        public void AddGameObject(GameObject pNewGameObject)
        {
            _gameObjects.Add(pNewGameObject);
        }

        //Only meant to be called from GameObject.Dispose()
        public void RemoveGameObject(GameObject pGameObject)
        {
            _gameObjects.Remove(pGameObject);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //Update
            for (int i = 0; i < _gameObjects.Count; i++)
            {
                _gameObjects[i].Update(gameTime);
            }

            //LateUpdate
            for (int i = 0; i < _gameObjects.Count; i++)
            {
                _gameObjects[i].LateUpdate(gameTime);
            }

            //Collision checks
            for (int outerI = 0; outerI < _gameObjects.Count - 1; outerI++)
            {
                GameObject outerGameObject = _gameObjects[outerI];

                for (int innerI = outerI + 1; innerI < _gameObjects.Count; innerI++)
                {
                    GameObject innerGameObject = _gameObjects[innerI];
                    outerGameObject.CollisionCheck(innerGameObject);
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(sortMode: SpriteSortMode.BackToFront);

            for (int i = 0; i < _gameObjects.Count; i++)
            {
                _gameObjects[i].Draw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

