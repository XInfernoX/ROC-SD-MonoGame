using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Examples
{
    public class Sine : Game
    {
        //Fields - MonoGame
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Vector2 _startPosition;
        private Vector2 _position;
        private Texture2D _texture;
        private float _amplitude = 200;
        private float _bounceSpeed = 2;
        private float _scale;


        //Constructor
        public Sine()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1000;
            _graphics.PreferredBackBufferHeight = 1000;

            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _texture = Content.Load<Texture2D>("LittleStar");
            _startPosition = _position = GraphicsDevice.Viewport.Bounds.Center.ToVector2();
        }

        protected override void Update(GameTime pGameTime)
        {
            base.Update(pGameTime);

            float time = (float)pGameTime.TotalGameTime.TotalSeconds;

            _scale = MathF.Sin(time * MathHelper.TwoPi * _bounceSpeed);

            //Range ([-1, 1] / 2) + 0.5f
            //Range [0,1]


            //-1 => 0
            //1 => 1


            // (-1 /2 ) + 0.5f;
            // -0.5f + 0.5f = 0;


            // (1 / 2) + 0.5f;
            // 0.5f + 0.5f = 1


        }

        protected override void Draw(GameTime pGameTime)
        {
            base.Draw(pGameTime);

            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.Draw(_texture, _position, null, Color.White, 0, new Vector2(_texture.Width * 0.5f, _texture.Height * 0.5f), _scale, SpriteEffects.None, 0);

            _spriteBatch.End();
        }
    }
}
