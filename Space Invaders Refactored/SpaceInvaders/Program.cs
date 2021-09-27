//#define REFACTORED
//#define SPACE_INVADERS
#define STATE_GAME

using System;
using Microsoft.Xna.Framework;

namespace CoreExample
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
#if SPACE_INVADERS && !REFACTORED
            using Game game = new SpaceInvaders.Example.SpaceInvaders();
#elif SPACE_INVADERS && REFACTORED
            using Game game = new SpaceInvadersRefactored.SpaceInvadersGame();
#elif STATE_GAME && !REFACTORED
            using Game game = new StateGame.StateGame();

#elif STATE_GAME && REFACTORED
            using Game game = new StateGameRefactored.StateGameRefactored();
#endif
            game.Run();
        }
    }
}
