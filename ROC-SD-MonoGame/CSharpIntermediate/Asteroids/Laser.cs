using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ROC_SD_MonoGame.CSharpIntermediate.Asteroids
{
    public class Laser : GameObject
    {
        //Field
        private float _speed;
        private Vector2 _direction;
        private float _radius;

        private Color _color = Color.White;

        //Properties
        public float Radius => _radius;

        //Constructor
        public Laser(Vector2 pPosition, float pSpeed, float pRotation) : base(pPosition)
        {
            _speed = pSpeed;
            _rotation = pRotation;

            _direction = DegreesToVector(_rotation);
        }

        public override void LoadContent(ContentManager pContent, Viewport pViewport)
        {
            Texture = pContent.Load<Texture2D>("laser1");
            _radius = Texture.Width / 2;
        }

        //Methods
        public override void UpdateGameObject(GameTime pTime)
        {
            _position += (float)pTime.ElapsedGameTime.TotalSeconds * _speed * _direction;

            MouseState state = Mouse.GetState();
            Vector2 mousePos = state.Position.ToVector2();

            float mouseDistance = (mousePos - Position).Length();

            if (mouseDistance < Radius)
                _color = Color.Red;
            else
                _color = Color.White;
        }

        public override void DrawGameObject(SpriteBatch pSpriteBatch)
        {
            if (_active)
            {
                Vector2 scaledOrigin = new Vector2(_texture.Width * _origin.X, _texture.Height * _origin.Y);
                float radians = MathHelper.ToRadians(_rotation);
                pSpriteBatch.Draw(_texture, _position, null, _color, radians, scaledOrigin, _scale, SpriteEffects.None, 0);
            }
        }

        public override void Dispose()
        {
        }
    }
}
