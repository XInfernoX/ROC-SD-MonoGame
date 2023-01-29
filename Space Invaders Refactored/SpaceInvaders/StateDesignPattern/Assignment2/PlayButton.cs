﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StateDesignPattern.Assignment2
{
    public class PlayButton : ButtonBase
    {
        public PlayButton(Vector2 pPosition, Game1 pGame, ButtonColorScheme pColorScheme, string pText = "")
            : base(pPosition, pGame, pColorScheme, pText) { }

        public PlayButton(Vector2 pPosition, Game1 pGame, ButtonColorScheme pColorScheme, Vector2 pOrigin, string pText = "")
            : base(pPosition, pGame, pColorScheme, pOrigin, pText) { }

        protected override void OnButtonClick()
        {
            _game.SetGameState(GameState.Level1);
        }
    }
}