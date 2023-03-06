using System;

namespace ROC_SD_MonoGame
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new CSharpExpert.ComponentDesignPattern.Assignment3.Game1())
                game.Run();
        }
    }
}
