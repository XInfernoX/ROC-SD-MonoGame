﻿namespace CoreRefactored.Interfaces
{
    //CONSIDER whether Origin should be in Transform or in SpriteRenderer

    public interface IComponent
    {
        IComponent Copy();
    }
}
