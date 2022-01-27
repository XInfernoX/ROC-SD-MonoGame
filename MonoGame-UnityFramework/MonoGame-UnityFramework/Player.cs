using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGame_UnityFramework
{
    public class Player : Component
    {
        private float _movementSpeed;
        private float _rotationSpeed;

        public Player(float pMovementSpeed, float pRotationSpeed)
        {
            _movementSpeed = pMovementSpeed;
            _rotationSpeed = pRotationSpeed;
        }

        public override void Update(GameTime pGameTime)
        {
            Vector2 movementInput = GetMovementInput();
            float rotationInput = GetRotationInput();

            transform.Translate( movementInput * _movementSpeed, Space.Self);
            transform.Rotate(rotationInput * _rotationSpeed);
        }

        private Vector2 GetMovementInput()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            Vector2 input = Vector2.Zero;

            if (keyboardState.IsKeyDown(Keys.Up))
                input.Y--;
            if (keyboardState.IsKeyDown(Keys.Down))
                input.Y++;
     
            return input;
        }

        private float GetRotationInput()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            float rotationChange = 0;

            //Rotation
            if (keyboardState.IsKeyDown(Keys.Left))
                rotationChange--;
            if (keyboardState.IsKeyDown(Keys.Right))
                rotationChange++;

            return rotationChange;
        }
    }
}
