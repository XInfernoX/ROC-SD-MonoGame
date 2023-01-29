using System;
using System.Collections.Generic;

namespace CSharpAdvanced.CSharpAdvanced.StateDesignPattern.Assignment3
{
    public class SceneManger
    {
        public class SceneComponent { }

        public class MenuScene2 : SceneComponent
        {

        }

        private List<SceneComponent> _scenes = new List<SceneComponent>();

        private void Test()
        {
            ChangeStateTo<MenuScene2>();
            ChangeStateTo<SceneComponent>();
        }

        public void ChangeStateTo<T>() where T : SceneComponent
        {
            for (int i = 0; i < _scenes.Count; i++)
            {
                if (_scenes[i].GetType() == typeof(T))
                {
                    //Load _scenes[i]

                }
                else
                {
                    SceneComponent newScene = (T)Activator.CreateInstance(typeof(T)/*, param1, param2*/);
                }
            }
        }
    }
}