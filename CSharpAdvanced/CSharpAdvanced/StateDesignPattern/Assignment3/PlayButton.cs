﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CSharpAdvanced.CSharpAdvanced.StateDesignPattern.Assignment3
{
    public class PlayButton : ButtonBase
    {
        public PlayButton(Vector2 pPosition, Game1 pGame, ButtonColorScheme pColorScheme, string pText)
            : base(pPosition, pGame, pColorScheme, pText)
        {

        }

        public PlayButton(Vector2 pPosition, Game1 pGame, ButtonColorScheme pColorScheme, Vector2 pOrigin, string pText = "")
            : base(pPosition, pGame, pColorScheme, pOrigin, pText)
        {

        }

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