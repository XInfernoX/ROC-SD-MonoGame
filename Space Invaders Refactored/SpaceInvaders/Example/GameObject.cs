using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaders
{
    class GameObject
    {
        // Attributes / Fields
        private Texture2D texture;
        private Int32 width;
        private Int32 height;
        private Vector2 position;

        // Properties read and write.
        public int Speed;
        public bool Active;
        public Rectangle Collider;


        public GameObject()
        {
            position = Vector2.Zero;
            Speed = 10;
            Active = false;
            Collider = new Rectangle(0, 0, 0, 0);
        }

        // Copy constructor
        public GameObject(GameObject obj)
        {
            this.texture = obj.texture;
            this.width = obj.width;
            this.height = obj.height;
            this.position = obj.position;
            this.Speed = obj.Speed;
            this.Active = obj.Active;
            this.Collider = obj.Collider;
        }

        public void AddTexture(Texture2D texture)
        {
            this.texture = texture;
            width = texture.Width;
            height = texture.Height;
            Collider = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        
        public void SetPosition( Vector2 position)
        {
            this.position = position;
            this.Collider.X = (int) position.X;
            this.Collider.Y = (int) position.Y;
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public Int32 Width // Property
        {
            get { return width; } // read-only
        }

        public Int32 Height // Property
        {
            get { return height; } // read-only
        }

        public bool Collision(GameObject other)
        {
            if (Active & Collider.Contains(other.position))
            {
                return true;
            }
            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Active)
            {
                spriteBatch.Draw(texture, position, Color.White);
            }
        }
    }
}
