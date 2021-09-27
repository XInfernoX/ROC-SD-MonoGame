using Microsoft.Xna.Framework;
using StateGame;

namespace StateGameRefactored
{
    public class MenuButton : Button
    {
        public MenuButton(StateGameRefactored pGame, Color pDefaultColor, Color pHoverColor, Color pPressedColor) : base(pGame, pDefaultColor, pHoverColor, pPressedColor)
        {
            _game = pGame;
        }

        protected override void OnButtonClick()
        {
            _game.SetGameState(GameState.Menu);
        }
    }
}