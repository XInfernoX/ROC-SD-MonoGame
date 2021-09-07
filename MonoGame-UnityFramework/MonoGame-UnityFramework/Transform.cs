using Microsoft.Xna.Framework;

namespace MonoGame_UnityFramework
{
    //CONSIDER whether Origin should be in Transform or in SpriteRenderer

    public class Transform : Component
    {
        //Fields
        private Vector2 _position;

        private Vector2 _origin;
        private float _rotation;


        private Vector2 _scale;

        //Properties
        public Vector2 Position => _position;

        public Vector2 Origin => _origin;
        public float Rotation => _rotation;

        public Vector2 Scale => _scale;


        //Constructors
        public Transform() : this(Vector2.Zero, 0.0f, new Vector2(1, 1)) { }

        public Transform(Vector2 pPosition)
        {
            _position = pPosition;
        }

        public Transform(Vector2 pPosition, float pRotation, Vector2 pScale) : this(pPosition)
        {
            _rotation = pRotation;
            _scale = pScale;
        }

        public Transform(Vector2 pPosition, float pRotation, Vector2 pScale, Vector2 pOrigin) : this(pPosition, pRotation, pScale)
        {
            _origin = pOrigin;
        }
        /// <summary>
        /// Translates the GameObject in WorldSpace by pTranslation
        /// </summary>
        /// <param name="pTranslation"></param>
        public void Translate(Vector2 pTranslation)
        {
            _position += pTranslation;
        }
        /// <summary>
        /// Translates the GameObject in desired Space by pTranslation
        /// </summary>
        /// <param name="pTranslation">Translation vector in 2D (X, Y)</param>
        /// <param name="pSpace">Orientation in which the translation needs to be applied</param>
        public void Translate(Vector2 pTranslation, Space pSpace = Space.World)
        {
            if (pSpace == Space.World)
            {
                Translate(pTranslation);
                return;
            }

            float radians = MathHelper.ToRadians(_rotation);

            //System.Console.WriteLine($"rotation: {_rotation} => radians: {radians}");
            //NOTE does not work!

            float cosine = System.MathF.Cos(radians);
            float sine = System.MathF.Sin(radians);

            float xTranslation = cosine * pTranslation.X -  sine * pTranslation.Y;
            float yTranslation = sine * pTranslation.X + cosine * pTranslation.Y;

            Vector2 transformedTranslation = new Vector2(xTranslation, yTranslation);
            //System.Console.WriteLine($"pTranslation: {pTranslation} => transformedTranslation: {transformedTranslation}");

            //System.Console.WriteLine(transformedTranslation);

            Translate(transformedTranslation);
        }

        public void Rotate(float pRotation)
        {
            _rotation += pRotation;
        }

        public override string ToString()
        {
            return $"Transform(" +
                $"Position: {_position}, " +
                $"Rotation: {_rotation} degrees, " +
                $"Scale: {_scale}, " +
                $"Origin: {_origin})";
        }
    }
}
