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

        private GameObject[] _objects;
        private int _circlingObjectCount = 6;

        //Main circling
        private float _rotationTime;
        private float _circlingSpeed = 0.5f;
        
        private float _minCirclingRadius = 50;
        private float _maxCirclingRadius = 150;

        //Sine circling
        private float _sineTime;
        private float _sineSpeed = 0.1f;


        //Debug
        private SpriteFont _font;
        private float sineValue;



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

            _font = Content.Load<SpriteFont>("Arial");
        }

        protected override void Update(GameTime pGameTime)
        {
            base.Update(pGameTime);

            MouseState mouseState = Mouse.GetState();

            _rotationTime += (float)pGameTime.ElapsedGameTime.TotalSeconds * _circlingSpeed;
            _sineTime += (float)pGameTime.ElapsedGameTime.TotalSeconds * _sineSpeed;
            
            float rotationPercentage = _rotationTime % 1.0f;
            float sinePercentage = _sineTime % 1.0f;

            float startRotation = rotationPercentage * 360.0f;

            sineValue = (MathF.Sin(sinePercentage * MathF.PI * 2) / 2) + 0.5f;//[0, 1]
            float sineRotation = sineValue * 360.0f;

            //startRotation += sineRotation;
            float rotationIncrement = 360.0f / _objects.Length;

            float currentCirclingRadius = MathHelper.Lerp(_minCirclingRadius, _maxCirclingRadius, sineValue);
            //float currentCirclingRadius = MathHelper.Lerp(_maxCirclingRadius, _minCirclingRadius, sineValue);

            for (int i = 0; i < _objects.Length; i++)
            {
                Vector2 mouseOffset = Vector2.Zero;
                float currentRotation = startRotation + rotationIncrement * i;

                //mouseOffset.X = MathF.Cos(currentRotation / 180 * MathF.PI) * _circlingRadius;
                //mouseOffset.Y = MathF.Sin(currentRotation / 180 * MathF.PI) * _circlingRadius;

                mouseOffset.X = MathF.Cos(MathHelper.ToRadians(currentRotation)) * currentCirclingRadius;
                mouseOffset.Y = MathF.Sin(MathHelper.ToRadians(currentRotation)) * currentCirclingRadius;

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

            _spriteBatch.DrawString(_font, sineValue.ToString(), Vector2.Zero, Color.Red);

            _spriteBatch.End();
        }
    }
}
