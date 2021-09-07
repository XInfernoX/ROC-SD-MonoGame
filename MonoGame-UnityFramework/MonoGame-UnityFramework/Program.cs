using System;

namespace MonoGame_UnityFramework
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using Game2 game = new Game2();
            game.Run();
        }
    }
}
