using Microsoft.Xna.Framework;

namespace ComponentDesignPattern.Assignment5.Interfaces
{
    public interface IUpdateableComponent
    {
        void Update(GameTime pGameTime);

        void LateUpdate(GameTime pGameTime);

        void OnCollision(GameObject pOther);
    }

    public interface ICollideableComponent<T, U>
    {
        U Collider { get; }
        void UpdateCollider();

        bool CollisionCheck(ICollideableComponent<T, U> pOther);
    }
}