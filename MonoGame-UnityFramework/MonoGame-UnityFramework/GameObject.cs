using Microsoft.Xna.Framework;

using System.Collections.Generic;

namespace MonoGame_UnityFramework
{
    public class GameObject
    {
        //CONSIDER creating a list of LogicComponents and a list of GraphicalComponents, one gets Updated while the other gets drawn

        //Fields
        private readonly string _name;
        private readonly Transform _transform;
        private readonly List<Component> _components = new List<Component>();

        //Properties
        public string name => _name;
        public Transform transform => _transform;

        //Constructors
        public GameObject(string pName) : this(pName, new Vector2(0,0), 0, new Vector2(1,1)) { }


        /// <summary>
        /// Creates a named GameObject
        /// </summary>
        /// <param name="pName">Name of the GameObject</param>
        /// <param name="pPosition">Starting position of the GameObject</param>
        /// <param name="pRotation">Starting rotation of the GameObject</param>
        /// <param name="pScale">Starting scale of the GameObject</param>
        public GameObject(string pName, Vector2 pPosition = new Vector2(), float pRotation = 0.0f, Vector2 pScale = new Vector2())
        {
            _name = pName;

            if (_transform == null)
            {
                _transform = new Transform(pPosition, pRotation, pScale);
                _components.Add(_transform);
            }
        }
        /// <summary>
        /// Creates a named GameObject with starting Components
        /// </summary>
        /// <param name="pName">Name of the GameObject</param>
        /// <param name="pPosition">Starting position of the GameObject</param>
        /// <param name="pRotation">Starting rotation of the GameObject</param>
        /// <param name="pScale">Starting scale of the GameObject</param>
        /// <param name="pComponents">Starting components of the GameObject</param>
        public GameObject(string pName, Vector2 pPosition = new Vector2(), float pRotation = 0.0f, Vector2 pScale = new Vector2(), params Component[] pComponents) : this(pName, pPosition, pRotation, pScale)
        {
            //_components.AddRange(pComponents);

            for (int i = 0; i < pComponents.Length; i++)
            {
                Component currentComponent = pComponents[i];
                if (currentComponent is Transform)
                    _transform = currentComponent as Transform;

                currentComponent.SetOwner(this);
                _components.Add(currentComponent);
            }
        }
        /// <summary>
        /// Looks for a Component of type T on this GameObject and returns the first instance found
        /// </summary>
        /// <typeparam name="T">Type of component to look for</typeparam>
        /// <returns>A reference to found component</returns>
        public T GetComponent<T>() where T : Component
        {
            for (int i = 0; i < _components.Count; i++)
            {
                if (_components[i] is T)
                    return _components[i] as T;
            }

            return null;
        }
        /// <summary>
        /// Looks for Components of type T on this GameObject and returns an array of instances found
        /// </summary>
        /// <typeparam name="T">Type of components to look for</typeparam>
        /// <returns>References to found components stored in a List</returns>
        public T[] GetComponents<T>() where T : Component
        {
            List<T> foundComponents = new List<T>();
            for (int i = 0; i < _components.Count; i++)
            {
                if (_components[i] is T)
                    foundComponents.Add(_components as T);
            }

            return foundComponents.ToArray();
        }

        /// <summary>
        /// Adds a new component of type T to this GameObject and returns a reference to it
        /// </summary>
        /// <typeparam name="T">Type of component to add</typeparam>
        /// <returns>A reference to added component</returns>
        public T AddComponent<T>() where T : Component
        {
            T newComponent = System.Activator.CreateInstance(typeof(T), this) as T;

            _components.Add(newComponent);
            return newComponent;
        }
        public void AddComponent(Component pComponent)
        {
            pComponent.SetOwner(this);
            _components.Add(pComponent);
        }


        public void Update(GameTime pGameTime)
        {
            for (int i = 0; i < _components.Count; i++)
            {
                _components[i].Update(pGameTime);
            }
        }

        public void LateUpdate(GameTime pGameTime)
        {
            for (int i = 0; i < _components.Count; i++)
            {
                _components[i].LateUpdate(pGameTime);
            }
        }

        public override string ToString()
        {
            return name;
        }
    }
}
