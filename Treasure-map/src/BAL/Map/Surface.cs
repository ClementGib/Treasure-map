using System;
using System.Runtime.Serialization;

namespace BAL


//Super-class of Surfaces : Plain, Mountain, Treasure
{
    //Object serializable to JSON
    [Serializable()]
    public abstract class Surface : ISerializable
    {

        protected bool accessible;

        //is accessible for the adventurer
        public abstract bool isAccessible();

        //image number of randomize the Map


        //Serialization method
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            try
            {
                info.AddValue("surface", this.GetType().Name);
                info.AddValue("accessible", accessible);
            }
            catch
            {
                throw new NotImplementedException();
            }
        }
    }
}
