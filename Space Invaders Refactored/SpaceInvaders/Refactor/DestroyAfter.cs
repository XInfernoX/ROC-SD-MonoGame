using Microsoft.Xna.Framework;
using SpaceInvaders.Refactor.Core.Components;

namespace SpaceInvaders.Refactor
{
    public class DestroyAfter : MonoBehaviour
    {
        //Fields
        private readonly float _destroyTime;

        private float _lifeTime;

        //Constructors
        public DestroyAfter(float pDestroyTime)
        {
            _destroyTime = pDestroyTime;
        }
        private DestroyAfter(float pDestroyTime, float pLifeTime)
        {
            _destroyTime = pDestroyTime;
            _lifeTime = pLifeTime;
        }

        //Constructor-ish
        //public override Component Copy()
        //{
        //    return new DestroyAfter(_destroyTime, _lifeTime);
        //}

        public override void Update(GameTime pGameTime)
        {
            _lifeTime += (float)pGameTime.ElapsedGameTime.TotalSeconds;

            if(_lifeTime >= _destroyTime)
            {
                //Destroy GameObject
                Destroy(gameObject);
            }
        }
    }
}
