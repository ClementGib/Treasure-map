using System;
using System.Collections.Generic;
using System.Linq;

namespace BAL
{
    //Instruction from input to create the Map
    public sealed class Instruction
    {

        private Dictionary<int, string> commentaries = new Dictionary<int, string>();
        private string mapInstruction;
        private List<string> mountainInstruction = new List<string>();
        private List<string> treasureInstruction = new List<string>();
        private string adventurerInstruction;


        //Default constructor
        public Instruction()
        {

        }

        //Parameterized constructor
        public Instruction(Dictionary<int, string> P_commentaries,
            string P_map_instruction,
            List<string> P_mountain_instruction,
            List<string> P_treasure_instruction,
            string P_adventurer_instruction
            )
        {
            commentaries = P_commentaries;
            mapInstruction = P_map_instruction;
            mountainInstruction = P_mountain_instruction;
            treasureInstruction = P_treasure_instruction;
            adventurerInstruction = P_adventurer_instruction;

        }


        //Getter & Setter of commentaries
        public Dictionary<int, string> Commentaries
        {
            get => commentaries;
            set
            {
                //check
                var item = value.First();
                if (!commentaries.ContainsKey(item.Key))
                {
                    commentaries.Add(item.Key, item.Value);
                }
                else
                {
                    throw new ArgumentException("Instruction commentaries is incorrectly set");

                }

            }
        }
        //Getter & Setter of mapInstruction
        public string MapInstruction
        {
            get => mapInstruction;
            set
            {
                //if instruction still composed of the letter : C
                if (value.Split(" - ").Length == 3 && char.Parse(value.Split(" - ")[0]) == 'C')
                {
                    //keep just the map instructions
                    mapInstruction = value.Split(" - ")[1] + " - " + value.Split(" - ")[2];
                }
                else
                {
                    if (value.Split(" - ").Length == 2)
                    {
                        //already refactor
                        mapInstruction = value;
                    }
                    else
                    {
                        if (value == "")
                        {
                            mapInstruction = "";
                        }
                        else
                        {
                            throw new ArgumentException("Instruction map is incorrectly set");
                        }


                    }
                }

            }
        }

        //Getter & Setter of mountainInstruction
        public List<string> MountainInstruction
        {
            get => mountainInstruction;
            set
            {
                List<string> temp_instruction = new List<string>();

                foreach (string mountain_insctruction in value)
                {
                    //if instruction still composed of the letter : M
                    if (mountain_insctruction.Split(" - ").Length == 3 && char.Parse(mountain_insctruction.Split(" - ")[0]) == 'M')
                    {
                        //keep just the map instructions
                        temp_instruction.Add(mountain_insctruction.Split(" - ")[1] + " - " + mountain_insctruction.Split(" - ")[2]);
                    }
                    else
                    {
                        if (Char.IsNumber(char.Parse(mountain_insctruction.Split(" - ")[0])) && mountain_insctruction.Split(" - ").Length == 2)
                        {
                            //already refactor
                            temp_instruction.Add(mountain_insctruction);
                        }
                        else
                        {
                            throw new ArgumentException("Instruction mountain is incorrectly set");

                        }
                    }
                }

                mountainInstruction = temp_instruction;
            }
        }
        //Getter & Setter of treasureInstruction
        public List<string> TreasureInstruction
        {
            get => treasureInstruction;
            set
            {


                List<string> temp_instruction = new List<string>();

                foreach (string treasure_instruction in value)
                {
                    //if instruction still composed of the letter : T
                    if (treasure_instruction.Split(" - ").Length == 4 && char.Parse(treasure_instruction.Split(" - ")[0]) == 'T')
                    {
                        //keep just the map instructions
                        temp_instruction.Add(treasure_instruction.Split(" - ")[1]
                            + " - " + treasure_instruction.Split(" - ")[2]
                            + " - " + treasure_instruction.Split(" - ")[3]);
                    }
                    else
                    {
                        if (Char.IsNumber(char.Parse(treasure_instruction.Split(" - ")[0])) && treasure_instruction.Split(" - ").Length == 3)
                        {
                            //already refactor
                            temp_instruction.Add(treasure_instruction);
                        }
                        else
                        {
                            throw new ArgumentException("Instruction treasure is incorrectly set");

                        }
                    }
                }

                treasureInstruction = temp_instruction;

            }
        }

        public string Adventurer_instruction
        {
            get => adventurerInstruction;
            set
            {
                //if instruction still composed of the letter : A
                if (value.Split(" - ").Length == 6 && char.Parse(value.Split(" - ")[0]) == 'A')
                {
                    //keep just the map instructions
                    adventurerInstruction = value.Split(" - ")[1]
                        + " - " + value.Split(" - ")[2]
                        + " - " + value.Split(" - ")[3]
                        + " - " + value.Split(" - ")[4]
                        + " - " + value.Split(" - ")[5];
                }
                else
                {
                    if (value.Split(" - ").Length == 5)
                    {
                        //already refactor
                        adventurerInstruction = value;
                    }
                    else
                    {
                        if (value == "")
                        {
                            adventurerInstruction = "";
                        }
                        else
                        {
                            throw new ArgumentException("Instruction adventurer is incorrectly set");
                        }
                    }
                }

            }
        }

        //if an element is not set in the instructions
        public bool isNull()
        {
            if (String.IsNullOrEmpty(mapInstruction) || String.IsNullOrEmpty(adventurerInstruction) || !mountainInstruction.Any() || !treasureInstruction.Any())
            {
                return true;
            }
            else
            {
                return false;
            }

        }


    }
}
