using System;
using Microsoft.Xna.Framework;

namespace ComponentDesignPattern.Assignment3
{
    public class OscillatorObject : GameObject
    {
        //Fields - configurable
        private readonly float _speed = 10;
        private readonly float _amplitude = 1;

        //Fields - internal
        private readonly Vector2 _oscillateMidPoint;
        private float _time;

        //Constructors
        public OscillatorObject(string pName, Transform pTransform, SpriteRenderer pRenderer, float pSpeed, float pAmplitude) : base(pName, pTransform, pRenderer)
        {
            _speed = pSpeed;
            _amplitude = pAmplitude;

            _oscillateMidPoint = Transform.Position;
        }

        //Methods
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