using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.Refactor.Core;
using SpaceInvaders.Refactor.Core.Components;

namespace SpaceInvaders.StateDesignPattern
{
    public enum ButtonState
    {
    }

    public class Button : MonoBehaviour
    {
        private Action OnButtonHoverEnter = delegate { };
        private Action OnButtonHoverExit = delegate { };
        private Action OnButtonClick = delegate { };

        private SpriteRenderer _spriteRenderer;
        private Collider _collider;

        public Button(SpriteRenderer pRenderer, Collider pCollider)
        {
            _spriteRenderer = pRenderer;
            _collider = pCollider;
        }

        public override void Update(GameTime pGameTime)
        {
            MouseState state = Mouse.GetState();

            if(_collider.OverLapCheck(state.Position))
            {
                Console.WriteLine($"Button overlap!");
            }

            base.Update(pGameTime);
        }
    }






    public class StateGame : Game
    {
        public StateGame()
        {

        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            SpriteRenderer keyRenderer = new SpriteRenderer(Content.Load<Texture2D>("key"));
            //GameObject keyButton = new GameObject();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }


    }
}
