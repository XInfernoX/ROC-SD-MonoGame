using Microsoft.Xna.Framework;
using CSharpAdvanced.CSharpExpert.ComponentDesignPattern.Assignment3.Interfaces;

namespace CSharpAdvanced.CSharpExpert.ComponentDesignPattern.Assignment3
{
    public class SphereCollider : Component, ICollideableComponent
    {
        //Fields
        private readonly IDrawableComponent _drawableComponent;
        private float _radius;
        private Vector2 _sphereCenter;

        //Constructors
        public SphereCollider(IDrawableComponent pDrawableComponent)
        {
            _drawableComponent = pDrawableComponent;
        }

        //Properties
        public float Radius => _radius;
        public Vector2 SphereCenter => _sphereCenter;

        //Methods
        public void UpdateCollider()
        {
            //Calculate the actual size of the image
            Vector2 actualSize = Transform.Scale * _drawableComponent.Size;

            //Calculates the "average" radius when not uniformally scaled.
            _radius = (actualSize.X + actualSize.Y) / 4;

            /* Calculates the origin offset depending on the origin of the transform
            Transform.Origin of [0,0] is at topleft       needs to offset with [0.5f, 0.5f]   to get center position
            Transform.Origin of [1,1] is at bottomRight   needs to offset with [-0.5f, -0.5f] to get center position*/
            Vector2 centerOriginOffset = Vector2.One - Transform.Origin - Vector2.One / 2;

            //Calculates the center position of the sphere by using the centerOriginOffset
            _sphereCenter = Transform.Position + centerOriginOffset * actualSize;
        }

        public bool CollisionCheck(ICollideableComponent pOther)
        {
            //Double dispatch
            return pOther.CollisionCheck(this);
        }

        public bool CollisionCheck(RectangleCollider pOther)
        {
            //Sphere->Rectangle or Rectangle->Sphere collision checks should not matter
            //Reusing the implementation in the RectangleCollider class to avoid code duplication
            return pOther.CollisionCheck(this);
        }

        public bool CollisionCheck(SphereCollider pOther)
        {
            //Makes sure all colliders are up to date right before the collision check
            UpdateCollider();
            pOther.UpdateCollider();

            //Calculates the vector difference between both center points
            Vector2 difference = pOther.SphereCenter - _sphereCenter;

            //Calculates the sum of both radii 
            float jointRadius = pOther.Radius + _radius;

            //If the difference is smaller than sum of both radii, there is a collision
            return difference.Length() < jointRadius;
        }
    }
}