using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace StateDesignPattern.Assignment3
{
    public class SceneBase
    {
        //Fields
        protected Game1 _game;
        protected string _name;
        protected Player _player;
        protected List<GameObject> _gameObjects = new List<GameObject>();

        private SpriteFont _font;
        private Vector2 _fontPosition;
        private Vector2 _fontOrigin;

        //Properties
        public string Name => _name;

        //Constructors
        public SceneBase(Game1 pGame, string pSceneName, Player pPlayer)
        {
            _game = pGame;
            _name = pSceneName;
            _player = pPlayer;
        }

        //Methods
        public virtual void LoadSceneContent(ContentManager pContent, Viewport pViewPort)
        {
            _font = pContent.Load<SpriteFont>("Arial");
            _fontPosition = new Vector2(pViewPort.Width / 2, 10);
            Vector2 stringSize = _font.MeasureString(_name);
            _fontOrigin = stringSize / 2;

            CreateObjects(pContent, pViewPort);

            for (int i = 0; i < _gameObjects.Count; i++)
            {
                _gameObjects[i].LoadContent(pContent, pViewPort);
            }
        }

        protected virtual void CreateObjects(ContentManager pContent, Viewport pViewPort)
        {
        }

        public void UpdateScene(GameTime pGameTime)
        {
            for (int i = 0; i < _gameObjects.Count; i++)
            {
                _gameObjects[i].Update(pGameTime);
            }
        }

        public void DrawScene(SpriteBatch pSpriteBatch)
        {
            pSpriteBatch.DrawString(_font, _name, _fontPosition, Color.White, 0, _fontOrigin, 1, SpriteEffects.None, 0);

            for (int i = 0; i < _gameObjects.Count; i++)
            {
                _gameObjects[i].Draw(pSpriteBatch);//Probleem van opdracht 2 naar 3 met overloaded functie
            }
        }

        public void AddGameObject(GameObject pNewObject)
        {
            _gameObjects.Add(pNewObject);
        }

        public void RemoveGameObject(GameObject pObject)
        {
            _gameObjects.Remove(pObject);
        }

        public virtual void OnSceneEnter()
        {

        }

        public virtual void OnSceneExit()
        {

        }
    }
}