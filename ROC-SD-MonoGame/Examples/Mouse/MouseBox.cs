using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ROC_SD_MonoGame.Examples.MouseExamples
{
    public class MouseBox
    {
        private Texture2D _texture;
        private Vector2 _position;
        private Rectangle _rectangle;
        private Color _color = Color.White;

        public MouseBox(Texture2D pTexture, Vector2 pPosition, Color pColor)
        {
            _texture = pTexture;
            _position = pPosition;

            _rectangle = pTexture.Bounds;
            _rectangle.X = (int)_position.X;
            _rectangle.Y = (int)_position.Y;

            _color = pColor;
        }

        public void UpdateMouseBox(GameTime pGameTime)
        {
            MouseState mouseState = Mouse.GetState();

            _color = _rectangle.Contains(mouseState.Position) ? Color.Red : Color.White;
        }

        public void DrawMouseBox(SpriteBatch pSpriteBatch)
        {
            pSpriteBatch.Draw(_texture, _position, _color);
        }

        public bool IsMouseOverBox(MouseState pMouseState)
        {
            return _rectangle.Contains(pMouseState.Position);
        }
    }
}