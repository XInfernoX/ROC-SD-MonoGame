using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using IDrawable = SpaceInvadersRefactored.Interfaces.IDrawable;

namespace SpaceInvadersRefactored.Components
{
    public class Text : Component, IDrawable
    {
        //Fields
        private SpriteFont _font;
        private string _label;
        private Vector2 _offset;
        private Vector2 _origin;
        private Color _color;


        //Properties
        public SpriteFont SpriteFont
        {
            get => _font;
            set => _font = value;
        }

        public string Label
        {
            get => _label;
            set => _label = value;
        }

        public Vector2 Offset
        {
            get => _offset;
            set => _offset = value;
        }

        public Vector2 Origin
        {
            get => _origin;
            set => _origin = value;
        }

        public Color Color
        {
            get => _color;
            set => _color = value;
        }


        //Constructor
        public Text(SpriteFont pFont, string pLabel, Vector2 pOffset, Vector2 pOrigin, Color pColor)
        {
            _font = pFont;
            _label = pLabel;

            _offset = pOffset;
            _origin = pOrigin;
            _color = pColor;
        }


        //Methods
        public void Draw(SpriteBatch pSpriteBatch)
        {
            Vector2 drawPosition = transform.Position + _offset;
            Vector2 scaledOrigin = _origin * _font.MeasureString(_label);

            pSpriteBatch.DrawString(_font, _label, drawPosition, _color, transform.Rotation, scaledOrigin, transform.Scale, SpriteEffects.None, 0.0f);
        }
    }
}