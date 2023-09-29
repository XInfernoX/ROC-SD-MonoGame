using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace ROC_SD_MonoGame.Examples.Ball
{
    public class BouncingBall : Game
    {
        //Fields - MonoGame
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Viewport _viewport;


        //Ball
        private Texture2D _texture;
        private Vector2 _position;
        private Vector2 _velocity;
        private Vector2 _origin;

        private float _speed = 200;

        private Random random = new Random();

        //Constructor
        public BouncingBall()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;

            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Console.WriteLine("LoadContent");

            base.LoadContent();

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _viewport = GraphicsDevice.Viewport;


            _texture = Content.Load<Texture2D>("ball");
            _origin = new Vector2(_texture.Width * 0.5f, _texture.Height * 0.5f);
            _position = new Vector2(_viewport.Width * 0.5f, _viewport.Height * 0.5f);

            _velocity.X = (float)random.NextDouble();
            _velocity.Y = (float)random.NextDouble();

            int randomDegree = random.Next(0, 360);
            _velocity.X = MathF.Cos(randomDegree);
            _velocity.Y = MathF.Sin(randomDegree);

            _velocity.Normalize();
        }

        protected override void Update(GameTime pGameTime)
        {
            base.Update(pGameTime);

            UpdateBall(pGameTime);
        }

        protected override void Draw(GameTime pGameTime)
        {
            base.Draw(pGameTime);

            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.Draw(_texture, _position, null, Color.White, 0, _origin, Vector2.One, SpriteEffects.None, 0);

            _spriteBatch.End();
        }

        private void UpdateBall(GameTime pGameTime)
        {
            BounceBall();

            _position += _velocity * (float)pGameTime.ElapsedGameTime.TotalSeconds * _speed;
        }

        //Methods

        private void BounceBall()
        {
            if (_position.X - _origin.X < 0 || _position.X + _origin.X > _viewport.Width)
                _velocity.X *= -1;

            if (_position.Y - _origin.Y < 0 || _position.Y + _origin.Y > _viewport.Height)
                _velocity.Y *= -1;
        }
    }
}
