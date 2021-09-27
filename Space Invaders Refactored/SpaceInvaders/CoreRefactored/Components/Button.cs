using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using StateGame;
using IDrawable = CoreRefactored.Interfaces.IDrawable;
using IUpdateable = CoreRefactored.Interfaces.IUpdateable;

namespace CoreRefactored.Components
{
    //CONSIDER making a IUIElement interface
    public class Button : Component, IDrawable, IUpdateable
    {
        //Events
        public event Action OnButtonHoverEnter = delegate { };
        public event Action OnButtonHoverExit = delegate { };
        public event Action OnButtonClick = delegate { };

        //Fields
        private readonly Texture2D _texture;
        private readonly Color _defaultColor;
        private readonly Color _hoverColor;
        private readonly Color _pressedColor;

        private Rectangle _collider;

        private ButtonStatus _currentStatus = ButtonStatus.Default;
        private Color _currentColor;

        //Constructor
        public Button(Texture2D pTexture, Color pDefaultColor, Color pHoverColor, Color pPressedColor)
        {
            _texture = pTexture;
            _defaultColor = pDefaultColor;
            _hoverColor = pHoverColor;
            _pressedColor = pPressedColor;

            //Default values
            _currentStatus = ButtonStatus.Default;
            _currentColor = _defaultColor;
        }

        public Button(Texture2D pTexture, ButtonColorScheme pColorScheme)
        {
            _texture = pTexture;
            _defaultColor = pColorScheme.DefaultColor;
            _hoverColor = pColorScheme.HoverColor;
            _pressedColor = pColorScheme.PressedColor;

            _currentStatus = ButtonStatus.Default;
            _currentColor = _defaultColor;
        }

        //Methods
        public void Draw(SpriteBatch pSpriteBatch)
        {
            Vector2 scaledOrigin = new Vector2(transform.Origin.X * _texture.Width, transform.Origin.Y * _texture.Height);
            float radians = MathHelper.ToRadians(transform.Rotation);
            pSpriteBatch.Draw(_texture, transform.Position, null, _currentColor, radians, scaledOrigin, transform.Scale, SpriteEffects.None, 0.5f);
        }

        public void Update(GameTime pGameTime)
        {
            MouseState mouseState = Mouse.GetState();

            switch (_currentStatus)
            {
                case ButtonStatus.Default:
                    DefaultState(mouseState);
                    break;
                case ButtonStatus.Hovered:
                    HoveredState(mouseState);
                    break;
                case ButtonStatus.Pressed:
                    PressedState(mouseState);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public void DefaultState(MouseState pMouseState)
        {
            if (OverLapCheck(pMouseState.Position))
            {
                OnButtonHoverEnter();
                Console.WriteLine("Change status to Hovered");
                _currentStatus = ButtonStatus.Hovered;
                _currentColor = _hoverColor;
            }
        }

        public void HoveredState(MouseState pMouseState)
        {
            if (!OverLapCheck(pMouseState.Position))
            {
                OnButtonHoverExit();
                Console.WriteLine("Change status to Default");
                _currentStatus = ButtonStatus.Default;
                _currentColor = _defaultColor;
            }

            if (pMouseState.LeftButton == ButtonState.Pressed)
            {
                Console.WriteLine("Change status to Pressed");
                _currentStatus = ButtonStatus.Pressed;
                _currentColor = _pressedColor;
            }
        }

        public void PressedState(MouseState pMouseState)
        {
            if (pMouseState.LeftButton == ButtonState.Released)
            {
                if (OverLapCheck(pMouseState.Position))
                {
                    //Click!
                    Console.WriteLine("Click!");
                    OnButtonClick();

                    _currentStatus = ButtonStatus.Hovered;
                    _currentColor = _hoverColor;
                }
                else
                {
                    Console.WriteLine("Change status to Default");
                    _currentStatus = ButtonStatus.Default;
                    _currentColor = _defaultColor;
                }
            }
        }

        public void LateUpdate(GameTime pGameTime)
        {
        }

        private void UpdateCollider()
        {
            Vector2 size = transform.Scale * new Vector2(_texture.Bounds.Width, _texture.Bounds.Height);
            Vector2 position = transform.Position - transform.Origin * size;

            _collider = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
        }

        public bool OverLapCheck(Point pPoint)
        {
            UpdateCollider();

            return _collider.Contains(pPoint);
        }
    }
}
