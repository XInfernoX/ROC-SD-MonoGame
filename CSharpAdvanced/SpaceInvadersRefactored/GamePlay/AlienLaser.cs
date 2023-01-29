using SpaceInvadersRefactored.Components;

namespace SpaceInvadersRefactored.GamePlay
{
    public class AlienLaser : MonoBehaviour
    {
        public override void OnCollision(GameObject pOther)
        {
            Player player = pOther.GetComponent<Player>();

            if (player != null)
            {
                Destroy(pOther); 
                Destroy(gameObject);
            }
        }
    }
}