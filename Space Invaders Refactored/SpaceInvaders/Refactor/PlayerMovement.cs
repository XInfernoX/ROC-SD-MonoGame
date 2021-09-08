using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaders.Refactor
{
    //NOTE Viewport is a struct (value type) won't update when game window size is changed during gameplay!
    public class PlayerMovement : MonoBehaviour
    {
        //Fields
        private float _speed;
        private Viewport _viewport;
        private int _playerWidth;

        //Constructor
        public PlayerMovement(float pSpeed, Viewport pViewPort, int pPlayerWidth)
        {
            _speed = pSpeed;

            _viewport = pViewPort;
            _playerWidth = pPlayerWidth;
        }
        //Copy Constructor-ish
        public override Component Copy()
        {
            return new PlayerMovement(_speed, _viewport, _playerWidth);
        }

        //Event functions
        public override void Update(GameTime pGameTime)
        {
            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.Right))
            {
                Vector2 pos = transform.Position;

                if (pos.X + (_playerWidth / 2) < _viewport.Width)
                {
                    pos.X += _speed;
                    transform.SetPosition(pos);
                }
            }
            if (state.IsKeyDown(Keys.Left))
            {
                Vector2 pos = transform.Position;

                if (pos.X + _playerWidth / 2 > 0)
                {
                    pos.X -= _speed;
                    transform.SetPosition(pos);
                }
            }
        }
    }
}
