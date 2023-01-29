using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StateGameRefactored2
{
    public class StateGame : Game
    {
        //Fields - MonoGame
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //Fields - Scenes
        private List<SceneBase> _scenes = new List<SceneBase>();
        private SceneBase _currentScene;
        private Player _player;

        //Fields - UI
        private SpriteFont _defaultFont;
        private ButtonColorScheme _buttonColorScheme;

        //Properties
        public SpriteFont DefaultFont => _defaultFont;
        public ButtonColorScheme ButtonColorScheme => _buttonColorScheme;

        //Constructor
        public StateGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _defaultFont = Content.Load<SpriteFont>("Arial");
            _buttonColorScheme = new ButtonColorScheme(Color.White, Color.Cyan, Color.Red, Color.White, _defaultFont);

            //Player setup
            _player = new Player(new Vector2(GraphicsDevice.Viewport.Width * 0.5f, GraphicsDevice.Viewport.Height * 0.66f), 140);
            _player.LoadContent(Content, GraphicsDevice.Viewport);

            //Scenes setup
            _scenes.Add(new MenuScene(this, "Menu", _player));
            _scenes.Add(new LevelScene(this, "Level1", _player));
            _scenes.Add(new Level2Scene(this, "Level2", _player));

            for (int i = 0; i < _scenes.Count; i++)
                _scenes[i].LoadSceneContent(Content, GraphicsDevice.Viewport);

            ChangeSceneTo("Menu");
        }

        protected override void Update(GameTime pGameTime)
        {
            base.Update(pGameTime);

            _currentScene.UpdateScene(pGameTime);
        }

        protected override void Draw(GameTime pGameTime)
        {
            base.Draw(pGameTime);

            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _currentScene.DrawScene(_spriteBatch);

            _spriteBatch.End();
        }

        public void ChangeSceneTo(string pSceneName)
        {
            for (int i = 0; i < _scenes.Count; i++)
            {
                SceneBase scene = _scenes[i];

                if (scene.Name == pSceneName)
                {
                    if (_currentScene != null)//In order to initialize a starting scene
                    {
                        _currentScene.OnSceneExit();
                        _currentScene.RemoveGameObject(_player);
                    }

                    _currentScene = scene;
                    _currentScene.AddGameObject(_player);
                    _currentScene.OnSceneEnter();
                }
            }
        }
    }
}
