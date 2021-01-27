using System;
using System.Runtime.Serialization;

namespace BAL
{
    //Object serializable to JSON
    [Serializable()]
    public sealed class Mountain : Surface, ISerializable
    {

        public Mountain()
        {
            accessible = false;

            var rand = new Random();
            imageNumber = rand.Next(1, 6);

        }

        public override bool isAccessible()
        {
            return accessible;
        }

        public override int getImageNumber()
        {
            return imageNumber;
        }

        //Serialization method
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            try
            {
                info.AddValue("surface", this.GetType().Name);
                info.AddValue("accessible", accessible);
                info.AddValue("imageNumber", imageNumber);
            }
            catch
            {
                throw new NotImplementedException();
            }
        }
    }
}
