using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CSharpAdvanced.CSharpAdvanced.StateDesignPattern.Assignment2
{
    public class ButtonColorScheme
    {
        //Fields
        private readonly Color _defaultColor;
        private readonly Color _hoverColor;
        private readonly Color _pressedColor;
        private readonly Color _textColor;

        private readonly SpriteFont _font;

        //Properties
        public Color DefaultColor => _defaultColor;
        public Color HoverColor => _hoverColor;
        public Color PressedColor => _pressedColor;
        public Color TextColor => _textColor;
        public SpriteFont Font => _font;

        //Constructor
        public ButtonColorScheme(Color pDefaultColor, Color pHoverColor, Color pPressedColor, Color pTextColor, SpriteFont pFont)
        {
            _defaultColor = pDefaultColor;
            _hoverColor = pHoverColor;
            _pressedColor = pPressedColor;
            _textColor = pTextColor;

            _font = pFont;
        }

        public override string ToString()
        {
            return $"Default: {_defaultColor}," +
                   $"Hover: {_hoverColor}," +
                   $"Pressed: {_pressedColor}," +
                   $"Text: {_textColor}," +
                   $"Font: {_font}";
        }
    }
}