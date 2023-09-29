using System;
using Microsoft.Xna.Framework;

namespace CSharpExpert.ComponentDesignPattern.Assignment2
{
    public class ColorShifter : MonoBehaviour
    {
        private float _shiftSpeed;
        private float _hue;

        private SpriteRenderer _spriteRenderer;

        public ColorShifter(float pShiftSpeed)
        {
            _shiftSpeed = pShiftSpeed;
        }

        public override void Awake()
        {
            Console.WriteLine("ColorShifterObject.Awake");
            _spriteRenderer = GetComponent<SpriteRenderer>();

            Console.WriteLine(_spriteRenderer == null);
        }

        public override void UpdateMonoBehaviour(GameTime pGameTime)
        {
            _hue += (float)(pGameTime.ElapsedGameTime.TotalSeconds * _shiftSpeed);

            _hue %= 1.0f;

            _spriteRenderer.Color = HSLToRGB(_hue, 1.0f, 0.5f);
        }
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

        public int To255(float pValue)
        {
            return (int)Math.Min(255, 256 * pValue);
        }

        public float HueToRGB(float p, float q, float t)
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

        //Other function found online in JS

        // input: h as an angle in [0,360] and s,l in [0,1] - output: r,g,b in [0,1]
        //function hsl2rgb(h, s, l)
        //{
        //    let a = s * Math.min(l, 1 - l);
        //    let f = (n, k = (n + h / 30) % 12) => l - a * Math.max(Math.min(k - 3, 9 - k, 1), -1);
        //    return [f(0), f(8), f(4)];
        //}
    }
}