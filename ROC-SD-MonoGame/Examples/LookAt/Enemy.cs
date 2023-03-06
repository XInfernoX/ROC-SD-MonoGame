using Microsoft.Xna.Framework;
using CSharpExpert.ComponentDesignPattern.Assignment3;

namespace ROC_SD_MonoGame.Examples.LookAt
{
    public class Enemy : MonoBehaviour
    {
        private readonly Transform _target;

        public Enemy(Transform pTarget)
        {
            _target = pTarget;
        }

        public override void Update(GameTime pGameTime)
        {
            Transform.LookAt(_target);
        }
    }
}
