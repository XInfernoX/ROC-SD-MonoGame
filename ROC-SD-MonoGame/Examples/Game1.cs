using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Tiled
{
    public enum States
    {
        Idle,
        Running,
        Jumping
    }

    public enum EquipmentSlot
    {
        Head,
        Left,
        Right,
        Torso,
        Leggings
    }




    public class StatesGame : Game
    {
        public States currentState = States.Idle;

        public Vector2[] equipmentOffsets = new Vector2[]
        {
            new Vector2(0, -1),//Head
            new Vector2(-1, 0),//Left
            new Vector2(1, 0),//Right
            new Vector2(0, 1)
        };

        //Fields - MonoGame
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //Constructor
        public StatesGame()
        {
            currentState = States.Jumping;


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
        }

        protected override void Update(GameTime pGameTime)
        {
            States state = States.Idle;

            int stateInt = (int)state;

            state = States.Jumping;

            if (state == States.Running)//Running
            {
                
            }
            else if(state == States.Jumping)//Jumping
            {
                
            }

            switch (state)
            {
                case States.Idle:
                    break;
                case States.Running:
                    break;
                case States.Jumping:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }



            EquipmentSlot slot = EquipmentSlot.Head;

            Vector2 localPosition = equipmentOffsets[(int) slot];




            base.Update(pGameTime);
        }

        protected override void Draw(GameTime pGameTime)
        {
            base.Draw(pGameTime);

            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.End();
        }
    }
}
