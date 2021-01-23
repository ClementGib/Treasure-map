using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BAL
{
    public sealed class Plain : Surface
    {

        public Plain()
        {
            accessible = true;

            var rand = new Random();
            imageNumber = rand.Next(1, 6);
            
        }

        public override int getImageValue()
        {
            return imageNumber;
        }
    }
}
