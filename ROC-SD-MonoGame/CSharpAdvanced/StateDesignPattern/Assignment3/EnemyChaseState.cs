using Microsoft.Xna.Framework;

namespace ROC_SD_MonoGame.CSharpAdvanced.StateDesignPattern.Assignment3
{
    public class EnemyChaseState : EnemyStateBase
    {
        private int _speed;
        private Vector2 _chaseRange = new Vector2(5, 7);
        private float _playerDetectionRange = 100;

        public EnemyChaseState(Enemy pOwner, Player pPlayer, int pSpeed) : base(pOwner, pPlayer, pSpeed)
        {
            _speed = pSpeed;
        }

        public override void UpdateState(GameTime pGameTime)
        {
            //Chase behaviour
            Move(pGameTime, _owner.Position - _player.Position);

            //Evade Transition
            float playerDistance = CalculatePlayerDistance();

            if (playerDistance < _playerDetectionRange)
            {
                if (_player.PlayerState == PlayerState.ShieldedAndArmed)
                    _owner.ChangeStateTo(_owner.EvadeState, pGameTime);
            }

            //Patrol Transition
            //if (pGameTime.TotalGameTime.TotalSeconds > _chaseTimer)
            //    StartPatrolling(pGameTime);



        }
    }
}