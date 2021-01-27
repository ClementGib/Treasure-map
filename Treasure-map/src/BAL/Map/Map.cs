using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using DAL;


namespace BAL
{
    [Serializable()]
    public sealed class Map : ISerializable
    {
        // Singleton instance
        private static Map instance = null;


        private const byte width_max = 125;
        private const byte height_max = 250;

        //X axe
        private static byte width = 0;
        //Y axe
        private static byte height = 0;

        // 2D map grid of surfaces with position on the matrix (Dictionary<TKey,TValue>)
        private static Dictionary<Position, Surface> mapGrids = new Dictionary<Position, Surface>();

        // Adventurer of the Map 
        Adventurer TheAdventurer;

        public Adventurer Adventurer
        {
            get => TheAdventurer;
        }

        /* Init Map */
        public Map(Instruction P_Instruction)
        {

            try
            {
                //check map size 
                if ((Byte.Parse(P_Instruction.Map_instruction.Split(" - ")[0]) > 0 && Byte.Parse(P_Instruction.Map_instruction.Split(" - ")[0]) < width_max)
                    &&
                    (Byte.Parse(P_Instruction.Map_instruction.Split(" - ")[1]) > 0 && Byte.Parse(P_Instruction.Map_instruction.Split(" - ")[1]) < height_max))
                {
                    width = Byte.Parse(P_Instruction.Map_instruction.Split(" - ")[0]);
                    height = Byte.Parse(P_Instruction.Map_instruction.Split(" - ")[1]);
                }
                else
                {
                    throw new ArgumentException("Map size is incorrectly defined (should be superior to 0 and inferior to the max size width:" + width_max + " height:" + height_max);
                }


            }
            catch (Exception exception)
            {
                if (P_Instruction.isNull())
                {
                    throw new ArgumentNullException("Instruction not defined : All the instructions should be defined");
                }
                else
                {
                    throw new ArgumentException("Map instruction is incorrectly defined");
                }

            }

            try
            {
                /* Map creation */

                //Used to store and sort the positions nto mapGrids
                Dictionary<Position, Surface> temp_mapGrids = new Dictionary<Position, Surface>();

                //Add the mountain(s) to the map
                foreach (string mountain in P_Instruction.Mountain_instruction)
                {
                    //set the position (x & y) to a temp Position object
                    Position L_Position = new Position(Byte.Parse(mountain.Split(" - ")[0]), Byte.Parse(mountain.Split(" - ")[1]));

                    if (checkPositionIsDefined(temp_mapGrids, L_Position))
                    {
                        temp_mapGrids.Add(new Position(L_Position.X, L_Position.Y), new Mountain());
                    }
                    else
                    {
                        throw new ArgumentException("Unexpected position : Mountain position already defined (Only one moutain should be at the same position).");
                    }

                }
                //Add the treasure(s) to the map
                foreach (string treasure in P_Instruction.Treasure_instruction)
                {
                    //set the position (x & y) to a temp Position object
                    Position L_Position = new Position(Byte.Parse(treasure.Split(" - ")[0]), Byte.Parse(treasure.Split(" - ")[1]));

                    if (checkPositionIsDefined(L_Position))
                    {
                        // create Treasure with the quantity
                        temp_mapGrids.Add(new Position(L_Position.X, L_Position.Y), new Treasure(Byte.Parse(treasure.Split(" - ")[2])));
                    }
                    else
                    {
                        throw new ArgumentException("Unexpected position : Treasure position already defined (Only one element should be at the same position).");
                    }


                }

                // set all the empty surface as Plain and ordered Keys
                for (byte index_height = 0; index_height < height; index_height++)
                {
                    for (byte index_width = 0; index_width < width; index_width++)
                    {
                        Position L_Position = new Position(index_width, index_height);
                        if (checkPositionIsDefined(temp_mapGrids, L_Position))
                        {
                            // add ordered plain surface with position in the map grid
                            mapGrids.Add(new Position(index_width, index_height), new Plain());
                        }
                        else
                        {
                            // add ordered Mountain and Treasure surface with position in the map grid
                            mapGrids.Add(new Position(index_width, index_height),
                                getSurfaceByPosition(temp_mapGrids, new Position(index_width, index_height)));
                        }
                    }
                }


                //check the position of adventurer (it can't be on a Mountain)
                if (getTypeElement(new Position(
                    Byte.Parse(P_Instruction.Adventurer_instruction.Split(" - ")[1]),
                    Byte.Parse(P_Instruction.Adventurer_instruction.Split(" - ")[2]))) == "Plain"
                    ||
                    getTypeElement(new Position(
                    Byte.Parse(P_Instruction.Adventurer_instruction.Split(" - ")[1]),
                    Byte.Parse(P_Instruction.Adventurer_instruction.Split(" - ")[2]))) == "Treasure")
                {
                    //Add the adventurer into the map 
                    TheAdventurer = new Adventurer(P_Instruction.Adventurer_instruction);
                }
                else
                {
                    throw new ArgumentException("Unexpected position : Adventurer can't spawn on a mountain.");
                }

            }
            catch (FormatException exeption)
            {
                //NaN values
                exeption.Data.Add("Construction map :", "Unexpected position value");
                throw;
            }
            catch (Exception exeption)
            {

                exeption.Data.Add("Construction map :", "Unexpected position value");
                throw;
            }

        }
        public static Map GetInstance(Instruction P_Instruction)
        {
            if (instance == null)
            {
                instance = new Map(P_Instruction);
            }
            return instance;
        }
        public bool checkPositionIsDefined(Position P_Position)
        {
            //Check all the element to compare the positions
            foreach (var position in mapGrids)
            {
                if (PositionComparer.EqualsPositions(position.Key, P_Position))
                {
                    return false;
                }
                else
                {
                    continue;
                }
            }

            return true;
        }
        public bool checkPositionIsDefined(Dictionary<Position, Surface> P_mapGrids, Position P_Position)
        {
            //Check all the element to compare the positions
            foreach (var position in P_mapGrids)
            {
                if (PositionComparer.EqualsPositions(position.Key, P_Position))
                {
                    return false;
                }
                else
                {
                    continue;
                }
            }

            return true;
        }
        public Surface getSurfaceByPosition(Dictionary<Position, Surface> P_mapGrids, Position P_Position)
        {
            Surface return_surface = new Plain();
            try
            {

                //Check all the element to compare the positions
                foreach (var position in P_mapGrids)
                {


                    if (PositionComparer.EqualsPositions(position.Key, P_Position))
                    {
                        return_surface = position.Value;
                    }
                    else
                    {
                        continue;
                    }


                }


            }
            catch
            {

            }

            return return_surface;
        }



        public void getTreasure(byte P_x, byte P_y)
        {
            Position ChestPosition = new Position(P_x, P_y);
            try
            {

                //Check all the element to compare the positions
                foreach (var position in mapGrids)
                {
                    if (PositionComparer.EqualsPositions(position.Key, ChestPosition))
                    {
                        Treasure elem = (Treasure)position.Value;
                        elem.getChest();
                    }
                    else
                    {
                        continue;
                    }


                }


            }
            catch(Exception exeption)
            {
            }
            
        }

        /* Map state */
        public static bool isInit()
        {
            if (instance == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static void clearMap()
        {
            width = 0;
            height = 0;
            mapGrids.Clear();
            instance = null;

        }
        public void updateMapGrid()
        {
            foreach (var position in mapGrids)
            {
                if (getTypeElement(position.Key) =="Treasure")
                {
                    Treasure treasure = (Treasure)position.Value;


                    //if treasure is null this is a Plain
                    if (treasure.getChests() == 0)
                    {
                        Surface surface = position.Value;
                        surface = new Plain();
                    }
                }
                else
                {
                    continue;
                }

            }
        }



        /* Map operations*/
        private string getTypeElement(Position P_Position)
        {

            //Check all the element to compare the positions
            foreach (var position in mapGrids)
            {
                if (PositionComparer.EqualsPositions(position.Key, P_Position))
                {

                    return position.Value.GetType().Name;

                }
                else
                {
                    continue;
                }
            }
            return "Plain";
        }

        private bool isAccessible(Position P_Position)
        {
            foreach (var position in mapGrids)
            {
                if (PositionComparer.EqualsPositions(position.Key, P_Position))
                {

                    return position.Value.getAccessible();

                }
                else
                {
                    continue;
                }
            }
            return false;
        }


        /* Adventurer game*/
        public void AdventurerMoveStepByStep()
        {
            Position nextPosition = TheAdventurer.wantMove();
            string nextSurface = getTypeElement(nextPosition);

            if (isAccessible(nextPosition))
            {
                TheAdventurer.moveNextStep();

            }
        }
        public void AdventurerFinishMovement()
        {
            for (int index_movement = 0; index_movement < TheAdventurer.Movement.Length; index_movement++)
            {
                Position nextPosition = TheAdventurer.wantMove();
                string nextSurface = getTypeElement(nextPosition);

                if (isAccessible(nextPosition))
                {
                    TheAdventurer.moveNextStep();

                }
            }

        }

        public int getNumberOfTreasure()
        {
            int numberChest = 0;
            //Check all the element to find treasure
            foreach (var surface in mapGrids)
            {
                if (surface.Value.GetType().Name == "Treasure")
                {
                    Treasure temp = (Treasure)surface.Value;
                    numberChest += temp.getChests();
                }
                else
                {
                    continue;
                }
            }
            return numberChest;
        }

        //Serialisation for the JSON POST
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            try
            {
                info.AddValue("width", width);
                info.AddValue("height", height);
                info.AddValue("mapGridsKeys", mapGrids.Keys);
                info.AddValue("mapGridsValues", mapGrids.Values);
                info.AddValue("Adventurer", TheAdventurer);
            }
            catch
            {
                throw new NotImplementedException();
            }

        }

        //Deserialisation from the JSON POST
        public Map(SerializationInfo info, StreamingContext context)
        {
            try
            {
                width = (byte)info.GetValue("width", typeof(byte));
                height = (byte)info.GetValue("height", typeof(byte));
                mapGrids = (Dictionary<Position, Surface>)info.GetValue("width", typeof(Dictionary<Position, Surface>));
                TheAdventurer = (Adventurer)info.GetValue("TheAdventurer", typeof(Adventurer));
            }
            catch
            {
                throw new NotImplementedException();
            }
        }

    }
}