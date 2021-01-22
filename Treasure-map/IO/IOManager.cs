using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;



namespace IO
{
    public sealed class IOManager
    {
        const byte width_max = 125;
        const byte height_max = 250;

        // size of instruction in the input file
        enum instruction_size
        {
            // Letter - Width - Height
            map = 3,
            // Letter - X - Y
            mountain = 3,
            // Letter - X - Y - Nb
            treasure = 4,
            // Letter - Name - X - Y - Orientation - Movement
            adventurer = 6
        }

        Dictionary<string, int> commentaries = new Dictionary<string, int>();
        protected string map_instruction;
        protected string adventurer_instruction;
        protected List<string> mountain_instruction = new List<string>();
        protected List<string> treasure_instruction = new List<string>();


        private IOManager() { }

        // Singleton instance
        private static IOManager instance = null;



        public static IOManager GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new IOManager();
                }
                return instance;
            }
        }

        public bool readFile(string P_fileName)
        {

            string[] readLines = null;

            try
            {
                //Initialization with each line of the file
                readLines = System.IO.File.ReadAllLines(P_fileName);
            }
            catch (FileNotFoundException exception)
            {
                Console.WriteLine("File not found");
                exception.Data.Add("detail message :", "File not found");
                throw;
            }
            catch (IOException exception)
            {
                Console.WriteLine("IOException I/O error occurs.");
                exception.Data.Add("detail message :", "IOException I / O error occurs.");
                throw;
            }


            try
            {
                //if the file check is conform
               if(checkFile(readLines))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (FormatException exception)
            {
                Console.WriteLine("FormatException error occurs, input file is incorrectly defined.");
                exception.Data.Add("detail message :", " input file is incorrectly defined.");
                throw;
            }

            return false;
        }


        protected bool checkFile(string[] P_inputLines)
        {

            //Empty instructions
            commentaries.Clear();
            map_instruction = "";
            adventurer_instruction = "";
            mountain_instruction.Clear();
            treasure_instruction.Clear();

            //Define last letter to check the order of the instructions C ...M ...T A
            char lastLetter = '\0';

            //Initialisation of instructions and commentaries
            for (int index_Lines = 0; index_Lines < P_inputLines.Length; index_Lines++)
            {


                //if the line is a commentary
                if (P_inputLines[index_Lines][0] == '#')
                {
                    // add commentary with its line position
                    commentaries.Add(P_inputLines[index_Lines], index_Lines);
                }
                //Else, the line is an instruction
                else
                {
                    string[] L_instructions = null;

                    switch (P_inputLines[index_Lines][0])
                    {

                        //Map definition
                        case 'C':

                            
                            L_instructions = P_inputLines[index_Lines].Split(" - ");

                            // check instruction string size && if it is the first instruction
                            if (L_instructions.Length == (int)instruction_size.map && lastLetter == '\0')
                            {
                                // if map undefined
                                if (String.IsNullOrEmpty(map_instruction))
                                {
                                    //check map size 
                                    if ((Int32.Parse(L_instructions[1]) >=0 && Int32.Parse(L_instructions[1]) <= width_max) && (Int32.Parse(L_instructions[2])>=0 && Int32.Parse(L_instructions[2]) <= height_max))
                                    {
                                        map_instruction = P_inputLines[index_Lines];
                                        lastLetter = 'C';
                                    }
                                    else
                                    {
                                        throw new FormatException("File format is invalid : Map size limited to [125,250] ");
                                    }

                                }
                                // Multiple C lines
                                else
                                {
                                    throw new FormatException("File format is invalid : Map redefined ");
                                }

                            }
                            else
                            {
                                throw new FormatException("File format is invalid : Map definition invalid [Letter - Width - Height]");
                            }
                            break;

                        //Mountain definition
                        case 'M':

                            
                            L_instructions = P_inputLines[index_Lines].Split(" - ");

                            // check instruction string size && if it is the second instruction
                            if (L_instructions.Length == (int)instruction_size.mountain && (lastLetter == 'C' || lastLetter == 'M'))
                            {
                                //check if position correspond to the map
                                if ((Int32.Parse(L_instructions[1]) >= 0 && Int32.Parse(L_instructions[1]) < Int32.Parse(map_instruction.Split(" - ")[1])) &&
                                    (Int32.Parse(L_instructions[2]) >= 0 && Int32.Parse(L_instructions[2]) < Int32.Parse(map_instruction.Split(" - ")[2])))
                                {
                                    if(!mountain_instruction.Contains(P_inputLines[index_Lines]))
                                    {
                                        mountain_instruction.Add(P_inputLines[index_Lines]);
                                        lastLetter = 'M';
                                    }
                                    
                                }
                                else
                                {
                                    throw new FormatException("File format is invalid : Position mountain should be in the range [" +
                                        Int32.Parse(map_instruction.Split(" - ")[1]) +"," +Int32.Parse(map_instruction.Split(" - ")[2]) 
                                        +"]");
                                }          
                            }
                            else
                            {
                                throw new FormatException("File format is invalid : Mountain definition invalid [Letter - X - Y]");
                            }
                            break;

                        //Treasure definition
                        case 'T':

                            L_instructions = P_inputLines[index_Lines].Split(" - ");

                       
                            // check instruction string size && if it is the third instruction
                            if (L_instructions.Length == (int)instruction_size.treasure && (lastLetter == 'M' || lastLetter == 'T'))
                            {
                                //check if position corresponds to the map
                                if ((Int32.Parse(L_instructions[1]) >= 0 && Int32.Parse(L_instructions[1]) < Int32.Parse(map_instruction.Split(" - ")[1])) &&
                                    (Int32.Parse(L_instructions[2]) >= 0 && Int32.Parse(L_instructions[2]) < Int32.Parse(map_instruction.Split(" - ")[2])))
                                {

                                    if (!treasure_instruction.Contains(P_inputLines[index_Lines]))
                                    {
                                        treasure_instruction.Add(P_inputLines[index_Lines]);
                                        lastLetter = 'T';
                                    }
                                    
                                }
                                else
                                {
                                    throw new FormatException("File format is invalid : Position treasure should be in the range [" +
                                        Int32.Parse(map_instruction.Split(" - ")[1]) + "," + Int32.Parse(map_instruction.Split(" - ")[2])
                                        + "]");
                                }
                            }
                            else
                            {
                                throw new FormatException("File format is invalid : Mountain definition invalid [Letter - X - Y - Nb]");
                            }
                            break;

                        //Adventurer definition
                        case 'A':

                            L_instructions = P_inputLines[index_Lines].Split(" - ");

                            // check instruction string size && if its the first instruction
                            if (L_instructions.Length == (int)instruction_size.adventurer && lastLetter == 'T')
                            {
                                // if adventurer undefined
                                if (String.IsNullOrEmpty(adventurer_instruction))
                                {
                                    //check if position corresponds to the map
                                    if ((Int32.Parse(L_instructions[2]) >= 0 && Int32.Parse(L_instructions[2]) < Int32.Parse(map_instruction.Split(" - ")[1]))
                                    && (Int32.Parse(L_instructions[3]) >= 0 && Int32.Parse(L_instructions[3]) < Int32.Parse(map_instruction.Split(" - ")[2])))
                                    {
                                        //check orientation
                                        if (L_instructions[4] == "N" || L_instructions[4] == "S" || L_instructions[4] == "E" || L_instructions[4] == "O")
                                        {
                                            //check movement
                                            foreach (char action in L_instructions[5])
                                            {
                                                if (action == 'A' || action == 'D' || action == 'G')
                                                {
                                                    continue;
                                                }
                                                else
                                                {
                                                    throw new FormatException("File format is invalid : Movement adventurer should be A or D or G");
                                                }
                                            }

                                            adventurer_instruction = P_inputLines[index_Lines];
                                            lastLetter = 'A';
                                        }
                                        else
                                        {
                                            throw new FormatException("File format is invalid : Orientation adventurer should be N or S or E or O ");
                                        }

                                    }
                                    else
                                    {
                                        throw new FormatException("File format is invalid : Map size limited to [125,250] ");
                                    }      
                                }
                                // Multiple A lines
                                else
                                {
                                    throw new FormatException("File format is invalid : Adventurer redefined");
                                }
                            }
                            else
                            {
                                throw new FormatException("File format is invalid : Map definition invalid [Letter - Name - X - Y - Orientation - Movement]");
                            }
                            break;

                        default:
                            // unexepted case, unknown instruction -> File invalid
                            throw new FormatException("File format is invalid : unknown instruction " + P_inputLines[index_Lines][0]);
                    }
                }
            }

            //check if every element are initialised
            if (!string.IsNullOrEmpty(map_instruction) && mountain_instruction.Any() && treasure_instruction.Any() && !string.IsNullOrEmpty(adventurer_instruction) )
            {
                return true;
            }
            else{
                throw new FormatException("File format is invalid : element definition is missing");
            }
        }


        public void writeFile()
        {

        }

    }


}

