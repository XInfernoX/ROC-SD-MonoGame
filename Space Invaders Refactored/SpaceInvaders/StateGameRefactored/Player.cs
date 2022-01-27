using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace StateGameRefactored
{
    public class Player : GameObject
    {
        public Player(Vector2 pPosition, Texture2D pTexture) : base(pPosition, pTexture)
        {

        }

        public override void Update(GameTime pGameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            Vector2 playerInput = Vector2.Zero;
            if (keyboardState.IsKeyDown(Keys.A))
                playerInput.X--;
            if (keyboardState.IsKeyDown(Keys.D))
                playerInput.X++;
            if (keyboardState.IsKeyDown(Keys.W))
                playerInput.Y--;
            if (keyboardState.IsKeyDown(Keys.S))
                playerInput.Y++;

            if (playerInput != Vector2.Zero)
            {
                playerInput.Normalize();
                Vector2 playerTranslation = playerInput * _speed;
                _position += playerTranslation;
            }
        }
    }
}