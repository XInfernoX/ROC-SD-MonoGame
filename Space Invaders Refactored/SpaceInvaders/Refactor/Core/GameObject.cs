using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SpaceInvaders.Refactor.Core.Components;

namespace SpaceInvaders.Refactor.Core
{
    public class GameObject : IDisposable
    {
        //CONSIDER renaming IUpdateable to ILogicComponent and IDrawable to IGraphicalComponent

        //Fields
        private readonly RefactoredGameBase _game;//CONSIDER whether _game should be readonly (game == scene, cannot move from scene to scene as readonly)
        private readonly string _name;
        private readonly Transform _transform;
        private readonly List<Component> _components = new List<Component>();

        private readonly List<IDrawable> _drawableComponents = new List<IDrawable>();
        private readonly List<IUpdateable> _updateableComponents = new List<IUpdateable>();

        private readonly List<ICollideable> _collideableComponents = new List<ICollideable>();
        private readonly List<Collider> _colliderComponents = new List<Collider>();

        //Properties - CONSIDER whether to use Unity's naming convention (camelCase) vs Conventional (PascalCase) for these Properties
        public string name => _name;
        public Transform transform => _transform;
        public RefactoredGameBase game => _game;

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
        public GameObject(RefactoredGameBase pGame, string pName, Vector2 pPosition = new Vector2(), Vector2 pOrigin = new Vector2(), float pRotation = 0.0f, Vector2 pScale = new Vector2(), params Component[] pComponents)
        {
            _game = pGame;
            _name = pName;

            for (int i = 0; i < pComponents.Length; i++)
            {
                Component currentComponent = pComponents[i];
                if (currentComponent is Transform component)
                    _transform = component;

                AddComponent(currentComponent);
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
        public GameObject(RefactoredGameBase pGame, string pName, params Component[] pComponents) : this(pGame, pName, Vector2.Zero, new Vector2(0.5f, 0.5f), 0, Vector2.One, pComponents) { }

        /// <summary>Creates a GameObject with starting Components</summary>
        /// <param name="pGame">A reference to the game this GameObject belongs to (usually you would need to use 'this' here)</param>
        /// <param name="pName">Name of the GameObject</param>
        /// <param name="pPosition">Starting position of the GameObject</param>
        /// <param name="pComponents">Starting components of the GameObject</param>
        public GameObject(RefactoredGameBase pGame, string pName, Vector2 pPosition, params Component[] pComponents) : this(pGame, pName, pPosition, new Vector2(0.5f, 0.5f), 0, Vector2.One, pComponents) { }

        /// <summary>Creates a GameObject with starting Components</summary>
        /// <param name="pGame">A reference to the game this GameObject belongs to (usually you would need to use 'this' here)</param>
        /// <param name="pName">Name of the GameObject</param>
        /// <param name="pPosition">Starting position of the GameObject</param>
        /// <param name="pOrigin">Location of the origin (center/pivot point) in UV coodinates (percentage-like values) Ranges[0,1]</param>
        /// <param name="pComponents">Starting components of the GameObject</param>public GameObject(SpaceInvadersGame pGame, string pName, Vector2 pPosition, params Component[] pComponents) : this(pGame, pName, pPosition, new Vector2(0.5f, 0.5f), 0, Vector2.One, pComponents) { }
        public GameObject(RefactoredGameBase pGame, string pName, Vector2 pPosition, Vector2 pOrigin, params Component[] pComponents) : this(pGame, pName, pPosition, pOrigin, 0, Vector2.One, pComponents) { }
        /// <summary>Creates a GameObject with starting Components</summary>
        /// <param name="pGame">A reference to the game this GameObject belongs to (usually you would need to use 'this' here)</param>
        /// <param name="pName">Name of the GameObject</param>
        /// <param name="pPosition">Starting position of the GameObject</param>
        /// <param name="pOrigin">Location of the origin (center/pivot point) in UV coodinates (percentage-like values) Ranges[0,1]</param>
        /// <param name="pRotation">Starting rotation of the GameObject</param>
        /// <param name="pComponents">Starting components of the GameObject</param>
        public GameObject(RefactoredGameBase pGame, string pName, Vector2 pPosition, Vector2 pOrigin, float pRotation, params Component[] pComponents) : this(pGame, pName, pPosition, pOrigin, pRotation, Vector2.One, pComponents) { }

        /// <summary>Looks for a Component of type T on this GameObject and returns the first instance found</summary>
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
        /// <summary>Looks for Components of type T on this GameObject and returns an array of instances found</summary>
        /// <typeparam name="T">Type of components to look for</typeparam>
        /// <returns>Collection of references to found components as an array</returns>
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

        /// <summary>Adds a new component to this GameObject</summary>
        /// <param name="pComponent">The component reference to add</param>
        public void AddComponent(Component pComponent)
        {
            pComponent.SetOwner(this);
            _components.Add(pComponent);

            if (pComponent is IDrawable drawableComponent)
                _drawableComponents.Add(drawableComponent);

            if (pComponent is IUpdateable updateableComponent)
                _updateableComponents.Add(updateableComponent);

            if (pComponent is Collider colliderCoponent)
                _colliderComponents.Add(colliderCoponent);

            //NOTE currently not used
            if (pComponent is ICollideable collideableComponent)
                _collideableComponents.Add(collideableComponent);
        }

        public void RemoveComponent(Component pComponent)
        {
            if (pComponent is IDrawable drawableComponent)
                _drawableComponents.Remove(drawableComponent);

            if (pComponent is IUpdateable updateableComponent)
                _updateableComponents.Remove(updateableComponent);

            if (pComponent is Collider colliderCoponent)
                _colliderComponents.Remove(colliderCoponent);

            //NOTE currently not used
            if (pComponent is ICollideable collideableComponent)
                _collideableComponents.Remove(collideableComponent);
        }

        /// <summary>Updates all Components attached to this GameObject</summary>
        /// <param name="pGameTime">GameTime reference (should be from Game.Update(GameTime))</param>
        public void Update(GameTime pGameTime)
        {
            for (int i = 0; i < _updateableComponents.Count; i++)
                _updateableComponents[i].Update(pGameTime);

        }
        /// <summary>Updates all Components attached to this GameObject a second time after the Update method</summary>
        /// <param name="pGameTime">GameTime reference (should be from Game.Update(GameTime))</param>
        public void LateUpdate(GameTime pGameTime)
        {
            for (int i = 0; i < _updateableComponents.Count; i++)
                _updateableComponents[i].LateUpdate(pGameTime);
        }

        /// <summary>Checks all collider components of this GameObject with all collider components of another GameObject for a collision</summary>
        /// <param name="pOther">Reference to the other GameObject to check collision against</param>
        public void CollisionCheck(GameObject pOther)
        {
            bool collision = false;

            for (int myColliderComponentIndex = 0; myColliderComponentIndex < _colliderComponents.Count; myColliderComponentIndex++)
            {
                Collider myCurrentCollider = _colliderComponents[myColliderComponentIndex];

                Collider[] otherColliders = pOther.GetComponents<Collider>();
                for (int otherComponentIndex = 0; otherComponentIndex < otherColliders.Length; otherComponentIndex++)
                {
                    if (myCurrentCollider.CollisionCheck(otherColliders[otherComponentIndex]))
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
        /// <summary>Notifies all MonoBehaviour components a collision has happened between two GameObjects</summary>
        /// <param name="pFirstGameObject">First GameObject to notify</param>
        /// <param name="pSecondGameObject">Second GameObject to notify</param>
        private void CallOnCollisionEventMethods(GameObject pFirstGameObject, GameObject pSecondGameObject)
        {
            MonoBehaviour[] firstMonoBehaviours = pFirstGameObject.GetComponents<MonoBehaviour>();
            for (int i = 0; i < firstMonoBehaviours.Length; i++)
                firstMonoBehaviours[i].OnCollision(pSecondGameObject);

            MonoBehaviour[] secondMonoBehaviours = pSecondGameObject.GetComponents<MonoBehaviour>();
            for (int i = 0; i < secondMonoBehaviours.Length; i++)
                secondMonoBehaviours[i].OnCollision(pFirstGameObject);
        }

        /// <summary>Draws all SpriteRenderer components attached to this GameObject</summary>
        /// <param name="pSpriteBatch">SpriteBatch reference used to draw</param>
        public void Draw(SpriteBatch pSpriteBatch)
        {
            for (int i = 0; i < _drawableComponents.Count; i++)
                _drawableComponents[i].Draw(pSpriteBatch);
        }

        /// <summary>
        /// Overrides object.ToString() method, this method gets automatically called when this GameObject is printed to the Console.
        /// This method prints the name of this GameObject and all attached Components, useful for debugging purposes
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string fullName = $"GameObject: {name}\n";

            for (int i = 0; i < _components.Count; i++)
            {
                fullName += $"\t{_components[i]}\n";
            }

            return fullName;
        }

        /// <summary>Gets called upon Destroying this GameObject, it cleans up itself by Destroying and removing all Components and finally itself from the Game</summary>
        public void Dispose()
        {
            for (int i = _components.Count - 1; i >= 0; i--)
            {
                Component currentComponent = _components[i];
                _components.RemoveAt(i);

                currentComponent.Dispose();
            }

            _drawableComponents.Clear();
            _updateableComponents.Clear();
            _colliderComponents.Clear();
            _collideableComponents.Clear();

            _game.RemoveGameObject(this);
        }
    }
}

