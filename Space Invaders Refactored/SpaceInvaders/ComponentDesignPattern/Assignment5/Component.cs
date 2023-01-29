namespace ComponentDesignPattern.Assignment5
{
    //Base class for all Components
    public class Component
    {
        //Fields
        protected GameObject _owner;

        //Properties
        public Transform Transform => _owner.Transform;

        //Methods
        public virtual void Awake()
        {

        }

        public virtual void Start()
        {

        }
        
        public void SetOwner(GameObject pOwner)
        {
            _owner = pOwner;
        }

        protected T GetComponent<T>() where T : Component
        {
            return _owner.GetComponent<T>();
        }

        protected T[] GetComponents<T>() where T : Component
        {
            return _owner.GetComponents<T>();
        }
    }
}