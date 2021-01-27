using System;
using System.Runtime.Serialization;

namespace BAL
{
    //Object serializable to JSON
    [Serializable()]
    public sealed class Plain : Surface, ISerializable
    {
        //Constructor
        public Plain()
        {
            accessible = true;

            var rand = new Random();
            imageNumber = rand.Next(1, 6);
            
        }

        //is accessible for the adventurer
        public override bool isAccessible()
        {
            return accessible;
        }



        //image number of randomize the Map
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

