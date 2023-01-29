using Microsoft.Xna.Framework;

namespace StateDesignPattern.Assignment3
{
    public class EnemyChaseState : EnemyStateBase
    {
        private int _speed;
        private Vector2 _chaseRange = new Vector2(5, 7);

        public EnemyChaseState(Enemy pOwner, Player pPlayer, int pSpeed) : base(pOwner, pPlayer, pSpeed)
        {
            _speed = pSpeed;
        }
    }
}