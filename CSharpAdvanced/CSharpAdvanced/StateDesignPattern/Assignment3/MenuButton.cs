using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CSharpAdvanced.CSharpAdvanced.StateDesignPattern.Assignment3
{
    public class MenuButton : ButtonBase
    {
        public MenuButton(Vector2 pPosition, Game1 pGame, ButtonColorScheme pColorScheme, string pText)
            : base(pPosition, pGame, pColorScheme, pText)
        {

        }

        public MenuButton(Vector2 pPosition, Game1 pGame, ButtonColorScheme pColorScheme, Vector2 pOrigin, string pText = "")
             : base(pPosition, pGame, pColorScheme, pOrigin, pText)
        {

        }

        public override void LoadContent(ContentManager pContent, Viewport pViewPort)
        {
            base.LoadContent(pContent, pViewPort);
            Position = new Vector2(pViewPort.Width, 0) - new Vector2(_texture.Width, 0);//To move the "origin" to the top right corner
        }

        protected override void OnButtonClick()
        {
            _game.ChangeSceneTo("Menu");
        }
    }
}