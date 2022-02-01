using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ComponentDesignPattern.Assignment4
{
    public class Game1 : Game
    {
        //Fields
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private GameObject _littleStar1;
        private GameObject _littleStar2;

        private GameObject _megaMan;

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

            Transform keyTransform = new Transform(new Vector2(50, 50), new Vector2(0.5f, 0.5f), 0, new Vector2(1, 1));
            SpriteRenderer keyRenderer = new SpriteRenderer(Content.Load<Texture2D>("LittleStar"), Color.Blue, 0.0f);

            Transform keyTransform2 = new Transform(new Vector2(75, 75), new Vector2(0.5f, 0.5f), 0, new Vector2(1, 1));
            SpriteRenderer keyRenderer2 = new SpriteRenderer(Content.Load<Texture2D>("LittleStar"), Color.Red, 1);

            _littleStar1 = new GameObject("Key", keyTransform, keyRenderer);
            _littleStar2 = new GameObject("Key2", keyTransform2, keyRenderer2);

            Transform megaManTransform = new Transform(new Vector2(viewport.Width * 0.5f, viewport.Height * 0.5f));
            AnimatedSpriteRenderer animatedSpriteRenderer = new AnimatedSpriteRenderer(
                Content.Load<Texture2D>("Megaman2"), 5, 2, 12f);

            _megaMan = new GameObject("MegaMan", megaManTransform, animatedSpriteRenderer);
        }

        protected override void Update(GameTime pGameTime)
        {
            _littleStar1.UpdateGameObject(pGameTime);
            _littleStar2.UpdateGameObject(pGameTime);

            _megaMan.UpdateGameObject(pGameTime);
        }

        protected override void Draw(GameTime pGameTime)
        {
            base.Draw(pGameTime);

            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(SpriteSortMode.BackToFront);

            _littleStar1.DrawGameObject(_spriteBatch);
            _littleStar2.DrawGameObject(_spriteBatch);

            _megaMan.DrawGameObject(_spriteBatch);

            _spriteBatch.End();
        }
    }
}