using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ROC_SD_MonoGame.CSharpAdvanced.StateDesignPattern.Assignment3
{
    public enum PlayerState
    {
        UnArmored,
        Shielded,
        Armed,
        ShieldedAndArmed
    }

    public class Player : GameObject
    {
        //Fields
        private PlayerState _playerState = PlayerState.UnArmored;
        private readonly int _speed;

        private Texture2D[] _textures;

        //Properties
        public PlayerState PlayerState => _playerState;

        public Player(Vector2 pPosition, int pSpeed) : base(pPosition)
        {
            _speed = pSpeed;
        }

        public override void LoadContent(ContentManager pContent, Viewport pViewport)
        {
            _textures = new[] { pContent.Load<Texture2D>("Knight"), pContent.Load<Texture2D>("KnightShield"), pContent.Load<Texture2D>("KnightWeapon"), pContent.Load<Texture2D>("KnightWeaponShield") };

            Texture = _textures[(int)_playerState];
        }

        public void SetPlayerState(PlayerState pState)
        {
            if (_playerState != pState)
            {
                _playerState = pState;
                Texture = _textures[(int)pState];
            }
        }


        public override void Update(GameTime pGameTime)
        {
            Movement(pGameTime);
        }

        private void Movement(GameTime pGameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            Vector2 playerInput = Vector2.Zero;
            if (keyboardState.IsKeyDown(Keys.A))
                playerInput.X--;
            if (keyboardState.IsKeyDown(Keys.D))
                playerInput.X++;
            if (keyboardState.IsKeyDown(Keys.W))
                playerInput.Y--;
            if (keyboardState.IsKeyDown(Keys.S))
                playerInput.Y++;

            if (playerInput != Vector2.Zero)
            {
                playerInput.Normalize();
                Vector2 playerTranslation = playerInput * _speed * (float)pGameTime.ElapsedGameTime.TotalSeconds;
                Position += playerTranslation;
            }
        }
    }
}