using System;

namespace SpaceInvaders.Refactor
{
    //CONSIDER whether we want to hide "_owner.", Makes sense from a Unity resemblance perspective, but might show students the relation between Components and owners
    public abstract class Component : IDisposable
    {
        //Fields
        private GameObject _gameObject;

        //Properties
        protected GameObject gameObject => _gameObject;
        protected Transform transform => _gameObject.transform;
        protected RefactoredGame game => _gameObject.game;

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

        public void Destroy(GameObject pGameObject)
        {
            pGameObject.Dispose();
        }
        public void Destroy(Component pComponent)
        {
            pComponent.Dispose();
        }

        public override string ToString()
        {
            return $"Component of: {gameObject}";
        }

        public virtual void Dispose() { }

        //public abstract Component Copy();
    }
}
