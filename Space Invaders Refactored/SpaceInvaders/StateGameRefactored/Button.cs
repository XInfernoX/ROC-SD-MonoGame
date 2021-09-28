using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StateGame;

using Color = Microsoft.Xna.Framework.Color;

namespace StateGameRefactored
{
    public class Button : GameObject
    {
        protected StateGameRefactored _game;

        private ButtonStatus _status;
        private Color _currentButtonColor;

        private Color _defaultColor;
        private Color _hoverColor;
        private Color _pressedColor;

        public Button(Vector2 pPosition, Texture2D pTexture, StateGameRefactored pGame, Color pDefaultColor, Color pHoverColor, Color pPressedColor) : base(pPosition, pTexture)
        {
            _game = pGame;

            _defaultColor = pDefaultColor;
            _hoverColor = pHoverColor;
            _pressedColor = pPressedColor;

            SetButtonState(ButtonStatus.Default, _defaultColor);
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
            if (Collision(pMouseState.Position))
            {
                SetButtonState(ButtonStatus.Hovered, _hoverColor);
            }
        }

        private void UpdateHoveredState(MouseState pMouseState)
        {
            if (!Collision(pMouseState.Position))
            {
                SetButtonState(ButtonStatus.Default, _defaultColor);
            }

            if (pMouseState.LeftButton == ButtonState.Pressed)
            {
                SetButtonState(ButtonStatus.Pressed, _pressedColor);
            }
        }

        private void UpdatePressedState(MouseState pMouseState)
        {
            if (pMouseState.LeftButton == ButtonState.Released)
            {
                if (Collision(pMouseState.Position))
                {
                    SetButtonState(ButtonStatus.Hovered, _hoverColor);
                    OnButtonClick();
                }
                else
                {
                    SetButtonState(ButtonStatus.Default, _defaultColor);
                }
            }
        }

        public override void Draw(SpriteBatch pSpriteBatch)
        {
            pSpriteBatch.Draw(_texture, _position, _currentButtonColor);
        }

        protected virtual void OnButtonClick() { }
    }
}