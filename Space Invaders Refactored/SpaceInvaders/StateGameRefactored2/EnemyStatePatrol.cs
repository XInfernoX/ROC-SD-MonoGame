using Microsoft.Xna.Framework;

namespace StateGameRefactored2
{
    public class EnemyStatePatrol : EnemyStateBase
    {
        //Fields
        private GameObject[] _wayPoints;
        private float _playerDetectionRange;

        private GameObject _weapon;
        private GameObject _shield;

        private int _currentWayPointIndex = 0;

        //Constructor
        public EnemyStatePatrol(Enemy pOwner, Player pPlayer, int pPatrolSpeed,
            GameObject[] pWayPoints, float pPlayerDetectionRange, GameObject pWeapon, GameObject pShield) : base(pOwner,
            pPlayer, pPatrolSpeed)
        {
            _wayPoints = pWayPoints;
            _playerDetectionRange = pPlayerDetectionRange;

            _weapon = pWeapon;
            _shield = pShield;
        }

        public override void UpdateState(GameTime pGameTime)
        {
            Vector2 currentWayPointPosition = _wayPoints[_currentWayPointIndex].Position;

            Patrol(pGameTime, currentWayPointPosition);
            UpdateCheckCurrentWayPoint(currentWayPointPosition);
            CheckPlayerDetection();
        }

        private void Patrol(GameTime pGameTime, Vector2 pWayPointPosition)
        {
            Vector2 patrolDirection = (pWayPointPosition - _owner.Position);

            patrolDirection.Normalize();
            Vector2 patrolTranslation = patrolDirection * _speed * (float)pGameTime.ElapsedGameTime.TotalSeconds;
            _owner.Position += patrolTranslation;
        }

        private void UpdateCheckCurrentWayPoint(Vector2 pWayPointPosition)
        {
            Vector2 wayPointDifference = _owner.Position - pWayPointPosition;
            if (wayPointDifference.Length() < 5)
            {
                _currentWayPointIndex++;
                _currentWayPointIndex %= _wayPoints.Length;
            }
        }

        private void CheckPlayerDetection()
        {
            //PlayerDetection
            Vector2 enemyToPlayer = _owner.Position - _player.Position;
            float playerDistance = enemyToPlayer.Length();

            if (playerDistance < _playerDetectionRange)
            {
                if (!_weapon.Active && !_shield.Active)
                    _owner.ChangeStateTo(_owner.EvadeState);
                else
                    _owner.ChangeStateTo(_owner.ChaseState);
            }
        }
    }
}