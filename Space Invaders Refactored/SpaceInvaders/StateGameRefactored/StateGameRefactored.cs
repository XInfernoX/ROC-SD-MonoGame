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

        private Color _defaultColor = Color.White;
        private Color _hoverColor = Color.Aquamarine;
        private Color _pressedColor = Color.Red;

        //Constructor
        public StateGameRefactored()
        {

        }

        protected override void LoadContent()
        {
            base.LoadContent();

            Viewport viewPort = GraphicsDevice.Viewport;
            float third = viewPort.Height / 3;

            //PlayButton
            _playButton = new PlayButton(this, _defaultColor, _hoverColor, _pressedColor);

            //QuitButton
            _quitButton = new QuitButton(this, _defaultColor, _hoverColor, _pressedColor);

            //MenuButton
            _menuButton = new MenuButton(this, _defaultColor, _hoverColor, _pressedColor);

            //Player
            _player = new Player();

            //Enemy
            Texture2D wayPointTexture = Content.Load<Texture2D>("Flag");
            GameObject[] wayPoints = new GameObject[]
            {
                new GameObject(new Vector2(viewPort.Width * 0.1f, viewPort.Height * 0.1f), wayPointTexture)
            };
            _enemy = new Enemy(wayPoints, _player, 100, _shield, _weapon);


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

        public void SetGameState(GameState pGameState)
        {
            _gameState = pGameState;
        }
    }
}
