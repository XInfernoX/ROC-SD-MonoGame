
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ComponentDesignPattern.Assignment2
{
    public class Game1 : Game
    {
        //Fields
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private RotatorObject[] _rotatorObjects;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1000;
            _graphics.PreferredBackBufferHeight = 800;

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Viewport viewport = _graphics.GraphicsDevice.Viewport;

            Texture2D starIndicator = Content.Load<Texture2D>("StarIndicators");
            SpriteFont defaultFont = Content.Load<SpriteFont>("Arial");


            _rotatorObjects = new RotatorObject[9];

            float horizontalSpacing = viewport.Width * 0.2f;
            float verticalSpacing = viewport.Height * 0.2f;

            for (int x = 0, i = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++, i++)
                {
                    Transform rotatorTransform = new Transform(
                        new Vector2(viewport.Width * (0.3f * x) + horizontalSpacing, viewport.Height * (0.3f * y) + verticalSpacing),
                        new Vector2(0.5f * x, 0.5f * y));
                    SpriteRenderer rotatorRenderer = new SpriteRenderer(starIndicator);
                    rotatorRenderer.SpriteFont = defaultFont;
                    rotatorRenderer.Text = $"Origin: [{x * 0.5f}, {y * 0.5f}]";

                    _rotatorObjects[i] = new RotatorObject($"RotatorTest{i}", rotatorTransform, rotatorRenderer, 2);
                }
            }
        }

        protected override void Update(GameTime pGameTime)
        {
            for (int i = 0; i < _rotatorObjects.Length; i++)
            {
                _rotatorObjects[i].UpdateGameObject(pGameTime);
            }
        }

        protected override void Draw(GameTime pGameTime)
        {
            base.Draw(pGameTime);

            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(SpriteSortMode.BackToFront);

            for (int i = 0; i < _rotatorObjects.Length; i++)
            {
                _rotatorObjects[i].DrawGameObject(_spriteBatch);
            }

            _spriteBatch.End();
        }
    }
}