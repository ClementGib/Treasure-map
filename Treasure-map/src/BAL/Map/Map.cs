using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;

namespace BAL
{
    public sealed class Map
    {


        private const byte width_max = 125;
        private const byte height_max = 250;

        //x
        private static byte width = 0;
        //y
        private static byte height = 0;

        // 2D map grid of surfaces with position on the matrix (Dictionary<TKey,TValue>)
        private static Dictionary<Position, Surface> mapGrids = new Dictionary<Position, Surface>();

        // Singleton instance
        private static Map instance = null;

        public Map(Instruction P_Instruction)
        {

            try
            {
                width = Byte.Parse(P_Instruction.Map_instruction.Split(" - ")[1]);
                height = Byte.Parse(P_Instruction.Map_instruction.Split(" - ")[2]);
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

            
            //Map creation
            

            //Add the mountain(s) to the map
            foreach(string mountain in P_Instruction.Mountain_instruction)
                {
                    //set the position (x & y) to a temp Position object
                    Position L_Position = new Position(Byte.Parse(mountain.Split(" - ")[1]), Byte.Parse(mountain.Split(" - ")[2]));

                    if (checkPositionIsNotDefined(L_Position))
                    {
                        //mapGrid.Add(new Position(index_width, index_height), new Plain());
                    }
                    else
                    {
                        throw new ArgumentException("Unexpected position : Mountain position already defined");
                    }                  
    
                }

            for (byte index_height = 0; index_height< height; index_height++)
            {
                for (byte index_width = 0; index_width < width; index_width++)
                {
                    //define all the map with plain surface
                    mapGrids.Add(new Position(index_width, index_height), new Plain());

                }
            }
            }
            catch(Exception e)
            {
                //TODO 
                //tests with NaN values ...
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

        public bool checkPositionIsNotDefined(Position P_Position)
        {
            Console.WriteLine(mapGrids.ContainsKey(P_Position).ToString());
            return true;
        } 

        public void updateMapGrid(string P_map_instruction)
        {

            width = Byte.Parse(P_map_instruction.Split(" - ")[1]);
            height = Byte.Parse(P_map_instruction.Split(" - ")[2]);

            for (byte index_height = 0; index_height < height; index_height++)
            {
                for (byte index_width = 0; index_width < width; index_width++)
                {
                    //define all the map with plain surface
                    //mapGrid.Add(new Plain(), new byte[index_width, index_height]);

                }
            }
        }
    }
}
