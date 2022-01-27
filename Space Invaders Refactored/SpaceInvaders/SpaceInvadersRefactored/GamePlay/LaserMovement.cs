using CoreRefactored.Components;
using Microsoft.Xna.Framework;

namespace SpaceInvadersRefactored.GamePlay
{
    public class LaserMovement : MonoBehaviour
    {
        private readonly Vector2 _direction;
        private readonly float _speed;

        public LaserMovement(Vector2 pDirection, float pSpeed)
        {
            _direction = pDirection;
            _speed = pSpeed;
        }

        public override void Update(GameTime pGameTime)
        {
            Vector2 translation = _speed * (float)pGameTime.ElapsedGameTime.TotalSeconds * _direction;
            transform.Translate(translation);
        }

        //public override Component Copy()
        //{
        //    return new LaserMovement(_direction, _speed);
        //}
    }
}
