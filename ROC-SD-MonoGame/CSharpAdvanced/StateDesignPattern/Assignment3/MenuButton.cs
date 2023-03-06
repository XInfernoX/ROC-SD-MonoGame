using Microsoft.Xna.Framework;

namespace ROC_SD_MonoGame.CSharpAdvanced.StateDesignPattern.Assignment3
{
    public class MenuButton : ButtonBase
    {
        public MenuButton(Game1 pGame, ButtonColorScheme pColorScheme, string pText = "Menu")
            : base(pGame, pColorScheme, pText)
        {
            
        }

        public MenuButton(Game1 pGame, ButtonColorScheme pColorScheme, Vector2 pOrigin, string pText = "Menu")
             : base(pGame, pColorScheme, pOrigin, pText)
        {

        }

        protected override void OnButtonClick()
        {
            _game.ChangeSceneTo("Menu");
        }
    }
}