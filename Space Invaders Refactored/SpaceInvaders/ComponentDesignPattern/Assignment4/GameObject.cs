using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ComponentDesignPattern.Assignment4
{
    public class GameObject
    {
        //Fields
        private string _name;

        private Transform _transform;

        private DrawableMonoBehaviour _renderer;

        private List<MonoBehaviour> _components = new List<MonoBehaviour>();

        //Properties
        public string Name => _name;
        public Transform Transform => _transform;

        protected DrawableMonoBehaviour Renderer => _renderer;//Temporarily

        //Constructor
        public GameObject(string pName, Transform pTransform, DrawableMonoBehaviour pRenderer, params MonoBehaviour[] pComponents)
        {
            _name = pName;
            _transform = pTransform;
            _renderer = pRenderer;

            _components.Add(pTransform);
            _components.Add(_renderer);

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
            _renderer.Draw(pSpriteBatch, _transform);
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
