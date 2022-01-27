using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StateGameRefactored1
{
    public class PlayButton : ButtonBase
    {
        public PlayButton(Vector2 pPosition, Texture2D pTexture, StateGameRefactored.StateGame pGame, Color pDefaultColor, Color pHoverColor, Color pPressedColor)
            : base(pPosition, pTexture, pGame, pDefaultColor, pHoverColor, pPressedColor) { }

        protected override void OnButtonClick()
        {
            _game.SetGameState(GameState.Level1);
        }
    }
}