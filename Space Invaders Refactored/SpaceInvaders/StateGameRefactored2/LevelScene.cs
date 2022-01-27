using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace StateGameRefactored2
{
    public class LevelScene : SceneBase
    {
        public LevelScene(StateGame pGame, string pSceneName, Player pPlayer) : base(pGame, pSceneName, pPlayer)
        {
        }

        protected override void CreateObjects(ContentManager pContent, Viewport pViewPort)
        {
            Viewport viewPort = _game.GraphicsDevice.Viewport;
            float third = viewPort.Height / 3.0f;

            //Buttons
            AddGameObject(new MenuButton(_game, _game.ButtonColorScheme, "Menu"));

            //Pickups
            GameObject shield = new Shield(new Vector2(100, third), pContent.Load<Texture2D>("Shield"), _player);
            GameObject weapon = new Weapon(new Vector2(viewPort.Width - 100, third), pContent.Load<Texture2D>("Weapon"), _player);
            AddGameObject(shield);
            AddGameObject(weapon);
            
            //Enemy
            Texture2D wayPointTexture = pContent.Load<Texture2D>("Flag");
            GameObject[] wayPoints = {
                new GameObject(new Vector2(viewPort.Width * 0.1f, viewPort.Height * 0.1f), wayPointTexture),
                new GameObject(new Vector2(viewPort.Width * 0.9f, viewPort.Height * 0.1f), wayPointTexture),
                new GameObject(new Vector2(viewPort.Width / 2.0f, viewPort.Height / 2.0f), wayPointTexture)
            };

            for (int i = 0; i < wayPoints.Length; i++)
                AddGameObject(wayPoints[i]);

            
            AddGameObject(new Enemy(new Vector2(viewPort.Width / 2.0f, third), pContent.Load<Texture2D>("Enemy"), 100, wayPoints, _player, 100, shield, weapon));

            Gate gate = new Gate(new Vector2(pViewPort.Width / 2.0f, 50), pContent.Load<Texture2D>("Gate"), _game, _player, "Level2", new Vector2(0, 50),_game.DefaultFont);
            AddGameObject(gate);
        }
    }

    public class Level2Scene : SceneBase
    {
        public Level2Scene(StateGame pGame, string pSceneName, Player pPlayer) : base(pGame, pSceneName, pPlayer)
        {
        }

        protected override void CreateObjects(ContentManager pContent, Viewport pViewPort)
        {
            Gate level1Gate = new Gate(new Vector2(pViewPort.Width / 2.0f, 50), pContent.Load<Texture2D>("Gate"), _game, _player, "Level1", new Vector2(0, 50),_game.DefaultFont);
            AddGameObject(level1Gate);


        }
    }

    public class Level3Scene : SceneBase
    {
        public Level3Scene(StateGame pGame, string pSceneName, Player pPlayer) : base(pGame, pSceneName, pPlayer)
        {
        }

        protected override void CreateObjects(ContentManager pContent, Viewport pViewPort)
        {
            Gate level2Gate = new Gate(new Vector2(pViewPort.Width / 2.0f, 50), pContent.Load<Texture2D>("Gate"), _game, _player, "Level2", new Vector2(0, 50), _game.DefaultFont);
            AddGameObject(level2Gate);

            Gate level4Gate = new Gate(new Vector2(pViewPort.Width / 2.0f, 50), pContent.Load<Texture2D>("Gate"), _game, _player, "Level4", new Vector2(0, 50), _game.DefaultFont);
            AddGameObject(level4Gate);
        }
    }

    public class Level4Scene : SceneBase
    {
        public Level4Scene(StateGame pGame, string pSceneName, Player pPlayer) : base(pGame, pSceneName, pPlayer)
        {
        }

        protected override void CreateObjects(ContentManager pContent, Viewport pViewPort)
        {

        }
    }
}