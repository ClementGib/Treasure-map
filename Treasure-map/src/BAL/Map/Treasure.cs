﻿using System;
using System.Runtime.Serialization;

namespace BAL
{
    //Object serializable to JSON
    [Serializable()]
    public class Treasure : Surface, ISerializable
    {

        const byte numberMax = 5;
        private byte numberChest;


        public Treasure(byte P_treasureNumber)
        {

            accessible = true;
            //number_max of treasure is defined as 5
            numberChest = P_treasureNumber >= numberMax ? numberMax : P_treasureNumber;
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
            }
        }


        public int getChests()
        {
            return numberChest;
        }




        //Serialization method
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            try
            {
                info.AddValue("surface", this.GetType().Name);
                info.AddValue("accessible", accessible);
                info.AddValue("numberChest", numberChest);
            }
            catch
            {
                throw new NotImplementedException();
            }
        }

    }



}
