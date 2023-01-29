using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StateDesignPattern.Assignment3
{
    public class Enemy : GameObject
    {
        //Fields
        private EnemyStateBase _patrolState;
        private EnemyStateBase _chaseState;
        private EnemyStateBase _evadeState;
        private EnemyStateBase _idleState;

        private EnemyStateBase _currentState;

        //Properties
        public EnemyStateBase PatrolState => _patrolState;
        public EnemyStateBase ChaseState => _chaseState;
        public EnemyStateBase EvadeState => _evadeState;
        public EnemyStateBase IdleState => _idleState;

        //Constructor
        public Enemy(Vector2 pPosition, Texture2D pTexture, int pSpeed, GameObject[] pWayPoints, Player pPlayer, float pPlayerDetectionRange) : base(pPosition, pTexture)
        {
            _patrolState = new EnemyPatrolState(this, pPlayer, pSpeed, pWayPoints);
            _chaseState = new EnemyChaseState(this, pPlayer, pSpeed);
            _evadeState = new EnemyEvadeState(this, pPlayer, pSpeed);
            _idleState = new EnemyIdleState(this, pPlayer, pSpeed);

            _currentState = _patrolState;
        }

        //Methods
        public override void Update(GameTime pGameTime)
        {
            _currentState.UpdateState(pGameTime);
        }

        public void ChangeStateTo(EnemyStateBase pState, GameTime pGameTime)
        {
            if (_currentState != pState)
            {
                _currentState.Exit(pGameTime);
                _currentState = pState;
                _currentState.Enter(pGameTime);
            }
        }
    }
}