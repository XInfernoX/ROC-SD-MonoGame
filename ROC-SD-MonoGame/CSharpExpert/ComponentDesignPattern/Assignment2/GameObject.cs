using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CSharpAdvanced.CSharpExpert.ComponentDesignPattern.Assignment2
{
    public class GameObject
    {
        //Fields
        private string _name;
        private Transform _transform;
        private List<MonoBehaviour> _components = new List<MonoBehaviour>();

        //Properties
        public string Name => _name;
        public Transform Transform => _transform;


        #region Not For Students to see >:E

        //Temporarily
        private SpriteRenderer _spriteRenderer;
        protected SpriteRenderer SpriteRenderer => _spriteRenderer;


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
        #endregion

        public T GetComponent<T>() where T : MonoBehaviour
        {
            for (int i = 0; i < _components.Count; i++)
            {
                if (_components[i] is T behaviour)
                    return behaviour;
            }

            return null;
        }

        public T[] GetComponents<T>() where T : MonoBehaviour
        {
            List<T> foundComponents = new List<T>();
            for (int i = 0; i < _components.Count; i++)
            {
                if (_components[i] is T type)
                    foundComponents.Add(type);
            }

            return foundComponents.ToArray();
        }
    }
}
