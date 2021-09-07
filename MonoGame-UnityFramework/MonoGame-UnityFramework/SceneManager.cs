using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame_UnityFramework
{
    //Considering whether to use a Dictionary or a simple List as Scene collections
    //Hypothesus: List has better performance, but more overhead upon collection change. Dictionary has worse performance, but has less overhead upon collection change
    //
    //(Does the collection of scenes get updated that often anyway?)
    //(Is it worth saving the overhead versus optimization?)
    //
    //List will provide better performance over Dictionaries since frequent updates require frequent lookups and Lists "should" be faster
    //Dictionary: provides easy way of Scene lookups by string and forces single entries in the collection, Lists have more overhead upon collection change

    public class SceneManager
    {
        private static SceneManager _instance;

        private List<Scene> _registeredScenes = new List<Scene>();
        private List<Scene> _activeScenes = new List<Scene>();

        private Dictionary<string, Scene> _registeredScenes2 = new Dictionary<string, Scene>();
        private Dictionary<string, Scene> _activeScenes2 = new Dictionary<string, Scene>();

        public SceneManager(Game2 pGame)
        {
            System.Diagnostics.Debug.Assert(_instance == null);

            _instance = this;
        }

        public void UpdateAllActiveScenes(GameTime pGameTime)
        {
            //CONSIDER whether using a List saves computation time
            foreach(KeyValuePair<string, Scene> scene in _activeScenes2)
            {
                scene.Value.UpdateScene(pGameTime);
            }

            foreach(KeyValuePair<string, Scene> scene in _activeScenes2)
            {
                scene.Value.LateUpdateScene(pGameTime);
            }
        }

        public void DrawAllActiveScenes(SpriteBatch pSpriteBatch)
        {
            foreach(KeyValuePair<string, Scene> scene in _activeScenes2)
            {
                scene.Value.DrawScene(pSpriteBatch);
            }
        }



        public static void RegisterScene(Scene pScene)
        {
            _instance.RegisterSceneInternal(pScene);
        }
        private void RegisterSceneInternal(Scene pScene)
        {
            //System.Diagnostics.Debug.Assert(!_registeredScenes.Contains(pScene));

            //_registeredScenes.Add(pScene);



            System.Diagnostics.Debug.Assert(!_registeredScenes2.ContainsKey(pScene.Name));
            System.Diagnostics.Debug.Assert(!_registeredScenes2.ContainsValue(pScene));

            _registeredScenes2.Add(pScene.Name, pScene);
        }

        public static void LoadScene(string pSceneName)
        {
            _instance.LoadSceneInternal(pSceneName);
        }
        private void LoadSceneInternal(string pSceneName)
        {
            //Scene foundScene = null;

            //for (int i = 0; i < _registeredScenes.Count; i++)
            //{
            //    if (_registeredScenes[i].Name == pSceneName)
            //    {
            //        foundScene = _registeredScenes[i];
            //        break;
            //    }
            //}

            //System.Diagnostics.Debug.Assert(foundScene != null);

            //_activeScenes.Add(foundScene);

            System.Diagnostics.Debug.Assert(_registeredScenes2.ContainsKey(pSceneName));
            System.Diagnostics.Debug.Assert(!_activeScenes2.ContainsKey(pSceneName));

            Scene sceneToLoad = _registeredScenes2[pSceneName];

            _activeScenes2.Add(pSceneName, sceneToLoad);
        }

        public void LoadSceneContents(Microsoft.Xna.Framework.Content.ContentManager pContent)
        {
            //CONSIDER whether only active scenes load their content or all registered scenes!
            //Upon changing it to only active scenes, don't forget to load it's content upon loading a new scene!
            foreach(KeyValuePair<string, Scene> scene in _registeredScenes2)
            {
                scene.Value.LoadScene(pContent);
            }
        }
    }
}
