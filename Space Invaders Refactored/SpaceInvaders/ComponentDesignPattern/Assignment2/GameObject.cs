using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ComponentDesignPattern.Assignment2
{
    public class GameObject
    {
        //Fields
        private string _name;

        private Transform _transform;
        private SpriteRenderer _spriteRenderer;

        private List<MonoBehaviour> _components = new List<MonoBehaviour>();

        //Properties
        public Transform Transform => _transform;

        protected SpriteRenderer SpriteRenderer => _spriteRenderer;//Temporarily

        //Constructor
        public GameObject(string pName, Transform pTransform, SpriteRenderer pRenderer, params MonoBehaviour[] pComponents)
        {
            _name = pName;
            _transform = pTransform;
            _spriteRenderer = pRenderer;

            _components.Add(pTransform);
            _components.Add(pRenderer);

            _components.AddRange(pComponents);

            for (int i = 0; i < pComponents.Length; i++)
            {
                pComponents[i].SetOwner(this);
            }
        }


        public void AwakeComponents()
        {
            for (int i = 0; i < _components.Count; i++)
            {
                _components[i].Awake();
            }
        }
        public void StartComponents()
        {
            for (int i = 0; i < _components.Count; i++)
            {
                _components[i].Start();
            }
        }

        public virtual void UpdateGameObject(GameTime pGameTime)
        {
            for (int i = 0; i < _components.Count; i++)
            {
                _components[i].UpdateMonoBehaviour(pGameTime);
            }
        }

        public virtual void DrawGameObject(SpriteBatch pSpriteBatch)
        {
            _spriteRenderer.Draw(_transform, pSpriteBatch);
        }

        public void AddComponent(MonoBehaviour pBehaviour)
        {
            _components.Add(pBehaviour);
            pBehaviour.SetOwner(this);
        }
        public void RemoveComponent(MonoBehaviour pBehaviour)
        {
            _components.Remove(pBehaviour);
            pBehaviour.SetOwner(null);
        }

        public T GetComponent<T>() where T : MonoBehaviour
        {
            for (int i = 0; i < _components.Count; i++)
            {
                if (_components[i] is T)
                {
                    Console.WriteLine("Found " + typeof(T));
                    return _components[i] as T;
                }
            }

            return null;
        }

        public T[] GetComponents<T>() where T : MonoBehaviour
        {
            List<T> foundComponents = new List<T>();
            for (int i = 0; i < _components.Count; i++)
            {
                if (_components[i] is T)
                    foundComponents.Add(_components[i] as T);
            }

            return foundComponents.ToArray();
        }
    }
}
