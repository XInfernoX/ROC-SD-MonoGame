using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StateGameRefactored1
{
    public class Enemy : GameObject
    {
        private int _speed;
        private GameObject[] _wayPoints;
        private Player _player;
        private float _playerDetectionRange = 100;
        private GameObject _shield;
        private GameObject _weapon;

        private EnemyState _state;
        private int _currentWayPointIndex = 0;

        public Enemy(Vector2 pPosition, Texture2D pTexture, int pSpeed, GameObject[] pWayPoints, Player pPlayer, float pPlayerDetectionRange, GameObject pShield, GameObject pWeapon) : base(pPosition, pTexture)
        {
            _speed = pSpeed;
            _wayPoints = pWayPoints;
            _player = pPlayer;
            _playerDetectionRange = pPlayerDetectionRange;

            _shield = pShield;
            _weapon = pWeapon;
        }

        public override void Update(GameTime pGameTime)
        {
            switch (_state)
            {
                case EnemyState.Patrolling:
                    Patrolling();
                    break;
                case EnemyState.Chasing:
                    Chasing();
                    break;
                case EnemyState.Evading:
                    Evading();
                    break;
            }

        }

        private void Patrolling()
        {
            Vector2 currentWayPoint = _wayPoints[_currentWayPointIndex].Position;

            Vector2 patrolDirection = (currentWayPoint - _position);

            patrolDirection.Normalize();
            Vector2 patrolTranslation =
                patrolDirection * _speed;
            _position += patrolTranslation;

            Vector2 wayPointDifference = _position - currentWayPoint;
            if (wayPointDifference.Length() < 5)
            {
                _currentWayPointIndex++;
                _currentWayPointIndex %= _wayPoints.Length;
            }

            //PlayerDetection
            Vector2 playerDifferencePatrol = _position - _player.Position;
            float playerDistancePatrol = playerDifferencePatrol.Length();
            if (playerDistancePatrol < _playerDetectionRange)
            {
                if (!_weapon.Active && !_shield.Active)
                    _state = EnemyState.Evading;
                else
                    _state = EnemyState.Chasing;
            }
        }

        private void Chasing()
        {
            Vector2 chaseDirection = _player.Position - _position;
            chaseDirection.Normalize();
            Vector2 chaseTranslation = chaseDirection * _speed;
            _position += chaseTranslation;

            Vector2 playerDifferenceChase = _position - _player.Position;
            float playerDistanceChase = playerDifferenceChase.Length();

            if (playerDistanceChase < _playerDetectionRange)
            {
                if (!_weapon.Active && !_shield.Active)
                    _state = EnemyState.Evading;
            }
            else
            {
                _state = EnemyState.Patrolling;
            }
        }

        private void Evading()
        {
            Vector2 evadeDirection = _player.Position - _position;
            evadeDirection.Normalize();
            Vector2 evadeTranslation = -evadeDirection * _speed;
            _position += evadeTranslation;
        }
    }
}