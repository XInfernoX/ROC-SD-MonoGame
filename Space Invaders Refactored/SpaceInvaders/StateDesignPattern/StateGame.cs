using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SpaceInvaders.Refactor;
using SpaceInvaders.Refactor.Core;
using SpaceInvaders.Refactor.Core.Components;

namespace SpaceInvaders.StateDesignPattern
{
    public class StateGame : RefactoredGameBase
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

            Viewport viewport = GraphicsDevice.Viewport;

            SpriteRenderer keyRenderer = new SpriteRenderer(Content.Load<Texture2D>("tile_0009"));
            Collider keyCollider = new Collider(keyRenderer);
            Button keyButton = new Button(keyRenderer, keyCollider);
            GameObject button = new GameObject(this, "Key", new Vector2(0f, 0f),new Vector2(0.9f, 0.5f),0, new Vector2(10,10),keyRenderer, keyCollider, keyButton);
            AddGameObject(button);
        }

        protected override void Update(GameTime pGameTime)
        {
            base.Update(pGameTime);
        }

        protected override void Draw(GameTime pGameTime)
        {
            base.Draw(pGameTime);
        }
    }
}
