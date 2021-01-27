using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace BAL
{
    [Serializable()]
    public abstract class Surface  : ISerializable
    {
        protected bool accessible;
        protected int imageNumber;


        public abstract bool getAccessible();
        public abstract int getImageValue();

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
