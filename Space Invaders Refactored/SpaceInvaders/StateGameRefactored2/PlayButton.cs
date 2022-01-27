using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace StateGameRefactored2
{
    public class PlayButton : ButtonBase
    {
        public PlayButton(StateGame pGame, ButtonColorScheme pColorScheme, string pText) :
            base(pGame, pColorScheme, pText)
        { }

        public override void LoadContent(ContentManager pContent, Viewport pViewPort)
        {
            base.LoadContent(pContent, pViewPort);
            Position = new Vector2(pViewPort.Width * 0.5f, pViewPort.Height * 0.33f);
        }

        protected override void OnButtonClick()
        {
            _game.ChangeSceneTo("Level1");
        }
    }
}