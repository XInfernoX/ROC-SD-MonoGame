using System;

namespace SpaceInvaders.Refactor.Core.Components
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
        protected Component()
        {
        }

        protected Component(GameObject pOwner)
        {
            _gameObject = pOwner;
        }

        //Copy Constructor
        protected Component(Component pOriginal)
        {
            _gameObject = pOriginal._gameObject;
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
            return $"Unspecified Component";
        }

        public virtual void Dispose() { }

        //public abstract Component Copy();
    }
}
