namespace CSharpExpert.ComponentDesignPattern.Assignment3.Interfaces
{
    public interface ICollideableComponent
    {
        void UpdateCollider();

        bool CollisionCheck(ICollideableComponent pOther);

        bool CollisionCheck(RectangleCollider pOther);

        bool CollisionCheck(SphereCollider pOther);

    }
}