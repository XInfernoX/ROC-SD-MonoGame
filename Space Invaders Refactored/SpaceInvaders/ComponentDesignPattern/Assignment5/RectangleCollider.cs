using Microsoft.Xna.Framework;

using ComponentDesignPattern.Assignment5.Interfaces;

namespace ComponentDesignPattern.Assignment5
{
    public class RectangleCollider : Component, ICollideableComponent
    {
        //Fields
        private readonly IDrawableComponent _drawableComponent;
        private Rectangle _collider;//Contains Position and Size

        //Constructor
        public RectangleCollider(IDrawableComponent pDrawableComponent)
        {
            //NOTE cannot call transform property, since Component has not been added to GameObject yet
            _drawableComponent = pDrawableComponent;
            _collider = Rectangle.Empty;
        }

        //Methods
        public void UpdateCollider()
        {
            Vector2 size = Transform.Scale * _drawableComponent.Size;
            Vector2 position = Transform.Position - Transform.Origin * size;

            _collider = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
        }

        public bool CollisionCheck(ICollideableComponent pOther)
        {
            //Double dispatch
            return pOther.CollisionCheck(this);
        }

        public bool CollisionCheck(RectangleCollider pOther)
        {
            //Makes sure all colliders are up to date right before the collision check
            UpdateCollider();
            pOther.UpdateCollider();

            //If both rectangles intersect with each other, there is a collision
            return _collider.Intersects(pOther._collider);
        }

        public bool CollisionCheck(SphereCollider pOther)
        {
            //NOTE this collisionCheck is not accurate, threat it as an approximation
            //It treats a rectangle as a sphere because actual rectangle sphere collision involves pretty difficult math and
            //I have no time to implement it correctly

            //Makes sure all colliders are up to date right before the collision check
            UpdateCollider();
            pOther.UpdateCollider();

            //Take the Rectangle's center as a Vector2
            Vector2 rectangleCenter = _collider.Center.ToVector2();

            //Calculates the "average" radius when not uniformally scaled.
            float radius = _collider.Width + _collider.Height / 4;

            //Calculates the vector difference between both center points
            Vector2 difference = pOther.SphereCenter - rectangleCenter;

            //Calculates the sum of both radii
            float jointRadius = pOther.Radius + radius;

            //If the difference is smaller than sum of both radii, there is a collision
            return difference.Length() < jointRadius;
        }
    }
}