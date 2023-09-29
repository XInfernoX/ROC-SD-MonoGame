using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CSharpExpert.ComponentDesignPattern.Assignment1
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
        public float ScaleFactor => _scaleFactor;

        //Constructors
        public ScalerObject(string pName, Transform pTransform, SpriteRenderer pRenderer, float pScaleSpeed = 1, float pScaleFactor = 1) : base(pName, pTransform, pRenderer)
        {
            _scaleSpeed = pScaleSpeed;
            _scaleFactor = pScaleFactor;
            
            _defaultScale = Transform.Scale;
        }

        //Methods
        public override void UpdateGameObject(GameTime pGameTime)
        {
            _time += (float)pGameTime.ElapsedGameTime.TotalSeconds * MathHelper.TwoPi;
            Vector2 currentScale = ((MathF.Sin(_time) + 1) * 0.5f) * _scaleFactor * Vector2.One;
            Transform.Scale = _defaultScale * currentScale;
        }
    }
}