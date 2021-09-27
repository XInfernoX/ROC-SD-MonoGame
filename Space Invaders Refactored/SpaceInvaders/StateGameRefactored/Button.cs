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

        public Button(StateGameRefactored pGame, Color pDefaultColor, Color pHoverColor, Color pPressedColor)
        {
            _game = pGame;

            _defaultColor = pDefaultColor;
            _hoverColor = pHoverColor;
            _pressedColor = pPressedColor;
        }

        public override void Update()
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

        private void UpdateDefaultState(MouseState pMouseState)
        {
            if (Collision(pMouseState.Position))
            {
                _status = ButtonStatus.Hovered;
                _currentButtonColor = _hoverColor;
            }
        }

        private void UpdateHoveredState(MouseState pMouseState)
        {
            if (!Collision(pMouseState.Position))
            {
                _status = ButtonStatus.Default;
                _currentButtonColor = _defaultColor;
            }

            if (pMouseState.LeftButton == ButtonState.Pressed)
            {
                _status = ButtonStatus.Pressed;
                _currentButtonColor = _pressedColor;
            }
        }

        private void UpdatePressedState(MouseState pMouseState)
        {
            if (pMouseState.LeftButton == ButtonState.Released)
            {
                if (Collision(pMouseState.Position))
                {
                    _status = ButtonStatus.Hovered;
                    _currentButtonColor = _hoverColor;
                    OnButtonClick();
                }
                else
                {
                    _status = ButtonStatus.Default;
                    _currentButtonColor = _defaultColor;
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