using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ROC_SD_MonoGame.CSharpAdvanced.StateDesignPattern.Assignment3
{
    public class QuitButton : ButtonBase
    {
        public QuitButton(Game1 pGame, ButtonColorScheme pColorScheme, string pText = "Quit")
            : base(pGame, pColorScheme, pText)
        {

        }

        public QuitButton(Game1 pGame, ButtonColorScheme pColorScheme, Vector2 pOrigin, string pText = "Quit") : 
            base(pGame, pColorScheme, pOrigin, pText)
        {

        }

        public override void LoadContent(ContentManager pContent, Viewport pViewport)
        {
            float third = pViewport.Height / 3;

            _position = new Vector2(pViewport.Width / 2, third * 2);

            base.LoadContent(pContent, pViewport);
        }

        protected override void OnButtonClick()
        {
            _game.Exit();
        }
    }
}