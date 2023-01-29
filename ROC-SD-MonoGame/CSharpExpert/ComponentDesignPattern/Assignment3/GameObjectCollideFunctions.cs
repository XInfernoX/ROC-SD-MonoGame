///// <summary>Checks if there is a collision between two GameObjects by checking all possible collisions between all collideabl components attached to both GameObjects</summary>
///// <param name="pOther">A reference to the other GameObject to check Collision against</param>
//public void CollisionCheck(GameObject pOther)
//{
//    //Loop through all "my" collideable components
//    for (int myColliderIndex = 0; myColliderIndex < _collideableComponents.Count; myColliderIndex++)
//    {
//        //Cache "my" current collider
//        ICollideableComponent myCurrentCollider = _collideableComponents[myColliderIndex];

//        //Retrieve list of "other's" colliders
//        List<ICollideableComponent> otherColliders = pOther._collideableComponents;

//        //Loop through all "other's" colliders to see if it colliders with myCurrentCollider
//        for (int otherColliderIndex = 0; otherColliderIndex < otherColliders.Count; otherColliderIndex++)
//        {
//            //Check if there is a collision between myCurrentCollider and otherCurrentCollider
//            if (myCurrentCollider.CollisionCheck(otherColliders[otherColliderIndex]))
//            {
//                InvokeOnCollisionEventMethods(pOther);
//                return;
//            }
//        }
//    }
//}

///// <summary>Invokes the OnCollision event method on all updateable components from both GameObjects (this one + other)</summary>
///// <param name="pOtherGameObject">A reference to the other GameObject to invoke its OnCollision event methods</param>
//private void InvokeOnCollisionEventMethods(GameObject pOtherGameObject)
//{
//    for (int i = 0; i < _updateableComponents.Count; i++)
//        _updateableComponents[i].OnCollision(pOtherGameObject);

//    List<IUpdateableComponent> otherUpdateableComponents = pOtherGameObject._updateableComponents;
//    for (int i = 0; i < otherUpdateableComponents.Count; i++)
//        otherUpdateableComponents[i].OnCollision(this);
//}
