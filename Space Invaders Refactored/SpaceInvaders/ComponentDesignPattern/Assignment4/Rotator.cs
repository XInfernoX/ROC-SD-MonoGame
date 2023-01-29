using Microsoft.Xna.Framework;

namespace ComponentDesignPattern.Assignment4
{
    public class Rotator : MonoBehaviour
    {
        private float _rotationSpeed;

        public Rotator(float pRotationSpeed)
        {
            _rotationSpeed = pRotationSpeed;
        }

        public override void UpdateMonoBehaviour(GameTime pGameTime)
        {
            Transform.Rotation += _rotationSpeed;
        }
    }
}