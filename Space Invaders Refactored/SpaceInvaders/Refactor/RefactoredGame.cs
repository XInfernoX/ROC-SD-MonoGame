using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceInvaders.Refactor
{
    public class AlienWave
    {
        private int _numberOfAliens;

        private int _alienSteps;
        private int _alienStepDelay;
        private int _alienStepCount;

        public AlienWave(int pNumberOfAliens, int pAlienSteps, int pAlienStepDelay, int pAlienStepCount)
        {

        }
    }

    //CONSIDER reusing the same SpriteRenderer for all playerLaser's alien's and alienLaser's
    public class RefactoredGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        private GameObject background;

        private GameObject player;
        private GameObject playerLaser;

        private GameObject alien;
        private GameObject alienLaser;

        //Prefab textures
        private Texture2D _playerLaserTexture;
        private Texture2D _alienLaserTexture;

        private List<GameObject> _gameObjects;




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

            _gameObjects = new List<GameObject>();


            player = new GameObject("player");
            playerLaser = new GameObject("playerLaser");
            _gameObjects.Add(player);

            alien = new GameObject("Alien");
            alienLaser = new GameObject("AlienLaser");
            _gameObjects.Add(alienLaser);

            background = new GameObject("background");
            _gameObjects.Add(background);









            aliens = new List<GameObject>();
            alienLasers = new List<GameObject>();
            alienLaserStock = new Stack<GameObject>();
            alienLaserRandomizer = new Random();
        }

        protected override void Initialize()
        {
            Console.WriteLine("Initialize");

            previousStepTime = TimeSpan.Zero;

            //Alien setup
            numberOfAliens = 30;
            alienSteps = 10;
            alienStepDelay = 500; // ms
            alienStepCount = 0;


            numberOfAlienLasers = 5;
            alienLaserReleaseDelay = 700; // ms

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Console.WriteLine("LoadContent");

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            //GraphicsDevice == _graphics.GraphicsDevice???
            Viewport viewPort = _graphics.GraphicsDevice.Viewport;

            _playerLaserTexture = Content.Load<Texture2D>("laser");
            _alienLaserTexture = Content.Load<Texture2D>("laser1");

            //SpriteRenderer setups
            SpriteRenderer backgroundRenderer = new SpriteRenderer("background", Content, Color.White);
            background.AddComponent(backgroundRenderer);

            SpriteRenderer playerRenderer = new SpriteRenderer("player", Content, Color.White);
            player.transform.SetPosition(new Vector2(viewPort.Width / 2, viewPort.Height - playerRenderer.Height));
            player.AddComponent(playerRenderer);

            SpriteRenderer laserRenderer = new SpriteRenderer("laser", Content, Color.White);
            playerLaser.AddComponent(laserRenderer);

            SpriteRenderer alienRenderer = new SpriteRenderer("alien1", Content, Color.White);
            alien.AddComponent(alienRenderer);

            SpriteRenderer alienLaserRenderer = new SpriteRenderer("laser1", Content);
            alienLaser.AddComponent(alienLaserRenderer);

            //Alien creation (need alienRenderer.Width and Height)
            int numberOfAliensInARow = viewPort.Width / (alienRenderer.Width * 2);
            int numberOfRows = numberOfAliens / numberOfAliensInARow;

            CreateAliens(numberOfRows, alienRenderer.Width, alienRenderer.Height);
        }

        //Creational Methods
        private void CreateAliens(int numberOfRows, int alienRendererWidth, int alienRendererHeight)
        {
            for (int i = 0; i < numberOfAliens; i++)
            {
                //GameObject nextAlien = new GameObject(alien);
                Vector2 spawnPosition = new Vector2(
                    (i / numberOfRows) * (alienRendererWidth * 1.5f) + (alienRendererWidth * 0.2f),
                    (i % numberOfRows) * (alienRendererHeight * 1.2f)
            );

                GameObject newAlien = CreateAlien(spawnPosition);
                aliens.Add(newAlien);
                _gameObjects.Add(newAlien);
            }
        }

        private GameObject CreateAlien(Vector2 pSpawnPosition)
        {
            GameObject newAlien = new GameObject("new Alien", pSpawnPosition);
            SpriteRenderer newAlienRendererer = new SpriteRenderer("alien1", Content);
            newAlien.AddComponent(newAlienRendererer);

            return newAlien;
        }

        //CONSIDER removing pSpawnPosition and using a Player reference instead
        private GameObject CreatePlayerLaser(Vector2 pSpawnPosition)
        {
            LaserMovement laserBehaviour = new LaserMovement(new Vector2(-1, 0), 5);
            SpriteRenderer laserRenderer = new SpriteRenderer(_playerLaserTexture);
            GameObject newPlayerLaser = new GameObject("new playerLaser", pSpawnPosition, 0, Vector2.One, laserBehaviour, laserRenderer);
            return newPlayerLaser;
        }

        private GameObject CreateAlienLaser(Vector2 pSpawnPosition)
        {
            LaserMovement laserBehaviour = new LaserMovement(new Vector2(1, 0), 3);
            SpriteRenderer laserRenderer = new SpriteRenderer(_alienLaserTexture);
            GameObject newAlienLaser = new GameObject("new alienLaser", pSpawnPosition, 0, Vector2.One, laserBehaviour, laserRenderer);
            return newAlienLaser;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            for (int i = 0; i < _gameObjects.Count; i++)
            {
                _gameObjects[i].Update(gameTime);
            }

            for (int i = 0; i < _gameObjects.Count; i++)
            {
                _gameObjects[i].LateUpdate(gameTime);
            }




            CollisionDetection();

   
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

            
            // Laser shot
            //if (!playerLaser.Active & state.IsKeyDown(Keys.Space))
            //{
            //    playerLaser.Active = true;
            //    Vector2 pos = player.GetPosition();
            //    pos.X += (player.Width / 2) - (playerLaser.Width / 2);
            //    pos.Y -= playerLaser.Height;
            //    playerLaser.SetPosition(pos);
            //}

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
                        laser = new GameObject(alienLaser);
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

            _spriteBatch.Begin();

            for (int i = 0; i < _gameObjects.Count; i++)
            {
                _gameObjects[i].Draw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void CollisionDetection()
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
