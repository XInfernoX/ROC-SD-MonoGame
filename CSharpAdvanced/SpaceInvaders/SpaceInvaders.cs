using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceInvadersNameSpace
{
    public class SpaceInvaders : Game
    {
        #region
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        
        
        
        // Background data model.
        private Texture2D background;
        // Player
        private GameObject player;
        // Laser
        private GameObject laser;
        // Aliens
        private List<GameObject> aliens;
        private int numberOfAliens;
        // Alien movement data model.
        private int alienSteps;
        private int alienStepDelay;
        private int alienStepCount;
        private TimeSpan previousStepTime;
        // Alien laser data model.
        private int numberOfAlienLasers;
        private List<GameObject> alienLasers;
        private Stack<GameObject> alienLaserStock;
        private GameObject alienLaserPrefab;
        private TimeSpan previousAlienLaserRelease;
        private int alienLaserReleaseDelay;
        private Random alienLaserRandomizer;
        #endregion

        public SpaceInvaders()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            player = new GameObject();
            laser = new GameObject();
            aliens = new List<GameObject>();
            alienLasers = new List<GameObject>();
            alienLaserStock = new Stack<GameObject>();
            alienLaserRandomizer = new Random();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            player.Active = true;
            numberOfAliens = 30;
            alienSteps = 10;
            alienStepDelay = 500; // ms
            alienStepCount = 0;
            previousStepTime = TimeSpan.Zero;

            numberOfAlienLasers = 5;
            alienLaserReleaseDelay = 700; // ms

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            background = Content.Load<Texture2D>("background");
            player.AddTexture(Content.Load<Texture2D>("player"));
            laser.AddTexture(Content.Load<Texture2D>("laser2"));

            player.SetPosition( new Vector2(
                x: _graphics.GraphicsDevice.Viewport.Width / 2,
                y: _graphics.GraphicsDevice.Viewport.Height - player.Height
                ));

            GameObject alien = new GameObject();
            alien.AddTexture(Content.Load<Texture2D>("alien1"));
            alien.Active = true;

            int numberOfAliensInARow = _graphics.GraphicsDevice.Viewport.Width / (alien.Width * 2);
            int numberOfRows = numberOfAliens / numberOfAliensInARow;
            for (int i = 0; i < numberOfAliens; i++)
            {
                GameObject nextAlien = new GameObject(alien);
                nextAlien.SetPosition( new Vector2(
                    (i / numberOfRows) * (alien.Width * 1.5f) + (alien.Width * 0.2f),
                    (i % numberOfRows) * (alien.Height * 1.2f)
                    ));
                aliens.Add(nextAlien);
            }

            // Load alien laser asset.
            alienLaserPrefab = new GameObject();
            alienLaserPrefab.Active = true;
            alienLaserPrefab.AddTexture(Content.Load<Texture2D>("laser1"));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            CollistionDetection();

            // TODO: Add your update logic here
            // Laser movement
            if (laser.Active)
            {
                Vector2 pos = laser.GetPosition();
                if (pos.Y > 0)
                {
                    pos.Y -= laser.Speed;
                }
                else
                {
                    laser.Active = false;
                }
                laser.SetPosition(pos);
            }
            // Alien laser moverment
            int n = alienLasers.Count;
            for( int i = 0; i < n; i++)
            {
                GameObject laser = alienLasers[i];
                Vector2 pos = laser.GetPosition();
                if (pos.Y < _graphics.GraphicsDevice.Viewport.Height)
                {
                    pos.Y += laser.Speed;
                    laser.SetPosition(pos);
                }
                else
                {
                    alienLaserStock.Push(laser);
                    alienLasers.RemoveAt(i);
                    n--;
                }
            }

            // player movement
            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.Right))
            {
                Vector2 pos = player.GetPosition();
                if (pos.X + (player.Width / 2) < _graphics.GraphicsDevice.Viewport.Width)
                {
                    pos.X += player.Speed;
                    player.SetPosition(pos);
                }
            }
            if (state.IsKeyDown(Keys.Left))
            {
                Vector2 pos = player.GetPosition();
                if (pos.X + player.Width / 2 > 0)
                {
                    pos.X -= player.Speed;
                    player.SetPosition(pos);
                }
            }
            // Laser shot
            if (!laser.Active & state.IsKeyDown(Keys.Space))
            {
                laser.Active = true;
                Vector2 pos = player.GetPosition();
                pos.X += (player.Width / 2) - (laser.Width / 2);
                pos.Y -= laser.Height;
                laser.SetPosition(pos);
            }

            // Alien movement
            if (gameTime.TotalGameTime - previousStepTime > new TimeSpan(0, 0, 0, 0, alienStepDelay))
            {
                foreach (GameObject alien in aliens)
                {
                    Vector2 pos = alien.GetPosition();
                    if ((alienStepCount % alienSteps) < (alienSteps / 2))
                    {
                        pos.X += (alien.Width * 3) / alienSteps;
                    }
                    else
                    {
                        pos.X -= (alien.Width * 3) / alienSteps;
                    }
                    if ((alienStepCount % (alienSteps / 2)) == 0) {
                        pos.Y += (alien.Height * 0.2f);
                    }
                    alien.SetPosition(pos);
                }
                alienStepCount++;
                previousStepTime = gameTime.TotalGameTime;
            }

            // Alien laser handling.
            if (gameTime.TotalGameTime - previousAlienLaserRelease > new TimeSpan(0, 0, 0, 0, alienLaserReleaseDelay))
            {
                for(int i = 0; i < numberOfAlienLasers; i++)
                {
                    GameObject laser;
                    if (alienLaserStock.Count > 0) // Recycle alien laser from stock.
                    {
                        laser = alienLaserStock.Pop();
                    }
                    else // Create a new alien laser
                    {
                        laser = new GameObject(alienLaserPrefab);
                    }
                    int randomSelectedAlien = alienLaserRandomizer.Next(aliens.Count);
                    if (aliens[randomSelectedAlien].Active)
                    {
                        laser.SetPosition(aliens[randomSelectedAlien].GetPosition());
                        alienLasers.Add(laser);
                    }
                }
                previousAlienLaserRelease = gameTime.TotalGameTime;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(background, Vector2.Zero, Color.White);

            player.Draw(_spriteBatch);
            laser.Draw(_spriteBatch);
            foreach(GameObject alien in aliens)
            {
                alien.Draw(_spriteBatch);
            }
            // Draw alien lasers.
            foreach(GameObject laser in alienLasers)
            {
                laser.Draw(_spriteBatch);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void CollistionDetection()
        {
            // Check if player lasers hit an alien.
            if (laser.Active)
            {
                foreach (GameObject alien in aliens)
                {
                    if (alien.Collision(laser))
                    {
                        alien.Active = false;
                        laser.Active = false;
                        break;
                    }
                }
            }
        }
    }
}
