using Microsoft.Xna.Framework;

namespace ComponentDesignPattern.Assignment3
{
    public class RotatorObject : GameObject
    {
        //Fields - configurable
        private readonly float _rotationSpeed;

        //Constructor
        public RotatorObject(string pName, Transform pTransform, SpriteRenderer pRenderer, float pRotationSpeed) : base(pName, pTransform, pRenderer)
        {
            _rotationSpeed = pRotationSpeed;
        }

        //Methods
        public override void UpdateGameObject(GameTime pGameTime)
        {
            Transform.Rotation += _rotationSpeed;
        }
    }
}