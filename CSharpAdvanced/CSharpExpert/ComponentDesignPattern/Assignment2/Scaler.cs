using System;
using Microsoft.Xna.Framework;

namespace CSharpAdvanced.CSharpExpert.ComponentDesignPattern.Assignment2
{
    public class Scaler : MonoBehaviour
    {
        //Fields
        private readonly float _scaleSpeed;
        private readonly float _scaleFactor;

        private float _time;
        private Vector2 _defaultScale;

        //Properties
        public float ScaleSpeed => _scaleSpeed;

        //Constructors
        public Scaler(float pScaleSpeed)
        {
            _scaleSpeed = pScaleSpeed;
            _scaleFactor = pScaleSpeed * MathHelper.TwoPi;
        }

        //Methods
        public override void Awake()
        {
            _defaultScale = Transform.Scale;
        }

        public override void UpdateMonoBehaviour(GameTime pGameTime)
        {
            _time += (float)pGameTime.ElapsedGameTime.TotalSeconds * _scaleSpeed;
            Transform.Scale = _defaultScale * Vector2.One * ((MathF.Sin(_time) + 1) / 2);
        }
    }
}