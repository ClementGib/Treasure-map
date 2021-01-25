using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BAL
{
    public sealed class Mountain : Surface
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
    }
}
