using System;
using Microsoft.Xna.Framework;

namespace ComponentDesignPattern.Assignment2
{
    public class Scaler : GameObject
    {
        //Fields
        private readonly float _scaleSpeed;

        private float _time;
        private Vector2 _defaultScale;

        public float ScaleSpeed => _scaleSpeed;

        //Constructors
        public Scaler(string pName, Transform pTransform, SpriteRenderer pRenderer, float pScaleSpeed) : base(pName, pTransform, pRenderer)
        {
            _scaleSpeed = pScaleSpeed;
            _defaultScale = Transform.Scale;

            _scaleSpeed *= MathHelper.TwoPi;
        }

        //Methods
        public override void UpdateGameObject(GameTime pGameTime)
        {
            _time += (float)pGameTime.ElapsedGameTime.TotalSeconds * _scaleSpeed;
            Transform.Scale = _defaultScale * Vector2.One * ((MathF.Sin(_time) + 1) / 2);
        }
    }
}