using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MonoGame_UnityFramework
{
    public class SpriteRenderer : Component
    {
        private string _assetName;
        private Texture2D _texture;
        private Color _color = Color.White;
        private float _layerDepth = 1.0f;

        public SpriteRenderer(string pAssetName, Color pColor, float pLayerDepth = 1.0f)
        {
            _assetName = pAssetName;
            _color = pColor;
            _layerDepth = pLayerDepth;
        }

        public SpriteRenderer(Texture2D pTexture, Color pColor, float pLayerDepth = 1.0f)
        {
            _texture = pTexture;
            _color = pColor;
            _layerDepth = pLayerDepth;
        }

        public SpriteRenderer(Texture2D pTexture) : this(pTexture, Color.White, 1.0f)
        {
        }
        public SpriteRenderer(Texture2D pTexture, float pLayerDepth) : this(pTexture, Color.White, pLayerDepth)
        { 
        }

        public void Load(ContentManager pContent)
        {
            _texture =  pContent.Load<Texture2D>(_assetName);
        }

        public void Draw(Transform pTransform, SpriteBatch pSpriteBatch)
        {
            //_spriteBatch.Draw(_texture, pTransform)
            //System.Console.WriteLine($"SpriteRenderer.Draw");

            //The origin argument of pSpriteBatch.Draw() is in pixels not in UV coordinates. CONSIDER changing it to UV coordinates and multiply it here with the texture's width and height


            //System.Console.WriteLine($"SpriteRenderer.Draw {pTransform.Origin}");

            Vector2 scaledOrigin = new Vector2(pTransform.Origin.X * _texture.Width, pTransform.Origin.Y * _texture.Height);
            float radians = MathHelper.ToRadians(pTransform.Rotation);
            pSpriteBatch.Draw(_texture, pTransform.Position, null, _color, radians, scaledOrigin, pTransform.Scale, SpriteEffects.None, _layerDepth);
        }
    }
}
