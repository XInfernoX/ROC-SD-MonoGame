using SpaceInvaders.Refactor.Core;
using SpaceInvaders.Refactor.Core.Components;

namespace SpaceInvaders.Refactor
{
    public class OneHitDeath : MonoBehaviour
    {
        public override void OnCollision(GameObject pOther)
        {
            Destroy(gameObject);
        }
    }

    //Test
    public class OnHitDestroy<T> : MonoBehaviour where T : Component
    {
        public override void OnCollision(GameObject pOther)
        {
            T component = pOther.GetComponent<T>();

            if (component != null)
            {
                Destroy(pOther);
                Destroy(gameObject);
            }
        }
    }
}

