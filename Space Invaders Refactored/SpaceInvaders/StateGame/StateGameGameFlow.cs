using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace StateGame
{
    public class StateGameGameFlow : Game
    {
        //Fields
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //State
        private GameState _gameState = GameState.Menu;

        //PlayButton
        private readonly GameObject _playButton;
        private ButtonStatus _playButtonStatus;
        private Color _currentPlayButtonColor = Color.White;

        //QuitButton
        private readonly GameObject _quitButton;
        private ButtonStatus _quitButtonStatus;
        private Color _currentQuitButtonColor = Color.White;

        //MenuButton
        private readonly GameObject _menuButton;
        private ButtonStatus _menuButtonStatus;
        private Color _currentMenuButtonColor = Color.White;

        //ButtonColors
        private readonly Color _defaultColor = Color.White;
        private readonly Color _hoverColor = Color.Aquamarine;
        private readonly Color _pressedColor = Color.Red;

        //Resources
        private Texture2D _buttonTexture;
        private SpriteFont _arial;

        //Constructor
        public StateGameGameFlow()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _playButton = new GameObject();
            _menuButton = new GameObject();
            _quitButton = new GameObject();
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //Textures
            _buttonTexture = Content.Load<Texture2D>("UI_Title_64x64");

            //Fonts
            _arial = Content.Load<SpriteFont>("Arial");


            Viewport viewPort = GraphicsDevice.Viewport;
            float third = viewPort.Height / 3;

            //PlayButton
            _playButton.Texture = _buttonTexture;
            _playButton.Position = new Vector2(viewPort.Width / 2, third);

            //QuitButton
            _quitButton.Texture = _buttonTexture;
            _quitButton.Position = new Vector2(viewPort.Width / 2, third * 2);

            //MenuButton
            _menuButton.Texture = _buttonTexture;
            _menuButton.Position = new Vector2(viewPort.Width - _buttonTexture.Width, 0);
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
                    if (_playButton.Contains(mouseState.Position))
                    {
                        _playButtonStatus = ButtonStatus.Hovered;
                        _currentPlayButtonColor = _hoverColor;
                    }
                    break;
                case ButtonStatus.Hovered:
                    if (!_playButton.Contains(mouseState.Position))
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
                        if (_playButton.Contains(mouseState.Position))
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
                    if (_quitButton.Contains(mouseState.Position))
                    {
                        _quitButtonStatus = ButtonStatus.Hovered;
                        _currentQuitButtonColor = _hoverColor;
                    }
                    break;
                case ButtonStatus.Hovered:
                    if (!_quitButton.Contains(mouseState.Position))
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
                        if (_quitButton.Contains(mouseState.Position))
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

            //MenuButton
            switch (_menuButtonStatus)
            {
                case ButtonStatus.Default:
                    if (_menuButton.Contains(mouseState.Position))
                    {
                        _menuButtonStatus = ButtonStatus.Hovered;
                        _currentMenuButtonColor = _hoverColor;
                    }
                    break;
                case ButtonStatus.Hovered:
                    if (!_menuButton.Contains(mouseState.Position))
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
                        if (_menuButton.Contains(mouseState.Position))
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
        }

        protected override void Draw(GameTime pGameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            switch (_gameState)
            {
                case GameState.Menu:

                    _playButton.Draw(_spriteBatch, _currentPlayButtonColor);
                    _quitButton.Draw(_spriteBatch, _currentQuitButtonColor);

                    Vector2 menuTextOffset = new Vector2(_buttonTexture.Width / 2, _buttonTexture.Height / 2);

                    Vector2 playSize = _arial.MeasureString("Play");
                    _spriteBatch.DrawString(_arial, "Play", _playButton.Position + menuTextOffset - playSize / 2, Color.White);

                    Vector2 quitSize = _arial.MeasureString("Quit");
                    _spriteBatch.DrawString(_arial, "Quit", _quitButton.Position + menuTextOffset - quitSize / 2, Color.White);

                    break;
                case GameState.Level1:

                    _menuButton.Draw(_spriteBatch, _currentMenuButtonColor);

                    Vector2 level1TextOffset = new Vector2(_buttonTexture.Width / 2, _buttonTexture.Height / 2);

                    Vector2 menuSize = _arial.MeasureString("Menu");
                    _spriteBatch.DrawString(_arial, "Menu", _menuButton.Position + level1TextOffset - menuSize / 2, Color.White);

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _spriteBatch.End();

            base.Draw(pGameTime);
        }
    }
}
