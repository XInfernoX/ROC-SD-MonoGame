using Microsoft.Xna.Framework;

namespace CSharpAdvanced.CSharpAdvanced.StateGameRefactored
{
    public class EnemyStateChase : EnemyStateBase
    {
        //Fields
        private float _playerDetectionRange;

        private GameObject _weapon;
        private GameObject _shield;

        //Constructor
        public EnemyStateChase(Enemy pEnemy, Player pPlayer, int pChaseSpeed,
            float pPlayerDetectionRange, GameObject pWeapon, GameObject pShield
        ) : base(pEnemy, pPlayer, pChaseSpeed)
        {
            _weapon = pWeapon;
            _shield = pShield;
            _playerDetectionRange = pPlayerDetectionRange;
        }


        public override void UpdateState(GameTime pGameTime)
        {
            Vector2 enemyToPlayer = _player.Position - _owner.Position;

            Chase(pGameTime, enemyToPlayer);
            CheckEvadeTransition(enemyToPlayer);
            CheckPatrolTransition(enemyToPlayer);
        }

        private void Chase(GameTime pGameTime, Vector2 pEnemyToPlayer)
        {
            pEnemyToPlayer.Normalize();
            Vector2 chaseTranslation = pEnemyToPlayer * _speed * (float)pGameTime.ElapsedGameTime.TotalSeconds;
            _owner.Position += chaseTranslation;
        }

        private void CheckEvadeTransition(Vector2 pEnemyToPlayer)
        {
            if (pEnemyToPlayer.Length() < _playerDetectionRange && !_weapon.Active && !_shield.Active)
            {
                //_state = EnemyState.Evading;
                _owner.ChangeStateTo(_owner.EvadeState);
            }
        }

        private void CheckPatrolTransition(Vector2 pEnemyToPlayer)
        {
            if (pEnemyToPlayer.Length() > _playerDetectionRange)
            {
                //_state = EnemyState.Patrolling;
                _owner.ChangeStateTo(_owner.PatrolState);
            }
        }
    }
}