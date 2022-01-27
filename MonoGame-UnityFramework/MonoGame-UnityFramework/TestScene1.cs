using Microsoft.Xna.Framework;

namespace MonoGame_UnityFramework
{
    public class TestScene1 : Scene
    {
        public TestScene1(string pName, params GameObject[] pStartingObjects) : base(pName, true, pStartingObjects)
        {
            Transform transform = new Transform(new Vector2(50, 50), 0f, new Vector2(1, 1), new Vector2(0.5f, 0.5f));
            Transform transform2 = new Transform(new Vector2(200, 200), 0f, new Vector2(1, 1), new Vector2(1, 1));

            SpriteRenderer renderer = new SpriteRenderer("tile_0000", Color.White);
            SpriteRenderer renderer2 = new SpriteRenderer("tile_0044", Color.White);

            //Rotator rotator = new Rotator(180f);
            //Rotator rotator2 = new Rotator(360f);

            Player player = new Player(2, 2);
            Player player2 = new Player(3, 3);


            GameObject firstObject = new GameObject("firstObject", pComponents: new Component[] { transform, renderer, player });
            GameObject secondObject = new GameObject("secondObject", pComponents: new Component[] { transform2, renderer2, player2 });

            AddGameObject(firstObject);
            AddGameObject(secondObject);
        }
    }
}
