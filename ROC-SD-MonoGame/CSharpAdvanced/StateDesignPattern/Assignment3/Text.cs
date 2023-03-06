using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ROC_SD_MonoGame.CSharpAdvanced.StateDesignPattern.Assignment3
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
            get => _text;
            set
            {
                _text = value;
                _offset = _font.MeasureString(value);
            }
        }

        public SpriteFont SpriteFont
        {
            get => _font;
            set
            {
                _font = value;
                _offset = value.MeasureString(_text);
            }
        }

        public Color Color
        {
            get => _color;
            set => _color = value;
        }

        //Constructors
        public Text(Vector2 pPosition, SpriteFont pSpriteFont, string pText, Color pColor) : base(pPosition)
        {
            _font = pSpriteFont;
            _text = pText;
            _color = pColor;

            _offset = pSpriteFont.MeasureString(pText);
        }

        public Text(Vector2 pPosition, ButtonColorScheme pButtonScheme, string pText) : base(pPosition)
        {
            _font = pButtonScheme.Font;
            _color = pButtonScheme.TextColor;

            _text = pText;
            _offset = pButtonScheme.Font.MeasureString(pText);
        }

        public override void Draw(SpriteBatch pSpriteBatch)
        {
            if (_active)
            {
                pSpriteBatch.DrawString(_font, _text, Position - _offset / 2, _color);
            }
        }
    }
}