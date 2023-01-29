using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CSharpAdvanced.CSharpAdvanced.StateDesignPattern.Assignment2
{
    public class QuitButton : ButtonBase
    {
        public QuitButton(Vector2 pPosition, Game1 pGame, ButtonColorScheme pColorScheme, string pText = "")
            : base(pPosition, pGame, pColorScheme, pText) { }

        public QuitButton(Vector2 pPosition, Game1 pGame, ButtonColorScheme pColorScheme, Vector2 pOrigin, string pText = "")
            : base(pPosition, pGame, pColorScheme, pOrigin, pText) { }

        protected override void OnButtonClick()
        {
            _game.Exit();
        }
    }
}