using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace StateDesignPattern.Assignment2
{
    public class Enemy : GameObject
    {
        private int _speed;
        private GameObject[] _wayPoints;
        private Player _player;
        private float _playerDetectionRange = 100;

        private EnemyState _state;
        private int _currentWayPointIndex = 0;

        public Enemy(Vector2 pPosition, int pSpeed, Player pPlayer, GameObject[] pWayPoints, float pPlayerDetectionRange) : base(pPosition)
        {
            _speed = pSpeed;
            _wayPoints = pWayPoints;
            _player = pPlayer;
            _playerDetectionRange = pPlayerDetectionRange;
        }

        public override void LoadContent(ContentManager pContent)
        {
            Texture = pContent.Load<Texture2D>("Enemy");
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

            Vector2 patrolDirection = currentWayPoint - Position;

            patrolDirection.Normalize();
            Vector2 patrolTranslation = patrolDirection * _speed;
            Position += patrolTranslation;

            Vector2 wayPointDifference = Position - currentWayPoint;
            if (wayPointDifference.Length() < 5)
            {
                _currentWayPointIndex++;
                _currentWayPointIndex %= _wayPoints.Length;
            }

            //PlayerDetection
            Vector2 playerDifferencePatrol = Position - _player.Position;
            float playerDistancePatrol = playerDifferencePatrol.Length();
            if (playerDistancePatrol < _playerDetectionRange)
            {
                if (_player.HasShield && _player.HasWeapon)
                    _state = EnemyState.Evading;
                else
                    _state = EnemyState.Chasing;
            }
        }

        private void Chasing()
        {
            Vector2 chaseDirection = _player.Position - Position;
            chaseDirection.Normalize();
            Vector2 chaseTranslation = chaseDirection * _speed;
            Position += chaseTranslation;

            Vector2 playerDifferenceChase = Position - _player.Position;
            float playerDistanceChase = playerDifferenceChase.Length();

            if (playerDistanceChase < _playerDetectionRange)
            {
                if (_player.HasShield && _player.HasWeapon)
                    _state = EnemyState.Evading;
            }
            else
            {
                _state = EnemyState.Patrolling;
            }
        }

        private void Evading()
        {
            Vector2 evadeDirection = _player.Position - Position;
            evadeDirection.Normalize();
            Vector2 evadeTranslation = -evadeDirection * _speed;
            Position += evadeTranslation;
        }
    }
}