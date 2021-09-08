using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SpaceInvaders.Refactor
{
    public class Collider : Component
    {
        //Fields
        private SpriteRenderer _spriteRenderer;

        private int _rectangleWidth;
        private int _rectangleHeight;

        //Constructor
        public Collider(SpriteRenderer pSpriteRenderer)
        {
            _spriteRenderer = pSpriteRenderer;

            _rectangleWidth = pSpriteRenderer.Width;
            _rectangleHeight = pSpriteRenderer.Height;
        }

        //Copy Constructor-ish
        public override Component Copy()
        {
            return new Collider(_spriteRenderer);
        }

        public bool CollidesWith(Collider pOther)
        {
            Vector2 myPosition = transform.Position;

            Rectangle myRectangle = new Rectangle((int)myPosition.X, (int)myPosition.Y, _rectangleWidth, _rectangleHeight);
        }
    }
}
