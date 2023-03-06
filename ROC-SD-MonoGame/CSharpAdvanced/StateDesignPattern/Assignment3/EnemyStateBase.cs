using Microsoft.Xna.Framework;
using System.Diagnostics;
using System;

namespace ROC_SD_MonoGame.CSharpAdvanced.StateDesignPattern.Assignment3
{
    public class EnemyStateBase
    {
        //Fields
        protected Enemy _owner;
        protected Player _player;
        private int _speed;

        private Random _random = new Random();

        //Constructor
        public EnemyStateBase(Enemy pOwner, Player pPlayer, int pSpeed)
        {
            _owner = pOwner;
            _player = pPlayer;
            _speed = pSpeed;
        }

        public virtual void UpdateState(GameTime pGameTime)
        {

        }

        public virtual void Enter(GameTime pGameTime)
        {

        }

        public virtual void Exit(GameTime pGameTime)
        {

        }


        //Utility Methods
        protected float GetRandomNumber(Vector2 pRange)
        {
            Debug.Assert(pRange.Y > pRange.X);

            float range = pRange.Y - pRange.X;
            float randomInRange = (float)_random.NextDouble() * range + pRange.X;

            return randomInRange;
        }

        protected float CalculatePlayerDistance()
        {
            Vector2 playerDifferenceChase = _owner.Position - _player.Position;
            float playerDistance = playerDifferenceChase.Length();

            return playerDistance;
        }

        protected void Move(GameTime pGameTime, Vector2 pDirection)
        {
            pDirection.Normalize();
            Vector2 translation = _speed * (float)pGameTime.ElapsedGameTime.TotalSeconds * -pDirection;
            _owner.Position += translation;
        }
    }
}