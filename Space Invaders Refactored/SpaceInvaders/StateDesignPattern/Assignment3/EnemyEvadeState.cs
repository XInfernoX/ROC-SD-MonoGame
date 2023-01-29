using Microsoft.Xna.Framework;

namespace StateDesignPattern.Assignment3
{
    public class EnemyEvadeState : EnemyStateBase
    {
        private int _speed;
        private Vector2 _evadeRange = new Vector2(2, 5);

        private float _evadeTimer = 0;
        private float _playerDetectionRange = 100;

        private bool _keepEvading = false;

        public EnemyEvadeState(Enemy pOwner, Player pPlayer, int pSpeed) : base(pOwner, pPlayer, pSpeed)
        {
            _speed = pSpeed;
        }

        public override void Enter(GameTime pGameTime)
        {
            _evadeTimer = (float)pGameTime.TotalGameTime.TotalSeconds + GetRandomNumber(_evadeRange);
        }

        public override void UpdateState(GameTime pGameTime)
        {
            //Evading
            Move(_player.Position - _owner.Position);

            //Patrol Transition
            float playerDistance = CalculatePlayerDistance();


            if (_keepEvading && _evadeTimer < pGameTime.TotalGameTime.TotalSeconds)
            {
                //StartPatrolling(pGameTime);
                _owner.ChangeStateTo(_owner.IdleState, pGameTime);
                _keepEvading = false;
            }

            if (playerDistance > _playerDetectionRange && _evadeTimer < pGameTime.TotalGameTime.TotalSeconds)
            {
                _evadeTimer = (float)pGameTime.TotalGameTime.TotalSeconds + GetRandomNumber(_evadeRange);
                _keepEvading = true;
            }
        }
    }
}