using ComponentDesignPattern.Assignment5.Interfaces;
using Microsoft.Xna.Framework;

namespace ComponentDesignPattern.Assignment5
{
    public class ColliderRectangle : Component, ICollideableComponent<ColliderRectangle, Rectangle>
    {
        //Fields
        private SpriteRenderer _spriteRenderer;
        private Rectangle _collider;

        //Constructor
        public ColliderRectangle(SpriteRenderer pSpriteRenderer)
        {
            //NOTE cannot call transform property, since Component has not been added to GameObject yet
            _spriteRenderer = pSpriteRenderer;
            _collider = Rectangle.Empty;
        }

        //public ColliderRectangle Collider => _collider;

        public Rectangle Collider => _collider;

        public void UpdateCollider()
        {
            Vector2 size = Transform.Scale * _spriteRenderer.Size;
            Vector2 position = Transform.Position - Transform.Origin * size;

            _collider = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
        }

        public bool CollisionCheck(ColliderRectangle pOther)
        {
            UpdateCollider();
            pOther.UpdateCollider();



            return _collider.Intersects(pOther._collider);
        }
    }
}