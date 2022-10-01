using System;

using Microsoft.Xna.Framework;

public static class Program
{
    public enum GameType
    {
        Assignment1,
        StateGameComplete,
        StateGameRefactored1,
        StateGameRefactored2,
        StateGameRefactored3,
        SpaceInvaders,
        SpaceInvadersRefactored
    }

    [STAThread]
    private static void Main()
    {
        #region Advanced1



        ////using (Game1 game = new Game1())
        ////    game.Run();


        GameType type = GameType.Assignment1;                   //End result of Assignment 1
        //GameType type = GameType.StateGameComplete;             //Assignment 2 - Intermediate result (students refactor this to StateGameRefactored1)
        //GameType type = GameType.StateGameRefactored1;        //Enums + basic classes
        //GameType type = GameType.StateGameRefactored2;        //Polymorphism + inheritance
        //GameType type = GameType.StateGameRefactored3;        //States in separate classes + FSM
        //GameType type = GameType.SpaceInvaders;               //StartPoint end year 2
        //GameType type = GameType.SpaceInvadersRefactored;     //Component pattern applied


        switch (type)
        {
            case GameType.Assignment1:
                using (Game game = new StateDesignPattern.Assignment1.Game1())
                    game.Run();
                break;

            case GameType.StateGameComplete:
                using (Game game = new StateDesignPattern.Assignment1.Game2())
                    game.Run();
                break;

            case GameType.StateGameRefactored1:
                using (Game game = new StateGameRefactored.StateGame())
                    game.Run();
                break;

            case GameType.StateGameRefactored2:
                using (Game game = new StateGameRefactored2.StateGame())
                    game.Run();
                break;


            case GameType.SpaceInvaders:
                using (Game game = new SpaceInvaders.SpaceInvaders())
                    game.Run();
                break;

            case GameType.SpaceInvadersRefactored:
                using (Game game = new SpaceInvadersRefactored.SpaceInvaders())
                    game.Run();
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }

        #endregion

        #region Advanced2

        //using (Game game = new ComponentDesignPattern.Assignment5.Game1())
        //    game.Run();
        #endregion
    }
}
