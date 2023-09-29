using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CSharpExpert.ComponentDesignPattern.Assignment1
{
    public class GameObject
    {
        //Fields
        private string _name;

        private Transform _transform;
        private SpriteRenderer _spriteRenderer;

        //Properties
        public string Name => _name;
        public Transform Transform => _transform;

        protected SpriteRenderer SpriteRenderer => _spriteRenderer;//Temporarily

        //Constructor
        public GameObject(string pName, Transform pTransform, SpriteRenderer pRenderer)
        {
            _name = pName;
            _transform = pTransform;
            _spriteRenderer = pRenderer;
        }

        public virtual void UpdateGameObject(GameTime pGameTime)
        {
            //To be overridden in child classes
        }

        public virtual void DrawGameObject(SpriteBatch pSpriteBatch)
        {
            _spriteRenderer.Draw(pSpriteBatch, _transform);
        }
    }
}
