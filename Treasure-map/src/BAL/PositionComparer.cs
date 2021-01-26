using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BAL
{
    //Comparer of Position object 
    public class PositionComparer : IEqualityComparer<Position>
    {
        public bool Equals(Position P_mapPosition, Position P_Position)
        {
            return P_mapPosition.X == P_Position.X && P_mapPosition.Y == P_Position.Y;
        }

        public  int GetHashCode(Position P_Position)
        {


            return P_Position.X.GetHashCode() + P_Position.Y.GetHashCode();
        }

        public static bool EqualsPositions(Position P_mapPosition, Position P_Position)
        {
            PositionComparer Comparer = new PositionComparer();
            return Comparer.Equals(P_mapPosition, P_Position);
        }

        public static bool EqualHashCodePositions(Position P_mapPosition, Position P_Position)
        {
            PositionComparer Comparer = new PositionComparer();
            return Comparer.GetHashCode(P_mapPosition) == Comparer.GetHashCode(P_Position);
        }



    }


}
