using Microsoft.Xna.Framework;

namespace CoreRefactored
{
    public interface IUpdateable
    {
        void Update(GameTime pGameTime);
        void LateUpdate(GameTime pGameTime);
    }
}