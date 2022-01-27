using System;
using Microsoft.Xna.Framework;

namespace ComponentDesignPattern.Assignment2
{
    public class Oscillator : MonoBehaviour
    {
        private readonly float _speed = 10;
        private readonly float _amplitude = 1;

        private Vector2 _oscillateMidPoint;
        private float _time;

        public Oscillator(float pSpeed, float pAmplitude)
        {
            _speed = pSpeed;
            _amplitude = pAmplitude;

        }

        public override void Start()
        {
            _oscillateMidPoint = Transform.Position;
        }

        public override void UpdateMonoBehaviour(GameTime pGameTime)
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