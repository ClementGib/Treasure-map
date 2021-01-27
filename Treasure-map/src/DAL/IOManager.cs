using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using BAL;



namespace DAL
{
    public sealed class IOManager
    {
        private const byte width_max = 125;
        private const byte height_max = 250;

        // size of instruction in the input file
        private enum instruction_size
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

        private Instruction instructionFromInput = new Instruction();

        // Singleton instance
        private static IOManager instance = null;

        private IOManager() { }


        public Instruction InstructionFromInput { get => instructionFromInput; }

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

        
        public bool readInstructionsFile(string P_fileName)
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
                if (checkLine(readLines))
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
        }
        public bool readInstructionsText(string P_text)
        {
            string[] readLines = null;
            try
            {
                //for each line from the text area 
                List<string> temp = new List<string>();
                foreach (string line in P_text.Split("\\n"))
                {
                    if( line.ToCharArray()[0] == '\"' || line.ToCharArray()[line.Length-1] == '\"')
                    {
                        temp.Add(line.Replace("\"",""));
                    }
                    else{
                        temp.Add(line);
                    }

                    
 
                }
                readLines = temp.ToArray();

            }
            catch (Exception exception)
            {
                exception.Data.Add("detail message :", "Error text area");
                throw;
            }


            try
            {
                //if the file check is conform
                if (checkLine(readLines))
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
        }
        private bool checkLine(string[] P_inputLines)
        {

            //Empty instructions
            InstructionFromInput.Commentaries.Clear();
            InstructionFromInput.Map_instruction = "";
            InstructionFromInput.Adventurer_instruction = "";
            InstructionFromInput.Mountain_instruction.Clear();
            InstructionFromInput.Treasure_instruction.Clear();

            //Define last letter to check the order of the instructions C ...M ...T A
            char lastLetter = '\0';

            //Initialisation of instructions and commentaries
            for (int index_Lines = 0; index_Lines < P_inputLines.Length; index_Lines++)
            {


                //if the line is a commentary
                if (P_inputLines[index_Lines][0] == '#')
                {
                    //temp_commentaries to use overriden setter -> Instruction.Commentaries
                    Dictionary<int, string> temp_commentaries = new Dictionary<int, string>();
                    temp_commentaries.Add(index_Lines, P_inputLines[index_Lines]);

                    // add commentary with its line position
                    InstructionFromInput.Commentaries = temp_commentaries;
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
                                if (String.IsNullOrEmpty(InstructionFromInput.Map_instruction))
                                {
                                    //check map size 
                                    if ((Int32.Parse(L_instructions[1]) >= 0 && Int32.Parse(L_instructions[1]) <= width_max) && (Int32.Parse(L_instructions[2]) >= 0 && Int32.Parse(L_instructions[2]) <= height_max))
                                    {
                                        //keep just the map instruction -> setter Map_instruction
                                        InstructionFromInput.Map_instruction = P_inputLines[index_Lines];
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
                                if ((Int32.Parse(L_instructions[1]) >= 0 && Int32.Parse(L_instructions[1]) < Int32.Parse(InstructionFromInput.Map_instruction.Split(" - ")[0])) &&
                                    (Int32.Parse(L_instructions[2]) >= 0 && Int32.Parse(L_instructions[2]) < Int32.Parse(InstructionFromInput.Map_instruction.Split(" - ")[1])))
                                {
                                    if (!InstructionFromInput.Mountain_instruction.Contains(P_inputLines[index_Lines]))
                                    {
                                        //keep just the mountain instructions
                                        InstructionFromInput.Mountain_instruction.Add(P_inputLines[index_Lines].Split(" - ")[1]
                                            + " - " + P_inputLines[index_Lines].Split(" - ")[2]);
                                        lastLetter = 'M';

                                    }

                                }
                                else
                                {
                                    throw new FormatException("File format is invalid : Position mountain should be in the range [" +
                                        Int32.Parse(InstructionFromInput.Map_instruction.Split(" - ")[0]) + "," + Int32.Parse(InstructionFromInput.Map_instruction.Split(" - ")[1])
                                        + "]");
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
                                if ((Int32.Parse(L_instructions[1]) >= 0 && Int32.Parse(L_instructions[1]) < Int32.Parse(InstructionFromInput.Map_instruction.Split(" - ")[0])) &&
                                    (Int32.Parse(L_instructions[2]) >= 0 && Int32.Parse(L_instructions[2]) < Int32.Parse(InstructionFromInput.Map_instruction.Split(" - ")[1])))
                                {

                                    if (!InstructionFromInput.Treasure_instruction.Contains(P_inputLines[index_Lines]))
                                    {
                                        //keep just the treasure instructions
                                        InstructionFromInput.Treasure_instruction.Add(P_inputLines[index_Lines].Split(" - ")[1]
                                            + " - " + P_inputLines[index_Lines].Split(" - ")[2]
                                            + " - " + P_inputLines[index_Lines].Split(" - ")[3]);
                                        lastLetter = 'T';

                                    }

                                }
                                else
                                {
                                    throw new FormatException("File format is invalid : Position treasure should be in the range [" +
                                        Int32.Parse(InstructionFromInput.Map_instruction.Split(" - ")[0]) + "," + Int32.Parse(InstructionFromInput.Map_instruction.Split(" - ")[1])
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
                                if (String.IsNullOrEmpty(InstructionFromInput.Adventurer_instruction))
                                {
                                    //check if position corresponds to the map
                                    if ((Int32.Parse(L_instructions[2]) >= 0 && Int32.Parse(L_instructions[2]) < Int32.Parse(InstructionFromInput.Map_instruction.Split(" - ")[0]))
                                    && (Int32.Parse(L_instructions[3]) >= 0 && Int32.Parse(L_instructions[3]) < Int32.Parse(InstructionFromInput.Map_instruction.Split(" - ")[1])))
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
                                            //keep just the adventurer instruction -> setter Adventurer_instruction
                                            InstructionFromInput.Adventurer_instruction = P_inputLines[index_Lines];
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
            if (!string.IsNullOrEmpty(InstructionFromInput.Map_instruction) && InstructionFromInput.Mountain_instruction.Any() && InstructionFromInput.Treasure_instruction.Any() && !string.IsNullOrEmpty(InstructionFromInput.Adventurer_instruction))
            {
                return true;
            }
            else
            {
                throw new FormatException("File format is invalid : element definition is missing");
            }
        }


        public void writeFile()
        {
            ///TODO
        }

    }


}

