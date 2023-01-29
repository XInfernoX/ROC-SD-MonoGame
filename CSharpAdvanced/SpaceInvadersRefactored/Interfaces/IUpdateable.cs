using Microsoft.Xna.Framework;

namespace SpaceInvadersRefactored.Interfaces
{
    public interface IUpdateable
    {
        void Update(GameTime pGameTime);
        void LateUpdate(GameTime pGameTime);
    }
}