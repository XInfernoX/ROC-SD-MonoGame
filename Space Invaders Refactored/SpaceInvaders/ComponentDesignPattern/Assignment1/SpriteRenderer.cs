using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.X3DAudio;

namespace ComponentDesignPattern.Assignment1
{
    public class SpriteRenderer : Component
    {
        //Fields
        private Texture2D _texture;
        private Color _color;
        private float _layerDepth;

        //Properties
        public int Width => _texture.Width;
        public int Height => _texture.Height;
        public Vector2 Size => new Vector2(_texture.Width, _texture.Height);
        public Rectangle Bounds => _texture.Bounds;

        public Color Color
        {
            get => _color;
            set => _color = value;
        }

        //Constructor
        public SpriteRenderer(Texture2D pTexture, Color pColor, float pLayerDepth = 1.0f)
        {
            _texture = pTexture;
            _color = pColor;
            _layerDepth = pLayerDepth;
        }

        public SpriteRenderer(Texture2D pTexture) : this(pTexture, Color.White) { }
        public SpriteRenderer(Texture2D pTexture, float pLayerDepth) : this(pTexture, Color.White, pLayerDepth) { }
        public SpriteRenderer(string pAssetName, ContentManager pContent) : this(pContent.Load<Texture2D>(pAssetName), Color.White) { }
        public SpriteRenderer(string pAssetName, ContentManager pContent, Color pColor, float pLayerDepth = 1.0f) : this(pContent.Load<Texture2D>(pAssetName), pColor, pLayerDepth) { }

        public override void Draw(Transform pTransform, SpriteBatch pSpriteBatch)
        {
            Vector2 scaledOrigin = new Vector2(pTransform.Origin.X * _texture.Width, pTransform.Origin.Y * _texture.Height);
            float radians = MathHelper.ToRadians(pTransform.Rotation);
            pSpriteBatch.Draw(_texture, pTransform.Position, null, _color, radians, scaledOrigin, pTransform.Scale, SpriteEffects.None, _layerDepth);

            //https://docs.monogame.net/api/Microsoft.Xna.Framework.Graphics.SpriteBatch.html

            //DestinationRectangle - The drawing bounds on screen.
            //SourceRectangle - An optional region on the texture which will be rendered. If null - draws full texture.

            //Texture2D texture;
            //Vector2 position;
            //Rectangle sourceRectangle;

            //Color color;

            //float rotation;
            //Vector2 origin;
            //Vector2 scale;
            //SpriteEffect spriteEffect;

            //float layerDepth;

            //============================================================


            //Texture2D texture
            //Vector2 position
            //Color color
            //Vector2 origin
            //float scale
            //Vector2 scale
            //SpriteEffect spriteEffect
            //float rotation
            //float layerDepth
            //Rectangle sourceRectangle
            //Rectangle destinationRectangle


        }
    }
}