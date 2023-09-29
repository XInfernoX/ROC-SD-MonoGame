using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ROC_SD_MonoGame.CSharpAdvanced.StateDesignPattern.Assignment3
{
    public class Level3Scene : SceneBase
    {
        private Gate _level2Gate;
        private Gate _level4Gate;

        public Gate Level2Gate => _level2Gate;
        public Gate Level4Gate => _level4Gate;

        public Level3Scene(Game1 pGame, string pSceneName, Player pPlayer) : base(pGame, pSceneName, pPlayer)
        {
        }

        protected override void CreateObjects(ContentManager pContent, Viewport pViewport)
        {
            float third = pViewport.Height / 3.0f;

            //Buttons
            AddGameObject(new MenuButton(_game, _game.ButtonColorScheme));

            //Gates
            _level2Gate = new Gate(new Vector2(400, 50), _player, _game, "Level2");
            AddGameObject(_level2Gate);

            _level4Gate = new Gate(new Vector2(400, 250), _player, _game, "Level4");
            AddGameObject(_level4Gate);
        }
    }
}