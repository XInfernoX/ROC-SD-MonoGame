namespace ComponentDesignPattern.Assignment5.Interfaces
{
    public interface ICollideableComponent
    {
        void UpdateCollider();

        bool CollisionCheck(ICollideableComponent pOther);

        bool CollisionCheck(RectangleCollider pOther);

        bool CollisionCheck(SphereCollider pOther);
    }
}