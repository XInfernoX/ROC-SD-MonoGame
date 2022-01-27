using Microsoft.Xna.Framework;

namespace ComponentDesignPattern.Assignment4
{
    public abstract class MonoBehaviour
    {
        private GameObject _owner;

        public Transform Transform => _owner.Transform;

        public void SetOwner(GameObject pOwner)
        {
            _owner = pOwner;
        }

        public virtual void Awake()
        {

        }

        public virtual void Start()
        {

        }

        public virtual void UpdateMonoBehaviour(GameTime pGameTime)
        {

        }

        protected T GetComponent<T>() where T : MonoBehaviour
        {
            return _owner.GetComponent<T>();
        }

        protected T[] GetComponents<T>() where T : MonoBehaviour
        {
            return _owner.GetComponents<T>();
        }
    }
}