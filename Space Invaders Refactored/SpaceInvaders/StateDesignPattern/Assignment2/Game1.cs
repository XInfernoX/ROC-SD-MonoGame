using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace StateDesignPattern.Assignment2
{
    public class Game1 : Game
    {
        //Fields
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //State
        private GameState _gameState = GameState.Menu;

        //Entities
        private Player _player;


        private Enemy _enemy;
        private GameObject[] _wayPoints;


        //Pickups
        private Shield _shield;
        private Weapon _weapon;
        private Gate _gate1;
        private Gate _gate2;




        //Buttons
        private PlayButton _playButton;
        private QuitButton _quitButton;
        private MenuButton _menuButton;

        //Text
        private Text _playText;
        private Text _quitText;
        private Text _menuText;

        //Colors
        private readonly Color _defaultColor = Color.White;
        private readonly Color _hoverColor = Color.Aquamarine;
        private readonly Color _pressedColor = Color.Red;

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

            Viewport viewPort = GraphicsDevice.Viewport;
            float third = viewPort.Height / 3;

            //Player
            _player = new Player(new Vector2(viewPort.Width / 2, third * 2), 140);
            _player.LoadContent(Content);

            //Pickups
            _shield = new Shield(new Vector2(100, third), _player);
            _shield.LoadContent(Content);

            _weapon = new Weapon(new Vector2(viewPort.Width - 100, third), _player);
            _weapon.LoadContent(Content);

            _gate1 = new Gate(new Vector2(viewPort.Width - 200, 10), _player, this, GameState.Level2);
            _gate1.LoadContent(Content);

            _gate2 = new Gate(new Vector2(10, 10), _player, this, GameState.Level1);
            _gate2.LoadContent(Content);

            _gate1.SetConnectedGate(_gate2);
            _gate2.SetConnectedGate(_gate1);

            //Enemy
            Texture2D wayPointTexture = Content.Load<Texture2D>("Flag");
            _wayPoints = new GameObject[]
            {
                new GameObject(new Vector2(viewPort.Width * 0.1f, viewPort.Height * 0.1f), wayPointTexture),
                new GameObject(new Vector2(viewPort.Width * 0.9f, viewPort.Height * 0.1f), wayPointTexture),
                new GameObject(new Vector2(viewPort.Width / 2, viewPort.Height / 2), wayPointTexture)
            };
            _enemy = new Enemy(new Vector2(viewPort.Width / 2, third), 2, _player, _wayPoints, 100);
            _enemy.LoadContent(Content);


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
                    _shield.Update(pGameTime);
                    _weapon.Update(pGameTime);
                    _gate1.Update(pGameTime);
                    break;


                case GameState.Level2:
                    _menuButton.Update(pGameTime);
                    _player.Update(pGameTime);
                    _gate2.Update(pGameTime);

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

                    _menuButton.Draw(_spriteBatch);
                    _menuText.Draw(_spriteBatch);

                    _player.Draw(_spriteBatch);
                    _enemy.Draw(_spriteBatch);
                    _shield.Draw(_spriteBatch);
                    _weapon.Draw(_spriteBatch);
                    _gate1.Draw(_spriteBatch);

                    for (int i = 0; i < _wayPoints.Length; i++)
                    {
                        _wayPoints[i].Draw(_spriteBatch);
                    }

                    break;

                case GameState.Level2:

                    _menuButton.Draw(_spriteBatch);
                    _player.Draw(_spriteBatch);
                    _gate2.Draw(_spriteBatch);

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
