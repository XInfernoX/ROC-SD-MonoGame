using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CSharpIntermediate.Asteroids
{
    public class Game1 : Game
    {
        //Fields - MonoGame
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Player2 _player;

        private List<Laser> _lasers = new List<Laser>();
        private List<Asteroid> _asteroids = new List<Asteroid>();

        private Viewport _viewport;
        private Random _random;

        //Constructor
        public Game1()
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
            _viewport = _graphics.GraphicsDevice.Viewport;

            _random = new Random();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Console.WriteLine("LoadContent");

            base.LoadContent();

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _player = new Player2(this, new Vector2(_viewport.Width * 0.5f, _viewport.Height * 0.9f));


            _player.LoadContent(Content, _viewport);

            AddAsteroid(new Vector2(100, 100), 1);
            AddAsteroid(new Vector2(600, 600), 2);
            AddAsteroid(new Vector2(600, 100), 3);
        }

        protected override void Update(GameTime pGameTime)
        {
            base.Update(pGameTime);

            _player.UpdateGameObject(pGameTime);

            //Updates
            for (int i = 0; i < _lasers.Count; i++)
                _lasers[i].UpdateGameObject(pGameTime);

            for (int i = 0; i < _asteroids.Count; i++)
                _asteroids[i].UpdateGameObject(pGameTime);


            //Collision checks
            for (int laserIndex = _lasers.Count - 1; laserIndex >= 0; laserIndex--)
            {
                Laser currentLaser = _lasers[laserIndex];
                for (int asteroidIndex = _asteroids.Count - 1; asteroidIndex >= 0; asteroidIndex--)
                {
                    Asteroid currentAsteroid = _asteroids[asteroidIndex];
                    float distance = (currentLaser.Position - currentAsteroid.Position).Length();

                    if (distance < currentLaser.Radius + currentAsteroid.Radius)
                    {
                        DestroyAsteroid(currentAsteroid);

                        _lasers.Remove(currentLaser);
                        currentLaser.Destroy();
                    }
                }
            }
        }


        private void DestroyAsteroid(Asteroid pAsteroid)
        {
            if (pAsteroid.AsteroidSize > 2)
            {
                int newAsteroidCount = _random.Next(1, 4);

                for (int i = 0; i < newAsteroidCount; i++)
                {
                    AddAsteroid(pAsteroid.Position, pAsteroid.AsteroidSize - 1);
                }
            }

            _asteroids.Remove(pAsteroid);
            pAsteroid.Destroy();
        }

        protected override void Draw(GameTime pGameTime)
        {
            base.Draw(pGameTime);

            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _player.DrawGameObject(_spriteBatch);

            for (int i = 0; i < _lasers.Count; i++)
            {
                _lasers[i].DrawGameObject(_spriteBatch);
            }

            for (int i = 0; i < _asteroids.Count; i++)
            {
                _asteroids[i].DrawGameObject(_spriteBatch);
            }

            _spriteBatch.End();
        }

        public void AddLaser(Vector2 pSpawnPosition, float pSpeed, float pRotation)
        {
            Laser newLaser = new Laser(pSpawnPosition, pSpeed, pRotation);
            newLaser.LoadContent(Content, _viewport);
            _lasers.Add(newLaser);
        }

        public void AddAsteroid(Vector2 pSpawnPosition, int pAsteroidSize)
        {
            Asteroid newAsteroid = new Asteroid(pSpawnPosition, pAsteroidSize);

            newAsteroid.LoadContent(Content, _viewport);
            _asteroids.Add(newAsteroid);
        }
    }
}
