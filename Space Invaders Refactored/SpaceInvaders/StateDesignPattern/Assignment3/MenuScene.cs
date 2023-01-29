using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace StateDesignPattern.Assignment3
{
    public class MenuScene : SceneBase
    {
        public MenuScene(Game1 pGame, string pSceneName, Player pPlayer) : base(pGame, pSceneName, pPlayer)
        {
        }

        public override void LoadSceneContent(ContentManager pContent, Viewport pViewPort)
        {
            //Buttons
            PlayButton playButton = new PlayButton(new Vector2(pViewPort.Width /2, pViewPort.Height * 0.33f),_game, _game.ButtonColorScheme, "Play");
            AddGameObject(playButton);

            QuitButton quitButton = new QuitButton(new Vector2(pViewPort.Width / 2, pViewPort.Height * 0.66f),_game,_game.ButtonColorScheme, "Quit");
            AddGameObject(quitButton);

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