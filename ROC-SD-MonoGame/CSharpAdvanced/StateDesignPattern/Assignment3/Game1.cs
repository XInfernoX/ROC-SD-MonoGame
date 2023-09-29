using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ROC_SD_MonoGame.CSharpAdvanced.StateDesignPattern.Assignment3
{
    //Added Scenes (SceneBase)
    //Added LoadContent function to GameObject to reduce Constructor arguments

    //TODO finish Enemy
    public class Game1 : Game
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
            _defaultFont = Content.Load<SpriteFont>("Arial");
            _buttonColorScheme = new ButtonColorScheme(Color.White, Color.Cyan, Color.Red, Color.White, _defaultFont);

            //Player setup
            _player = new Player(new Vector2(GraphicsDevice.Viewport.Width * 0.5f, GraphicsDevice.Viewport.Height * 0.66f), 140);
            _player.LoadContent(Content, GraphicsDevice.Viewport);

            //Scenes setup

            MenuScene menuScene = new MenuScene(this, "Menu", _player);
            _scenes.Add(menuScene);

            Level1Scene level1Scene = new Level1Scene(this, "Level1", _player);
            _scenes.Add(level1Scene);

            Level2Scene level2Scene = new Level2Scene(this, "Level2", _player);
            _scenes.Add(level2Scene);

            Level3Scene level3Scene = new Level3Scene(this, "Level3", _player);
            _scenes.Add(level3Scene);

            Level4Scene level4Scene = new Level4Scene(this, "Level4", _player);
            _scenes.Add(level4Scene);

            //Scenes LoadContent
            for (int i = 0; i < _scenes.Count; i++)
                _scenes[i].LoadSceneContent(Content, GraphicsDevice.Viewport);
            

            //Gate connections
            ConnectTwoGates(level1Scene.Level2Gate, level2Scene.Level1Gate);
            ConnectTwoGates(level2Scene.Level3Gate, level3Scene.Level2Gate);
            ConnectTwoGates(level3Scene.Level4Gate, level4Scene.Level3Gate);

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
                    ChangeSceneTo(scene);
            }
        }

        public void ChangeSceneTo(SceneBase pScene)
        {
            if (_currentScene != null)
            {
                _currentScene.OnSceneExit();
                _currentScene.RemoveGameObject(_player);
            }

            _currentScene = pScene;
            _currentScene.AddGameObject(_player);
            _currentScene.OnSceneEnter();
        }

        private void ConnectTwoGates(Gate pGate1, Gate pGate2)
        {
            pGate1.ConnectedGate = pGate2;
            pGate2.ConnectedGate = pGate1;
        }
    }
}
