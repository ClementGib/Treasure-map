using System;
using System.Runtime.Serialization;

namespace BAL
{

    //Object serializable to JSON
    [Serializable()]
    public class Adventurer : ISerializable
    {
        //Name Adventurer
        string name;

        //Position on the map
        byte xPosition;
        byte yPosition;

        //orientation (Notrh, South, East, West)
        char orientation;

        //array of movement
        char[] movement;

        //step of movements array set to 0 before the adventurer moves 
        byte movementStep = 0;

        public int XPosition
        {
            get => xPosition;
        }

        public int YPosition
        {
            get => yPosition;
        }

        public char Orientation
        {
            get => orientation;
        }

        public char[] Movement
        {
            get => movement;
        }



        public Adventurer(string P_adventurer_instruction)
        {
            name = P_adventurer_instruction.Split(" - ")[0];
            yPosition = Byte.Parse(P_adventurer_instruction.Split(" - ")[1]);
            xPosition = Byte.Parse(P_adventurer_instruction.Split(" - ")[2]);

            orientation = P_adventurer_instruction.Split(" - ")[3].ToCharArray()[0];

            movement = P_adventurer_instruction.Split(" - ")[4].ToCharArray();
        }




        //get the next position of Adventurer
        public Position getNextPosition()
        {
            switch (orientation)
            {
                //North
                case 'N':
                    return new Position(xPosition, yPosition - 1);

                //South
                case 'S':
                    return new Position(xPosition, yPosition + 1);


                //East
                case 'E':
                    return new Position(xPosition + 1, yPosition);

                //West
                case 'W':
                    return new Position(xPosition - 1, yPosition);

                default:
                    return new Position(xPosition, yPosition);
            }

        }


        /* Movement*/

        //check if possible to move
        public Position wantMove()
        {
            if (movementStep < movement.Length)
            {
                if (movement[movementStep] == 'A')
                {
                    // return the next position   
                    return getNextPosition();

                }
                else
                {
                    if (movement[movementStep] == 'D' || movement[movementStep] == 'G')
                    {

                        return new Position(xPosition, yPosition);

                    }
                    else
                    {
                        throw new ArgumentException("Adventurer movement are incorrectly defined");
                    }
                }
            }

            // return current position
            return new Position(xPosition, yPosition);

        }


        /* Movement */
        //Move to the next the next position
        public void moveToNextStep()
        {
            if (movementStep < movement.Length)
            {
                if (movement[movementStep] == 'A')
                {
                    forwardMovement();

                }
                else
                {
                    if (movement[movementStep] == 'D' || movement[movementStep] == 'G')
                    {

                        spinMovement(movement[movementStep]);


                    }
                    else
                    {
                        throw new ArgumentException("Adventurer movement are incorrectly defined");
                    }
                }
            }

        }

        //Move forward
        public void forwardMovement()
        {
            switch (orientation)
            {
                //North
                case 'N':
                    --yPosition;
                    movementStep++;
                    break;

                //South
                case 'S':
                    ++yPosition;
                    movementStep++;
                    break;

                //East
                case 'E':
                    ++xPosition;
                    movementStep++;
                    break;

                //West
                case 'W':
                    --xPosition;
                    movementStep++;
                    break;
            }

        }


        //Spin movement
        public void spinMovement(char P_movement)
        {
            if (P_movement == 'D')
            {
                switch (orientation)
                {
                    //North
                    case 'N':
                        orientation = 'E';
                        movementStep++;
                        break;

                    //South
                    case 'S':
                        orientation = 'W';
                        movementStep++;
                        break;

                    //East
                    case 'E':
                        orientation = 'S';
                        movementStep++;
                        break;

                    //West
                    case 'W':
                        orientation = 'N';
                        movementStep++;
                        break;

                    default:
                        break;
                }
            }

            if (P_movement == 'G')
            {
                switch (orientation)
                {
                    //North
                    case 'N':
                        orientation = 'W';
                        movementStep++;
                        break;

                    //South
                    case 'S':
                        orientation = 'E';
                        movementStep++;
                        break;

                    //East
                    case 'E':
                        orientation = 'N';
                        movementStep++;
                        break;

                    //West
                    case 'W':
                        orientation = 'S';
                        movementStep++;
                        break;

                    default:
                        break;
                }
            }


        }

        //Serialization method
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            try
            {
                info.AddValue("name", name);
                info.AddValue("yPosition", yPosition);
                info.AddValue("xPosition", xPosition);
                info.AddValue("orientation", orientation);
                info.AddValue("movement", movement);
                info.AddValue("movementStep", movementStep);
            }
            catch
            {
                throw new NotImplementedException();
            }


        }
    }

}

/*            _______
         ..-'`       ````---.
       .'          ___ .'````.'SS'.
      /        ..-SS####'.  /SSHH##'.
     |       .'SSSHHHH##|/#/#HH#H####'.
    /      .'SSHHHHH####/||#/: \SHH#####\
   /      /SSHHHHH#####/!||;`___|SSHH###\
-..__    /SSSHHH######.         \SSSHH###\
`.'-.''--._SHHH#####.'           '.SH####/
  '. ``'-  '/SH####`/_             `|H##/
  | '.     /SSHH###|`'==.       .=='/\H|
  |   `'-.|SHHHH##/\__\/        /\//|~|/
  |    |S#|/HHH##/             |``  |
  |    \H' |H#.'`              \    |
  |        ''`|               -     /
  |          /H\          .----    /
  |         |H#/'.           `    /
  |          \| | '..            /
  |    ^~DLF   /|    ''..______.'
   \          //\__    _..-. | 
    \         ||   ````     \ |_
     \    _.-|               \| |_
     _\_.-'   `'''''-.        |   `--.
 ''``    \            `''-;    \ /
          \      .-'|     ````.' -
          |    .'  `--'''''-.. |/
          |  .'               \|
          |*/