using Microsoft.Xna.Framework;

namespace CSharpAdvanced.CSharpAdvanced.StateGameRefactored
{
    public class EnemyStateBase
    {
        //Fields
        protected Enemy _owner;
        protected Player _player;
        protected int _speed;

        //Constructor
        public EnemyStateBase(Enemy pOwner, Player pPlayer, int pSpeed)
        {
            _owner = pOwner;
            _player = pPlayer;
            _speed = pSpeed;
        }

        public virtual void UpdateState(GameTime pGameTime)
        {

        }

        public virtual void Enter()
        {

        }

        public virtual void Exit()
        {

        }
    }
}