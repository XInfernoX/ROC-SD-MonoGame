using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ComponentDesignPattern.Assignment3
{
    public class Game1 : Game
    {
        private enum TransformTest
        {
            PositionTest,
            RotationTest,
            ScaleTest
        }

        //Fields
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private TransformTest _state;

        private KeyboardState _previousState;

        private GameObject[] _positionTestObjects;
        private RotaterObject[] _rotateTestObjects;
        private ScalerObject[] _scaleTestObjects;

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

            //PositionTestSetup
            _positionTestObjects = new GameObject[4];

            Transform transformTopLeft = new Transform(new Vector2(0, 0), new Vector2(0f, 0f));
            SpriteRenderer spriteRendererTopLeft = new SpriteRenderer(starIndicator);
            spriteRendererTopLeft.SpriteFont = defaultFont;
            spriteRendererTopLeft.Text = $"Position: {transformTopLeft.Position}\nOrigin:{transformTopLeft.Origin}";
            spriteRendererTopLeft.TextAlignment = LocationPresets.BottomLeft;
            spriteRendererTopLeft.TextOrigin = LocationPresets.TopLeft;
            _positionTestObjects[0] = new GameObject("Star1", transformTopLeft, spriteRendererTopLeft);

            Transform transformTopRight = new Transform(new Vector2(viewport.Width, 0), new Vector2(1f, 0f));
            SpriteRenderer spriteRendererTopRight = new SpriteRenderer(starIndicator);
            spriteRendererTopRight.SpriteFont = defaultFont;
            spriteRendererTopRight.Text = $"Position: {transformTopRight.Position}\nOrigin:{transformTopRight.Origin}";
            spriteRendererTopRight.TextAlignment = LocationPresets.BottomRight;
            spriteRendererTopRight.TextOrigin = LocationPresets.TopRight;
            _positionTestObjects[1] = new GameObject("Star2", transformTopRight, spriteRendererTopRight);

            Transform transformBottomLeft = new Transform(new Vector2(0, viewport.Height), new Vector2(0f, 1f));
            SpriteRenderer spriteRendererBottomLeft = new SpriteRenderer(starIndicator);
            spriteRendererBottomLeft.SpriteFont = defaultFont;
            spriteRendererBottomLeft.Text = $"Position: {transformBottomLeft.Position}\nOrigin:{transformBottomLeft.Origin}";
            spriteRendererBottomLeft.TextAlignment = LocationPresets.TopLeft;
            spriteRendererBottomLeft.TextOrigin = LocationPresets.BottomLeft;
            _positionTestObjects[2] = new GameObject("Star3", transformBottomLeft, spriteRendererBottomLeft);

            Transform transformBottomRight = new Transform(new Vector2(viewport.Width, viewport.Height), new Vector2(1f, 1f));
            SpriteRenderer spriteRendererBottomRight = new SpriteRenderer(starIndicator);
            spriteRendererBottomRight.SpriteFont = defaultFont;
            spriteRendererBottomRight.Text = $"Position: {transformBottomRight.Position}\nOrigin:{transformBottomRight.Origin}";
            spriteRendererBottomRight.TextAlignment = LocationPresets.TopRight;
            spriteRendererBottomRight.TextOrigin = LocationPresets.BottomRight;
            _positionTestObjects[3] = new GameObject("Star4", transformBottomRight, spriteRendererBottomRight);


            //RotationTestSetup
            _rotateTestObjects = new RotaterObject[9];

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

                    _rotateTestObjects[i] = new RotaterObject($"RotatorTest{i}", rotatorTransform, rotatorRenderer, 2);
                }
            }

            //ScaleTestSetup
            _scaleTestObjects = new ScalerObject[4];

            Transform scaleTestTransform1 = new Transform(new Vector2(viewport.Width * 0.3f, viewport.Height * 0.3f), new Vector2(0.5f, 0.5f), 0, new Vector2(0.5f, 0.5f));
            SpriteRenderer scaleTestRenderer1 = new SpriteRenderer(starIndicator);
            scaleTestRenderer1.SpriteFont = defaultFont;
            _scaleTestObjects[0] = new ScalerObject("Scaler1", scaleTestTransform1, scaleTestRenderer1, 1);
            scaleTestRenderer1.Text = $"Scale:{scaleTestTransform1.Scale}\nScaleSpeed:{_scaleTestObjects[0].ScaleSpeed}";

            Transform scaleTestTransform2 = new Transform(new Vector2(viewport.Width * 0.7f, viewport.Height * 0.3f), new Vector2(0.5f, 0.5f), 0, new Vector2(1f, 1f));
            SpriteRenderer scaleTestRenderer2 = new SpriteRenderer(starIndicator);
            scaleTestRenderer2.SpriteFont = defaultFont;
            _scaleTestObjects[1] = new ScalerObject("Scaler2", scaleTestTransform2, scaleTestRenderer2, 2);
            scaleTestRenderer2.Text = $"Scale:{scaleTestTransform2.Scale}\nScaleSpeed:{_scaleTestObjects[1].ScaleSpeed}";

            Transform scaleTestTransform3 = new Transform(new Vector2(viewport.Width * 0.3f, viewport.Height * 0.7f), new Vector2(0.5f, 0.5f), 0, new Vector2(1.5f, 1.5f));
            SpriteRenderer scaleTestRenderer3 = new SpriteRenderer(starIndicator);
            scaleTestRenderer3.SpriteFont = defaultFont;
            _scaleTestObjects[2] = new ScalerObject("Scaler3", scaleTestTransform3, scaleTestRenderer3, 3);
            scaleTestRenderer3.Text = $"Scale:{scaleTestTransform3.Scale}\nScaleSpeed:{_scaleTestObjects[2].ScaleSpeed}";

            Transform scaleTestTransform4 = new Transform(new Vector2(viewport.Width * 0.7f, viewport.Height * 0.7f), new Vector2(0.5f, 0.5f), 0, new Vector2(2f, 2f));
            SpriteRenderer scaleTestRenderer4 = new SpriteRenderer(starIndicator);
            scaleTestRenderer4.SpriteFont = defaultFont;
            _scaleTestObjects[3] = new ScalerObject("Scaler4", scaleTestTransform4, scaleTestRenderer4, 4);
            scaleTestRenderer4.Text = $"Scale:{scaleTestTransform4.Scale}\nScaleSpeed:{_scaleTestObjects[3].ScaleSpeed}";

        }

        //Update Methods
        protected override void Update(GameTime pGameTime)
        {
            StateTogglerCheck();

            switch (_state)
            {
                case TransformTest.PositionTest:
                    UpdatePositionTest(pGameTime);
                    break;
                case TransformTest.RotationTest:
                    UpdateRotationTest(pGameTime);
                    break;
                case TransformTest.ScaleTest:
                    UpdateScaleTest(pGameTime);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void UpdatePositionTest(GameTime pGameTime)
        {
            for (int i = 0; i < _positionTestObjects.Length; i++)
            {
                _positionTestObjects[i].UpdateGameObject(pGameTime);
            }
        }

        private void UpdateRotationTest(GameTime pGameTime)
        {
            for (int i = 0; i < _rotateTestObjects.Length; i++)
            {
                _rotateTestObjects[i].UpdateGameObject(pGameTime);
            }
        }

        private void UpdateScaleTest(GameTime pGameTime)
        {
            for (int i = 0; i < _scaleTestObjects.Length; i++)
            {
                _scaleTestObjects[i].UpdateGameObject(pGameTime);
            }
        }

        //Draw Methods
        protected override void Draw(GameTime pGameTime)
        {
            base.Draw(pGameTime);

            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(SpriteSortMode.BackToFront);

            switch (_state)
            {
                case TransformTest.PositionTest:
                    DrawPositionTest();
                    break;
                case TransformTest.RotationTest:
                    DrawRotationTest();
                    break;
                case TransformTest.ScaleTest:
                    DrawScaleTest();
                    break;
            }

            _spriteBatch.End();
        }

        private void DrawPositionTest()
        {
            for (int i = 0; i < _positionTestObjects.Length; i++)
            {
                _positionTestObjects[i].DrawGameObject(_spriteBatch);
            }
        }

        private void DrawRotationTest()
        {
            for (int i = 0; i < _rotateTestObjects.Length; i++)
            {
                _rotateTestObjects[i].DrawGameObject(_spriteBatch);
            }
        }

        private void DrawScaleTest()
        {
            for (int i = 0; i < _scaleTestObjects.Length; i++)
            {
                _scaleTestObjects[i].DrawGameObject(_spriteBatch);
            }
        }

        private void StateTogglerCheck()
        {
            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Space) && !_previousState.IsKeyDown(Keys.Space))//aka OnKeyDown
            {

                int intState = (int)_state;
                intState++;
                intState %= Enum.GetNames(typeof(TransformTest)).Length;

                _state = (TransformTest)intState;


                //_state = (TransformTest)(((int)_state + 1) % Enum.GetNames(typeof(TransformTest)).Length);
            }

            _previousState = keyboard;
        }
    }
}