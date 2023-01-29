using System;
using Microsoft.Xna.Framework;

namespace CSharpAdvanced.CSharpExpert.ComponentDesignPattern.Assignment1
{
    public class ScalerObject : GameObject
    {
        //Fields
        private readonly float _scaleSpeed;
        private readonly float _scaleFactor;

        private float _time;
        private Vector2 _defaultScale;

        //Properties
        public float ScaleSpeed => _scaleSpeed;

        //Constructors
        public ScalerObject(string pName, Transform pTransform, SpriteRenderer pRenderer, float pScaleSpeed) : base(pName, pTransform, pRenderer)
        {
            _scaleSpeed = pScaleSpeed;
            _scaleFactor = pScaleSpeed * MathHelper.TwoPi;
            
            _defaultScale = Transform.Scale;
        }

        //Methods
        public override void UpdateGameObject(GameTime pGameTime)
        {
            _time += (float)pGameTime.ElapsedGameTime.TotalSeconds * _scaleFactor;
            Transform.Scale = _defaultScale * Vector2.One * ((MathF.Sin(_time) + 1) / 2);
        }
    }
}