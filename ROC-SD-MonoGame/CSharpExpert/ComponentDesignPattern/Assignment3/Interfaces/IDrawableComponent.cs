using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CSharpExpert.ComponentDesignPattern.Assignment3.Interfaces
{
    public interface IDrawableComponent
    {
        void Draw(SpriteBatch pSpriteBatch, Transform pTransform) { }

        Vector2 Size { get; }
    }
}
