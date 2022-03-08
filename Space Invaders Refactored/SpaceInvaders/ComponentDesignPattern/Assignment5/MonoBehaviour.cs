using Microsoft.Xna.Framework;

using ComponentDesignPattern.Assignment5.Interfaces;

namespace ComponentDesignPattern.Assignment5
{
    public abstract class MonoBehaviour : Component, IUpdateableComponent
    {
        //Methods
        public virtual void Update(GameTime pGameTime)
        {
        }

        public void LateUpdate(GameTime pGameTime)
        {
        }

        public virtual void OnCollision(GameObject pOther)
        {

        }
    }
}