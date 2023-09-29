using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ROC_SD_MonoGame.CSharpAdvanced.StateDesignPattern.Assignment2
{
    public class Player : GameObject
    {
        //Fields
        private float _speed;
        private bool _hasWeapon;
        private bool _hasShield;

        //Fields - Textures
        private Texture2D _playerTexture;
        private Texture2D _playerShieldTexture;
        private Texture2D _playerWeaponTexture;
        private Texture2D _playerWeaponShieldTexture;

        //Properties
        public bool HasWeapon => _hasWeapon;
        public bool HasShield => _hasShield;

        //Constructor
        public Player(Vector2 pPosition, float pSpeed, ContentManager pContent) : base(pPosition)
        {
            _speed = pSpeed;

            _playerTexture = pContent.Load<Texture2D>("Knight");
            _playerShieldTexture = pContent.Load<Texture2D>("KnightShield");
            _playerWeaponTexture = pContent.Load<Texture2D>("KnightWeapon");
            _playerWeaponShieldTexture = pContent.Load<Texture2D>("KnightWeaponShield");

            Texture = _playerTexture;
        }

        //Methods
        public override void Update(GameTime pGameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            Movement(pGameTime, keyboardState);
        }

        private void Movement(GameTime pGameTime, KeyboardState pKeyboardState)
        {
            //Player input registration
            Vector2 playerInput = Vector2.Zero;
            if (pKeyboardState.IsKeyDown(Keys.A))
                playerInput.X--;
            if (pKeyboardState.IsKeyDown(Keys.D))
                playerInput.X++;
            if (pKeyboardState.IsKeyDown(Keys.W))
                playerInput.Y--;
            if (pKeyboardState.IsKeyDown(Keys.S))
                playerInput.Y++;

            if (playerInput != Vector2.Zero)
            {
                playerInput.Normalize();
                Vector2 playerTranslation = playerInput * _speed * (float)pGameTime.ElapsedGameTime.TotalSeconds;
                Position += playerTranslation;
            }
        }


        public void AddWeapon()
        {
            System.Console.WriteLine("AddWeapon()");

            _hasWeapon = true;
            if (Texture == _playerTexture)
            {
                Texture = _playerWeaponTexture;
            }
            else
            {
                Texture = _playerWeaponShieldTexture;
            }
        }

        public void AddShield()
        {
            _hasShield = true;
            if (Texture == _playerTexture)
            {
                Texture = _playerShieldTexture;
            }
            else
            {
                Texture = _playerWeaponShieldTexture;
            }
        }
    }
}
