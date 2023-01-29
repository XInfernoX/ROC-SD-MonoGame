using System;
using System.Diagnostics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CSharpAdvanced.CSharpAdvanced.StateDesignPattern.Assignment2
{
    public class Enemy : GameObject
    {
        //Fields - Constructor
        private int _speed;
        private GameObject[] _wayPoints;
        private Player _player;
        private float _playerDetectionRange;

        //Field - Random Ranges
        private Random _random;
        private Vector2 _idleRange = new Vector2(2, 5);
        private Vector2 _patrolRange = new Vector2(8, 10);
        private Vector2 _chaseRange = new Vector2(5, 7);
        private Vector2 _evadeRange = new Vector2(2, 5);

        //Fields - Timers
        private float _patrolTimer;
        private float _idleTimer;
        private float _evadeTimer;
        private float _chaseTimer;

        //Fields - State
        private EnemyState _state;
        private int _currentWayPointIndex = 0;
        private bool _keepEvading = false;

        //Fields - Debug
        private Text _text;
        private Vector2 _textOffset = new Vector2(0, -40);

        //Constructors
        public Enemy(Vector2 pPosition, int pSpeed, Player pPlayer, GameObject[] pWayPoints, float pPlayerDetectionRange = 100) : base(pPosition)
        {
            _speed = pSpeed;
            _wayPoints = pWayPoints;
            _player = pPlayer;
            _playerDetectionRange = pPlayerDetectionRange;

            _random = new Random();
            StartPatrolling(new GameTime());
        }

        //Methods - override
        public override void LoadContent(ContentManager pContent)
        {
            Texture = pContent.Load<Texture2D>("Enemy");
            SpriteFont font = pContent.Load<SpriteFont>("Arial");
            _text = new Text(_position + _textOffset, font, _state.ToString(), Color.White);
        }

        public override void Draw(SpriteBatch pSpiteBatch)
        {
            base.Draw(pSpiteBatch);
            _text.Draw(pSpiteBatch);
        }

        public override void Update(GameTime pGameTime)
        {
            switch (_state)
            {
                case EnemyState.Patrolling:
                    Patrolling(pGameTime);
                    break;
                case EnemyState.Idling:
                    Idling(pGameTime);
                    break;
                case EnemyState.Chasing:
                    Chasing(pGameTime);
                    break;
                case EnemyState.Evading:
                    Evading(pGameTime);
                    break;
            }

            _text.Label = _state.ToString();//Could be optimized
            _text.Position = _position + _textOffset;
        }

        //State Methods
        private void Patrolling(GameTime pGameTime)
        {
            //Patrol behaviour movement
            Vector2 currentWayPoint = _wayPoints[_currentWayPointIndex].Position;
            Move(Position - currentWayPoint);

            //Patrol behaviour update waypoint
            Vector2 wayPointDifference = Position - currentWayPoint;
            if (wayPointDifference.Length() < 5)
            {
                _currentWayPointIndex++;
                _currentWayPointIndex %= _wayPoints.Length;
            }


            //Evade & Chase Transition
            float playerDistance = CalculatePlayerDistance();
            if (playerDistance < _playerDetectionRange)
            {
                if (_player.HasShield && _player.HasWeapon)
                    StartEvading(pGameTime);
                else
                    StartChasing(pGameTime);
            }

            //Idle Transition
            if (pGameTime.TotalGameTime.TotalSeconds > _patrolTimer)
                StartIdle(pGameTime);
        }

        private void Idling(GameTime pGameTime)
        {
            //Idle behaviour
            //  Nothing :E

            //Patroll Transition
            if (pGameTime.TotalGameTime.TotalSeconds > _idleTimer)
                StartPatrolling(pGameTime);

            //Transition to Evade
            float playerDistance = CalculatePlayerDistance();
            if (playerDistance < _playerDetectionRange)
            {
                if (_player.HasShield && _player.HasWeapon)
                    StartEvading(pGameTime);
                else
                    StartChasing(pGameTime);
            }
        }

        private void Chasing(GameTime pGameTime)
        {
            //Chase behaviour
            Move(Position - _player.Position);

            //Evade Transition
            float playerDistance = CalculatePlayerDistance();

            if (playerDistance < _playerDetectionRange)
            {
                if (_player.HasShield && _player.HasWeapon)
                    StartEvading(pGameTime);
            }

            //Patrol Transition
            if (pGameTime.TotalGameTime.TotalSeconds > _chaseTimer)
                StartPatrolling(pGameTime);
        }

        private void Evading(GameTime pGameTime)
        {
            //Evading
            Move(_player.Position - Position);

            //Patrol Transition
            float playerDistance = CalculatePlayerDistance();


            if (_keepEvading && _evadeTimer < pGameTime.TotalGameTime.TotalSeconds)
            {
                //StartPatrolling(pGameTime);
                StartIdle(pGameTime);
                _keepEvading = false;
            }

            if (playerDistance > _playerDetectionRange && _evadeTimer < pGameTime.TotalGameTime.TotalSeconds)
            {
                _evadeTimer = (float)pGameTime.TotalGameTime.TotalSeconds + GetRandomNumber(_evadeRange);
                _keepEvading = true;
            }




            //if (_evadeTimer < (float)pGameTime.TotalGameTime.TotalSeconds)
            //{
            //    _evadeTimer = (float)pGameTime.TotalGameTime.TotalSeconds + 5;
            //}
            //else
            //    StartIdle(pGameTime);
        }


        //Start State Methods
        private void StartPatrolling(GameTime pGameTime)
        {
            _patrolTimer = (float)pGameTime.TotalGameTime.TotalSeconds + GetRandomNumber(_patrolRange);
            _state = EnemyState.Patrolling;
        }

        private void StartIdle(GameTime pGameTime)
        {
            _idleTimer = (float)pGameTime.TotalGameTime.TotalSeconds + GetRandomNumber(_idleRange);
            _state = EnemyState.Idling;
        }

        private void StartChasing(GameTime pGameTime)
        {
            _chaseTimer = (float)pGameTime.TotalGameTime.TotalSeconds + GetRandomNumber(_chaseRange);
            _state = EnemyState.Chasing;
        }

        private void StartEvading(GameTime pGameTime)
        {
            _evadeTimer = (float)pGameTime.TotalGameTime.TotalSeconds + GetRandomNumber(_evadeRange);
            _state = EnemyState.Evading;
        }

        //Helper Methods
        private float GetRandomNumber(Vector2 pRange)
        {
            Debug.Assert(pRange.Y > pRange.X);

            float range = pRange.Y - pRange.X;
            float randomInRange = (float)_random.NextDouble() * range + pRange.X;

            return randomInRange;
        }

        private float CalculatePlayerDistance()
        {
            Vector2 playerDifferenceChase = Position - _player.Position;
            float playerDistance = playerDifferenceChase.Length();

            return playerDistance;
        }

        private void Move(Vector2 pDirection)
        {
            pDirection.Normalize();
            Vector2 evadeTranslation = -pDirection * _speed;
            Position += evadeTranslation;
        }
    }
}