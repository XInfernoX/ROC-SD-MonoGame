﻿using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ComponentDesignPattern.Assignment5.Interfaces;


namespace ComponentDesignPattern.Assignment5
{
    public class GameObject
    {
        //Fields
        private Game1 _game;

        private readonly string _name;
        private readonly Transform _transform;

        private readonly List<Component> _allComponents = new List<Component>();

        private readonly List<IUpdateableComponent> _updateableComponents = new List<IUpdateableComponent>();
        private readonly List<IDrawableComponent> _drawableComponents = new List<IDrawableComponent>();
        //private readonly List<RectangleCollider> _colliderComponents = new List<RectangleCollider>();
        private readonly List<ICollideableComponent> _collideableComponents = new List<ICollideableComponent>();

        //Properties
        public string Name => _name;
        public Transform Transform => _transform;

        //Constructors
        //Constructors
        /// <summary>Creates a GameObject with starting Components</summary>
        /// <param name="pGame">A reference to the game this GameObject belongs to (usually you would need to use 'this' here)</param>
        /// <param name="pName">Name of the GameObject</param>
        /// <param name="pPosition">Starting position of the GameObject</param>
        /// <param name="pOrigin">Location of the origin (center/pivot point) in UV coodinates (percentage-like values) Ranges[0,1]</param>
        /// <param name="pRotation">Starting rotation of the GameObject</param>
        /// <param name="pScale">Starting scale of the GameObject</param>
        /// <param name="pComponents">Starting components of the GameObject</param>
        public GameObject(Game1 pGame, string pName, Vector2 pPosition = new Vector2(), Vector2 pOrigin = new Vector2(), float pRotation = 0.0f, Vector2 pScale = new Vector2(), params Component[] pComponents)
        {
            _game = pGame;
            _name = pName;

            for (int i = 0; i < pComponents.Length; i++)
            {
                Component currentComponent = pComponents[i];
                if (currentComponent is Transform component)
                {
                    System.Diagnostics.Debug.Assert(_transform == null, "Can not have more than one Transform component!");
                    _transform = component;
                }

                AddComponent(currentComponent);
            }

            if (_transform == null)
            {
                _transform = new Transform(pPosition, pOrigin, pRotation, pScale);
                AddComponent(_transform);
            }
        }
        /// <summary>Creates a GameObject with starting Components</summary>
        /// <param name="pGame">A reference to the game this GameObject belongs to (usually you would need to use 'this' here)</param>
        /// <param name="pName">Name of the GameObject</param>
        /// <param name="pComponents">Starting components of the GameObject</param>
        public GameObject(Game1 pGame, string pName, params Component[] pComponents) : this(pGame, pName, Vector2.Zero, new Vector2(0.5f, 0.5f), 0, Vector2.One, pComponents) { }

        /// <summary>Creates a GameObject with starting Components</summary>
        /// <param name="pGame">A reference to the game this GameObject belongs to (usually you would need to use 'this' here)</param>
        /// <param name="pName">Name of the GameObject</param>
        /// <param name="pPosition">Starting position of the GameObject</param>
        /// <param name="pComponents">Starting components of the GameObject</param>
        public GameObject(Game1 pGame, string pName, Vector2 pPosition, params Component[] pComponents) : this(pGame, pName, pPosition, new Vector2(0.5f, 0.5f), 0, Vector2.One, pComponents) { }

        /// <summary>Creates a GameObject with starting Components</summary>
        /// <param name="pGame">A reference to the game this GameObject belongs to (usually you would need to use 'this' here)</param>
        /// <param name="pName">Name of the GameObject</param>
        /// <param name="pPosition">Starting position of the GameObject</param>
        /// <param name="pOrigin">Location of the origin (center/pivot point) in UV coordinates (percentage-like values) Ranges[0,1]</param>
        /// <param name="pComponents">Starting components of the GameObject</param>public GameObject(SpaceInvaders pGame, string pName, Vector2 pPosition, params Component[] pComponents) : this(pGame, pName, pPosition, new Vector2(0.5f, 0.5f), 0, Vector2.One, pComponents) { }
        public GameObject(Game1 pGame, string pName, Vector2 pPosition, Vector2 pOrigin, params Component[] pComponents) : this(pGame, pName, pPosition, pOrigin, 0, Vector2.One, pComponents) { }
        /// <summary>Creates a GameObject with starting Components</summary>
        /// <param name="pGame">A reference to the game this GameObject belongs to (usually you would need to use 'this' here)</param>
        /// <param name="pName">Name of the GameObject</param>
        /// <param name="pPosition">Starting position of the GameObject</param>
        /// <param name="pOrigin">Location of the origin (center/pivot point) in UV coodinates (percentage-like values) Ranges[0,1]</param>
        /// <param name="pRotation">Starting rotation of the GameObject</param>
        /// <param name="pComponents">Starting components of the GameObject</param>
        public GameObject(Game1 pGame, string pName, Vector2 pPosition, Vector2 pOrigin, float pRotation, params Component[] pComponents) : this(pGame, pName, pPosition, pOrigin, pRotation, Vector2.One, pComponents) { }

        //Methods
        //public void CollisionCheck(GameObject pOther)
        //{
        //    bool collision = false;

        //    for (int myColliderComponentIndex = 0; myColliderComponentIndex < _colliderComponents.Count; myColliderComponentIndex++)
        //    {
        //        RectangleCollider myCurrentColliderRectangle = _colliderComponents[myColliderComponentIndex];

        //        RectangleCollider[] otherColliders = pOther.GetComponents<RectangleCollider>();
        //        for (int otherComponentIndex = 0; otherComponentIndex < otherColliders.Length; otherComponentIndex++)
        //        {
        //            if (myCurrentColliderRectangle.CollisionCheck(otherColliders[otherComponentIndex]))
        //            {
        //                collision = true;
        //                InvokeOnCollisionEventMethods(pOther);
        //                break;
        //            }
        //        }

        //        if (collision)
        //            break;
        //    }
        //}

        /// <summary>Checks if there is a collision between two GameObjects by checking all possible collisions between all collideabl components attached to both GameObjects</summary>
        /// <param name="pOther">A reference to the other GameObject to check Collision against</param>
        public void CollisionCheck(GameObject pOther)
        {
            //Loop through all "my" collideable components
            for (int myColliderIndex = 0; myColliderIndex < _collideableComponents.Count; myColliderIndex++)
            {
                //Cache "my" current collider
                ICollideableComponent myCurrentCollider = _collideableComponents[myColliderIndex];

                //Retrieve list of "other's" colliders
                List<ICollideableComponent> otherColliders = pOther._collideableComponents;

                //Loop through all "other's" colliders to see if it colliders with myCurrentCollider
                for (int otherColliderIndex = 0; otherColliderIndex < otherColliders.Count; otherColliderIndex++)
                {
                    //Check if there is a collision between myCurrentCollider and otherCurrentCollider
                    if (myCurrentCollider.CollisionCheck(otherColliders[otherColliderIndex]))
                    {
                        InvokeOnCollisionEventMethods(pOther);
                        return;
                    }
                }
            }
        }

        /// <summary>Invokes the OnCollision event method on all updateable components from both GameObjects (this one + other)</summary>
        /// <param name="pOtherGameObject">A reference to the other GameObject to invoke its OnCollision event methods</param>
        private void InvokeOnCollisionEventMethods(GameObject pOtherGameObject)
        {
            for (int i = 0; i < _updateableComponents.Count; i++)
                _updateableComponents[i].OnCollision(pOtherGameObject);

            List<IUpdateableComponent> otherUpdateableComponents = pOtherGameObject._updateableComponents;
            for (int i = 0; i < otherUpdateableComponents.Count; i++)
                otherUpdateableComponents[i].OnCollision(this);
        }

        /// <summary>Looks for a Component of type T on this GameObject and returns the first instance found</summary>
        /// <typeparam name="T">Type of component to look for</typeparam>
        /// <returns>A reference to found component</returns>
        public T GetComponent<T>() where T : Component
        {
            for (int i = 0; i < _allComponents.Count; i++)
            {
                if (_allComponents[i] is T)
                    return _allComponents[i] as T;
            }

            return null;
        }

        /// <summary>Looks for Components of type T on this GameObject and returns an array of instances found</summary>
        /// <typeparam name="T">Type of components to look for</typeparam>
        /// <returns>Collection of references to found components as an array</returns>
        public T[] GetComponents<T>() where T : Component
        {
            List<T> foundComponents = new List<T>();
            for (int i = 0; i < _allComponents.Count; i++)
            {
                if (_allComponents[i] is T)
                    foundComponents.Add(_allComponents[i] as T);
            }

            return foundComponents.ToArray();
        }

        /// <summary>Adds a new component to this GameObject</summary>
        /// <param name="pComponent">The component reference to add</param>
        public void AddComponent(Component pComponent)
        {
            pComponent.SetOwner(this);
            _allComponents.Add(pComponent);

            if (pComponent is IDrawableComponent)
            {
                IDrawableComponent component = pComponent as IDrawableComponent;
                _drawableComponents.Add(component);
            }

            if (pComponent is IUpdateableComponent updateableComponent)
                _updateableComponents.Add(updateableComponent);

            //if(pComponent is RectangleCollider colliderComponent)
            //    _colliderComponents.Add(colliderComponent);

            if (pComponent is ICollideableComponent collideableComponent)
                _collideableComponents.Add(collideableComponent);
        }

        public void RemoveComponent(Component pComponent)
        {
            if (pComponent is IDrawableComponent drawableComponent)
                _drawableComponents.Remove(drawableComponent);

            if (pComponent is IUpdateableComponent updateableComponent)
                _updateableComponents.Remove(updateableComponent);

            //if (pComponent is RectangleCollider colliderComponent)
            //    _colliderComponents.Remove(colliderComponent);

            if (pComponent is ICollideableComponent collideableComponent)
                _collideableComponents.Remove(collideableComponent);

            _allComponents.Remove(pComponent);
            pComponent.SetOwner(null);
        }
        /// <summary>Awakes all Components attached to this GameObject</summary>
        public void AwakeComponents()
        {
            for (int i = 0; i < _allComponents.Count; i++)
                _allComponents[i].Awake();
        }
        /// <summary>Starts all Components attached to this GameObject</summary>
        public void StartComponents()
        {
            for (int i = 0; i < _allComponents.Count; i++)
                _allComponents[i].Start();
        }
        /// <summary>Updates all Components attached to this GameObject</summary>
        /// <param name="pGameTime">GameTime reference (should be from _game.Update(GameTime))</param>
        public void Update(GameTime pGameTime)
        {
            for (int i = 0; i < _updateableComponents.Count; i++)
                _updateableComponents[i].Update(pGameTime);

        }
        /// <summary>Updates all Components attached to this GameObject a second time after the Update method</summary>
        /// <param name="pGameTime">GameTime reference (should be from _game.Update(GameTime))</param>
        public void LateUpdate(GameTime pGameTime)
        {
            for (int i = 0; i < _updateableComponents.Count; i++)
                _updateableComponents[i].LateUpdate(pGameTime);
        }

        /// <summary>Draws all SpriteRenderer components attached to this GameObject</summary>
        /// <param name="pSpriteBatch">SpriteBatch reference used to draw</param>
        public void Draw(SpriteBatch pSpriteBatch)
        {
            for (int i = 0; i < _drawableComponents.Count; i++)
                _drawableComponents[i].Draw(pSpriteBatch, _transform);
        }

        /// <summary>
        /// Overrides object.ToString() method, this method gets automatically called when this GameObject is printed to the Console.
        /// This method prints the name of this GameObject and all attached Components, useful for debugging purposes
        /// </summary>
        public override string ToString()
        {
            string fullName = $"GameObject: {_name}\n";

            for (int i = 0; i < _allComponents.Count; i++)
            {
                fullName += $"\t{_allComponents[i]}\n";
            }

            return fullName;
        }
    }
}
