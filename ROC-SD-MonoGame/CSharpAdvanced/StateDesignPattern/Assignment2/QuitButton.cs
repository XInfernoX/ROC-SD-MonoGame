using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CSharpAdvanced.CSharpAdvanced.StateDesignPattern.Assignment2
{
    public class QuitButton : ButtonBase
    {
        public QuitButton(Vector2 pPosition, Texture2D pTexture, Game1 pGame, ButtonColorScheme pColorScheme, string pText = "")
    : base(pPosition, pTexture, pGame, pColorScheme, pText) { }

        protected override void OnButtonClick()
        {
            _game.Exit();
        }
    }
}