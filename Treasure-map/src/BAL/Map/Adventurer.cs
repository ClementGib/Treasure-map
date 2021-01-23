using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BAL
{
    public sealed class Adventurer
    {
        string name;
        byte xPosition;
        byte yPosition;
        
        char orientation;
        char[] movement;
        byte movementStep;

        Adventurer(string P_adventurer_instruction)
        {
            name = P_adventurer_instruction.Split(" - ")[1];
            yPosition = Byte.Parse(P_adventurer_instruction.Split(" - ")[2]);
            xPosition = Byte.Parse(P_adventurer_instruction.Split(" - ")[3]);

            orientation = P_adventurer_instruction.Split(" - ")[4].ToCharArray()[0];

            movement = P_adventurer_instruction.Split(" - ")[5].ToCharArray();

            //Init movement step to 0 before adventurer moves 
            movementStep = 0;
        }

        public Tuple<byte,byte> checkNextPosition()
        {

            switch (orientation)
            {
                //North
                case 'N':
                    return Tuple.Create(xPosition,--yPosition);
                    break;

                //South
                case 'S':
                    return Tuple.Create(xPosition, ++yPosition);
                    break;

                //East
                case 'E':
                    return Tuple.Create(++xPosition, yPosition);
                    break;

                //West
                case 'W':
                    return Tuple.Create(--xPosition, yPosition);
                    break;

                default:
                    return Tuple.Create(xPosition, yPosition);
            }

        }
    }
}
