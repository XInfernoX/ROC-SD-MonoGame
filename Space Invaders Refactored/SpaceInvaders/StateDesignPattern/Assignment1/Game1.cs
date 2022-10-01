using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StateDesignPattern.Assignment1
{
    //Class
    //Namespace
    //Inheritance
    //Access modifiers
    //virtual + override
    //Constructor

    //Rectangle tip
    //Active tip






    public class Game1 : Game
    {
        //Fields
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //Player
        private Player _player;

        //Interactables
        private Shield _shield;
        private Weapon _weapon;
        private Gate _gate;

        //Constructor
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //Viewport information
            Viewport viewPort = GraphicsDevice.Viewport;
            float third = viewPort.Height / 3;

            //Player
            _player = new Player(new Vector2(viewPort.Width * 0.5f, viewPort.Height * 0.66f), 140);
            _player.LoadContent(Content);

            //Pickups
            _weapon = new Weapon(new Vector2(100, third), _player);
            _weapon.LoadContent(Content);

            _shield = new Shield(new Vector2(viewPort.Width - 100, third), _player);
            _shield.LoadContent(Content);

            _gate = new Gate(new Vector2(viewPort.Width - 75, 10), _player, this);
            _gate.LoadContent(Content);
        }

        protected override void Update(GameTime pGameTime)
        {
            base.Update(pGameTime);

            _player.Update(pGameTime);
            _shield.Update(pGameTime);
            _weapon.Update(pGameTime);
            _gate.Update(pGameTime);
        }

        protected override void Draw(GameTime pGameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _player.Draw(_spriteBatch);
            _shield.Draw(_spriteBatch);
            _weapon.Draw(_spriteBatch);
            _gate.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(pGameTime);
        }

    }
}

