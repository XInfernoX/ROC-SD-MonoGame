using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace StateDesignPattern.Assignment2
{
    public class ButtonBase : GameObject
    {
        //Fields
        protected Game1 _game;

        private ButtonStatus _status;
        private Color _currentButtonColor;
        private ButtonColorScheme _colorScheme;

        private string _text;
        private Vector2 _textOffset;

        //Properties
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                _textOffset = _colorScheme.Font.MeasureString(_text) / 2;
            }
        }

        //Constructors
        public ButtonBase(Vector2 pPosition, Game1 pGame, ButtonColorScheme pColorScheme, string pText = "") : base(pPosition)
        {
            _game = pGame;
            _colorScheme = pColorScheme;
            Text = pText;

            SetButtonState(ButtonStatus.Default, _colorScheme.DefaultColor);
        }

        public ButtonBase(Vector2 pPosition, Game1 pGame, ButtonColorScheme pColorScheme, Vector2 pOrigin, string pText = "") : base(pPosition)
        {
            _game = pGame;
            _colorScheme = pColorScheme;
            Text = pText;
            _origin = pOrigin;

            SetButtonState(ButtonStatus.Default, _colorScheme.DefaultColor);
        }

        public override void LoadContent(ContentManager pContent)
        {
            Texture = pContent.Load<Texture2D>("UI_Title_64x64");
            base.LoadContent(pContent);
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

            if (pMouseState.LeftButton == ButtonState.Pressed)
            {
                SetButtonState(ButtonStatus.Pressed, _colorScheme.PressedColor);
            }
        }

        private void UpdatePressedState(MouseState pMouseState)
        {
            if (pMouseState.LeftButton == ButtonState.Released)
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
            if(Active)
            {
                pSpriteBatch.Draw(_texture, _position, _currentButtonColor);

                Vector2 buttonCenter = new Vector2(_position.X + Width / 2, _position.Y + Height / 2);
                pSpriteBatch.DrawString(_colorScheme.Font, _text, buttonCenter - _textOffset, _colorScheme.TextColor);
            }


            //pSpriteBatch.Draw(_texture, _position, _currentButtonColor);
            //pSpriteBatch.Draw(Texture, Position, _currentButtonColor);
        }

        protected virtual void OnButtonClick() { }
    }
}