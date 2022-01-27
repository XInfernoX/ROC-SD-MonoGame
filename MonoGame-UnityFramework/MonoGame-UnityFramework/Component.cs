using Microsoft.Xna.Framework;

namespace MonoGame_UnityFramework
{
    public abstract class Component
    {
        private GameObject _gameObject;

        //CONSIDER whether we want to hide "_owner.", Makes sense from a Unity resemblance perspective, but might show students the relation between Components and owners
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

        public virtual void Update(GameTime pGameTime) { }
        public virtual void FixedUpdate(GameTime pGameTime) { }
        public virtual void LateUpdate(GameTime pGameTime) { }

    }


    public abstract class MonoBehaviour : Component
    {
        
    }
}
