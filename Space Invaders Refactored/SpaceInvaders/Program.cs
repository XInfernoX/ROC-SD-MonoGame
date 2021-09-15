#define USE_REFACTOR

using System;
using Microsoft.Xna.Framework;

using SpaceInvaders.Refactor;

namespace SpaceInvaders
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
#if !USE_REFACTOR
            using Game game = new Game1();
            game.Run();
#else
            using Game game = new RefactoredGame();
            game.Run();
#endif
        }
    }
}
