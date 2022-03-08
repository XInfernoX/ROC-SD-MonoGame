using System;
using Microsoft.Xna.Framework;

namespace ComponentDesignPattern.Assignment4
{
    public class Scaler : MonoBehaviour
    {
        private readonly float _scaleSpeed;

        private float _time;
        private Vector2 _defaultScale;

        public float ScaleSpeed => _scaleSpeed;

        public Scaler(float pScaleSpeed)
        {
            _scaleSpeed = pScaleSpeed;
            _scaleSpeed *= MathHelper.TwoPi;
        }

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