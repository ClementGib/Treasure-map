using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace BAL
{
    [Serializable()]
    public class Position : ISerializable
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

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            try
            {
                info.AddValue("x", x);
                info.AddValue("y", y);
            }
            catch
            {
                throw new NotImplementedException();
            }
        }

        public void setPosition(int P_x, int P_y)
        {
            x = P_x;
            y = P_y;
        }



    }
}
