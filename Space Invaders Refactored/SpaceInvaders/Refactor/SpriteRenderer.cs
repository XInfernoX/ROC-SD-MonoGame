using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SpaceInvaders.Refactor
{
    public class SpriteRenderer : Component
    {
        //Fields
        private string _assetName;
        private Texture2D _texture;
        private Color _color = Color.White;
        private float _layerDepth = 1.0f;

        //Properties
        public int Width => _texture.Width;
        public int Height => _texture.Height;

        //Constructors
        public SpriteRenderer(string pAssetName, ContentManager pContent, Color pColor, float pLayerDepth = 1.0f)
        {
            _assetName = pAssetName;
            _texture = pContent.Load<Texture2D>(pAssetName);

            _color = pColor;
            _layerDepth = pLayerDepth;
        }
        public SpriteRenderer(string pAssetName, ContentManager pContent) : this(pAssetName, pContent, Color.White, 1.0f) { }

        public override Component Copy()
        {
            return new SpriteRenderer(_texture, _color, _layerDepth);
        }

        public SpriteRenderer(Texture2D pTexture, Color pColor, float pLayerDepth = 1.0f)
        {
            _texture = pTexture;
            _color = pColor;
            _layerDepth = pLayerDepth;
        }

        public SpriteRenderer(Texture2D pTexture) : this(pTexture, Color.White, 1.0f) { }
        public SpriteRenderer(Texture2D pTexture, float pLayerDepth) : this(pTexture, Color.White, pLayerDepth) { }

        public void Draw(Transform pTransform, SpriteBatch pSpriteBatch)
        {
            Vector2 scaledOrigin = new Vector2(pTransform.Origin.X * _texture.Width, pTransform.Origin.Y * _texture.Height);
            float radians = MathHelper.ToRadians(pTransform.Rotation);
            pSpriteBatch.Draw(_texture, pTransform.Position, null, _color, radians, scaledOrigin, pTransform.Scale, SpriteEffects.None, _layerDepth);
        }
    }
}
