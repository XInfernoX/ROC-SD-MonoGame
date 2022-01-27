using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ComponentDesignPattern.Assignment1
{
    public class Game1 : Game
    {
        //Fields
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private GameObject _littleStar1;
        private GameObject _littleStar2;
        private GameObject _littleStar3;
        private GameObject _littleStar4;

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
            Texture2D starIndicator = Content.Load<Texture2D>("StarIndicators");


            Transform transformTopLeft = new Transform(new Vector2(0, 0), new Vector2(0f, 0f));
            SpriteRenderer spriteRendererTopLeft = new SpriteRenderer(starIndicator);
            _littleStar1 = new GameObject("Star1", transformTopLeft, spriteRendererTopLeft);

            Transform transformTopRight = new Transform(new Vector2(viewport.Width, 0), new Vector2(1f, 0f));
            SpriteRenderer spriteRendererTopRight = new SpriteRenderer(starIndicator);
            _littleStar2 = new GameObject("Star2", transformTopRight, spriteRendererTopRight);

            Transform transformBottomLeft = new Transform(new Vector2(0, viewport.Height), new Vector2(0f, 1f));
            SpriteRenderer spriteRendererBottomLeft = new SpriteRenderer(starIndicator);
            _littleStar3 = new GameObject("Star1", transformBottomLeft, spriteRendererBottomLeft);

            Transform transformBottomRight = new Transform(new Vector2(viewport.Width, viewport.Height), new Vector2(1f, 1f));
            SpriteRenderer spriteRendererBottomRight = new SpriteRenderer(starIndicator);
            _littleStar4 = new GameObject("Star2", transformBottomRight, spriteRendererBottomRight);


        }

        protected override void Update(GameTime pGameTime)
        {
            _littleStar1.UpdateGameObject(pGameTime);
            _littleStar2.UpdateGameObject(pGameTime);
            _littleStar3.UpdateGameObject(pGameTime);
            _littleStar4.UpdateGameObject(pGameTime);
        }

        protected override void Draw(GameTime pGameTime)
        {
            base.Draw(pGameTime);

            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(SpriteSortMode.BackToFront);

            _littleStar1.DrawGameObject(_spriteBatch);
            _littleStar2.DrawGameObject(_spriteBatch);
            _littleStar3.DrawGameObject(_spriteBatch);
            _littleStar4.DrawGameObject(_spriteBatch);

            _spriteBatch.End();
        }
    }
}