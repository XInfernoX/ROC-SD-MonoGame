using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CSharpAdvanced.CSharpAdvanced.StateDesignPattern.Assignment3
{
    public class Weapon : Pickup
    {
        public Weapon(Vector2 pPosition, Texture2D pTexture, Player pPlayer) : base(pPosition, pTexture, pPlayer)
        {
        }

        public override void LoadContent(ContentManager pContent, Viewport pViewport)
        {
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