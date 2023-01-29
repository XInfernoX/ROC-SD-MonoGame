using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CSharpAdvanced.CSharpExpert.ComponentDesignPattern.Assignment3
{
    public class MegaMan : MonoBehaviour
    {
        public override void OnCollision(GameObject pOther)
        {
            Console.WriteLine($"MegaMan collides with: {pOther.Name}");
        }

        public override void Update(GameTime pGameTime)
        {
            KeyboardState state = Keyboard.GetState();

            Vector2 input = Vector2.Zero;

            if (state.IsKeyDown(Keys.Up))
            {
                input.Y--;
            }
            if (state.IsKeyDown(Keys.Down))
            {
                input.Y++;
            }
            if (state.IsKeyDown(Keys.Left))
            {
                input.X--;
            }
            if (state.IsKeyDown(Keys.Right))
            {
                input.X++;
            }

            if (input != Vector2.Zero)
            {
                input.Normalize();

                Transform.Position += input * 5;
            }
        }
    }
}