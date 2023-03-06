using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ROC_SD_MonoGame.CSharpAdvanced.StateDesignPattern.Assignment3
{

    public class Shield : Pickup
    {   
        public Shield(Player pPlayer) : base(pPlayer)
        {

        }

        public override void LoadContent(ContentManager pContent, Viewport pViewport)
        {
            float third = pViewport.Height / 3.0f;

            Position = new Vector2(100, third);
            Texture = pContent.Load<Texture2D>("Shield");
        }

        protected override void OnPlayerCollision(Player pPlayer)
        {
            if (pPlayer.PlayerState == PlayerState.UnArmored)
                pPlayer.SetPlayerState(PlayerState.Shielded);
            else
                pPlayer.SetPlayerState(PlayerState.ShieldedAndArmed);
        }
    }
}