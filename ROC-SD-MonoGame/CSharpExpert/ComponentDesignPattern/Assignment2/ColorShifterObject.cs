using System;
using Microsoft.Xna.Framework;

namespace CSharpExpert.ComponentDesignPattern.Assignment2
{
    public class ColorShifterObject : GameObject
    {
        //Fields - configurable
        private float _shiftSpeed;

        //Fields - internal
        private float _hue;

        //Properties
        public float ShiftSpeed
        {
            get => _shiftSpeed;
            set => _shiftSpeed = value;
        }

        //Constructors
        public ColorShifterObject(string pName, Transform pTransform, SpriteRenderer pRenderer, float pShiftSpeed) : base(pName, pTransform, pRenderer)
        {
            _shiftSpeed = pShiftSpeed;
        }

        //Methods - overridden
        public override void UpdateGameObject(GameTime pGameTime)
        {
            //Updating the _hue value according to the passed time since the last Update call, multiplied with the configured shift speed.
            _hue += (float)(pGameTime.ElapsedGameTime.TotalSeconds * _shiftSpeed);

            //Keeps the _hue value below 1.0f, while keeping the excess value, example: 1.03f becomes 0.03f 
            _hue %= 1.0f;

            //Applying the calculated RGB value to the SpriteRenderer based on the _hue value
            //Saturation is hardcoded to 1.0f and Lightness is hardcoded to 0.5f for the brightest color representation

            //TODO Change SpriteRenderer property to a cached reference to the attached SpriteRenderer component via GetComponent<T>
            SpriteRenderer.Color = HSLToRGB(_hue, 1.0f, 0.5f);
        }

        //Methods - ColorShift
        //The remaining Methods below are not mine, but found online and modified conform C# and MonoGame

        //Resources used to create ColorShift effect Methods
        //https://www.alanzucconi.com/2016/01/06/colour-interpolation/
        //https://stackoverflow.com/questions/2353211/hsl-to-rgb-color-conversion
        //http://en.wikipedia.org/wiki/HSL_color_space.
        public Color HSLToRGB(float pHue, float pSaturation, float pLightness)
        {
            float red, green, blue;

            if (pSaturation == 0f)
            {
                red = green = blue = pLightness; // achromatic
            }
            else
            {
                float q = pLightness < 0.5f ? pLightness * (1 + pSaturation) : pLightness + pSaturation - pLightness * pSaturation;
                float p = 2 * pLightness - q;
                red = HueToRGB(p, q, pHue + 1f / 3f);
                green = HueToRGB(p, q, pHue);
                blue = HueToRGB(p, q, pHue - 1f / 3f);
            }
            return new Color(To255(red), To255(green), To255(blue));
        }

        private float HueToRGB(float p, float q, float t)
        {
            if (t < 0f)
                t += 1f;
            if (t > 1f)
                t -= 1f;

            if (t < 1f / 6f)
                return p + (q - p) * 6f * t;
            if (t < 1f / 2f)
                return q;
            if (t < 2f / 3f)
                return p + (q - p) * (2f / 3f - t) * 6f;
            return p;
        }

        //Utility function to convert a float range value[0.0f - 1.0f] to an int range value[0 - 255]
        private int To255(float pValue)
        {
            return (int)Math.Min(255, 256 * pValue);
        }
    }
}