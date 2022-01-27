using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using StateGame;

namespace StateGameRefactored
{
    public class StateGameRefactored : Game
    {
        //Fields
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //State
        private GameState _gameState = GameState.Menu;

        //Entities
        private Player _player;
        private Enemy _enemy;
        private GameObject _shield;
        private GameObject _weapon;

        //Buttons
        private PlayButton _playButton;
        private QuitButton _quitButton;
        private MenuButton _menuButton;

        //Text
        private Text _playText;
        private Text _quitText;
        private Text _menuText;

        private readonly Color _defaultColor = Color.White;
        private readonly Color _hoverColor = Color.Aquamarine;
        private readonly Color _pressedColor = Color.Red;

        //Constructor
        public StateGameRefactored()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Viewport viewPort = GraphicsDevice.Viewport;
            float third = viewPort.Height / 3;

            //Buttons
            Texture2D buttonTexture = Content.Load<Texture2D>("UI_Title_64x64");
            _playButton = new PlayButton(new Vector2(viewPort.Width / 2, third), buttonTexture, this, _defaultColor, _hoverColor, _pressedColor);
            _quitButton = new QuitButton(new Vector2(viewPort.Width / 2, third * 2), buttonTexture, this, _defaultColor, _hoverColor, _pressedColor);
            _menuButton = new MenuButton(new Vector2(viewPort.Width - buttonTexture.Width, 0), buttonTexture, this, _defaultColor, _hoverColor, _pressedColor);

            //Text
            Vector2 buttonSize = new Vector2(buttonTexture.Width, buttonTexture.Height);
            SpriteFont font = Content.Load<SpriteFont>("Arial");
            _playText = new Text(_playButton.Position + buttonSize / 2, font, "Play", Color.White);
            _quitText = new Text(_quitButton.Position + buttonSize / 2, font, "Quit", Color.White);
            _menuText = new Text(_menuButton.Position + buttonSize / 2, font, "Menu", Color.White);

            //Player
            Texture2D playerTexture = Content.Load<Texture2D>("Knight");
            _player = new Player(new Vector2(viewPort.Width / 2, third), playerTexture);

            //Enemy
            Texture2D wayPointTexture = Content.Load<Texture2D>("Flag");
            GameObject[] wayPoints = new GameObject[]
            {
                new GameObject(new Vector2(viewPort.Width * 0.1f, viewPort.Height * 0.1f), wayPointTexture)
            };
            Texture2D enemyTexture = Content.Load<Texture2D>("Enemy");
            _enemy = new Enemy(new Vector2(viewPort.Width / 2, third), enemyTexture, wayPoints, _player, 100, _shield, _weapon);


            ////Resources
            //SpriteFont font = Content.Load<SpriteFont>("Arial");
            //Texture2D buttonTexture = Content.Load<Texture2D>("UI_Title_64x64");
            //ButtonColorScheme buttonColorScheme = new ButtonColorScheme(Color.White, Color.Aquamarine, Color.Red);

            ////PlayButton
            //Text playText = new Text(font, "Play", new Vector2(0, 0), new Vector2(0.5f, 0.5f), Color.White);
            //Button playButton = new Button(buttonTexture, buttonColorScheme);
            //playButton.OnButtonClick += MoveToLevel1;
            //GameObject playButtonObject = new GameObject(this, "PlayButton", new Vector2(viewport.Width / 2, third), new Vector2(0.5f, 0.5f), 0, new Vector2(1, 1), playButton, playText);
            //AddGameObject(playButtonObject);

            ////QuitButton
            //Text quitText = new Text(font, "Quit", new Vector2(0, 0), new Vector2(0.5f, 0.5f), Color.White);
            //Button quitButton = new Button(buttonTexture, buttonColorScheme);
            //quitButton.OnButtonClick += QuitGame;
            //GameObject quitButtonObject = new GameObject(this, "QuitButton", new Vector2(viewport.Width / 2, third * 2), new Vector2(0.5f, 0.5f), 0, new Vector2(1, 1), quitButton, quitText);
            //AddGameObject(quitButtonObject);
        }

        protected override void Update(GameTime pGameTime)
        {
            base.Update(pGameTime);

            switch (_gameState)
            {
                case GameState.Menu:
                    _playButton.Update(pGameTime);
                    _quitButton.Update(pGameTime);
                    break;
                case GameState.Level1:

                    _menuButton.Update(pGameTime);
                    _player.Update(pGameTime);

                    _enemy.Update(pGameTime);
                    break;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            switch (_gameState)
            {
                case GameState.Menu:

                    _playButton.Draw(_spriteBatch);
                    _playText.Draw(_spriteBatch);
                    _quitButton.Draw(_spriteBatch);
                    _quitText.Draw(_spriteBatch);

                    break;
                case GameState.Level1:

                    _player.Draw(_spriteBatch);
                    _enemy.Draw(_spriteBatch);
                    _shield.Draw(_spriteBatch);
                    _weapon.Draw(_spriteBatch);

                    _menuButton.Draw(_spriteBatch);
                    _menuText.Draw(_spriteBatch);

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _spriteBatch.End();
            
            base.Draw(gameTime);
        }

        

        public void SetGameState(GameState pGameState)
        {
            _gameState = pGameState;
        }
    }
}
