using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BAL
{
    public sealed class Mountain : Surface
{

        Mountain()
        {
            accessible = false;

            var rand = new Random();
            imageNumber = rand.Next(1,6);
            
        }

        public override int getImageValue()
        {
            return imageNumber;
        }
    }
}
