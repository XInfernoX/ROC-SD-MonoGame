using System;

namespace ROC_SD_MonoGame
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            using (var game = new Examples.Ball.CannonPlacer())
                game.Run();
        }
    }
}
