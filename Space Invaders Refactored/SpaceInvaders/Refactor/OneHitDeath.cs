namespace SpaceInvaders.Refactor
{
    public class OneHitDeath : MonoBehaviour
    {
        public override void OnCollision()
        {
            Destroy(gameObject);
        }
    }
}

