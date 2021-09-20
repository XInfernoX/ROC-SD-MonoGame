using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaders.Refactor.Core
{
    public interface IDrawable
    {
        void Draw(SpriteBatch pSpriteBatch);
    }
}