using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace CSharpExpert.ComponentDesignPattern.Assignment3
{
    public class Game1 : Game
    {
        //Fields
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private readonly List<GameObject> _gameObjects = new List<GameObject>();

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
            Viewport viewport = GraphicsDevice.Viewport;

            Texture2D circleTexture = Content.Load<Texture2D>("CircleTransparent");
            Texture2D rectangleTexture = Content.Load<Texture2D>("StarIndicators");

            //GameObject1
            Transform transform = new Transform(new Vector2(200, 200), new Vector2(0.5f, 0.5f), 0, Vector2.One);
            SpriteRenderer spriteRenderer = new SpriteRenderer(circleTexture);
            AnimatedSpriteRenderer animatedSpriteRenderer = new AnimatedSpriteRenderer(Content.Load<Texture2D>("Megaman"), 5, 2, 12f);
            //SphereCollider collider = new SphereCollider(spriteRenderer);
            RectangleCollider collider = new RectangleCollider(spriteRenderer);


            SimpleMovement simpleMovement = new SimpleMovement();
            _gameObjects.Add(new GameObject(this, "MegaMan", transform, animatedSpriteRenderer, collider, simpleMovement));


            //GameObject2
            Transform transform2 = new Transform(new Vector2(viewport.Width / 2, viewport.Height / 2), Vector2.Zero, 0, Vector2.One);
            SpriteRenderer spriteRenderer2 = new SpriteRenderer(circleTexture);
            AnimatedSpriteRenderer animatedSpriteRenderer2 = new AnimatedSpriteRenderer(Content.Load<Texture2D>("Megaman2"), 5, 2, 12f);
            SphereCollider collider2 = new SphereCollider(spriteRenderer2);
            _gameObjects.Add(new GameObject(this, "LittleStar", transform2, animatedSpriteRenderer2, collider2));

            AwakeGameObjects();
            StartGameObjects();


            //Rectangle to sphere check
            _gameObjects[1].CollisionCheck(_gameObjects[0]);

        }

        private void AwakeGameObjects()
        {
            for (int i = 0; i < _gameObjects.Count; i++)
                _gameObjects[i].AwakeComponents();
        }

        private void StartGameObjects()
        {
            for (int i = 0; i < _gameObjects.Count; i++)
                _gameObjects[i].StartComponents();
        }

        protected override void Update(GameTime pGameTime)
        {
            UpdateAllGameObjects(pGameTime);
            LateUpdateAllGameObjects(pGameTime);
            //CollisionCheckAllGameObjects(pGameTime);
        }

        protected void UpdateAllGameObjects(GameTime pGameTime)
        {
            for (int i = 0; i < _gameObjects.Count; i++)
                _gameObjects[i].Update(pGameTime);
        }
        protected void LateUpdateAllGameObjects(GameTime pGameTime)
        {
            for (int i = 0; i < _gameObjects.Count; i++)
                _gameObjects[i].LateUpdate(pGameTime);
        }

        //Game1 class
        protected void CollisionCheckAllGameObjects(GameTime pGameTime)//CONSIDER passing pGameTime to CollisionCheck
        {
            //Loop through all GameObjects
            for (int outerI = 0; outerI < _gameObjects.Count - 1; outerI++)
            {
                GameObject outerGameObject = _gameObjects[outerI];

                //Huge optimization: Setting innerI to outerI + 1, this halves the amount of collision checks
                //(Collision A with B == collision B with A, and it does not matter for this implementation
                //This makes sure all possible combinations are checked only once
                for (int innerI = outerI + 1; innerI < _gameObjects.Count; innerI++)
                    outerGameObject.CollisionCheck(_gameObjects[innerI]);
            }
        }

        protected override void Draw(GameTime pGameTime)
        {
            base.Draw(pGameTime);

            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(SpriteSortMode.BackToFront);

            for (int i = 0; i < _gameObjects.Count; i++)
                _gameObjects[i].Draw(_spriteBatch);

            _spriteBatch.End();
        }
    }
}