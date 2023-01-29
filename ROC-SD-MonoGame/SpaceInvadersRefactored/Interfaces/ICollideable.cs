using Microsoft.Xna.Framework;

using SpaceInvadersRefactored.Components;

namespace SpaceInvadersRefactored.Interfaces
{
    //TODO Continue once different Collider types are introduced
    public interface ICollideable
    {
        public bool CollisionCheck(Collider pOther);//CONSIDER changing to ICollideable parameter type OR make overload functions for each ICollideable implementation

        public bool OverLapCheck(Point pPoint);
    }
}