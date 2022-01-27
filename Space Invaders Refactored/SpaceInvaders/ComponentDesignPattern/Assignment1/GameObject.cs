using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ComponentDesignPattern.Assignment1
{
    public class GameObject
    {
        //Fields
        private string _name;

        private Transform _transform;
        private Component _spriteRenderer;

        private List<Component> _components = new List<Component>();

        //Properties
        protected Transform Transform => _transform;
        protected Component SpriteRenderer => _spriteRenderer;

        //Constructor
        public GameObject(string pName, Transform pTransform, Component pRenderer)
        {
            _name = pName;
            _transform = pTransform;
            _spriteRenderer = pRenderer;

            _components.Add(pTransform);
            _components.Add(pRenderer);
        }

        public virtual void UpdateGameObject(GameTime pGameTime)
        {
            //To be overridden in child class

            for (int i = 0; i < _components.Count; i++)
            {
                _components[i].Update(pGameTime);
            }
        }

        public virtual void DrawGameObject(SpriteBatch pSpriteBatch)
        {
            _spriteRenderer.Draw(_transform, pSpriteBatch);

            for (int i = 0; i < _components.Count; i++)
            {
                _components[i].Draw(_transform, pSpriteBatch);
            }
        }
    }
}
