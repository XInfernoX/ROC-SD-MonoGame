using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace StateGameRefactored2
{
    public class Pickup : GameObject
    {
        private Player _player;

        public Pickup(Vector2 pPosition, Texture2D pTexture, Player pPlayer) : base(pPosition, pTexture)
        {
            _player = pPlayer;
        }

        public override void Update(GameTime pGameTime)
        {
            if (Collision(_player))
            {
                OnPlayerCollision(_player);
                Active = false;
            }
        }

        protected virtual void OnPlayerCollision(Player pPlayer)
        {
        }
    }


    public class Weapon : Pickup
    {
        public Weapon(Vector2 pPosition, Texture2D pTexture, Player pPlayer) : base(pPosition, pTexture, pPlayer)
        {
        }

        public override void LoadContent(ContentManager pContent, string pFileName)
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

    public class Shield : Pickup
    {
        public Shield(Vector2 pPosition, Texture2D pTexture, Player pPlayer) : base(pPosition, pTexture, pPlayer)
        {
        }

        public override void LoadContent(ContentManager pContent, string pFileName)
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