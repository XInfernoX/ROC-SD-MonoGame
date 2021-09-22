using Microsoft.Xna.Framework;

namespace StateGameRefactored
{
    public class ButtonColorScheme
    {
        //Fields
        private readonly Color _defaultColor;
        private readonly Color _hoverColor;
        private readonly Color _pressedColor;

        //Properties
        public Color DefaultColor => _defaultColor;
        public Color HoverColor => _hoverColor;
        public Color PressedColor => _pressedColor;

        //Constructor
        public ButtonColorScheme(Color pDefaultColor, Color pHoverColor, Color pPressedColor)
        {
            _defaultColor = pDefaultColor;
            _hoverColor = pHoverColor;
            _pressedColor = pPressedColor;
        }
    }
}