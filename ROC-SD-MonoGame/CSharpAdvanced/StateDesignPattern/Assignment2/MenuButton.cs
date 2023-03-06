using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CSharpAdvanced.CSharpAdvanced.StateDesignPattern.Assignment2
{
    public class MenuButton : ButtonBase
    {
        public MenuButton(Vector2 pPosition, Texture2D pTexture, Game1 pGame, ButtonColorScheme pColorScheme, Vector2 pOrigin, string pText = "")
            : base(pPosition, pTexture, pGame, pColorScheme, pOrigin, pText) { }

        protected override void OnButtonClick()
        {
            _game.SetGameState(GameState.Menu);
        }
    }
}