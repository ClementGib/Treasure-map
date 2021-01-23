using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BAL
{
    public abstract class Surface
{
        protected bool accessible;
        protected int imageNumber;
        public abstract int getImageValue();

    }
}
