//#define USE_EXAMPLE
//#define USE_REFACTOR
#define USE_STATE

using System;
using Microsoft.Xna.Framework;

namespace SpaceInvaders
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
#if USE_EXAMPLE
            using Game game = new SpaceInvaders.Example.Game1();
#elif USE_REFACTOR
            using Game game = new SpaceInvaders.Refactor.SpaceInvadersGame();
#elif USE_STATE
            using Game game = new SpaceInvaders.StateDesignPattern.StateGame();
#endif
            game.Run();
        }
    }
}
