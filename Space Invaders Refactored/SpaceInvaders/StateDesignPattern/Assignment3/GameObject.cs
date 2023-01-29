using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace StateDesignPattern.Assignment3
{
    public class GameObject : IDisposable
    {
        // Fields
        protected Vector2 _position = Vector2.Zero;
        protected Texture2D _texture;
        protected Vector2 _origin = new Vector2(0.5f, 0.5f);
        
        protected Rectangle _collider = Rectangle.Empty;
        protected bool _active = true;

        // Properties
        public Vector2 Position
        {
            get => _position;
            set => _position = value;
        }

        public Texture2D Texture
        {
            get => _texture;
            set
            {
                _texture = value;
                _collider = new Rectangle((int)_position.X, (int)_position.Y, value.Width, value.Height);
            }
        }

        public bool Active
        {
            get => _active;
            set => _active = value;
        }

        //Properties - Readonly
        public Rectangle Collider=> _collider;
        public int Width => _texture.Width;
        public int Height => _texture.Height;

        //Constructors
        public GameObject()
        {

        }

        public GameObject(Vector2 pPosition)
        {
            _position = pPosition;
        }

        public GameObject(Vector2 pPosition, Texture2D pTexture)
        {
            _position = pPosition;
            Texture = pTexture;
        }

        public GameObject(Vector2 pPosition, Texture2D pTexture, bool pActive = true)
        {
            _position = pPosition;
            Texture = pTexture;//Property also creates _collider from texture data

            _active = pActive;
        }

        public GameObject(Vector2 pPosition, Texture2D pTexture, Vector2 pOrigin, bool pActive = true)
        {
            _position = pPosition;
            Texture = pTexture;//Property also creates _collider from texture data
            _origin = pOrigin;

            _active = pActive;
        }

        // Copy constructor
        public GameObject(GameObject pOriginal)
        {
            _position = pOriginal._position;
            _texture = pOriginal._texture;
            _collider = pOriginal._collider;
        }

        //Methods
        public virtual void LoadContent(ContentManager pContent, Viewport pViewport)
        {

        }
        public virtual void Update(GameTime pTime)
        {

        }

        public bool Collision(GameObject pOther)
        {
            if (_active)
            {
                UpdateCollider();
                pOther.UpdateCollider();

                if (_collider.Intersects(pOther._collider))
                {
                    return true;
                }
            }

            return false;
        }

        public bool Contains(Point pPoint)
        {
            if (_active)
            {
                UpdateCollider();
                if (_collider.Contains(pPoint))
                {
                    return true;
                }
            }

            return false;
        }

        public virtual void Draw(SpriteBatch pSpriteBatch)
        {
            if (_active)
            {
                pSpriteBatch.Draw(_texture, _position, null, Color.White, 0, _origin, 1, SpriteEffects.None, 0);
            }
        }

        private void UpdateCollider()
        {
            _collider.X = (int)_position.X;
            _collider.Y = (int)_position.Y;
        }

        public void Destroy()
        {
            Dispose();
        }

        public virtual void Dispose()
        {
            Console.WriteLine("GameObject.Dispose()");

            _texture.Dispose();
        }
    }
}
