using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace StateGameRefactored2
{
    public class QuitButton : ButtonBase
    {
        public QuitButton(StateGame pGame, ButtonColorScheme pColorScheme, string pText) : base(pGame, pColorScheme, pText)
        {
        }

        public override void LoadContent(ContentManager pContent, Viewport pViewPort)
        {
            base.LoadContent(pContent, pViewPort);
            Position = new Vector2(pViewPort.Width * 0.5f, pViewPort.Height * 0.66f);
        }

        protected override void OnButtonClick()
        {
            _game.Exit();
        }
    }
}