using System;
using System.Runtime.Serialization;

namespace BAL
{
    //Object serializable to JSON
    [Serializable()]
    public class Mountain : Surface, ISerializable
    {

        public Mountain()
        {
            accessible = false;

        }

        public override bool isAccessible()
        {
            return accessible;
        }

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
