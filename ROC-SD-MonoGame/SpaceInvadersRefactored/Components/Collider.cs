using System;
using Microsoft.Xna.Framework;

using SpaceInvadersRefactored.Interfaces;

namespace SpaceInvadersRefactored.Components
{
    public class Collider : Component, ICollideable
    {
        //Events
        public Action<Collider> OnCollision = delegate { };

        //Fields
        private SpriteRenderer _spriteRenderer;
        private Rectangle _collider;

        //Constructor
        public Collider(SpriteRenderer pSpriteRenderer)
        {
            //NOTE cannot call transform property, since Component has not been added to GameObject yet
            _spriteRenderer = pSpriteRenderer;
            _collider = Rectangle.Empty;
        }

        //Copy Constructor-ish
        //public override Component Copy()
        //{
        //    return new Collider(_size);
        //}

        private void UpdateCollider()
        {
            Vector2 size = transform.Scale * _spriteRenderer.Size;
            Vector2 position = transform.Position - transform.Origin * size;

            _collider = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
        }

        public bool CollisionCheck(Collider pOther)
        {
            UpdateCollider();
            pOther.UpdateCollider();

            if(_collider.Intersects(pOther._collider))
            {
                OnCollision(pOther);
                return true;
            }

            return false;
        }

        public bool OverLapCheck(Point pPoint)
        {
            UpdateCollider();

            return _collider.Contains(pPoint);
        }

        public override string ToString()
        {
            return $"Collider";
        }
    }
}
