using Microsoft.Xna.Framework;

namespace CSharpAdvanced.CSharpAdvanced.StateGameRefactored
{
    public class EnemyStateEvade : EnemyStateBase
    {
        public EnemyStateEvade(Enemy pOwner, Player pPlayer, int pEvadeSpeed) : base(pOwner, pPlayer, pEvadeSpeed)
        {

        }

        public override void UpdateState(GameTime pGameTime)
        {
            Vector2 evadeDirection = _player.Position - _owner.Position;
            evadeDirection.Normalize();
            Vector2 evadeTranslation = -evadeDirection * _speed * (float)pGameTime.ElapsedGameTime.TotalSeconds;
            _owner.Position += evadeTranslation;
        }
    }
}