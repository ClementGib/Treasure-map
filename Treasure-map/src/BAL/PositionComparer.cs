using System;
using System.Collections.Generic;

namespace BAL
{
    //Comparer of object Position
    public class PositionComparer : IEqualityComparer<Position>
    {
        //Compare 2 object Position and return if its equal or not
        public bool Equals(Position P_MapPosition, Position P_Position)
        {
            return P_MapPosition.X == P_Position.X && P_MapPosition.Y == P_Position.Y;
        }

        //outer static method to compare 2 object Position
        public static bool EqualsPositions(Position P_mapPosition, Position P_Position)
        {
            PositionComparer Comparer = new PositionComparer();
            return Comparer.Equals(P_mapPosition, P_Position);
        }


        public int GetHashCode(Position P_Position)
        {


            return P_Position.X.GetHashCode() + P_Position.Y.GetHashCode();
        }

        public static bool EqualHashCodePositions(Position P_MapPosition, Position P_Position)
        {
            PositionComparer Comparer = new PositionComparer();
            return Comparer.GetHashCode(P_MapPosition) == Comparer.GetHashCode(P_Position);
        }



    }


}
