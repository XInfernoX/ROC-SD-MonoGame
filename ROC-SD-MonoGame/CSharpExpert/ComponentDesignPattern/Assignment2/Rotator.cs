using Microsoft.Xna.Framework;

namespace CSharpExpert.ComponentDesignPattern.Assignment2
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
        public override void UpdateMonoBehaviour(GameTime pGameTime)
        {
            Transform.Rotation += _rotationSpeed;
        }
    }
}