using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ROC_SD_MonoGame.CSharpAdvanced.StateDesignPattern.Assignment3
{
    public class Level1Scene : SceneBase
    {
        //Fields
        private Gate _level2Gate;

        //Properties
        public Gate Level2Gate => _level2Gate;


        public Level1Scene(Game1 pGame, string pSceneName, Player pPlayer) : base(pGame, pSceneName, pPlayer)
        {

        }

        protected override void CreateObjects(ContentManager pContent, Viewport pViewport)
        {
            float third = pViewport.Height / 3.0f;

            //Buttons
            AddGameObject(new MenuButton(_game, _game.ButtonColorScheme, new Vector2(1, 0)));

            //Pickups
            AddGameObject(new Shield(_player));
            AddGameObject(new Weapon(_player));

            //Enemy
            Texture2D wayPointTexture = pContent.Load<Texture2D>("Flag");
            GameObject[] wayPoints = {
                new GameObject(new Vector2(pViewport.Width * 0.1f, pViewport.Height * 0.1f), wayPointTexture),
                new GameObject(new Vector2(pViewport.Width * 0.9f, pViewport.Height * 0.1f), wayPointTexture),
                new GameObject(new Vector2(pViewport.Width / 2.0f, pViewport.Height / 2.0f), wayPointTexture)
            };

            for (int i = 0; i < wayPoints.Length; i++)
                AddGameObject(wayPoints[i]);

            AddGameObject(new Enemy(new Vector2(pViewport.Width / 2.0f, third), pContent.Load<Texture2D>("Enemy"), 100, wayPoints, _player, 100));

            //Gates
            _level2Gate = new Gate(new Vector2(100, 50), _player, _game, "Level2");
            AddGameObject(_level2Gate);
        }
    }
}