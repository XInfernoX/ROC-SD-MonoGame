//#define USE_EXAMPLE
//#define USE_REFACTOR
#define USE_STATE

using System;
using Microsoft.Xna.Framework;

using SpaceInvaders.Example;
using SpaceInvaders.Refactor;
using SpaceInvaders.StateDesignPattern;

namespace SpaceInvaders
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
#if USE_EXAMPLE
            using Game game = new Game1();
#elif USE_REFACTOR
            using Game game = new SpaceInvadersGame();
#elif USE_STATE
            using Game game = new StateGame();
#endif
            game.Run();
        }
    }
}
