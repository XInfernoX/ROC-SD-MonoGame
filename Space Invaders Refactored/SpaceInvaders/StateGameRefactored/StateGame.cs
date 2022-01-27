using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StateGameRefactored1;

namespace StateGameRefactored
{
    public class StateGame : Game
    {
        //Fields
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //State
        private GameState _gameState = GameState.Menu;

        //Entities
        private Player _player;
        private Enemy _enemy;

        //Pickups
        private GameObject _shield;
        private GameObject _weapon;

        private GameObject[] _wayPoints;

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

            Viewport viewPort = GraphicsDevice.Viewport;
            float third = viewPort.Height / 3;

            //Buttons
            Texture2D buttonTexture = Content.Load<Texture2D>("UI_Title_64x64");
            _playButton = new PlayButton(new Vector2(viewPort.Width / 2, third), buttonTexture, this, _defaultColor,
                _hoverColor, _pressedColor);
            _quitButton = new QuitButton(new Vector2(viewPort.Width / 2, third * 2), buttonTexture, this, _defaultColor,
                _hoverColor, _pressedColor);
            _menuButton = new MenuButton(new Vector2(viewPort.Width - buttonTexture.Width, 0), buttonTexture, this,
                _defaultColor, _hoverColor, _pressedColor);

            //Text
            Vector2 buttonSize = new Vector2(buttonTexture.Width, buttonTexture.Height);
            SpriteFont font = Content.Load<SpriteFont>("Arial");
            _playText = new Text(_playButton.Position + buttonSize / 2, font, "Play", Color.White);
            _quitText = new Text(_quitButton.Position + buttonSize / 2, font, "Quit", Color.White);
            _menuText = new Text(_menuButton.Position + buttonSize / 2, font, "Menu", Color.White);

            //Pickups
            Texture2D shieldTexture = Content.Load<Texture2D>("Shield");
            _shield = new GameObject(new Vector2(100, third), shieldTexture);

            Texture2D weaponTexture = Content.Load<Texture2D>("Weapon");
            _weapon = new GameObject(new Vector2(viewPort.Width - 100, third), weaponTexture);

            //Player
            Texture2D playerTexture = Content.Load<Texture2D>("Knight");
            Texture2D playerShieldTexture = Content.Load<Texture2D>("KnightShield");
            Texture2D playerWeaponTexture = Content.Load<Texture2D>("KnightWeapon");
            Texture2D playerWeaponShieldTexture = Content.Load<Texture2D>("KnightWeaponShield");
            _player = new Player(new Vector2(viewPort.Width / 2, third * 2), playerTexture, 3, 
                _shield, _weapon, playerShieldTexture, playerWeaponTexture, playerWeaponShieldTexture);

            //Enemy
            Texture2D wayPointTexture = Content.Load<Texture2D>("Flag");
            _wayPoints = new GameObject[]
            {
                new GameObject(new Vector2(viewPort.Width * 0.1f, viewPort.Height * 0.1f), wayPointTexture),
                new GameObject(new Vector2(viewPort.Width * 0.9f, viewPort.Height * 0.1f), wayPointTexture),
                new GameObject(new Vector2(viewPort.Width / 2, viewPort.Height / 2), wayPointTexture)
            };
            Texture2D enemyTexture = Content.Load<Texture2D>("Enemy");
            _enemy = new Enemy(new Vector2(viewPort.Width / 2, third), enemyTexture, 2, _wayPoints, _player, 100, _shield, _weapon);
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

        protected override void Draw(GameTime pGameTime)
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

                    for (int i = 0; i < _wayPoints.Length; i++)
                    {
                        _wayPoints[i].Draw(_spriteBatch);
                    }

                    _menuButton.Draw(_spriteBatch);
                    _menuText.Draw(_spriteBatch);

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _spriteBatch.End();
            
            base.Draw(pGameTime);
        }

        public void SetGameState(GameState pGameState)
        {
            _gameState = pGameState;
        }
    }
}
