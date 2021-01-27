using System;
using System.Runtime.Serialization;

namespace BAL
{
    //Object serializable to JSON
    [Serializable()]
    public sealed class Treasure : Surface, ISerializable
    {

        const byte numberMax = 5;
        private byte numberChest;


        public Treasure(byte P_treasureNumber)
        {

            accessible = true;
            //number_max of treasure is defined as 5
            numberChest = P_treasureNumber >= numberMax ? numberMax : P_treasureNumber;
            setImage();
        }




        //get image number
        public override int getImageNumber()
        {
            return imageNumber;
        }

        //is accessible for the adventurer
        public override bool isAccessible()
        {

            return accessible;
        }




   
        //adventurer take a chest
        public void takeChest()
        {
            if (numberChest >= 0)
            {
                numberChest--;
                setImage();
            }
        }


        public int getChests()
        {
            return numberChest;
        }


  

        //set image show the changement in the UI
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


        //Serialization method
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            try
            {
                info.AddValue("surface", this.GetType().Name);
                info.AddValue("accessible", accessible);
                info.AddValue("imageNumber", imageNumber);
                info.AddValue("numberChest", numberChest);
            }
            catch
            {
                throw new NotImplementedException();
            }
        }

    }



}
