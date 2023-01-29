using System;

namespace CSharpAdvanced
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new CSharpAdvanced.StateDesignPattern.Assignment3.Game1())
                game.Run();
        }
    }
}
