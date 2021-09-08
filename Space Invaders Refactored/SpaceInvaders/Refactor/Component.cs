using Microsoft.Xna.Framework;

namespace SpaceInvaders.Refactor
{
    //CONSIDER whether we want to hide "_owner.", Makes sense from a Unity resemblance perspective, but might show students the relation between Components and owners
    public abstract class Component
    {
        //Fields
        private GameObject _gameObject;

        //Properties
        protected GameObject gameObject => _gameObject;
        protected Transform transform => _gameObject.transform;

        //Constructors
        public Component() : this(null) { }

        public Component(GameObject pOwner)
        {
            _gameObject = pOwner;
        }

        //Methods
        public void SetOwner(GameObject pOwner)
        {
            _gameObject = pOwner;
        }

        public abstract Component Copy();
    }


    public abstract class MonoBehaviour : Component
    {
        public virtual void Update(GameTime pGameTime) { }
        public virtual void FixedUpdate(GameTime pGameTime) { }
        public virtual void LateUpdate(GameTime pGameTime) { }
    }
}
