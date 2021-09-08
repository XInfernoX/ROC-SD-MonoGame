using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SpaceInvaders.Refactor
{
    public class Collider : Component
    {
        //Events
        public Action<Collider> OnCollision = delegate { };

        //Fields
        private Point _size;
        private Rectangle _collider;

        //Constructor
        public Collider(SpriteRenderer pSpriteRenderer)
        {
            _size = new Point(pSpriteRenderer.Width, pSpriteRenderer.Height);

            UpdateColliderPosition();
        }
        public Collider(Point pSize)
        {
            _size = pSize;
        }

        //Copy Constructor-ish
        public override Component Copy()
        {
            return new Collider(_size);
        }

        private void UpdateColliderPosition()
        {
            Vector2 position = transform.Position;
            _collider = new Rectangle(_size.X, _size.Y, (int)position.X, (int)position.Y);
        }

        public void CollisionCheck(Collider pOther)
        {
            UpdateColliderPosition();

            if (_collider.Contains(pOther._collider))
            {
                OnCollision(pOther);
            }
        }
    }
}
