using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StateGameRefactored2
{
    public class Enemy : GameObject
    {
        //Fields
        private EnemyStateBase _patrolState;
        private EnemyStateBase _chaseState;
        private EnemyStateBase _evadeState;

        private EnemyStateBase _currentState;

        //Properties
        public EnemyStateBase PatrolState => _patrolState;
        public EnemyStateBase ChaseState => _chaseState;
        public EnemyStateBase EvadeState => _evadeState;

        //Constructor
        public Enemy(Vector2 pPosition, Texture2D pTexture, int pSpeed, GameObject[] pWayPoints, Player pPlayer, float pPlayerDetectionRange, GameObject pShield, GameObject pWeapon) : base(pPosition, pTexture)
        {
            _patrolState = new EnemyStatePatrol(this, pPlayer, pSpeed, pWayPoints, pPlayerDetectionRange, pWeapon, pShield);
            _chaseState = new EnemyStateChase(this, pPlayer, pSpeed, pPlayerDetectionRange, pWeapon, pShield);
            _evadeState = new EnemyStateEvade(this, pPlayer, pSpeed);

            _currentState = _patrolState;
        }

        //Methods
        public override void Update(GameTime pGameTime)
        {
            _currentState.UpdateState(pGameTime);
        }

        public void ChangeStateTo(EnemyStateBase pState)
        {
            if (_currentState != pState)
            {
                _currentState.Exit();
                _currentState = pState;
                _currentState.Enter();
            }
        }
    }
}