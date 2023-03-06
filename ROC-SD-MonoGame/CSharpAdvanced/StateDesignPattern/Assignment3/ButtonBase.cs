using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ROC_SD_MonoGame.CSharpAdvanced.StateDesignPattern.Assignment3
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
        public ButtonBase(Game1 pGame, ButtonColorScheme pColorScheme, Vector2 pOrigin, string pText = "")
        {
            _game = pGame;
            _colorScheme = pColorScheme;
            _origin = pOrigin;

            _text = new Text(Vector2.Zero, pColorScheme, pText);

            SetButtonState(ButtonStatus.Default, _colorScheme.DefaultColor);
        }

        public ButtonBase(Game1 pGame, ButtonColorScheme pColorScheme, string pText = "") : this(pGame, pColorScheme, new Vector2(0.5f, 0.5f), pText) { }

        public override void LoadContent(ContentManager pContent, Viewport pViewport)
        {
            Texture = pContent.Load<Texture2D>("UI_Title_64x64");

            //Makes sure the text will always be in the center of the Button
            Vector2 textOriginOffset = _origin - new Vector2(0.5f, 0.5f);
            Vector2 buttonCenterPosition = new Vector2(_position.X - _texture.Width * textOriginOffset.X, _position.Y - _texture.Height * textOriginOffset.Y);
            _text.Position = buttonCenterPosition;

            base.LoadContent(pContent, pViewport);
        }

        public override void Update(GameTime pGameTime)
        {
            MouseState mouseState = Mouse.GetState();

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