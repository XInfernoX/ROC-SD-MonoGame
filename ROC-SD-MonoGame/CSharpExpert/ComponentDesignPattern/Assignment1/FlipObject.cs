using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CSharpAdvanced.CSharpExpert.ComponentDesignPattern.Assignment1
{
    public class FlipObject : GameObject
    {
        //Fields
        private readonly float _flipSpeed;

        private float _time;
        private Vector2 _defaultScale;
        private int _sign;


        public float ScaleSpeed => _flipSpeed;

        public FlipObject(string pName, Transform pTransform, SpriteRenderer pRenderer, float pScaleSpeed) : base(pName, pTransform, pRenderer)
        {
            _flipSpeed = pScaleSpeed;

            _defaultScale = Transform.Scale;

            _flipSpeed *= MathHelper.TwoPi;
            _sign = MathF.Sign(pScaleSpeed);
        }

        //Methods
        public override void UpdateGameObject(GameTime pGameTime)
        {
            _time += (float)pGameTime.ElapsedGameTime.TotalSeconds * _flipSpeed;

            //float remainder = _time % MathHelper.TwoPi;

            float remainder = (_time % MathHelper.TwoPi + MathHelper.TwoPi) % MathHelper.TwoPi;



            int quadrant = (int)(remainder / MathHelper.PiOver2);
            int octant = (int)(remainder / MathHelper.PiOver4);

            SpriteEffects spriteEffect = SpriteEffects.None;
            Vector2 currentOrigin;
            float xScale = 0;
            float yScale = 0;

            switch (quadrant)
            {
                case 0:
                    spriteEffect |= 0;
                    currentOrigin = LocationPresets.BottomLeft.ToCoordinate();
                    xScale = octant % 2 == 0 ? MathF.Abs(MathF.Sin(remainder * 2)) : 1.0f;
                    yScale = octant % 2 == 1 ? MathF.Abs(MathF.Sin(remainder * 2)) : 1.0f;
                    SpriteRenderer.Color = Color.White;
                    break;
                case 1:
                    spriteEffect |= SpriteEffects.FlipVertically;
                    currentOrigin = LocationPresets.TopLeft.ToCoordinate();
                    xScale = octant % 2 == 1 ? MathF.Abs(MathF.Sin(remainder * 2)) : 1.0f;
                    yScale = octant % 2 == 0 ? MathF.Abs(MathF.Sin(remainder * 2)) : 1.0f;
                    SpriteRenderer.Color = Color.Black;
                    break;
                case 2:
                    spriteEffect |= SpriteEffects.FlipVertically | SpriteEffects.FlipHorizontally;
                    currentOrigin = LocationPresets.TopRight.ToCoordinate();
                    xScale = octant % 2 == 0 ? MathF.Abs(MathF.Sin(remainder * 2)) : 1.0f;
                    yScale = octant % 2 == 1 ? MathF.Abs(MathF.Sin(remainder * 2)) : 1.0f;
                    SpriteRenderer.Color = Color.White;
                    break;
                case 3:
                    spriteEffect |= SpriteEffects.FlipHorizontally;
                    currentOrigin = LocationPresets.BottomRight.ToCoordinate();
                    xScale = octant % 2 == 1 ? MathF.Abs(MathF.Sin(remainder * 2)) : 1.0f;
                    yScale = octant % 2 == 0 ? MathF.Abs(MathF.Sin(remainder * 2)) : 1.0f;
                    SpriteRenderer.Color = Color.Black;
                    break;
                default:
                    currentOrigin = Vector2.Zero;
                    break;
            }

            SpriteRenderer.SpriteEffects = spriteEffect;
            Transform.Origin = currentOrigin;
            Transform.Scale = _defaultScale * new Vector2(xScale, yScale);
        }
    }
}