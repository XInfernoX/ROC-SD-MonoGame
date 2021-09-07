using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceInvaders.Refactor
{
    public class RefactoredGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        private GameObject background;
        
        private GameObject player;
        private GameObject playerLaser;

        private GameObject alien;
        private GameObject alienLaserPrefab;



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
        private TimeSpan previousAlienLaserRelease;
        private int alienLaserReleaseDelay;
        private Random alienLaserRandomizer;


        public RefactoredGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            player = new GameObject("player");
            playerLaser = new GameObject("laser");


            alien = new GameObject("Alien");


            background = new GameObject("background");

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
            //GraphicsDevice == _graphics.GraphicsDevice???
            Viewport viewPort = _graphics.GraphicsDevice.Viewport;


            // TODO: use this.Content to load your game content here

            SpriteRenderer backgroundRenderer = new SpriteRenderer("background", Content, Color.White);
            background.AddComponent(backgroundRenderer);

            SpriteRenderer playerRenderer = new SpriteRenderer("player", Content, Color.White);
            player.AddComponent(playerRenderer);

            SpriteRenderer laserRenderer = new SpriteRenderer("laser", Content, Color.White);
            playerLaser.AddComponent(laserRenderer);


            SpriteRenderer alienRenderer = new SpriteRenderer("alien1", Content, Color.White);
            alien.AddComponent(alienRenderer);


            alienLaserPrefab = new GameObject();
            alienLaserPrefab.Active = true;
            alienLaserPrefab.AddTexture(Content.Load<Texture2D>("laser1"));



            player.transform.SetPosition(new Vector2(viewPort.Width / 2,viewPort.Height - playerRenderer.Height));

            int numberOfAliensInARow = viewPort.Width / (alienRenderer.Width * 2);
            int numberOfRows = numberOfAliens / numberOfAliensInARow;

            for (int i = 0; i < numberOfAliens; i++)
            {
                //GameObject nextAlien = new GameObject(alien);
                Vector2 spawnPosition = new Vector2(
                    (i / numberOfRows) * (alienRenderer.Width * 1.5f) + (alienRenderer.Width * 0.2f),
                    (i % numberOfRows) * (alienRenderer.Height * 1.2f)
            );

                GameObject newAlien = CreateAlien(spawnPosition);
                aliens.Add(newAlien);
            }


        }

        private GameObject CreateAlien(Vector2 pSpawnPosition)
        {
            GameObject newAlien = new GameObject("new Alien", pSpawnPosition);
            SpriteRenderer newAlienRendererer = new SpriteRenderer("alien1", Content);
            newAlien.AddComponent(newAlienRendererer);

            return newAlien;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            CollistionDetection();

            // TODO: Add your update logic here
            // Laser movement
            if (playerLaser.Active)
            {
                Vector2 pos = playerLaser.GetPosition();
                if (pos.Y > 0)
                {
                    pos.Y -= playerLaser.Speed;
                }
                else
                {
                    playerLaser.Active = false;
                }
                playerLaser.SetPosition(pos);
            }
            // Alien laser moverment
            int n = alienLasers.Count;
            for (int i = 0; i < n; i++)
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
            if (!playerLaser.Active & state.IsKeyDown(Keys.Space))
            {
                playerLaser.Active = true;
                Vector2 pos = player.GetPosition();
                pos.X += (player.Width / 2) - (playerLaser.Width / 2);
                pos.Y -= playerLaser.Height;
                playerLaser.SetPosition(pos);
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
                    if ((alienStepCount % (alienSteps / 2)) == 0)
                    {
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
                for (int i = 0; i < numberOfAlienLasers; i++)
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
            playerLaser.Draw(_spriteBatch);
            foreach (GameObject alien in aliens)
            {
                alien.Draw(_spriteBatch);
            }
            // Draw alien lasers.
            foreach (GameObject laser in alienLasers)
            {
                laser.Draw(_spriteBatch);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void CollistionDetection()
        {
            // Check if player lasers hit an alien.
            if (playerLaser.Active)
            {
                foreach (GameObject alien in aliens)
                {
                    if (alien.Collision(playerLaser))
                    {
                        alien.Active = false;
                        playerLaser.Active = false;
                        break;
                    }
                }
            }
        }
    }
}
}
