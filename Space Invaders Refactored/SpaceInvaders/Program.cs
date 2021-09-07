#define USE_REFACTOR

using System;
using Microsoft.Xna.Framework;

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
            using Game game = new Game();
            game.Run();
#endif
        }
    }
}
