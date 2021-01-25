using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BAL
{
    public sealed class Treasure : Surface
    {
        const byte number_max = 5;
        private byte numberChest;

        public Treasure(byte P_treasureNumber)
        {

            accessible = true;
            //number_max of treasure is defined as 5
            numberChest = P_treasureNumber >= number_max ? number_max : P_treasureNumber;
            setImage();
        }

        public override bool getAccessible()
        {
            if (numberChest >= 0)
            {
                numberChest--;
                setImage();
            }
            return accessible;
        }


        public override int getImageValue()
        {
            return imageNumber;
        }


        private void setImage()
        {

            if (numberChest >= 5)
            {
                imageNumber = 5;
            }
            else
            {

                if (numberChest >= 3)
                {
                    imageNumber = 4;
                }
                else
                {
                    if (numberChest == 2)
                    {
                        imageNumber = 3;
                    }
                    else
                    {
                        if (numberChest == 1)
                        {
                            imageNumber = 2;
                        }
                        else
                        {
                            imageNumber = 1;
                        }

                    }
                }

            }
        }


    }



}
