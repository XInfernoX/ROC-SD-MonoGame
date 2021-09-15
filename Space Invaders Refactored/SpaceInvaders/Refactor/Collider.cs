﻿using System;
using Microsoft.Xna.Framework;

namespace SpaceInvaders.Refactor
{
    public class Collider : Component
    {
        //Events
        public Action<Collider> OnCollision = delegate { };

        //Fields
        private Rectangle _collider;

        //Constructor
        public Collider(SpriteRenderer pSpriteRenderer)
        {
            //NOTE cannot call transform property, since Component has not been added to GameObject yet
            _collider = new Rectangle(0, 0, pSpriteRenderer.Width, pSpriteRenderer.Height);
        }

        //Copy Constructor-ish
        //public override Component Copy()
        //{
        //    return new Collider(_size);
        //}

        private void UpdateColliderPosition()
        {
            Vector2 position = transform.Position;

            _collider.X = (int)position.X;
            _collider.Y = (int)position.Y;
        }

        public bool CollisionCheck(Collider pOther)
        {
            UpdateColliderPosition();
            pOther.UpdateColliderPosition();

            if(_collider.Intersects(pOther._collider))
            {
                OnCollision(pOther);
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return $"Collider of: {gameObject}";
        }
    }
}
