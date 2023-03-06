using Microsoft.Xna.Framework;

namespace ROC_SD_MonoGame.CSharpAdvanced.StateDesignPattern.Assignment3
{
    public class EnemyIdleState : EnemyStateBase
    {
        private Vector2 _idleRange = new Vector2(2, 5);

        private float _idleTimer = 0;
        private Vector2 _chaseRange = new Vector2(5, 7);
        private float _playerDetectionRange = 100;

        public EnemyIdleState(Enemy pOwner, Player pPlayer, int pSpeed) : base(pOwner, pPlayer, pSpeed)
        {
        }

        public override void Enter(GameTime pGameTime)
        {
            _idleTimer = (float)pGameTime.TotalGameTime.TotalSeconds + GetRandomNumber(_chaseRange);
        }

        public override void UpdateState(GameTime pGameTime)
        {
            //Idle behaviour
            //  Nothing :E

            //Patroll Transition
            if (pGameTime.TotalGameTime.TotalSeconds > _idleTimer)
                _owner.ChangeStateTo(_owner.PatrolState, pGameTime);

            //Transition to Evade
            float playerDistance = CalculatePlayerDistance();
            if (playerDistance < _playerDetectionRange)
            {
                if (_player.PlayerState == PlayerState.ShieldedAndArmed)
                    _owner.ChangeStateTo(_owner.EvadeState, pGameTime);
                else
                    _owner.ChangeStateTo(_owner.ChaseState, pGameTime);
            }
        }

    }
}