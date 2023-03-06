using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using CSharpExpert.ComponentDesignPattern.Assignment3;

namespace ROC_SD_MonoGame.Examples.LookAt
{
    public class LookAt : Game
    {
        //Fields - MonoGame
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private GameObject _player;
        private GameObject _enemy;

        //Constructor
        public LookAt()
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
            base.LoadContent();

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            SimpleMovement simpleMovement = new SimpleMovement();
            SpriteRenderer playerRenderer = new SpriteRenderer(Content.Load<Texture2D>("Knight"));
            _player = new GameObject(null, "Player", new Vector2(100, 100), simpleMovement, playerRenderer);




            Enemy enemy = new Enemy(_player.Transform);
            SpriteRenderer enemyRenderer = new SpriteRenderer(Content.Load<Texture2D>("Enemy"));
            _enemy = new GameObject(null, "Target", new Vector2(200, 200), enemy, enemyRenderer);
        }

        protected override void Update(GameTime pGameTime)
        {
            base.Update(pGameTime);

            _player.Update(pGameTime);
            _enemy.Update(pGameTime);
        }

        protected override void Draw(GameTime pGameTime)
        {
            base.Draw(pGameTime);

            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _player.Draw(_spriteBatch);
            _enemy.Draw(_spriteBatch);

            _spriteBatch.End();
        }
    }
}
