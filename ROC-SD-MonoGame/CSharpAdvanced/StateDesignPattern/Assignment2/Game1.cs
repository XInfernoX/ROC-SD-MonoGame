using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CSharpAdvanced.CSharpAdvanced.StateDesignPattern.Assignment2
{
    //Strings en magic numbers
    //Enums
    //State Design Pattern
    //Overload
    //Overload vs Override

    public class Game1 : Game
    {
        //Fields
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //State
        private GameState _gameState = GameState.Menu;

        //Entities
        private Player _player;

        private Enemy _enemy1;
        private GameObject[] _wayPoints1;

        private Enemy _enemy2;
        private GameObject[] _wayPoints2;

        private Enemy _enemy3;
        private GameObject[] _wayPoints3;


        //Pickups
        private Shield _shield;
        private Weapon _weapon;
        private Gate _gate1;
        private Gate _gate2;

        //Buttons
        private PlayButton _playButton;
        private QuitButton _quitButton;
        private MenuButton _menuButton;

        //Constructor
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Viewport viewPort = GraphicsDevice.Viewport;
            float third = viewPort.Height / 3;

            //Textures and fonts
            Texture2D wayPointTexture = Content.Load<Texture2D>("Flag");
            Texture2D buttonTexture = Content.Load<Texture2D>("UI_Title_64x64");
            Texture2D gateTexture = Content.Load<Texture2D>("Gate");
            Texture2D weaponTexture = Content.Load<Texture2D>("Weapon");
            Texture2D shieldTexture = Content.Load<Texture2D>("Weapon");

            SpriteFont font = Content.Load<SpriteFont>("Arial");

            //Player
            _player = new Player(new Vector2(viewPort.Width / 2, third * 2), 300, Content);

            //Pickups
            _shield = new Shield(new Vector2(100, third), shieldTexture, _player);
            _weapon = new Weapon(new Vector2(viewPort.Width - 100, third), weaponTexture, _player);
            _gate1 = new Gate(new Vector2(viewPort.Width / 2.0f, 40), gateTexture, _player, this, GameState.Level2);
            _gate2 = new Gate(new Vector2(40, 40), gateTexture, _player, this, GameState.Level1);

            _gate1.SetConnectedGate(_gate2);
            _gate2.SetConnectedGate(_gate1);

            //Enemies
            _wayPoints1 = new GameObject[]
            {
                new GameObject(new Vector2(viewPort.Width * 0.1f, viewPort.Height * 0.1f), wayPointTexture),
                new GameObject(new Vector2(viewPort.Width * 0.9f, viewPort.Height * 0.1f), wayPointTexture),
                new GameObject(new Vector2(viewPort.Width / 2, viewPort.Height / 2), wayPointTexture)
            };
            _enemy1 = new Enemy(new Vector2(viewPort.Width / 2, viewPort.Height / 2), Content, 2, _player, _wayPoints1, 100);


            _wayPoints2 = new GameObject[]
            {
                new GameObject(new Vector2(viewPort.Width * 0.1f, viewPort.Height * 0.1f), wayPointTexture),
                new GameObject(new Vector2(viewPort.Width * 0.4f, viewPort.Height * 0.1f), wayPointTexture),
                new GameObject(new Vector2(viewPort.Width * 0.4f, viewPort.Height * 0.9f), wayPointTexture),
                new GameObject(new Vector2(viewPort.Width * 0.1f, viewPort.Height * 0.9f), wayPointTexture),
            };
            _enemy2 = new Enemy(new Vector2(viewPort.Width * 0.35f, viewPort.Height * 0.5f), Content, 2, _player, _wayPoints2, 100);

            _wayPoints3 = new GameObject[]
            {
                new GameObject(new Vector2(viewPort.Width * 0.6f, viewPort.Height * 0.1f), wayPointTexture),
                new GameObject(new Vector2(viewPort.Width * 0.9f, viewPort.Height * 0.1f), wayPointTexture),
                new GameObject(new Vector2(viewPort.Width * 0.6f, viewPort.Height * 0.9f), wayPointTexture),
                new GameObject(new Vector2(viewPort.Width * 0.9f, viewPort.Height * 0.9f), wayPointTexture),
             };
            _enemy3 = new Enemy(new Vector2(viewPort.Width * 0.75f, viewPort.Height * 0.5f), Content, 2, _player, _wayPoints3, 100);


            //Buttons
            ButtonColorScheme colorScheme = new ButtonColorScheme(Color.White, Color.Aquamarine, Color.Red, Color.White, font);

            _playButton = new PlayButton(new Vector2(viewPort.Width / 2, third), buttonTexture, this, colorScheme, "Play");
            _quitButton = new QuitButton(new Vector2(viewPort.Width / 2, third * 2), buttonTexture, this, colorScheme, "Quit");
            _menuButton = new MenuButton(new Vector2(viewPort.Width, 0), buttonTexture, this, colorScheme, new Vector2(1, 0), "Menu");
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
                    _enemy1.Update(pGameTime);

                    _shield.Update(pGameTime);
                    _weapon.Update(pGameTime);
                    _gate1.Update(pGameTime);
                    break;


                case GameState.Level2:
                    _menuButton.Update(pGameTime);

                    _player.Update(pGameTime);
                    _enemy2.Update(pGameTime);
                    _enemy3.Update(pGameTime);

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
                    _quitButton.Draw(_spriteBatch);

                    break;
                case GameState.Level1:

                    _menuButton.Draw(_spriteBatch);

                    _player.Draw(_spriteBatch);
                    _enemy1.Draw(_spriteBatch);
                    for (int i = 0; i < _wayPoints1.Length; i++)
                        _wayPoints1[i].Draw(_spriteBatch);

                    _shield.Draw(_spriteBatch);
                    _weapon.Draw(_spriteBatch);
                    _gate1.Draw(_spriteBatch);
                    break;

                case GameState.Level2:
                    _menuButton.Draw(_spriteBatch);

                    _player.Draw(_spriteBatch);
                    _enemy2.Draw(_spriteBatch);
                    for (int i = 0; i < _wayPoints2.Length; i++)
                    {
                        _wayPoints2[i].Draw(_spriteBatch);
                    }
                    _enemy3.Draw(_spriteBatch);
                    for (int i = 0; i < _wayPoints3.Length; i++)
                    {
                        _wayPoints3[i].Draw(_spriteBatch);
                    }

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
