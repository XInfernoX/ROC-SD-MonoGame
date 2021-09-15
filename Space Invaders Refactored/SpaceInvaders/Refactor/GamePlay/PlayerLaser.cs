using SpaceInvaders.Refactor.Core;
using SpaceInvaders.Refactor.Core.Components;

namespace SpaceInvaders.Refactor.GamePlay
{
    public class PlayerLaser : MonoBehaviour
    {
        public override void OnCollision(GameObject pOther)
        {
            Alien alien = pOther.GetComponent<Alien>();

            if (alien != null)
            {
                Destroy(pOther);
                Destroy(gameObject);
            }
        }
    }
}