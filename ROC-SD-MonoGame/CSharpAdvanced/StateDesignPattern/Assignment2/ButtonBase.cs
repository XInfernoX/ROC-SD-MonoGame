using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ROC_SD_MonoGame.CSharpAdvanced.StateDesignPattern.Assignment2
{
    public class ButtonBase : GameObject
    {
        //Fields
        protected Game1 _game;

        private ButtonStatus _status;
        private Color _currentButtonColor;
        private ButtonColorScheme _colorScheme;

        private MouseState _lastMouseState;

        private Text _text;

        //Properties
        public Text Text
        {
            get => _text;
            set => _text = value;
        }

        //Constructors
        public ButtonBase(Vector2 pPosition, Texture2D pTexture, Game1 pGame, ButtonColorScheme pColorScheme, string pText = "") : base(pPosition, pTexture)
        {
            _game = pGame;
            _colorScheme = pColorScheme;

            //Makes sure the text will always be in the center of the Button
            Vector2 textOriginOffset = _origin - new Vector2(0.5f, 0.5f);
            Vector2 buttonCenterPosition = new Vector2(_position.X - pTexture.Width * textOriginOffset.X, _position.Y - pTexture.Height * textOriginOffset.Y);

            _text = new Text(buttonCenterPosition, pColorScheme, pText);

            SetButtonState(ButtonStatus.Default, _colorScheme.DefaultColor);
        }

        public ButtonBase(Vector2 pPosition, Texture2D pTexture, Game1 pGame, ButtonColorScheme pColorScheme, Vector2 pOrigin, string pText = "") : base(pPosition, pTexture, pOrigin)
        {
            _game = pGame;
            _colorScheme = pColorScheme;

            //Makes sure the text will always be in the center of the Button
            Vector2 textOrigin = _origin - new Vector2(0.5f, 0.5f);
            Vector2 buttonCenter = new Vector2(_position.X - pTexture.Width * textOrigin.X, _position.Y - pTexture.Height * textOrigin.Y);

            _text = new Text(buttonCenter, pColorScheme, pText);

            SetButtonState(ButtonStatus.Default, _colorScheme.DefaultColor);
        }

        public override void Update(GameTime pGameTime)
        {
            MouseState mouseState = Mouse.GetState();

            //PlayButtonBehaviour
            switch (_status)
            {
                case ButtonStatus.Default:
                    UpdateDefaultState(mouseState);
                    break;
                case ButtonStatus.Hovered:
                    UpdateHoveredState(mouseState);
                    break;
                case ButtonStatus.Pressed:
                    UpdatePressedState(mouseState);
                    break;
            }

            _lastMouseState = mouseState;
        }

        private void SetButtonState(ButtonStatus pStatus, Color pColor)
        {
            _status = pStatus;
            _currentButtonColor = pColor;
        }

        private void UpdateDefaultState(MouseState pMouseState)
        {
            if (Contains(pMouseState.Position))
            {
                SetButtonState(ButtonStatus.Hovered, _colorScheme.HoverColor);
            }
        }

        private void UpdateHoveredState(MouseState pMouseState)
        {
            if (!Contains(pMouseState.Position))
            {
                SetButtonState(ButtonStatus.Default, _colorScheme.DefaultColor);
            }

            if (pMouseState.LeftButton == ButtonState.Pressed && _lastMouseState.LeftButton == ButtonState.Released)
            {
                SetButtonState(ButtonStatus.Pressed, _colorScheme.PressedColor);
            }
        }

        private void UpdatePressedState(MouseState pMouseState)
        {
            if (pMouseState.LeftButton == ButtonState.Released && _lastMouseState.LeftButton == ButtonState.Pressed)
            {
                if (Contains(pMouseState.Position))
                {
                    SetButtonState(ButtonStatus.Hovered, _colorScheme.HoverColor);
                    OnButtonClick();
                }
                else
                {
                    SetButtonState(ButtonStatus.Default, _colorScheme.DefaultColor);
                }
            }
        }

        public override void Draw(SpriteBatch pSpriteBatch)
        {
            if (Active)
            {
                Vector2 scaledOrigin = new Vector2(_texture.Width * _origin.X, _texture.Height * _origin.Y);
                pSpriteBatch.Draw(_texture, _position, null, _currentButtonColor, 0, scaledOrigin, 1, SpriteEffects.None, 0);
            }
            _text.Draw(pSpriteBatch);
        }

        protected virtual void OnButtonClick() { }
    }
}