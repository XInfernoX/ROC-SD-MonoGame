using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace ROC_SD_MonoGame.Examples.MouseExamples
{
    public class MouseCircling : Game
    {
        //Fields - MonoGame
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private int _circlingObjectCount = 6;
        private float _circlingRadius = 100;

        private float _circlingSpeed = 0.5f;

        private float _time;

        private GameObject[] _objects;


        //Constructor
        public MouseCircling()
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
            Console.WriteLine("LoadContent");

            base.LoadContent();

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _objects = new GameObject[_circlingObjectCount];
            Texture2D texture = Content.Load<Texture2D>("cannonBallSmall");

            for (int i = 0; i < _circlingObjectCount; i++)
            {
                _objects[i] = new GameObject(Vector2.Zero, texture);
            }
        }

        protected override void Update(GameTime pGameTime)
        {
            base.Update(pGameTime);

            MouseState mouseState = Mouse.GetState();

            _time += (float) pGameTime.ElapsedGameTime.TotalSeconds * _circlingSpeed;

            float currentCirclingSpeed = ((MathF.Sin(_time * MathF.PI * 2) / 2) + 0.5f) * _circlingSpeed;



            float percentage = _time % 1.0f;
            float startRotation = percentage * 360.0f;
            float rotationIncrement = 360.0f / _objects.Length;

            for (int i = 0; i < _objects.Length; i++)
            {
                Vector2 mouseOffset = Vector2.Zero;
                float currentRotation = startRotation + rotationIncrement * i;

                //mouseOffset.X = MathF.Cos(currentRotation / 180 * MathF.PI) * _circlingRadius;
                //mouseOffset.Y = MathF.Sin(currentRotation / 180 * MathF.PI) * _circlingRadius;

                mouseOffset.X = MathF.Cos(MathHelper.ToRadians(currentRotation)) * _circlingRadius;
                mouseOffset.Y = MathF.Sin(MathHelper.ToRadians(currentRotation)) * _circlingRadius;

                //_objects[i].Position = mouseState.Position.ToVector2() + mouseOffset;
                _objects[i].Position = mouseState.Position.ToVector2() + mouseOffset;
            }
        }

        protected override void Draw(GameTime pGameTime)
        {
            base.Draw(pGameTime);

            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            for (int i = 0; i < _objects.Length; i++)
            {
                _objects[i].Draw(_spriteBatch);
            }

            _spriteBatch.End();
        }
    }
}
