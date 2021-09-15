
using Microsoft.Xna.Framework;

namespace SpaceInvaders.Refactor
{
    public class Rotator : MonoBehaviour
    {
        //Fields
        private float _rotationSpeed;

        //Constructor
        public Rotator(float pRotationSpeed)
        {
            _rotationSpeed = pRotationSpeed;
        }

        //Methods
        public override void Update(GameTime pGameTime)
        {
            transform.Rotate((float)pGameTime.ElapsedGameTime.TotalSeconds * _rotationSpeed);
        }
    }
}
