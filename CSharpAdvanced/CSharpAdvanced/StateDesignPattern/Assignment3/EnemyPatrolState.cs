using Microsoft.Xna.Framework;

namespace CSharpAdvanced.CSharpAdvanced.StateDesignPattern.Assignment3
{
    public class EnemyPatrolState : EnemyStateBase
    {
        //Fields
        private GameObject[] _wayPoints;

        private float _playerDetectionRange = 100;
        private int _currentWayPointIndex = 0;
        private Vector2 _patrolRange = new Vector2(8, 10);
        private float _patrolTimer;

        //Constructor
        public EnemyPatrolState(Enemy pOwner, Player pPlayer, int pSpeed, GameObject[] pWaypoints) : base(pOwner, pPlayer, pSpeed)
        {
            _wayPoints = pWaypoints;
        }

        public override void Enter(GameTime pGameTime)
        {
            _patrolTimer = (float)pGameTime.TotalGameTime.TotalSeconds + GetRandomNumber(_patrolRange);
        }

        public override void UpdateState(GameTime pGameTime)
        {
            //Patrol behaviour movement
            Vector2 currentWayPoint = _wayPoints[_currentWayPointIndex].Position;
            Move(_owner.Position - currentWayPoint);

            //Patrol behaviour update waypoint
            Vector2 wayPointDifference = _owner.Position - currentWayPoint;
            if (wayPointDifference.Length() < 5)
            {
                _currentWayPointIndex++;
                _currentWayPointIndex %= _wayPoints.Length;
            }


            //Evade & Chase Transition
            float playerDistance = CalculatePlayerDistance();
            if (playerDistance < _playerDetectionRange)
            {
                if (_player.PlayerState == PlayerState.ShieldedAndArmed)
                    _owner.ChangeStateTo(_owner.EvadeState, pGameTime);
                else
                    _owner.ChangeStateTo(_owner.ChaseState, pGameTime);
            }

            //Idle Transition
            if (pGameTime.TotalGameTime.TotalSeconds > _patrolTimer)
                _owner.ChangeStateTo(_owner.IdleState, pGameTime);
        }
    }
}