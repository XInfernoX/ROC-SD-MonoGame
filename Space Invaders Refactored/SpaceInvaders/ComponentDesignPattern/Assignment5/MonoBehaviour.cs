using Microsoft.Xna.Framework;

using ComponentDesignPattern.Assignment5.Interfaces;

namespace ComponentDesignPattern.Assignment5
{
    //Base class for most custom components, it is Updateable and gets notified upon certain events, such as OnCollision
    public abstract class MonoBehaviour : Component, IUpdateableComponent
    {
        //Methods
        public virtual void Update(GameTime pGameTime)
        {
        }

        public void LateUpdate(GameTime pGameTime)
        {
        }

        //Event methods
        public virtual void OnCollision(GameObject pOther)
        {

        }
    }
}