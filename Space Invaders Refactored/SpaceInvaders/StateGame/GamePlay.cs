using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace StateGame
{
    public class GamePlay : Game
    {
        //Fields
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //Resources
        private Texture2D _playerTexture;
        private Texture2D _playerShieldTexture;
        private Texture2D _playerWeaponTexture;
        private Texture2D _playerWeaponShieldTexture;
        private Texture2D _enemyTexture;
        private Texture2D _weaponTexture;
        private Texture2D _shieldTexture;
        private Texture2D _flagTexture;

        //Player
        private readonly GameObject _player;
        private float _playerSpeed = 140;//pixels per second

        //Enemy
        private readonly GameObject _enemy;
        private float _enemySpeed = 100;
        private EnemyState _enemyState = EnemyState.Patrolling;
        private readonly GameObject[] _wayPoints;
        private int _currentWayPointIndex = 0;
        private float _playerDetectionRange = 100;

        //Pickups
        private readonly GameObject _shield;
        private readonly GameObject _weapon;

        //Constructor
        public GamePlay()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _player = new GameObject();
            _enemy = new GameObject();

            _weapon = new GameObject();
            _shield = new GameObject();

            _wayPoints = new GameObject[]
            {
                new GameObject(),
                new GameObject(),
                new GameObject()
            };
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //Textures
            _playerTexture = Content.Load<Texture2D>("Knight");
            _playerShieldTexture = Content.Load<Texture2D>("KnightShield");
            _playerWeaponTexture = Content.Load<Texture2D>("KnightWeapon");
            _playerWeaponShieldTexture = Content.Load<Texture2D>("KnightWeaponShield");
            _enemyTexture = Content.Load<Texture2D>("Enemy");
            _weaponTexture = Content.Load<Texture2D>("Weapon");
            _shieldTexture = Content.Load<Texture2D>("Shield");
            _flagTexture = Content.Load<Texture2D>("Flag");

            Viewport viewPort = GraphicsDevice.Viewport;
            float third = viewPort.Height / 3;

            //Player
            _player.Texture = _playerTexture;
            _player.Position = new Vector2(viewPort.Width / 2, third * 2);

            //Enemy
            _enemy.Texture = _enemyTexture;
            _enemy.Position = new Vector2(viewPort.Width / 2, third);

            //Pickups
            _weapon.Texture = _weaponTexture;
            _weapon.Position = new Vector2(100, third);
            _shield.Texture = _shieldTexture;
            _shield.Position = new Vector2(viewPort.Width - 100, third);

            //WayPoints
            for (int i = 0; i < _wayPoints.Length; i++)
            {
                _wayPoints[i].Texture = _flagTexture;
            }
            _wayPoints[0].Position = new Vector2(viewPort.Width * 0.1f, viewPort.Height * 0.1f);
            _wayPoints[1].Position = new Vector2(viewPort.Width * 0.9f, viewPort.Height * 0.1f);
            _wayPoints[2].Position = new Vector2(viewPort.Width / 2, viewPort.Height / 2);
        }

        protected override void Update(GameTime pGameTime)
        {
            base.Update(pGameTime);

            KeyboardState keyboardState = Keyboard.GetState();

            //Player input registration
            Vector2 playerInput = Vector2.Zero;
            if (keyboardState.IsKeyDown(Keys.A))
                playerInput.X--;
            if (keyboardState.IsKeyDown(Keys.D))
                playerInput.X++;
            if (keyboardState.IsKeyDown(Keys.W))
                playerInput.Y--;
            if (keyboardState.IsKeyDown(Keys.S))
                playerInput.Y++;

            if (playerInput != Vector2.Zero)
            {
                playerInput.Normalize();
                Vector2 playerTranslation = playerInput * _playerSpeed * (float)pGameTime.ElapsedGameTime.TotalSeconds;
                _player.Position += playerTranslation;
            }

            //Player collision check with weapon
            if (_player.Collision(_weapon))
            {
                _weapon.Active = false;

                if (_shield.Active)
                {
                    _player.Texture = _playerWeaponTexture;
                }
                else
                {
                    _player.Texture = _playerWeaponShieldTexture;
                }
            }

            //Player collision check with shield
            if (_player.Collision(_shield))
            {
                _shield.Active = false;

                if (_weapon.Active)
                {
                    _player.Texture = _playerShieldTexture;
                }
                else
                {
                    _player.Texture = _playerWeaponShieldTexture;
                }
            }

            //Enemy Behaviour
            switch (_enemyState)
            {
                case EnemyState.Patrolling:
                    //Patrol movement
                    Vector2 currentWayPoint = _wayPoints[_currentWayPointIndex].Position;
                    Vector2 patrolDirection = (currentWayPoint - _enemy.Position);
                    patrolDirection.Normalize();
                    Vector2 patrolTranslation =
                        patrolDirection * _enemySpeed * (float)pGameTime.ElapsedGameTime.TotalSeconds;
                    _enemy.Position += patrolTranslation;

                    //WayPoint distance check and update
                    Vector2 wayPointDifference = _enemy.Position - currentWayPoint;
                    if (wayPointDifference.Length() < 5)
                    {
                        _currentWayPointIndex++;
                        _currentWayPointIndex %= _wayPoints.Length;
                    }

                    //PlayerDetection
                    Vector2 playerDifferencePatrol = _enemy.Position - _player.Position;
                    float playerDistancePatrol = playerDifferencePatrol.Length();
                    if (playerDistancePatrol < _playerDetectionRange)
                    {
                        if (!_weapon.Active && !_shield.Active)
                            _enemyState = EnemyState.Evading;
                        else
                            _enemyState = EnemyState.Chasing;
                    }
                    break;
                case EnemyState.Chasing:
                    //Chase movement
                    Vector2 chaseDirection = _player.Position - _enemy.Position;
                    chaseDirection.Normalize();
                    Vector2 chaseTranslation = chaseDirection * _enemySpeed * (float)pGameTime.ElapsedGameTime.TotalSeconds;

                    _enemy.Position += chaseTranslation;

                    //Player distance check
                    Vector2 playerDifferenceChase = _enemy.Position - _player.Position;
                    float playerDistanceChase = playerDifferenceChase.Length();

                    if (playerDistanceChase < _playerDetectionRange)
                    {
                        if (!_weapon.Active && !_shield.Active)
                            _enemyState = EnemyState.Evading;
                    }
                    else
                    {
                        _enemyState = EnemyState.Patrolling;
                    }

                    break;
                case EnemyState.Evading:
                    //Evade movement
                    Vector2 evadeDirection = _player.Position - _enemy.Position;
                    evadeDirection.Normalize();
                    Vector2 evadeTranslation = -evadeDirection * _enemySpeed * (float)pGameTime.ElapsedGameTime.TotalSeconds;
                    _enemy.Position += evadeTranslation;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }

        protected override void Draw(GameTime pGameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _player.Draw(_spriteBatch, Color.White);
            _enemy.Draw(_spriteBatch, Color.White);
            _shield.Draw(_spriteBatch, Color.White);
            _weapon.Draw(_spriteBatch, Color.White);

            for (int i = 0; i < _wayPoints.Length; i++)
            {
                _wayPoints[i].Draw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(pGameTime);
        }
    }
}

