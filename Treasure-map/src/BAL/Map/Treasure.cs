using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BAL
{
    public sealed class Treasure : Surface
    {

        int amount;

        Treasure(string P_treasureInstruction)
        {

            accessible = true;

            //amount max of treasure is defined as 5
            amount = Int32.Parse(P_treasureInstruction.Split(" - ")[3]) >= 5 ?  5 :  Int32.Parse(P_treasureInstruction.Split(" - ")[3]);
            setImage();
        }


        protected void setImage()
        {

            if (amount >= 5)
            {
                imageNumber = 5;
            }
            else
            {

                if (amount >= 3)
                {
                    imageNumber = 4;
                }
                else
                {
                    if (amount == 2)
                    {
                        imageNumber = 3;
                    }
                    else
                    {
                        if (amount == 1)
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

        public void getTreasureChest()
        {
            amount--;
            setImage();
        }


        public override int getImageValue()
        {
            return imageNumber;
        }
    }
}
