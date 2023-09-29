using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ROC_SD_MonoGame.CSharpAdvanced.StateDesignPattern.Assignment3
{
    public class Level4Scene : SceneBase
    {
        private Gate _level3Gate;

        public Gate Level3Gate => _level3Gate;

        public Level4Scene(Game1 pGame, string pSceneName, Player pPlayer) : base(pGame, pSceneName, pPlayer)
        {
        }

        protected override void CreateObjects(ContentManager pContent, Viewport pViewPort)
        {
            _level3Gate = new Gate(new Vector2(500, 500), _player, _game, "Level3");
        }
    }
}