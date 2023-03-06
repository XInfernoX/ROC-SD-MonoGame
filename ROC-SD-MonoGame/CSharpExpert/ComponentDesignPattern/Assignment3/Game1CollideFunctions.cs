using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

using CSharpExpert.ComponentDesignPattern.Assignment3;

public class Test
{
    private List<GameObject> _gameObjects;

    protected void CollisionCheckAllGameObjects(GameTime pGameTime)//CONSIDER passing pGameTime to CollisionCheck
    {
        //Loop through all GameObjects
        for (int outerI = 0; outerI < _gameObjects.Count - 1; outerI++)
        {
            GameObject outerGameObject = _gameObjects[outerI];

            //Huge optimization: Setting innerI to outerI + 1, this halves the amount of collision checks
            //(Collision A with B == collision B with A, and it does not matter for this implementation
            //This makes sure all possible combinations are checked only once
            for (int innerI = outerI + 1; innerI < _gameObjects.Count; innerI++)
                outerGameObject.CollisionCheck(_gameObjects[innerI]);
        }
    }
}

