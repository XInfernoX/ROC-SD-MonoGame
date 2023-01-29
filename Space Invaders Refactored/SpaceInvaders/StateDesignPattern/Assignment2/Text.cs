using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StateDesignPattern.Assignment2
{
    public class Text : GameObject
    {
        //Fields
        private SpriteFont _font;
        private string _text;
        private Color _color;

        private Vector2 _offset;

        //Properties
        public string Label
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                _offset = _font.MeasureString(value);
            }
        }

        public SpriteFont SpriteFont
        {
            get
            {
                return _font;
            }
            set
            {
                _font = value;
                _offset = value.MeasureString(_text);
            }
        }

        public Color Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
            }
        }

        //Constructors
        public Text(Vector2 pPosition, SpriteFont pSpriteFont, string pText, Color pColor) : base(pPosition)
        {
            _font = pSpriteFont;
            _text = pText;
            _color = pColor;

            _offset = pSpriteFont.MeasureString(pText);
        }

        public override void Draw(SpriteBatch pSpriteBatch)
        {
            pSpriteBatch.DrawString(_font, _text, Position - _offset / 2, _color);
        }
    }
}