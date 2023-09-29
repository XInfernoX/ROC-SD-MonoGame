using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ROC_SD_MonoGame.CSharpAdvanced.StateDesignPattern.Assignment2
{
    public class PlayButton : ButtonBase
    {
        public PlayButton(Vector2 pPosition, Texture2D pTexture, Game1 pGame, ButtonColorScheme pColorScheme, string pText = "")
            : base(pPosition, pTexture, pGame, pColorScheme, pText) { }

        protected override void OnButtonClick()
        {
            _game.SetGameState(GameState.Level1);
        }
    }
}