using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ComponentDesignPattern.Assignment4
{
    public enum AssignmentState
    {
        StartingPoint,
        EndResult,
        AnimatedSprite
    }

    public class Game1 : Game
    {
        //Fields
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private AssignmentState _state;
        private KeyboardState _previousState;

        //Object given to Students to refactor to Components
        private RotatorObject _rotatorObject;
        private OscillatorObject _oscillatorObject;
        private ColorShifterObject _colorShifterObject;

        //Example for sneakpeak
        private GameObject[] _littleStars;
        Texture2D _littleStarTexture;
        private SpriteFont _defaultFont;

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
            base.LoadContent();

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Viewport viewport = _graphics.GraphicsDevice.Viewport;
            _littleStarTexture = Content.Load<Texture2D>("LittleStar");
            _defaultFont = Content.Load<SpriteFont>("Arial");


            Transform testTransform = new Transform(new Vector2(viewport.Width * 0.5f, viewport.Height * 0.5f));
            SpriteRenderer testRenderer = new SpriteRenderer(_littleStarTexture);
            Rotator testRotator = new Rotator(10);
            Oscillator testOscillator = new Oscillator(10, 2);
            ColorShifter testColorShifter = new ColorShifter(2.0f);
            Scaler testScaler = new Scaler(2.0f);//Optional

            GameObject test = new GameObject("Test", testTransform, 
                testRenderer, testRotator, testOscillator, testColorShifter, testScaler);













            //Starting point for students
            Transform rotatorTransform = new Transform(new Vector2(viewport.Width * 0.2f, viewport.Height * 0.5f));
            SpriteRenderer rotatorRenderer = new SpriteRenderer(_littleStarTexture);
            rotatorRenderer.SpriteFont = _defaultFont;
            rotatorRenderer.Text = "Rotator (A)";
            _rotatorObject = new RotatorObject("RotatorTest", rotatorTransform, rotatorRenderer, 10);

            Transform oscillatorTransform = new Transform(new Vector2(viewport.Width * 0.5f, viewport.Height * 0.5f));
            SpriteRenderer oscillatorRenderer = new SpriteRenderer(_littleStarTexture);
            oscillatorRenderer.SpriteFont = _defaultFont;
            oscillatorRenderer.Text = "Oscillator (B)";
            _oscillatorObject = new OscillatorObject("OscillatorTest", oscillatorTransform, oscillatorRenderer, 1, 10);

            Transform colorShifterTransform = new Transform(new Vector2(viewport.Width * 0.8f, viewport.Height * 0.5f));
            SpriteRenderer colorShifterSpriteRenderer = new SpriteRenderer(_littleStarTexture);
            colorShifterSpriteRenderer.SpriteFont = _defaultFont;
            colorShifterSpriteRenderer.Text = "ColorShifter (C)";
            _colorShifterObject = new ColorShifterObject("ColorShifterTest", colorShifterTransform, colorShifterSpriteRenderer, 0.1f);


            //Example for sneakpeak
            _littleStars = new GameObject[7];
            CreateStar(0, new Vector2(viewport.Width * 0.5f, viewport.Height * 0.1f), false, false, false); // Default

            CreateStar(1, new Vector2(viewport.Width * 0.2f, viewport.Height * 0.3f), true, false, false);
            CreateStar(2, new Vector2(viewport.Width * 0.5f, viewport.Height * 0.3f), false, true, false);
            CreateStar(3, new Vector2(viewport.Width * 0.8f, viewport.Height * 0.3f), false, false, true);

            CreateStar(4, new Vector2(viewport.Width * 0.35f, viewport.Height * 0.6f), true, true, false);
            CreateStar(5, new Vector2(viewport.Width * 0.65f, viewport.Height * 0.6f), false, true, true);

            CreateStar(6, new Vector2(viewport.Width * 0.5f, viewport.Height * 0.8f), true, true, true);

            for (int i = 0; i < _littleStars.Length; i++)
            {
                _littleStars[i].AwakeComponents();
            }

            for (int i = 0; i < _littleStars.Length; i++)
            {
                _littleStars[i].StartComponents();
            }



            //_megaMan = new GameObject()

        }

        private void CreateStar(int pIndex, Vector2 pPosition, bool pRotator, bool pOscillator, bool pColorShifter)
        {
            Transform transform = new Transform(pPosition);
            SpriteRenderer renderer = new SpriteRenderer(_littleStarTexture);

            List<MonoBehaviour> components = new List<MonoBehaviour>();
            string componentsIndicator = "";

            if (pRotator)
            {
                components.Add(new Rotator(5));
                componentsIndicator += "A";
            }

            if (pOscillator)
            {
                components.Add(new Oscillator(1, 60));
                componentsIndicator += "B";

            }

            if (pColorShifter)
            {
                components.Add(new ColorShifter(1));
                componentsIndicator += "C";
            }

            renderer.SpriteFont = _defaultFont;
            renderer.Text = componentsIndicator;

            _littleStars[pIndex] = new GameObject("LittleStar" + pIndex, transform, renderer, components.ToArray());
        }

        protected override void Update(GameTime pGameTime)
        {
            StateTogglerCheck();

            switch (_state)
            {
                case AssignmentState.StartingPoint:
                    _oscillatorObject.UpdateGameObject(pGameTime);
                    _rotatorObject.UpdateGameObject(pGameTime);
                    _colorShifterObject.UpdateGameObject(pGameTime);
                    break;
                case AssignmentState.EndResult:
                    for (int i = 0; i < _littleStars.Length; i++)
                    {
                        _littleStars[i].UpdateGameObject(pGameTime);
                    }
                    break;
                case AssignmentState.AnimatedSprite:

                    break;
            }
        }

        private void StateTogglerCheck()
        {
            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Space) && !_previousState.IsKeyDown(Keys.Space))//aka OnKeyDown
            {
                Console.WriteLine($"before:{_state}");


                int intState = (int)_state;
                intState++;
                intState %= 2;

                _state = (AssignmentState)intState;

                Console.WriteLine($"After:{_state}");

                //_state = (AssignmentState)((int)_state++ % Enum.GetNames(typeof(AssignmentState)).Length);
            }

            _previousState = keyboard;
        }

        protected override void Draw(GameTime pGameTime)
        {
            base.Draw(pGameTime);

            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(SpriteSortMode.BackToFront);

            switch (_state)
            {
                case AssignmentState.StartingPoint:
                    _oscillatorObject.DrawGameObject(_spriteBatch);
                    _rotatorObject.DrawGameObject(_spriteBatch);
                    _colorShifterObject.DrawGameObject(_spriteBatch);
                    break;
                case AssignmentState.EndResult:
                    for (int i = 0; i < _littleStars.Length; i++)
                    {
                        _littleStars[i].DrawGameObject(_spriteBatch);
                    }
                    break;
                case AssignmentState.AnimatedSprite:

                    break;
            }

            _spriteBatch.End();
        }
    }
}