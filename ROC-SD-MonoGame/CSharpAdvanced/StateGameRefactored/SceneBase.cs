using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CSharpAdvanced.CSharpAdvanced.StateGameRefactored
{
    public class SceneBase
    {
        //Fields
        protected StateGame _game;
        protected string _name;
        protected Player _player;
        protected List<GameObject> _gameObjects = new List<GameObject>();

        //Properties
        public string Name => _name;

        //Constructors
        public SceneBase(StateGame pGame, string pSceneName, Player pPlayer)
        {
            _game = pGame;
            _name = pSceneName;
            _player = pPlayer;
        }

        //Methods
        public virtual void LoadSceneContent(ContentManager pContent, Viewport pViewPort)
        {
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