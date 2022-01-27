using Microsoft.Xna.Framework;

namespace ComponentDesignPattern.Assignment2
{
    public class RotatorObject : GameObject
    {
        private float _rotationSpeed;

        public RotatorObject(string pName, Transform pTransform, SpriteRenderer pRenderer, float pRotationSpeed) : base(pName, pTransform, pRenderer)
        {
            _rotationSpeed = pRotationSpeed;
        }

        public override void UpdateGameObject(GameTime pGameTime)
        {
            Transform.Rotation += _rotationSpeed;
        }
    }
}