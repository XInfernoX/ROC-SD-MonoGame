using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using SpaceInvaders.Refactor.Core.Components;
using IUpdateable = SpaceInvaders.Refactor.Core.IUpdateable;

namespace SpaceInvaders.StateDesignPattern
{
    public class Button : Component, IUpdateable
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

        public void Update(GameTime pGameTime)
        {
            MouseState state = Mouse.GetState();

            if(_collider.OverLapCheck(state.Position))
            {
                Console.WriteLine($"Button overlap!");
            }
        }

        public void LateUpdate(GameTime pGameTime)
        {
        }
    }
}