using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGame_UnityFramework
{
    public class Game2 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private SceneManager _sceneManager;

        public Game2()
        {
            Console.WriteLine("Game2 ctor");

            //CONSIDER whether this needs to be in the ctor or in the Initialize function

            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _sceneManager = new SceneManager(this);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Console.WriteLine("Game2 Initialize()");

            TestScene1 testScene = new TestScene1("TestScene1");

            base.Initialize();
        }


        protected override void LoadContent()
        {
            Console.WriteLine("Game2 LoadContent()");
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _sceneManager.LoadSceneContents(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _sceneManager.UpdateAllActiveScenes(gameTime);

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            //Console.WriteLine("Game2 Draw()");

            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);

            _sceneManager.DrawAllActiveScenes(_spriteBatch);

            //CONSIDER drawing frameRate to the screen in Debug mode or something

            //_spriteBatch.Begin();
            //_spriteBatch.Draw(_playerTile, _playerPosition, Color.White);
            //_spriteBatch.Draw(_mouseTile, _mousePosition, Color.White);
            //_spriteBatch.Draw(_controllerTile, _controllerPosition, Color.White);
            //_spriteBatch.End();
        }
    }
}
