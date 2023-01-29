using Microsoft.Xna.Framework;

namespace CSharpAdvanced.CSharpExpert.ComponentDesignPattern.Assignment3.Interfaces
{
    public interface IUpdateableComponent
    {
        void Update(GameTime pGameTime);

        void LateUpdate(GameTime pGameTime);

        void OnCollision(GameObject pOther);
    }
}