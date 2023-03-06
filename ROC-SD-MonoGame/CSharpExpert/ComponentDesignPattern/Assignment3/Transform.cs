using System;
using Microsoft.Xna.Framework;

namespace CSharpExpert.ComponentDesignPattern.Assignment3
{
    //Component that keeps track of transformation data. It determines how a GameObject is placed in a 2D world
    public class Transform : Component
    {
        //Fields
        private Vector2 _position;
        private float _rotation;
        private Vector2 _scale;
        private Vector2 _origin;

        //Properties
        public Vector2 Position
        {
            get => _position;
            set => _position = value;
        }

        public float Rotation
        {
            get => _rotation;
            set => _rotation = value;
        }

        public Vector2 Scale
        {
            get => _scale;
            set => _scale = value;
        }

        public Vector2 Origin
        {
            get => _origin;
            set => _origin = value;
        }

        //Constructor
        public Transform(Vector2 pPosition, Vector2 pOrigin, float pRotation, Vector2 pScale)
        {
            _position = pPosition;
            _rotation = pRotation;
            _scale = pScale;
            _origin = pOrigin;
        }

        public Transform(Vector2 pPosition) : this(pPosition, new Vector2(0.5f, 0.5f), 0, Vector2.One) { }
        public Transform(Vector2 pPosition, Vector2 pOrigin) : this(pPosition, pOrigin, 0, Vector2.One) { }
        public Transform(Vector2 pPosition, Vector2 pOrigin, float pRotation) : this(pPosition, pOrigin, pRotation, Vector2.One) { }

        //Methods
        public void Translate(Vector2 pPosition)
        {
            _position += pPosition;
        }

        public void LookAt(Transform pTarget)
        {
            Vector2 direction = Vector2.Normalize(pTarget._position - _position);

            _rotation = MathHelper.ToDegrees(MathF.Atan2(direction.Y, direction.X));
        }
    }
}