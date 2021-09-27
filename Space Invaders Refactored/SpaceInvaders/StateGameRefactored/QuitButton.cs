using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StateGameRefactored
{
    public class QuitButton : Button
    {
        public QuitButton(Vector2 pPosition, Texture2D pTexture, StateGameRefactored pGame, Color pDefaultColor, Color pHoverColor, Color pPressedColor) : 
            base(pPosition, pTexture, pGame, pDefaultColor, pHoverColor, pPressedColor)
        {
            _game = pGame;
        }

        protected override void OnButtonClick()
        {
            _game.Exit();
        }
    }
}