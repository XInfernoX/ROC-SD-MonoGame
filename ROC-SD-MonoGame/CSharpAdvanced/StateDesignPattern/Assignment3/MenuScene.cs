using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ROC_SD_MonoGame.CSharpAdvanced.StateDesignPattern.Assignment3
{
    public class MenuScene : SceneBase
    {
        public MenuScene(Game1 pGame, string pSceneName, Player pPlayer) : base(pGame, pSceneName, pPlayer)
        {
        }

        protected override void CreateObjects(ContentManager pContent, Viewport pViewPort)
        {
            PlayButton playButton = new PlayButton(_game, _game.ButtonColorScheme);
            AddGameObject(playButton);

            QuitButton quitButton = new QuitButton(_game, _game.ButtonColorScheme);
            AddGameObject(quitButton);
        }

        public override void OnSceneEnter()
        {
            _player.Active = false;
        }

        public override void OnSceneExit()
        {
            _player.Active = true;
        }
    }
}