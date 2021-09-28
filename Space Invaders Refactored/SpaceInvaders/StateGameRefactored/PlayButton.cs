using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StateGame;

namespace StateGameRefactored
{
    public class PlayButton : Button
    {
        public PlayButton(Vector2 pPosition, Texture2D pTexture, StateGameRefactored pGame, Color pDefaultColor, Color pHoverColor, Color pPressedColor)
            : base(pPosition, pTexture, pGame, pDefaultColor, pHoverColor, pPressedColor) { }

        protected override void OnButtonClick()
        {
            _game.SetGameState(GameState.Level1);
        }
    }
}