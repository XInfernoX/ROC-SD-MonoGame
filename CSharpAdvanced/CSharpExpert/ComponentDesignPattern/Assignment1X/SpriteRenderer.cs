using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CSharpAdvanced.CSharpExpert.ComponentDesignPattern.Assignment1X
{
    public class SpriteRenderer
    {
        //Fields
        private Texture2D _texture;
        private Color _color;
        private float _layerDepth;

        //Temporarily
        private SpriteFont _font;
        private string _text;
        private LocationPresets _textAnchorPoint = LocationPresets.Bottom;
        private LocationPresets _textOrigin = LocationPresets.Top;

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

        //Temporarily
        public string Text
        {
            get => _text;
            set => _text = value;
        }

        public SpriteFont SpriteFont
        {
            get => _font;
            set => _font = value;
        }

        public LocationPresets TextAlignment
        {
            get => _textAnchorPoint;
            set => _textAnchorPoint = value;
        }

        public LocationPresets TextOrigin
        {
            get => _textOrigin;
            set => _textOrigin = value;
        }

        //Constructors
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

        public void Draw(SpriteBatch pSpriteBatch, Transform pTransform)
        {
            Vector2 scaledOrigin = new Vector2(pTransform.Origin.X * _texture.Width, pTransform.Origin.Y * _texture.Height);
            float radians = MathHelper.ToRadians(pTransform.Rotation);
            pSpriteBatch.Draw(_texture, pTransform.Position, null, _color, radians, scaledOrigin, pTransform.Scale, SpriteEffects.None, _layerDepth);

            if (_font != null)
            {
                Vector2 textAnchorPoint = _textAnchorPoint.ToCoordinate();

                Vector2 relativeScaledAnchorPoint = new Vector2((textAnchorPoint.X - pTransform.Origin.X) * _texture.Width, (textAnchorPoint.Y - pTransform.Origin.Y) * _texture.Height);
                Vector2 textPosition = pTransform.Position + relativeScaledAnchorPoint;

                Vector2 textSize = _font.MeasureString(_text);
                Vector2 textOrigin = _textOrigin.ToCoordinate();
                pSpriteBatch.DrawString(_font, _text, textPosition, Color.Red, 0, textOrigin * textSize, 1.0f, SpriteEffects.None, _layerDepth);
            }
        }
    }
}