using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.Example;

namespace SpaceInvaders.StateDesignPattern
{
    public enum GameState
    {
        Menu,
        Level1
    }

    public class StateGame : Game
    {
        //Fields
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private GameState _gameState = GameState.Menu;

        //PlayButton
        private readonly GameObject _playButton;
        private ButtonStatus _playbuttonStatus;
        private Color _currentPlayButtonColor;

        //QuitButton
        private readonly GameObject _quitButton;
        private ButtonStatus _quitbuttonStatus;
        private Color _currentQuitButtonColor;

        //ButtonColors
        private readonly Color _defaultColor;
        private readonly Color _hoverColor;
        private readonly Color _pressedColor;


        //Constructor
        public StateGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _playButton = new GameObject();
            _quitButton = new GameObject();
        }

        protected override void Initialize()
        {
            _playButton.Active = true;
            _quitButton.Active = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //Textures
            Texture2D buttonTexture = Content.Load<Texture2D>("UI_Title_64x64");

            //Fonts
            SpriteFont arial = Content.Load<SpriteFont>("Arial");

            _playButton.AddTexture(buttonTexture);
            _quitButton.AddTexture(buttonTexture);

            Viewport viewPort = GraphicsDevice.Viewport;
            float third = viewPort.Height / 3;

            _playButton.SetPosition(new Vector2(viewPort.Width / 2, third));
            _quitButton.SetPosition(new Vector2(viewPort.Width / 2, third * 2));


















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
            switch (_playbuttonStatus)
            {
                case ButtonStatus.Default:
                    if (_playButton.Collision(mouseState.Position))
                    {
                        _playbuttonStatus = ButtonStatus.Hovered;
                        _currentPlayButtonColor = _hoverColor;
                    }
                    break;
                case ButtonStatus.Hovered:
                    if (!_playButton.Collision(mouseState.Position))
                    {
                        _playbuttonStatus = ButtonStatus.Default;
                        _currentPlayButtonColor = _defaultColor;
                    }

                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        Console.WriteLine("Change status to Pressed");
                        _playbuttonStatus = ButtonStatus.Pressed;
                        _currentPlayButtonColor = _pressedColor;
                    }
                    break;
                case ButtonStatus.Pressed:
                    if (mouseState.LeftButton == ButtonState.Released)
                    {
                        if (_playButton.Collision(mouseState.Position))
                        {
                            //Click!
                            Console.WriteLine("Click!");
                            _playbuttonStatus = ButtonStatus.Hovered;
                            _currentPlayButtonColor = _hoverColor;
                            MoveToLevel1();
                        }
                        else
                        {
                            Console.WriteLine("Change status to Default");
                            _playbuttonStatus = ButtonStatus.Default;
                            _currentPlayButtonColor = _defaultColor;
                        }
                    }
                    break;
            }

            //QuitButtonBehaviour
            switch (_quitbuttonStatus)
            {
                case ButtonStatus.Default:
                    if (_playButton.Collision(mouseState.Position))
                    {
                        _quitbuttonStatus = ButtonStatus.Hovered;
                        _currentQuitButtonColor = _hoverColor;
                    }
                    break;
                case ButtonStatus.Hovered:
                    if (!_playButton.Collision(mouseState.Position))
                    {
                        _quitbuttonStatus = ButtonStatus.Default;
                        _currentQuitButtonColor = _defaultColor;
                    }

                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        Console.WriteLine("Change status to Pressed");
                        _quitbuttonStatus = ButtonStatus.Pressed;
                        _currentQuitButtonColor = _pressedColor;
                    }
                    break;
                case ButtonStatus.Pressed:
                    if (mouseState.LeftButton == ButtonState.Released)
                    {
                        if (_playButton.Collision(mouseState.Position))
                        {
                            //Click!
                            Console.WriteLine("Click!");
                            _quitbuttonStatus = ButtonStatus.Hovered;
                            _currentQuitButtonColor = _hoverColor;
                            QuitGame();
                        }
                        else
                        {
                            Console.WriteLine("Change status to Default");
                            _quitbuttonStatus = ButtonStatus.Default;
                            _currentQuitButtonColor = _defaultColor;
                        }
                    }
                    break;
            }
        }

        private void UpdateLevel1(GameTime pGameTime)
        {

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
        }

        private void DrawLevel1(GameTime pGameTime)
        {

        }
    }
}
