using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BAL
{
    [Serializable()]
    public sealed class Mountain : Surface, ISerializable
{

        public Mountain()
        {
            accessible = false;

            var rand = new Random();
            imageNumber = rand.Next(1,6);
            
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
