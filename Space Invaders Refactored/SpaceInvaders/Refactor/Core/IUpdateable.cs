using Microsoft.Xna.Framework;

namespace SpaceInvaders.Refactor.Core
{
    public interface IUpdateable
    {
        void Update(GameTime pGameTime);
        void LateUpdate(GameTime pGameTime);
    }
}