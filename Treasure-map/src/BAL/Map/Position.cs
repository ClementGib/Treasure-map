using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BAL
{
    public class Position
    {
        private int x;
        private int y;

        public Position(int P_x, int P_y)
        {
            x = P_x;
            y = P_y;
        }

        public int X
        {
            get => x;
            set => x = value;
        }

        public int Y
        {
            get => y;
            set => y = value;
        }

        public void setPosition(int P_x, int P_y)
        {
            x = P_x;
            y = P_y;
        }



        /// <summary>
        /// Override method to compar 2 Positions
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            if (x == null || y == null) return 0;
            return x.GetHashCode() + y.GetHashCode();
        }

    }
}
