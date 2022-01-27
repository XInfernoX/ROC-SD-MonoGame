using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ComponentDesignPattern.Assignment1
{
    public class AnimatedSpriteRenderer : Component
    {
        //Fields
        private readonly Texture2D _spriteSheet;
        private readonly int _horizontalSpriteCount;
        private readonly int _verticalSpriteCount;
        private readonly float _animationSpeed;
        private Color _color;
        private float _layerDepth;



        private float _animationTime;
        private readonly float _timeModifier;
        private readonly int _spriteCount;
        private readonly Rectangle[] _sourceRectangles;
        private Rectangle _currentSourceRectangle;

        //Properties
        public int SingleSpriteWidth => _spriteSheet.Width / _horizontalSpriteCount;
        public int SingleSpriteHeight => _spriteSheet.Height / _verticalSpriteCount;

        public int SpriteSheetWidth => _spriteSheet.Width;
        public int SpriteSheetHeight => _spriteSheet.Height;

        public Vector2 SingeSpriteSize => new Vector2(SingleSpriteWidth, SingleSpriteHeight);
        public Vector2 SpriteSheetSize => new Vector2(_spriteSheet.Width, _spriteSheet.Height);

        public Rectangle SingleSpriteBounds => new Rectangle(0, 0, SingleSpriteWidth, SingleSpriteHeight);
        public Rectangle SpriteSheetBounds => _spriteSheet.Bounds;

        public Color Color
        {
            get => _color;
            set => _color = value;
        }

        public float LayerDepth
        {
            get => _layerDepth;
            set
            {
                _layerDepth = MathF.Min(1.0f, value);
                _layerDepth = MathF.Max(0.0f, _layerDepth);
            }
        }


        //Constructor
        public AnimatedSpriteRenderer(Texture2D pSpriteSheet, int pHorizontalSpriteCount, int pVerticalSpriteCount, float pAnimationSpeed, Color pColor, float pLayerDepth = 1.0f)
        {
            _spriteSheet = pSpriteSheet;
            _horizontalSpriteCount = pHorizontalSpriteCount;
            _verticalSpriteCount = pVerticalSpriteCount;
            _animationSpeed = pAnimationSpeed;

            _color = pColor;
            _layerDepth = pLayerDepth;

            _spriteCount = pHorizontalSpriteCount * pVerticalSpriteCount;
            _sourceRectangles = new Rectangle[_spriteCount];

            for (int y = 0, i = 0; y < pVerticalSpriteCount; y++)
            {
                for (int x = 0; x < pHorizontalSpriteCount; x++, i++)
                {
                    _sourceRectangles[i] = new Rectangle(x * SingleSpriteWidth, y * SingleSpriteHeight, SingleSpriteWidth, SingleSpriteHeight);
                }
            }

            _timeModifier = pAnimationSpeed / _spriteCount;
        }

        public AnimatedSpriteRenderer(Texture2D pSpriteSheet, int pHorizontalSpriteCount, int pVerticalSpriteCount) :
        this(pSpriteSheet, pHorizontalSpriteCount, pVerticalSpriteCount, 1.0f, Color.White)
        {
        }

        public AnimatedSpriteRenderer(Texture2D pSpriteSheet, int pHorizontalSpriteCount, int pVerticalSpriteCount, float pAnimationSpeed) :
        this(pSpriteSheet, pHorizontalSpriteCount, pVerticalSpriteCount, pAnimationSpeed, Color.White)
        {
        }

        public AnimatedSpriteRenderer(Texture2D pSpriteSheet, int pHorizontalSpriteCount, int pVerticalSpriteCount, float pAnimationSpeed, float pLayerDepth) :
        this(pSpriteSheet, pHorizontalSpriteCount, pVerticalSpriteCount, pAnimationSpeed, Color.White, pLayerDepth)
        {
        }





        public AnimatedSpriteRenderer(string pAssetName, ContentManager pContent, int pHorizontalSpriteCount, int pVerticalSpriteCount) :
        this(pContent.Load<Texture2D>(pAssetName), pHorizontalSpriteCount, pVerticalSpriteCount, 1.0f, Color.White)
        {
        }

        public AnimatedSpriteRenderer(string pAssetName, ContentManager pContent, int pHorizontalSpriteCount, int pVerticalSpriteCount, float pAnimationSpeed) :
        this(pContent.Load<Texture2D>(pAssetName), pHorizontalSpriteCount, pVerticalSpriteCount, pAnimationSpeed, Color.White)
        {
        }

        public AnimatedSpriteRenderer(string pAssetName, ContentManager pContent, int pHorizontalSpriteCount, int pVerticalSpriteCount, float pAnimationSpeed, Color pColor, float pLayerDepth = 1.0f) :
        this(pContent.Load<Texture2D>(pAssetName), pHorizontalSpriteCount, pVerticalSpriteCount, pAnimationSpeed, pColor, pLayerDepth)
        {
        }

        public override void Update(GameTime pGameTime)
        {
            _animationTime += (float)pGameTime.ElapsedGameTime.TotalSeconds * _timeModifier;
            _animationTime %= 1.0f;

            int currentFrame = (int)(_animationTime * _spriteCount);

            Console.WriteLine($"time:{_animationTime}, currentFrame:{currentFrame}");

            _currentSourceRectangle = _sourceRectangles[currentFrame];
        }

        public override void Draw(Transform pTransform, SpriteBatch pSpriteBatch)
        {
            Vector2 scaledOrigin =
                new Vector2(pTransform.Origin.X * SingleSpriteWidth, pTransform.Origin.Y * SingleSpriteHeight);
            float radians = MathHelper.ToRadians(pTransform.Rotation);
            pSpriteBatch.Draw(_spriteSheet, pTransform.Position, _currentSourceRectangle, _color, radians, scaledOrigin, pTransform.Scale,
                SpriteEffects.None, _layerDepth);
        }
    }
}