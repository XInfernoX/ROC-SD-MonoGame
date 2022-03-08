using Microsoft.Xna.Framework.Graphics;

namespace ComponentDesignPattern.Assignment5.Interfaces
{
    public interface IDrawableComponent
    {
        void Draw(SpriteBatch pSpriteBatch, Transform pTransform) { }
    }
}
