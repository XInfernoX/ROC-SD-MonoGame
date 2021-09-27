using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StateGameRefactored
{
    public class GameObject
    {
        // Fields
        protected bool _active = true;
        protected Vector2 _position = Vector2.Zero;
        protected Texture2D _texture;
        protected Rectangle _collider = Rectangle.Empty;

        protected int _speed = 10;

        // Properties
        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;
                _collider.X = (int)value.X;
                _collider.Y = (int)value.Y;
            }
        }

        public Texture2D Texture
        {
            get { return _texture; }
            set
            {
                _texture = value;
                _collider = new Rectangle((int)_position.X, (int)_position.Y, value.Width, value.Height);
            }
        }

        public Rectangle Collider//Readonly
        {
            get { return _collider; }
        }

        public int Width//Readonly
        {
            get { return _texture.Width; }
        }

        public int Height//Readonly
        {
            get { return _texture.Height; }
        }

        public int Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }
        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }


        //Constructors
        public GameObject() { }

        public GameObject(Vector2 pPosition, Texture2D pTexture, int pSpeed, bool pActive = true)
        {
            _position = pPosition;
            Texture = pTexture;//Property also creates _collider from texture data

            _speed = pSpeed;
            _active = pActive;
        }

        public GameObject(Vector2 pPosition, Texture2D pTexture)
        {
            _position = pPosition;
            Texture = pTexture;

            _speed = 0;
            _active = true;
        }

        // Copy constructor
        public GameObject(GameObject pOriginal)
        {
            _active = pOriginal._active;
            _position = pOriginal._position;
            _texture = pOriginal._texture;

            _collider = pOriginal._collider;

            _speed = pOriginal._speed;
        }

        //Methods
        public bool Collision(GameObject pOther)
        {
            if (_active & _collider.Contains(pOther._position))
            {
                return true;
            }
            return false;
        }

        public bool Collision(Point pPoint)
        {
            if (_active & _collider.Contains(pPoint))
            {
                return true;
            }
            return false;
        }

        public virtual void Draw(SpriteBatch pSpriteBatch)
        {
            if (Active)
            {
                pSpriteBatch.Draw(_texture, _position, Color.White);
            }
        }

        public virtual void Draw(SpriteBatch pSpriteBatch, Color pColor, float pScale = 1)
        {
            if (Active)
            {
                Vector2 scale = Vector2.One * pScale;
                pSpriteBatch.Draw(_texture, _position, null, pColor, 0, Vector2.One / 2, scale, SpriteEffects.None, 0);
            }
        }

        public virtual void Update() { }
    }
}