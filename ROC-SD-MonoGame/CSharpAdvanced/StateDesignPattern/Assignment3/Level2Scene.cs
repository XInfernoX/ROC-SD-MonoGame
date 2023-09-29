using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ROC_SD_MonoGame.CSharpAdvanced.StateDesignPattern.Assignment3
{
    public class Level2Scene : SceneBase
    {
        //Fields
        private Gate _level1Gate;
        private Gate _level3Gate;

        //Properties
        public Gate Level1Gate => _level1Gate;
        public Gate Level3Gate => _level1Gate;

        public Level2Scene(Game1 pGame, string pSceneName, Player pPlayer) : base(pGame, pSceneName, pPlayer)
        {
        }

        protected override void CreateObjects(ContentManager pContent, Viewport pViewport)
        {
            float third = pViewport.Height / 3.0f;

            //Buttons
            AddGameObject(new MenuButton(_game, _game.ButtonColorScheme, new Vector2(1, 0)));

            //Gates
            _level1Gate = new Gate(new Vector2(200, 50), _player, _game, "Level1");
            AddGameObject(_level1Gate);


            _level3Gate = new Gate(new Vector2(200, 300), _player, _game, "Level3");
            AddGameObject(_level3Gate);
        }
    }
}