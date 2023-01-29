using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CSharpAdvanced.CSharpAdvanced.StateGameRefactored
{
    public class MenuScene : SceneBase
    {
        public MenuScene(StateGame pGame, string pSceneName, Player pPlayer) : base(pGame, pSceneName, pPlayer)
        {
        }

        public override void LoadSceneContent(ContentManager pContent, Viewport pViewPort)
        {
            //Buttons
            PlayButton playButton = new PlayButton(_game, _game.ButtonColorScheme, "Play");
            AddGameObject(playButton);

            //QuitButton quitButton = new QuitButton(_game,_game.ButtonColorScheme, "Quit");
            //AddGameObject(quitButton);

            base.LoadSceneContent(pContent, pViewPort);
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