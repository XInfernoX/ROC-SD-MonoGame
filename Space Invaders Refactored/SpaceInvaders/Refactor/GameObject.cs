using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace SpaceInvaders.Refactor
{
    public class GameObject : IDisposable
    {
        //CONSIDER creating a list of LogicComponents and a list of GraphicalComponents, one gets Updated while the other gets drawn
        //CONSIDER Removing copy constructor of GameObject and also all Component.Copy() functions, let them make a method to create a prefab instead!

        //Fields
        private readonly RefactoredGame _game;
        private readonly string _name;
        private readonly Transform _transform;
        private readonly List<Component> _components = new List<Component>();

        //Properties
        public string name => _name;
        public Transform transform => _transform;
        public RefactoredGame game => _game;

        //Copy-Constructor
        //public GameObject(GameObject pOriginal)
        //{
        //    _name = pOriginal._name;

        //    for (int i = 0; i < pOriginal._components.Count; i++)
        //    {
        //        Component copiedComponent = _components[i].Copy();
        //        if (copiedComponent is Transform)
        //            _transform = copiedComponent as Transform;

        //        copiedComponent.SetOwner(this);
        //        _components.Add(copiedComponent);
        //    }
        //}

        //Constructors
        /// <summary>Creates a GameObject with starting Components</summary>
        /// <param name="pGame">A reference to the game this GameObject belongs to (usually you would need to use 'this' here)</param>
        /// <param name="pName">Name of the GameObject</param>
        /// <param name="pPosition">Starting position of the GameObject</param>
        /// <param name="pOrigin">Location of the origin (center/pivot point) in UV coodinates (percentage-like values) Ranges[0,1]</param>
        /// <param name="pRotation">Starting rotation of the GameObject</param>
        /// <param name="pScale">Starting scale of the GameObject</param>
        /// <param name="pComponents">Starting components of the GameObject</param>
        public GameObject(RefactoredGame pGame, string pName, Vector2 pPosition = new Vector2(), Vector2 pOrigin = new Vector2(), float pRotation = 0.0f, Vector2 pScale = new Vector2(), params Component[] pComponents)
        {
            _game = pGame;
            _name = pName;

            for (int i = 0; i < pComponents.Length; i++)
            {
                Component currentComponent = pComponents[i];
                if (currentComponent is Transform)
                    _transform = currentComponent as Transform;

                currentComponent.SetOwner(this);
                _components.Add(currentComponent);
            }

            if (_transform == null)
            {
                _transform = new Transform(pPosition, pRotation, pScale, pOrigin);
                _components.Add(_transform);
            }
        }
        /// <summary>Creates a GameObject with starting Components</summary>
        /// <param name="pGame">A reference to the game this GameObject belongs to (usually you would need to use 'this' here)</param>
        /// <param name="pName">Name of the GameObject</param>
        /// <param name="pComponents">Starting components of the GameObject</param>
        public GameObject(RefactoredGame pGame, string pName, params Component[] pComponents) : this(pGame, pName, Vector2.Zero, new Vector2(0.5f, 0.5f), 0, Vector2.One, pComponents) { }

        /// <summary>Creates a GameObject with starting Components</summary>
        /// <param name="pGame">A reference to the game this GameObject belongs to (usually you would need to use 'this' here)</param>
        /// <param name="pName">Name of the GameObject</param>
        /// <param name="pPosition">Starting position of the GameObject</param>
        /// <param name="pComponents">Starting components of the GameObject</param>
        public GameObject(RefactoredGame pGame, string pName, Vector2 pPosition, params Component[] pComponents) : this(pGame, pName, pPosition, new Vector2(0.5f, 0.5f), 0, Vector2.One, pComponents) { }

        /// <summary>Creates a GameObject with starting Components</summary>
        /// <param name="pGame">A reference to the game this GameObject belongs to (usually you would need to use 'this' here)</param>
        /// <param name="pName">Name of the GameObject</param>
        /// <param name="pPosition">Starting position of the GameObject</param>
        /// <param name="pOrigin">Location of the origin (center/pivot point) in UV coodinates (percentage-like values) Ranges[0,1]</param>
        /// <param name="pComponents">Starting components of the GameObject</param>public GameObject(RefactoredGame pGame, string pName, Vector2 pPosition, params Component[] pComponents) : this(pGame, pName, pPosition, new Vector2(0.5f, 0.5f), 0, Vector2.One, pComponents) { }
        public GameObject(RefactoredGame pGame, string pName, Vector2 pPosition, Vector2 pOrigin, params Component[] pComponents) : this(pGame, pName, pPosition, pOrigin, 0, Vector2.One, pComponents) { }
        /// <summary>Creates a GameObject with starting Components</summary>
        /// <param name="pGame">A reference to the game this GameObject belongs to (usually you would need to use 'this' here)</param>
        /// <param name="pName">Name of the GameObject</param>
        /// <param name="pPosition">Starting position of the GameObject</param>
        /// <param name="pOrigin">Location of the origin (center/pivot point) in UV coodinates (percentage-like values) Ranges[0,1]</param>
        /// <param name="pRotation">Starting rotation of the GameObject</param>
        /// <param name="pComponents">Starting components of the GameObject</param>
        public GameObject(RefactoredGame pGame, string pName, Vector2 pPosition, Vector2 pOrigin, float pRotation, params Component[] pComponents) : this(pGame, pName, pPosition, pOrigin, pRotation, Vector2.One, pComponents) { }

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
                    foundComponents.Add(_components[i] as T);
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
            T newComponent = Activator.CreateInstance(typeof(T), this) as T;

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
                Component currentComponent = _components[i];

                if (currentComponent is MonoBehaviour)
                {
                    ((MonoBehaviour)currentComponent).Update(pGameTime);
                }
            }
        }

        public void LateUpdate(GameTime pGameTime)
        {
            for (int i = 0; i < _components.Count; i++)
            {
                Component currentComponent = _components[i];

                if (currentComponent is MonoBehaviour)
                {
                    ((MonoBehaviour)currentComponent).LateUpdate(pGameTime);
                }
            }
        }

        //public void CollisionCheck(Collider pOther)
        //{
        //    for (int i = 0; i < _components.Count; i++)
        //    {
        //        Component currentComponent = _components[i];

        //        if(currentComponent is Collider)
        //        {
        //            ((Collider)currentComponent).CollisionCheck(pOther);
        //        }
        //    }
        //}

        public void CollisionCheck(GameObject pOther)
        {
            bool collision = false;

            for (int myComponentIndex = 0; myComponentIndex < _components.Count; myComponentIndex++)
            {
                Component currentComponent = _components[myComponentIndex];

                if (currentComponent is Collider collider)
                {
                    Collider[] otherColliders = pOther.GetComponents<Collider>();
                    for (int otherComponentIndex = 0; otherComponentIndex < otherColliders.Length; otherComponentIndex++)
                    {
                        if (collider.CollisionCheck(otherColliders[otherComponentIndex]))
                        {
                            collision = true;
                            CallOnCollisionEventMethods(this, pOther);
                            break;
                        }
                    }

                    if (collision)
                        break;
                }
            }
        }

        private void CallOnCollisionEventMethods(GameObject pOne, GameObject pOther)
        {
            MonoBehaviour[] oneMonoBehaviours = pOne.GetComponents<MonoBehaviour>();
            for (int i = 0; i < oneMonoBehaviours.Length; i++)
            {
                //oneMonoBehaviours[i].OnCollision();
                oneMonoBehaviours[i].OnCollision(pOther);
            }

            MonoBehaviour[] otherMonoBehaviours = pOther.GetComponents<MonoBehaviour>();
            for (int i = 0; i < otherMonoBehaviours.Length; i++)
            {
                //otherMonoBehaviours[i].OnCollision();
                otherMonoBehaviours[i].OnCollision(pOther);
            }
        }

        public void Draw(SpriteBatch pSpriteBatch)
        {
            for (int i = 0; i < _components.Count; i++)
            {
                Component currentComponent = _components[i];

                if (currentComponent is SpriteRenderer)
                {
                    ((SpriteRenderer)currentComponent).Draw(_transform, pSpriteBatch);
                }
            }
        }

        public override string ToString()
        {
            return name;
        }

        public void Dispose()
        {
            for (int i = _components.Count - 1; i >= 0; i--)
            {
                Component currentComponent = _components[i];
                _components.RemoveAt(i);
                currentComponent.Dispose();
            }

            _game.RemoveGameObject(this);
        }
    }
}

