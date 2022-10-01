﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StateDesignPattern.Assignment2
{
    public class MenuButton : ButtonBase
    {
        public MenuButton(Vector2 pPosition, Texture2D pTexture, Game1 pGame, Color pDefaultColor, Color pHoverColor, Color pPressedColor)
            : base(pPosition, pTexture, pGame, pDefaultColor, pHoverColor, pPressedColor) { }

        protected override void OnButtonClick()
        {
            _game.SetGameState(GameState.Menu);
        }
    }
}