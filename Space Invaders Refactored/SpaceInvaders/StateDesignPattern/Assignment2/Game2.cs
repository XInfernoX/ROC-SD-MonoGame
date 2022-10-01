using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace StateDesignPattern.Assignment2
{
    //TODO swap CoreExample.GameObject for StateGameComplete.GameObject 
    public class Game2 : Game
    {
        //Fields
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //State
        private GameState _gameState = GameState.Menu;

        //NewButtons
        private PlayButton _playButton2;
        private QuitButton _quitButton2;
        private MenuButton _menuButton2;

        //ButtonColors
        private readonly Color _defaultColor = Color.White;
        private readonly Color _hoverColor = Color.Aquamarine;
        private readonly Color _pressedColor = Color.Red;

        //Resources
        private Texture2D _buttonTexture;


        private Texture2D _enemyTexture;
        private Texture2D _weaponTexture;
        private Texture2D _shieldTexture;
        private Texture2D _flagTexture;

        private SpriteFont _arial;

        //Player
        private  GameObject _player;
        private float _playerSpeed = 3;

        //Enemy
        private Enemy _enemy;


        //Pickups
        private readonly GameObject _shield;
        private readonly GameObject _weapon;

        //Constructor
        public Game2()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _playButton = new GameObject();
            _menuButton = new GameObject();
            _quitButton = new GameObject();

            _player = new GameObject();
            _enemy = new GameObject();

            _weapon = new GameObject();
            _shield = new GameObject();

            _wayPoints = new GameObject[]
            {
                new GameObject(),
                new GameObject(),
                new GameObject()
            };
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Viewport viewPort = GraphicsDevice.Viewport;
            float third = viewPort.Height / 3;


            //NewButtons
            _playButton2 = new PlayButton(new Vector2(viewPort.Width / 2, third), _buttonTexture, this, _defaultColor, _hoverColor, _pressedColor);
            _quitButton2 = new QuitButton(new Vector2(viewPort.Width / 2, third * 2), _buttonTexture, this, _defaultColor, _hoverColor, _pressedColor);
            _menuButton2 = new MenuButton(new Vector2(viewPort.Width - _buttonTexture.Width, 0), _buttonTexture, this, _defaultColor, _hoverColor, _pressedColor);


            //Textures
            _buttonTexture = Content.Load<Texture2D>("UI_Title_64x64");
            _playerTexture = Content.Load<Texture2D>("Knight");
            _playerShieldTexture = Content.Load<Texture2D>("KnightShield");
            _playerWeaponTexture = Content.Load<Texture2D>("KnightWeapon");
            _playerWeaponShieldTexture = Content.Load<Texture2D>("KnightWeaponShield");
            _enemyTexture = Content.Load<Texture2D>("Enemy");
            _weaponTexture = Content.Load<Texture2D>("Weapon");
            _shieldTexture = Content.Load<Texture2D>("Shield");
            _flagTexture = Content.Load<Texture2D>("Flag");

            //Fonts
            _arial = Content.Load<SpriteFont>("Arial");




            //PlayButton
            _playButton.Texture = _buttonTexture;
            _playButton.Position = new Vector2(viewPort.Width / 2, third);

            //QuitButton
            _quitButton.Texture = _buttonTexture;
            _quitButton.Position = new Vector2(viewPort.Width / 2, third * 2);

            //MenuButton
            _menuButton.Texture = _buttonTexture;
            _menuButton.Position = new Vector2(viewPort.Width - _buttonTexture.Width, 0);

            //Player
            _player.Texture = _playerTexture;
            _player.Position = new Vector2(viewPort.Width / 2, third * 2);

            //Enemy
            _enemy.Texture = _enemyTexture;
            _enemy.Position = new Vector2(viewPort.Width / 2, third);

            //Pickups
            _weapon.Texture = _weaponTexture;
            _weapon.Position = new Vector2(100, third);
            _shield.Texture = _shieldTexture;
            _shield.Position = new Vector2(viewPort.Width - 100, third);
            _shield.Position = new Vector2(viewPort.Width - 100, third);

            //WayPoints
            for (int i = 0; i < _wayPoints.Length; i++)
            {
                _wayPoints[i].Texture = _flagTexture;
            }
            _wayPoints[0].Position = new Vector2(viewPort.Width * 0.1f, viewPort.Height * 0.1f);
            _wayPoints[1].Position = new Vector2(viewPort.Width * 0.9f, viewPort.Height * 0.1f);
            _wayPoints[2].Position = new Vector2(viewPort.Width / 2, viewPort.Height / 2);
        }

        private void MoveToLevel1()
        {
            _gameState = GameState.Level1;
        }

        private void MoveToMenu()
        {
            _gameState = GameState.Menu;
        }

        private void QuitGame()
        {
            Exit();
        }

        protected override void Update(GameTime pGameTime)
        {
            base.Update(pGameTime);

            switch (_gameState)
            {
                case GameState.Menu:
                    UpdateMenu(pGameTime);
                    break;
                case GameState.Level1:
                    UpdateLevel1(pGameTime);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void UpdateMenu(GameTime pGameTime)
        {
            _playButton2.Update(pGameTime);
            _quitButton2.Update(pGameTime);
        }

        private void UpdateLevel1(GameTime pGameTime)
        {
            _menuButton2.Update(pGameTime);
            _player.Update(pGameTime);
        }

        protected override void Draw(GameTime pGameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            switch (_gameState)
            {
                case GameState.Menu:
                    DrawMenu(pGameTime);
                    break;
                case GameState.Level1:
                    DrawLevel1(pGameTime);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _spriteBatch.End();

            base.Draw(pGameTime);
        }

        private void DrawMenu(GameTime pGameTime)
        {
            _playButton.Draw(_spriteBatch, _currentPlayButtonColor);
            _quitButton.Draw(_spriteBatch, _currentQuitButtonColor);

            Vector2 textOffset = new Vector2(_buttonTexture.Width / 2, _buttonTexture.Height / 2);

            Vector2 playSize = _arial.MeasureString("Play");
            _spriteBatch.DrawString(_arial, "Play", _playButton.Position + textOffset - playSize / 2, Color.White);

            Vector2 quitSize = _arial.MeasureString("Quit");
            _spriteBatch.DrawString(_arial, "Quit", _quitButton.Position + textOffset - quitSize / 2, Color.White);
        }

        private void DrawLevel1(GameTime pGameTime)
        {
            _menuButton.Draw(_spriteBatch, _currentMenuButtonColor);

            Vector2 textOffset = new Vector2(_buttonTexture.Width / 2, _buttonTexture.Height / 2);

            Vector2 menuSize = _arial.MeasureString("Menu");
            _spriteBatch.DrawString(_arial, "Menu", _menuButton.Position + textOffset - menuSize / 2, Color.White);

            _player.Draw(_spriteBatch, Color.White);
            _enemy.Draw(_spriteBatch, Color.White);
            _shield.Draw(_spriteBatch, Color.White);
            _weapon.Draw(_spriteBatch, Color.White);

            for (int i = 0; i < _wayPoints.Length; i++)
            {
                _wayPoints[i].Draw(_spriteBatch);
            }
        }
    }
}
