using Microsoft.Xna.Framework;

namespace CoreRefactored.Interfaces
{
    public interface IUpdateable
    {
        void Update(GameTime pGameTime);
        void LateUpdate(GameTime pGameTime);
    }
}