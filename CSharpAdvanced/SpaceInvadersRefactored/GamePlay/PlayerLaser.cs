using SpaceInvadersRefactored.Components;

namespace SpaceInvadersRefactored.GamePlay
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