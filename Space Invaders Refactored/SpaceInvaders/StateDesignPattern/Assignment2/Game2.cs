using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StateDesignPattern.Assignment2
{
    //TODO swap CoreExample.GameObject for Assignment2.GameObject 
    public class Game2 : Game
    {
        //Fields
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //State
        private GameState _gameState = GameState.Menu;

        //NewButtons
        private PlayButton _playButton;
        private QuitButton _quitButton;
        private MenuButton _menuButton;

        //Player
        private Player _player;

        //Enemy
        private Enemy _enemy;
        private Enemy _enemy2;

        //Pickups
        private Shield _shield;
        private Weapon _weapon;

        //Resources
        private SpriteFont _arial;
        private Texture2D _buttonTexture;

        //ButtonColors
        private readonly Color _defaultColor = Color.White;
        private readonly Color _hoverColor = Color.Aquamarine;
        private readonly Color _pressedColor = Color.Red;


        //WayPoints
        private GameObject[] _wayPoints1;
        private GameObject[] _wayPoints2;


        //Constructor
        public Game2()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
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

            //Resources
            SpriteFont arial = Content.Load<SpriteFont>("Arial");
            _buttonTexture = Content.Load<Texture2D>("UI_Title_64x64");
            Texture2D _flagTexture = Content.Load<Texture2D>("Flag");

            //NewButtons
            // _playButton = new PlayButton(new Vector2(viewPort.Width / 2, third), _buttonTexture, this, _defaultColor, _hoverColor, _pressedColor);
            //_quitButton = new QuitButton(new Vector2(viewPort.Width / 2, third * 2), _buttonTexture, this, _defaultColor, _hoverColor, _pressedColor);
            //_menuButton = new MenuButton(new Vector2(viewPort.Width - _buttonTexture.Width, 0), _buttonTexture, this, _defaultColor, _hoverColor, _pressedColor);

            //Player
            _player = new Player(new Vector2(viewPort.Width / 2, third * 2), 140);
            _player.LoadContent(Content);

            //Enemy1
            _wayPoints1 = new GameObject[]
            {
                new GameObject(new Vector2(viewPort.Width * 0.1f, viewPort.Height * 0.1f), _flagTexture),
                new GameObject(new Vector2(viewPort.Width * 0.9f, viewPort.Height * 0.1f), _flagTexture),
                new GameObject(new Vector2(viewPort.Width / 2, viewPort.Height / 2), _flagTexture)
            };
            _enemy = new Enemy(new Vector2(viewPort.Width / 2, third), 100, _player, _wayPoints1, 10);
            _enemy.LoadContent(Content);

            _wayPoints2 = new GameObject[]
            {
                new GameObject(new Vector2(viewPort.Width * 0.1f, viewPort.Height * 0.1f), _flagTexture),
                new GameObject(new Vector2(viewPort.Width * 0.9f, viewPort.Height * 0.1f), _flagTexture),
                new GameObject(new Vector2(viewPort.Width / 2, viewPort.Height / 2), _flagTexture)
            };
            _enemy2 = new Enemy(new Vector2(viewPort.Width / 2, third), 100, _player, _wayPoints2, 10);
            _enemy2.LoadContent(Content);




            //Pickups
            _weapon = new Weapon(new Vector2(100, third), _player);
            _weapon.LoadContent(Content);

            _shield = new Shield(new Vector2(viewPort.Width - 100, third), _player);
            _shield.LoadContent(Content);
        }

        public void MoveGameStateTo(GameState pState)
        {
            _gameState = pState;
        }

        public void QuitGame()
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
            _playButton.Update(pGameTime);
            _quitButton.Update(pGameTime);
        }

        private void UpdateLevel1(GameTime pGameTime)
        {
            _menuButton.Update(pGameTime);
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
            _playButton.Draw(_spriteBatch);
            _quitButton.Draw(_spriteBatch);

            Vector2 textOffset = new Vector2(_buttonTexture.Width / 2, _buttonTexture.Height / 2);

            Vector2 playSize = _arial.MeasureString("Play");
            _spriteBatch.DrawString(_arial, "Play", _playButton.Position + textOffset - playSize / 2, Color.White);

            Vector2 quitSize = _arial.MeasureString("Quit");
            _spriteBatch.DrawString(_arial, "Quit", _quitButton.Position + textOffset - quitSize / 2, Color.White);
        }

        private void DrawLevel1(GameTime pGameTime)
        {
            _menuButton.Draw(_spriteBatch);

            Vector2 textOffset = new Vector2(_buttonTexture.Width / 2, _buttonTexture.Height / 2);

            Vector2 menuSize = _arial.MeasureString("Menu");
            _spriteBatch.DrawString(_arial, "Menu", _menuButton.Position + textOffset - menuSize / 2, Color.White);

            _player.Draw(_spriteBatch, Color.White);
            _enemy.Draw(_spriteBatch, Color.White);
            _shield.Draw(_spriteBatch, Color.White);
            _weapon.Draw(_spriteBatch, Color.White);

            for (int i = 0; i < _wayPoints1.Length; i++)
            {
                _wayPoints1[i].Draw(_spriteBatch);
            }

            for (int i = 0; i < _wayPoints2.Length; i++)
            {
                _wayPoints2[i].Draw(_spriteBatch);
            }
        }
    }
}
