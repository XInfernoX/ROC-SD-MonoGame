using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ROC_SD_MonoGame.CSharpAdvanced.StateDesignPattern.Assignment3
{
    public class Weapon : Pickup
    {
        public Weapon(Player pPlayer) : base(pPlayer)
        {
        }

        public override void LoadContent(ContentManager pContent, Viewport pViewport)
        {
            float third = pViewport.Height / 3.0f;

            Position = new Vector2(pViewport.Width - 100, third);
            Texture = pContent.Load<Texture2D>("Weapon");
        }

        protected override void OnPlayerCollision(Player pPlayer)
        {
            Console.WriteLine("Weapon.OnPlayerCollision");

            if (pPlayer.PlayerState == PlayerState.UnArmored)
                pPlayer.SetPlayerState(PlayerState.Armed);
            else
                pPlayer.SetPlayerState(PlayerState.ShieldedAndArmed);
        }
    }
}