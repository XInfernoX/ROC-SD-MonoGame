using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids
{
    public class Game1 : Game
    {
        //Fields - MonoGame
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private GameObject _player;

        private Texture2D _laserTexture;
        private List<Laser> _lasers = new List<Laser>();

        private Texture2D _asteroidTexture;
        private List<Asteroid> _asteroids = new List<Asteroid>();

        //Constructor
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _player = new Player2(this, new Vector2(100,100), GraphicsDevice.Viewport);
            _player.LoadContent(Content, GraphicsDevice.Viewport);

            _laserTexture = Content.Load<Texture2D>("laser1");
            _asteroidTexture = Content.Load<Texture2D>("Asteroids");


            _asteroids.Add(new Asteroid());

        }

        protected override void Update(GameTime pGameTime)
        {
            base.Update(pGameTime);

            _player.Update(pGameTime);

            for (int i = 0; i < _lasers.Count; i++)
            {
                _lasers[i].Update(pGameTime);
            }
        }

        protected override void Draw(GameTime pGameTime)
        {
            base.Draw(pGameTime);

            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _player.Draw(_spriteBatch);

            for (int i = 0; i < _lasers.Count; i++)
            {
                _lasers[i].Draw(_spriteBatch);
            }

            _spriteBatch.End();
        }

        public void AddLaser(Vector2 pSpawnPosition, float pRotation)
        {
            _lasers.Add(new Laser(_laserTexture, pSpawnPosition, 200, pRotation));
        }
    }
}
