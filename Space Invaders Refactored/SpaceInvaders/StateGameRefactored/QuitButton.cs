using Microsoft.Xna.Framework;

namespace StateGameRefactored
{
    public class QuitButton : Button
    {
        public QuitButton(StateGameRefactored pGame, Color pDefaultColor, Color pHoverColor, Color pPressedColor) : base(pGame, pDefaultColor, pHoverColor, pPressedColor)
        {
            _game = pGame;
        }

        protected override void OnButtonClick()
        {
            _game.Exit();
        }
    }
}