using System;
using Microsoft.Xna.Framework;

namespace ComponentDesignPattern.Assignment2
{
    public class OscillatorObject : GameObject
    {
        private readonly float _speed = 10;
        private readonly float _amplitude = 1;

        private readonly Vector2 _oscillateMidPoint;
        private float _time;

        public OscillatorObject(string pName, Transform pTransform, SpriteRenderer pRenderer, float pSpeed, float pAmplitude) : base(pName, pTransform, pRenderer)
        {
            _speed = pSpeed;
            _amplitude = pAmplitude;

            _oscillateMidPoint = Transform.Position;
        }

        public override void UpdateGameObject(GameTime pGameTime)
        {
            _time += (float)(pGameTime.ElapsedGameTime.TotalSeconds * _speed);

            Vector2 oscillateVector = new Vector2
            {
                Y = MathF.Sin(_time * MathHelper.TwoPi)
            };

            Transform.Position = _oscillateMidPoint + (oscillateVector * _amplitude);
        }
    }
}