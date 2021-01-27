using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace BAL
{
    [Serializable()]
    public sealed class Plain : Surface, ISerializable
    {

        public Plain()
        {
            accessible = true;

            var rand = new Random();
            imageNumber = rand.Next(1, 6);
            
        }

        public override bool getAccessible()
        {
            return accessible;
        }

        public override int getImageValue()
        {
            return imageNumber;
        }

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

