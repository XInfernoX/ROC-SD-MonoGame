using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using System.Collections.Generic;
using System.Diagnostics;

namespace MonoGame_UnityFramework
{
    //TODO make abstract once all core functionalties are working and are checked
    public abstract class Scene
    {
        //Fields
        private string _name;
        private GraphicsDevice _graphicsDevice;

        private List<GameObject> _gameObjects = new List<GameObject>();

        //Properties
        public string Name => _name;


        //Constructor
        public Scene(string pName, bool pLoadScene = true, params GameObject[] pStartingObjects)
        {
            _name = pName;
            _gameObjects.AddRange(pStartingObjects);

            SceneManager.RegisterScene(this);

            if (pLoadScene)
                SceneManager.LoadScene(_name);
        }

        //Logic

        public void LoadScene(ContentManager pContent)
        {
            for (int i = 0; i < _gameObjects.Count; i++)
            {
                GameObject go = _gameObjects[i];

                SpriteRenderer renderer = go.GetComponent<SpriteRenderer>();
                renderer?.Load(pContent);
            }
        }

        public void UpdateScene(GameTime pGameTime)
        {
            for (int i = 0; i < _gameObjects.Count; i++)
            {
                _gameObjects[i].Update(pGameTime);
            }
        }

        public void LateUpdateScene(GameTime pGameTime)
        {
            for (int i = 0; i < _gameObjects.Count; i++)
            {
                _gameObjects[i].LateUpdate(pGameTime);
            }
        }

        public void DrawScene(SpriteBatch pSpriteBatch)
        {
            pSpriteBatch.Begin();

            for (int i = 0; i < _gameObjects.Count; i++)
            {
                GameObject go = _gameObjects[i];

                SpriteRenderer renderer = go.GetComponent<SpriteRenderer>();
                renderer?.Draw(go.transform, pSpriteBatch);
            }

            pSpriteBatch.End();
        }
        //public void DrawDefaultSprites()
        //{
        //    _defaultSpriteBatch.Begin();

        //    for (int i = 0; i < _gameObjects.Count; i++)
        //    {
        //        GameObject go = _gameObjects[i];

        //        SpriteRenderer renderer = go.GetComponent<SpriteRenderer>();
        //        renderer?.Draw(go.transform);
        //    }

        //    _defaultSpriteBatch.End();
        //}

        //Object management
        public void AddGameObject(GameObject pGameObject)
        {
            Debug.Assert(pGameObject != null);
            Debug.Assert(!_gameObjects.Contains(pGameObject));

            _gameObjects.Add(pGameObject);
        }
        public void RemoveGameObject(GameObject pGameObject)
        {
            Debug.Assert(pGameObject != null);
            Debug.Assert(_gameObjects.Contains(pGameObject));

            _gameObjects.Remove(pGameObject);
        }

    }
}
