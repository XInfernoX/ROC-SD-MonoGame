using System;
using Microsoft.Xna.Framework;

namespace SpaceInvaders.Refactor.Core.Components
{
    public abstract class MonoBehaviour : Component
    {
        //Events
        public event Action OnCollisionEnter = delegate { };
        public event Action OnCollisionStay = delegate { };
        public event Action OnCollisionExit = delegate { };

        //Methods
        public virtual void Update(GameTime pGameTime) { }
        //public virtual void FixedUpdate(GameTime pGameTime) { }
        public virtual void LateUpdate(GameTime pGameTime) { }

        //public virtual void OnCollision() { }
        public virtual void OnCollision(GameObject pOther) { }

        public override string ToString()
        {
            return $"Unspecified MonoBehaviour";
        }
    }
}
