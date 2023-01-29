using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace StateDesignPattern.Assignment3
{

    public class Shield : Pickup
    {
        public Shield(Vector2 pPosition, Texture2D pTexture, Player pPlayer) : base(pPosition, pTexture, pPlayer)
        {
        }

        public override void LoadContent(ContentManager pContent, Viewport pViewport)
        {
            Texture = pContent.Load<Texture2D>("Shield");
        }

        protected override void OnPlayerCollision(Player pPlayer)
        {
            Console.WriteLine("Shield.OnPlayerCollision");
            if (pPlayer.PlayerState == PlayerState.UnArmored)
                pPlayer.SetPlayerState(PlayerState.Shielded);
            else
                pPlayer.SetPlayerState(PlayerState.ShieldedAndArmed);
        }
    }
}