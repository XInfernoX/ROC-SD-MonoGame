using System;

namespace SpaceInvaders.Refactor
{
    public class Player : MonoBehaviour
    {
        public override void OnCollision(GameObject pOther)
        {
            Console.WriteLine("Player has been hit!");
            Destroy(gameObject);
            Console.WriteLine("Game over!");
        }

        public override string ToString()
        {
            return $"Player component of: {gameObject}";
        }
    }
}
