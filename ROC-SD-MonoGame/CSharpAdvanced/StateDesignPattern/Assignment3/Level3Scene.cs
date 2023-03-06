using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ROC_SD_MonoGame.CSharpAdvanced.StateDesignPattern.Assignment3
{
    public class Level3Scene : SceneBase
    {
        public Level3Scene(Game1 pGame, string pSceneName, Player pPlayer) : base(pGame, pSceneName, pPlayer)
        {
        }

        protected override void CreateObjects(ContentManager pContent, Viewport pViewport)
        {
            float third = pViewport.Height / 3.0f;

            //Buttons
            AddGameObject(new MenuButton(_game, _game.ButtonColorScheme));


            //Gates
            AddGameObject(new Gate(new Vector2(pViewport.Width / 2.0f, 50), _player, _game, "Level2"));
            AddGameObject(new Gate(new Vector2(pViewport.Width / 2.0f, 50), _player, _game, "Level4"));
        }
    }
}