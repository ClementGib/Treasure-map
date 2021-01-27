using System;
using System.Runtime.Serialization;
namespace BAL
{
    //Object serializable to JSON
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

        //getter & setter X : horizontal position
        public int X
        {
            get => x;
            set => x = value;
        }

        //getter & setter X : vertical position
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



        //Serialization method
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


    }
}
