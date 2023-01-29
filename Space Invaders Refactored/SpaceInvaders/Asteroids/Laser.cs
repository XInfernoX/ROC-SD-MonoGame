using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids
{
    public class Laser : GameObject
    {
        private float _speed;
        private Vector2 _direction;

        public Laser(Texture2D pTexture, Vector2 pPosition, float pSpeed, float pRotation) : base(pPosition, pTexture)
        {
            _speed = pSpeed;
            _rotation = pRotation;

            _direction = DegreesToVector(_rotation);
        }

        public override void Update(GameTime pTime)
        {
            _position += (float)pTime.ElapsedGameTime.TotalSeconds * _speed * _direction;
        }
    }
}
