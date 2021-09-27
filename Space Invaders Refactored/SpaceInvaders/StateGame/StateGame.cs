using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace StateGame
{
    //TODO swap CoreExample.GameObject for StateGame.GameObject 
    public class StateGame : Game
    {
        //Fields
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //State
        private GameState _gameState = GameState.Menu;

        //PlayButton
        private readonly CoreExample.GameObject _playButton;
        private ButtonStatus _playButtonStatus;
        private Color _currentPlayButtonColor = Color.White;

        //QuitButton
        private readonly CoreExample.GameObject _quitButton;
        private ButtonStatus _quitButtonStatus;
        private Color _currentQuitButtonColor = Color.White;

        //MenuButton
        private readonly CoreExample.GameObject _menuButton;
        private ButtonStatus _menuButtonStatus;
        private Color _currentMenuButtonColor = Color.White;

        //ButtonColors
        private readonly Color _defaultColor = Color.White;
        private readonly Color _hoverColor = Color.Aquamarine;
        private readonly Color _pressedColor = Color.Red;

        //Resources
        private Texture2D _buttonTexture;
        private Texture2D _playerTexture;
        private Texture2D _playerShieldTexture;
        private Texture2D _playerWeaponTexture;
        private Texture2D _playerWeaponShieldTexture;
        private Texture2D _enemyTexture;
        private Texture2D _weaponTexture;
        private Texture2D _shieldTexture;
        private Texture2D _flagTexture;

        private SpriteFont _arial;

        //Player
        private readonly CoreExample.GameObject _player;
        private float _playerSpeed = 3;

        //Enemy
        private readonly CoreExample.GameObject _enemy;
        private float _enemySpeed = 2;
        private EnemyState _enemyState = EnemyState.Patrolling;
        private readonly CoreExample.GameObject[] _wayPoints;
        private int _currentWayPointIndex = 0;
        private float _playerDetectionRange = 100;

        //Pickups
        private readonly CoreExample.GameObject _shield;
        private readonly CoreExample.GameObject _weapon;

        //Constructor
        public StateGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _playButton = new CoreExample.GameObject();
            _menuButton = new CoreExample.GameObject();
            _quitButton = new CoreExample.GameObject();

            _player = new CoreExample.GameObject();
            _enemy = new CoreExample.GameObject();

            _weapon = new CoreExample.GameObject();
            _shield = new CoreExample.GameObject();

            _wayPoints = new CoreExample.GameObject[]
            {
                new CoreExample.GameObject(),
                new CoreExample.GameObject(),
                new CoreExample.GameObject()
            };
        }

        protected override void Initialize()
        {
            _playButton.Active = true;
            _menuButton.Active = true;
            _quitButton.Active = true;

            _player.Active = true;
            _enemy.Active = true;

            _weapon.Active = true;
            _shield.Active = true;

            for (int i = 0; i < _wayPoints.Length; i++)
            {
                _wayPoints[i].Active = true;
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            _spriteBatch = new SpriteBatch(GraphicsDevice);

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


            Viewport viewPort = GraphicsDevice.Viewport;
            float third = viewPort.Height / 3;

            //PlayButton
            _playButton.AddTexture(_buttonTexture);
            _playButton.SetPosition(new Vector2(viewPort.Width / 2, third));

            //QuitButton
            _quitButton.AddTexture(_buttonTexture);
            _quitButton.SetPosition(new Vector2(viewPort.Width / 2, third * 2));

            //MenuButton
            _menuButton.AddTexture(_buttonTexture);
            _menuButton.SetPosition(new Vector2(viewPort.Width - _buttonTexture.Width, 0));

            //Player
            _player.AddTexture(_playerTexture);
            _player.SetPosition(new Vector2(viewPort.Width / 2, third * 2));

            //Enemy
            _enemy.AddTexture(_enemyTexture);
            _enemy.SetPosition(new Vector2(viewPort.Width / 2, third));

            //Pickups
            _weapon.AddTexture(_weaponTexture);
            _weapon.SetPosition(new Vector2(100, third));
            _shield.AddTexture(_shieldTexture);
            _shield.SetPosition(new Vector2(viewPort.Width - 100, third));

            //WayPoints
            for (int i = 0; i < _wayPoints.Length; i++)
            {
                _wayPoints[i].AddTexture(_flagTexture);
            }
            _wayPoints[0].SetPosition(new Vector2(viewPort.Width * 0.1f, viewPort.Height * 0.1f));
            _wayPoints[1].SetPosition(new Vector2(viewPort.Width * 0.9f, viewPort.Height * 0.1f));
            _wayPoints[2].SetPosition(new Vector2(viewPort.Width / 2, viewPort.Height / 2));

            //Viewport viewport = GraphicsDevice.Viewport;
            //float third = viewport.Height / 3;

            //Resources
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
            MouseState mouseState = Mouse.GetState();

            //PlayButtonBehaviour
            switch (_playButtonStatus)
            {
                case ButtonStatus.Default:
                    if (_playButton.Collision(mouseState.Position))
                    {
                        _playButtonStatus = ButtonStatus.Hovered;
                        _currentPlayButtonColor = _hoverColor;
                    }
                    break;
                case ButtonStatus.Hovered:
                    if (!_playButton.Collision(mouseState.Position))
                    {
                        _playButtonStatus = ButtonStatus.Default;
                        _currentPlayButtonColor = _defaultColor;
                    }

                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        _playButtonStatus = ButtonStatus.Pressed;
                        _currentPlayButtonColor = _pressedColor;
                    }
                    break;
                case ButtonStatus.Pressed:
                    if (mouseState.LeftButton == ButtonState.Released)
                    {
                        if (_playButton.Collision(mouseState.Position))
                        {
                            _playButtonStatus = ButtonStatus.Hovered;
                            _currentPlayButtonColor = _hoverColor;
                            MoveToLevel1();
                        }
                        else
                        {
                            _playButtonStatus = ButtonStatus.Default;
                            _currentPlayButtonColor = _defaultColor;
                        }
                    }
                    break;
            }

            //QuitButtonBehaviour
            switch (_quitButtonStatus)
            {
                case ButtonStatus.Default:
                    if (_quitButton.Collision(mouseState.Position))
                    {
                        _quitButtonStatus = ButtonStatus.Hovered;
                        _currentQuitButtonColor = _hoverColor;
                    }
                    break;
                case ButtonStatus.Hovered:
                    if (!_quitButton.Collision(mouseState.Position))
                    {
                        _quitButtonStatus = ButtonStatus.Default;
                        _currentQuitButtonColor = _defaultColor;
                    }

                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        _quitButtonStatus = ButtonStatus.Pressed;
                        _currentQuitButtonColor = _pressedColor;
                    }
                    break;
                case ButtonStatus.Pressed:
                    if (mouseState.LeftButton == ButtonState.Released)
                    {
                        if (_quitButton.Collision(mouseState.Position))
                        {
                            //Click!
                            _quitButtonStatus = ButtonStatus.Hovered;
                            _currentQuitButtonColor = _hoverColor;
                            QuitGame();
                        }
                        else
                        {
                            _quitButtonStatus = ButtonStatus.Default;
                            _currentQuitButtonColor = _defaultColor;
                        }
                    }
                    break;
            }
        }

        private void UpdateLevel1(GameTime pGameTime)
        {
            MouseState mouseState = Mouse.GetState();
            KeyboardState keyboardState = Keyboard.GetState();

            //MenuButton
            switch (_menuButtonStatus)
            {
                case ButtonStatus.Default:
                    if (_menuButton.Collision(mouseState.Position))
                    {
                        _menuButtonStatus = ButtonStatus.Hovered;
                        _currentMenuButtonColor = _hoverColor;
                    }
                    break;
                case ButtonStatus.Hovered:
                    if (!_menuButton.Collision(mouseState.Position))
                    {
                        _menuButtonStatus = ButtonStatus.Default;
                        _currentMenuButtonColor = _defaultColor;
                    }

                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        _menuButtonStatus = ButtonStatus.Pressed;
                        _currentMenuButtonColor = _pressedColor;
                    }
                    break;
                case ButtonStatus.Pressed:
                    if (mouseState.LeftButton == ButtonState.Released)
                    {
                        if (_menuButton.Collision(mouseState.Position))
                        {
                            _menuButtonStatus = ButtonStatus.Hovered;
                            _currentMenuButtonColor = _hoverColor;
                            MoveToMenu();
                        }
                        else
                        {
                            _menuButtonStatus = ButtonStatus.Default;
                            _currentMenuButtonColor = _defaultColor;
                        }
                    }
                    break;
            }

            //PlayerInput
            Vector2 playerInput = Vector2.Zero;
            if (keyboardState.IsKeyDown(Keys.A))
                playerInput.X--;
            if (keyboardState.IsKeyDown(Keys.D))
                playerInput.X++;
            if (keyboardState.IsKeyDown(Keys.W))
                playerInput.Y--;
            if (keyboardState.IsKeyDown(Keys.S))
                playerInput.Y++;

            if (playerInput != Vector2.Zero)
            {
                playerInput.Normalize();
                Vector2 playerTranslation = playerInput * _playerSpeed;
                _player.SetPosition(_player.GetPosition() + playerTranslation);
            }

            if (_player.Collision(_weapon))
            {
                _weapon.Active = false;

                if (_shield.Active)
                {
                    _player.AddTexture(_playerWeaponTexture);
                }
                else
                {
                    _player.AddTexture(_playerWeaponShieldTexture);
                }
            }

            if (_player.Collision(_shield))
            {
                _shield.Active = false;

                if (_weapon.Active)
                {
                    _player.AddTexture(_playerShieldTexture);
                }
                else
                {
                    _player.AddTexture(_playerWeaponShieldTexture);
                }
            }




            //Enemy Behaviour
            switch (_enemyState)
            {
                case EnemyState.Patrolling:
                    //PatrolMovement
                    Vector2 currentWayPoint = _wayPoints[_currentWayPointIndex].GetPosition();
                    Vector2 patrolDirection = (currentWayPoint - _enemy.GetPosition());
                    patrolDirection.Normalize();
                    Vector2 patrolTranslation =
                        patrolDirection * _enemySpeed;
                    _enemy.SetPosition(_enemy.GetPosition() + patrolTranslation);

                    Vector2 wayPointDifference = _enemy.GetPosition() - currentWayPoint;
                    if (wayPointDifference.Length() < 5)
                    {
                        _currentWayPointIndex++;
                        _currentWayPointIndex %= _wayPoints.Length;
                    }

                    //PlayerDetection
                    Vector2 playerDifferencePatrol = _enemy.GetPosition() - _player.GetPosition();
                    float playerDistancePatrol = playerDifferencePatrol.Length();
                    if (playerDistancePatrol < _playerDetectionRange)
                    {
                        if (!_weapon.Active && !_shield.Active)
                            _enemyState = EnemyState.Evading;
                        else
                            _enemyState = EnemyState.Chasing;
                    }
                    break;
                case EnemyState.Chasing:
                    Vector2 chaseDirection = _player.GetPosition() - _enemy.GetPosition();
                    chaseDirection.Normalize();
                    Vector2 chaseTranslation = chaseDirection * _enemySpeed;
                    _enemy.SetPosition(_enemy.GetPosition() + chaseTranslation);

                    Vector2 playerDifferenceChase = _enemy.GetPosition() - _player.GetPosition();
                    float playerDistanceChase = playerDifferenceChase.Length();

                    if (playerDistanceChase < _playerDetectionRange)
                    {
                        if (!_weapon.Active && !_shield.Active)
                            _enemyState = EnemyState.Evading;
                    }
                    else
                    {
                        _enemyState = EnemyState.Patrolling;
                    }

                    break;
                case EnemyState.Evading:
                    Console.WriteLine("Evading");
                    Vector2 evadeDirection = _player.GetPosition() - _enemy.GetPosition();
                    evadeDirection.Normalize();
                    Vector2 evadeTranslation = -evadeDirection * _enemySpeed;
                    _enemy.SetPosition(_enemy.GetPosition() + evadeTranslation);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
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
            _spriteBatch.DrawString(_arial, "Play", _playButton.GetPosition() + textOffset - playSize / 2, Color.White);

            Vector2 quitSize = _arial.MeasureString("Quit");
            _spriteBatch.DrawString(_arial, "Quit", _quitButton.GetPosition() + textOffset - playSize / 2, Color.White);
        }

        private void DrawLevel1(GameTime pGameTime)
        {
            _menuButton.Draw(_spriteBatch, _currentMenuButtonColor);

            Vector2 textOffset = new Vector2(_buttonTexture.Width / 2, _buttonTexture.Height / 2);

            Vector2 menuSize = _arial.MeasureString("Menu");
            _spriteBatch.DrawString(_arial, "Menu", _menuButton.GetPosition() + textOffset - menuSize / 2, Color.White);

            _player.Draw(_spriteBatch, Color.White, 2);
            _enemy.Draw(_spriteBatch, Color.White, 2);
            _shield.Draw(_spriteBatch, Color.White, 2);
            _weapon.Draw(_spriteBatch, Color.White, 2);

            for (int i = 0; i < _wayPoints.Length; i++)
            {
                _wayPoints[i].Draw(_spriteBatch);
            }
        }
    }
}
