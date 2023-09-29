﻿using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ROC_SD_MonoGame.CSharpIntermediate.Asteroids
{
    public class GameObject : IDisposable
    {
        // Fields
        protected Vector2 _position = Vector2.Zero;
        protected float _rotation = 0;
        protected float _scale = 1;

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
        public GameObject(float pScale = 1, bool pActive = true)
        {
            _scale = pScale;
            _active = pActive;
        }

        public GameObject(Vector2 pPosition, float pScale = 1, bool pActive = true)
        {
            _position = pPosition;
            _scale = pScale;
            _active = pActive;
        }

        public GameObject(Vector2 pPosition, Texture2D pTexture, float pScale = 1, bool pActive = true)
        {
            _position = pPosition;
            Texture = pTexture;//Property also creates _collider from texture data
            _scale = pScale;

            _active = pActive;
        }

        public GameObject(Vector2 pPosition, Texture2D pTexture, Vector2 pOrigin, float pScale = 1, bool pActive = true)
        {
            _position = pPosition;
            Texture = pTexture;//Property also creates _collider from texture data
            _origin = pOrigin;
            _scale = pScale;

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
        public virtual void UpdateGameObject(GameTime pTime)
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

        public virtual void DrawGameObject(SpriteBatch pSpriteBatch)
        {
            if (_active)
            {
                Vector2 scaledOrigin = new Vector2(_texture.Width * _origin.X, _texture.Height * _origin.Y);
                float radians = MathHelper.ToRadians(_rotation);
                pSpriteBatch.Draw(_texture, _position, null, Color.White, radians, scaledOrigin, _scale, SpriteEffects.None, 0);
            }
        }

        private void UpdateCollider()
        {
            _collider.X = (int)_position.X;
            _collider.Y = (int)_position.Y;
        }

        public Vector2 DegreesToVector(float pDegrees)
        {
            float radians = MathHelper.ToRadians(pDegrees);

            return new Vector2(MathF.Sin(radians), -MathF.Cos(radians));
        }

        public void Destroy()
        {
            _active = false;

            Dispose();
        }

        public virtual void Dispose()
        {
            Console.WriteLine("GameObject.Dispose()");

            _texture.Dispose();
        }
    }
}
