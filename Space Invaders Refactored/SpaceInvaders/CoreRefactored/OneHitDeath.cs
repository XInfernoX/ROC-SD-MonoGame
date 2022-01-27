﻿using CoreRefactored.Components;

namespace CoreRefactored
{
    public class OneHitDeath : MonoBehaviour
    {
        public override void OnCollision(CoreRefactored.GameObject pOther)
        {
            Destroy(gameObject);
        }
    }

    //Test
    public class OnHitDestroy<T> : MonoBehaviour where T : Component
    {
        public override void OnCollision(CoreRefactored.GameObject pOther)
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

