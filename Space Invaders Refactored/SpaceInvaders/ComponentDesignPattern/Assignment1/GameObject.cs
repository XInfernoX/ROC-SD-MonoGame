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
        private SpriteRenderer _spriteRenderer;

        //Properties
        protected Transform Transform => _transform;
        protected SpriteRenderer SpriteRenderer => _spriteRenderer;

        //Constructor
        public GameObject(string pName, Transform pTransform, SpriteRenderer pRenderer)
        {
            _name = pName;
            _transform = pTransform;
            _spriteRenderer = pRenderer;
        }

        public virtual void UpdateGameObject(GameTime pGameTime)
        {
            //To be overridden in child class
        }

        public virtual void DrawGameObject(SpriteBatch pSpriteBatch)
        {
            _spriteRenderer.Draw(_transform, pSpriteBatch);
        }
    }
}
