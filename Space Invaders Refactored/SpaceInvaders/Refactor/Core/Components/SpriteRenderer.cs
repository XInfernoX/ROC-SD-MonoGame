using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaders.Refactor.Core.Components
{
    public class SpriteRenderer : Component, IDrawable
    {
        //Fields
        private readonly Texture2D _texture;
        private readonly Color _color;
        private readonly float _layerDepth;

        //Properties
        public int Width => _texture.Width;
        public int Height => _texture.Height;
        public Vector2 Size => new Vector2(_texture.Width, _texture.Height);
        public Rectangle Bounds => _texture.Bounds;

        //Constructors
        public SpriteRenderer(string pAssetName, ContentManager pContent, Color pColor, float pLayerDepth = 1.0f)
        {
            _texture = pContent.Load<Texture2D>(pAssetName);

            _color = pColor;
            _layerDepth = pLayerDepth;
        }
        public SpriteRenderer(string pAssetName, ContentManager pContent) : this(pAssetName, pContent, Color.White, 1.0f) { }

        //public override Component Copy()
        //{
        //    return new SpriteRenderer(_texture, _color, _layerDepth);
        //}

        public SpriteRenderer(Texture2D pTexture, Color pColor, float pLayerDepth = 1.0f)
        {
            _texture = pTexture;
            _color = pColor;
            _layerDepth = pLayerDepth;
        }

        public SpriteRenderer(Texture2D pTexture) : this(pTexture, Color.White, 1.0f) { }
        public SpriteRenderer(Texture2D pTexture, float pLayerDepth) : this(pTexture, Color.White, pLayerDepth) { }

        public void Draw(SpriteBatch pSpriteBatch)
        {
            Vector2 scaledOrigin = new Vector2(transform.Origin.X * _texture.Width, transform.Origin.Y * _texture.Height);
            float radians = MathHelper.ToRadians(transform.Rotation);
            pSpriteBatch.Draw(_texture, transform.Position, null, _color, radians, scaledOrigin, transform.Scale, SpriteEffects.None, _layerDepth);
        }
    }
}
