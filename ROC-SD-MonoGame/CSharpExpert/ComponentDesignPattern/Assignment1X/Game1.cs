using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CSharpAdvanced.CSharpExpert.ComponentDesignPattern.Assignment1X
{

    //Tijdens de lessen
    //Unity onderzoek opdracht
    //Mathf.Sin
    //Range change, mapping[-1, 1] -> [0,1]
    //GameObject leeg trekken => Transform + SpriteRenderer
    //Transform Responsibility => Nuttige positionele data opslaan in format
    //SpriteRenderer Responsibility => Er voor zorgen dat een Sprite correct getekend wordt


    //Opdracht
    //GameObject criteria(rotatie in graden, Origin in norm values,
    //GameObject opsplitsen -> Transform + SpriteRenderer???
    //Simple behaviours maken (Rotator, Bouncer, Scaler)
    //TestScenes maken



    public class Game1 : Game
    {
        //Fields
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private RotaterObject _rotatorObject;
        private ScalerObject _scalerObject;
        private OscillatorObject _oscillatorObject;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = 1000;
            _graphics.PreferredBackBufferHeight = 800;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Viewport viewport = _graphics.GraphicsDevice.Viewport;

            Texture2D littleStarTexture = Content.Load<Texture2D>("LittleStar");
            SpriteFont defaultFont = Content.Load<SpriteFont>("Arial");

            //RotatorObject
            Transform rotatorTransform = new Transform(new Vector2(viewport.Width * 0.2f, viewport.Height * 0.5f));
            SpriteRenderer rotatorRenderer = new SpriteRenderer(littleStarTexture);
            rotatorRenderer.SpriteFont = defaultFont;
            rotatorRenderer.Text = "RotaterObject";
            _rotatorObject = new RotaterObject("RotatorObject", rotatorTransform, rotatorRenderer, 5);

            //OscillatorObject
            Transform oscillatorTransform = new Transform(new Vector2(viewport.Width * 0.5f, viewport.Height * 0.5f));
            SpriteRenderer oscillatorRenderer = new SpriteRenderer(littleStarTexture);
            oscillatorRenderer.SpriteFont = defaultFont;
            oscillatorRenderer.Text = "OscillatorObject";
            _oscillatorObject = new OscillatorObject("OscillatorObject", oscillatorTransform, oscillatorRenderer, 1, 20);

            //ScalerObject
            Transform scalerTransform = new Transform(new Vector2(viewport.Width * 0.8f, viewport.Height * 0.5f));
            SpriteRenderer scalerRenderer = new SpriteRenderer(littleStarTexture);
            scalerRenderer.SpriteFont = defaultFont;
            scalerRenderer.Text = "ScalerObject";
            _scalerObject = new ScalerObject("ScalerObject", scalerTransform, scalerRenderer, 1);
        }

        protected override void Update(GameTime pGameTime)
        {
            _scalerObject.UpdateGameObject(pGameTime);
            _rotatorObject.UpdateGameObject(pGameTime);
            _oscillatorObject.UpdateGameObject(pGameTime);
        }

        protected override void Draw(GameTime pGameTime)
        {
            base.Draw(pGameTime);

            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(SpriteSortMode.BackToFront);

            _scalerObject.DrawGameObject(_spriteBatch);
            _rotatorObject.DrawGameObject(_spriteBatch);
            _oscillatorObject.DrawGameObject(_spriteBatch);

            _spriteBatch.End();
        }
    }
}