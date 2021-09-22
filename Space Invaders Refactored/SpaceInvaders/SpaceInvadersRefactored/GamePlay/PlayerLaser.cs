using CoreRefactored.Components;

namespace SpaceInvadersRefactored.GamePlay
{
    public class PlayerLaser : MonoBehaviour
    {
        public override void OnCollision(CoreRefactored.GameObject pOther)
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