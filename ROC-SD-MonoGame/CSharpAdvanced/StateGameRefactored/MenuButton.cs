using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CSharpAdvanced.CSharpAdvanced.StateGameRefactored
{
    public class MenuButton : ButtonBase
    {
        public MenuButton(StateGame pGame, ButtonColorScheme pColorScheme, string pText) : base(pGame, pColorScheme, pText)
        {
        }

        public override void LoadContent(ContentManager pContent, Viewport pViewPort)
        {
            base.LoadContent(pContent, pViewPort);
            Position = new Vector2(pViewPort.Width, 0) - new Vector2(_texture.Width, 0);//To move the "origin" to the top right corner
        }

        protected override void OnButtonClick()
        {
            //OLD
            //_game.SetGameState(GameState.Menu);

            //NEW
            _game.ChangeSceneTo("Menu");
        }
    }
}