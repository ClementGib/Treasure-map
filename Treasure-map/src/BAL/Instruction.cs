﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BAL
{
    public sealed class Instruction
    {
        private Dictionary<int, string> commentaries = new Dictionary< int, string>();
        private string map_instruction;
        private List<string> mountain_instruction = new List<string>();
        private List<string> treasure_instruction = new List<string>();
        private string adventurer_instruction;



        public Instruction()
        {

        }

        public Instruction(Dictionary<int, string> P_commentaries,
            string P_map_instruction,
            List<string> P_mountain_instruction,
            List<string> P_treasure_instruction,
            string P_adventurer_instruction
            )
        {
            commentaries = P_commentaries;
            map_instruction = P_map_instruction;
            mountain_instruction = P_mountain_instruction;
            treasure_instruction = P_treasure_instruction;
            adventurer_instruction = P_adventurer_instruction;

        }



        public Dictionary<int, string> Commentaries{
            get => commentaries;
            set
            {
                var item = value.First();
                   if (!commentaries.ContainsKey( item.Key))
                    {
                        commentaries.Add(item.Key,item.Value);
                    }
                else
                {
                    throw new ArgumentException("Instruction commentaries is incorrectly set");

                }
                
            }
        }
        public string Map_instruction
        {
            get => map_instruction;
            set
            {
                //if instruction still composed of the letter : C
                if (value.Split(" - ").Length == 3 && char.Parse(value.Split(" - ")[0]) == 'C')
                {
                    //keep just the map instructions
                    map_instruction = value.Split(" - ")[1] +" - "+ value.Split(" - ")[2];
                }
                else
                {
                    if(value.Split(" - ").Length == 2)
                    {
                        //already refactor
                        map_instruction = value;
                    }
                    else
                    {
                        if(value == "")
                        {
                            map_instruction = "";
                        }
                        else
                        {
                            throw new ArgumentException("Instruction map is incorrectly set");
                        }
                        

                    }
                }
                
            }
        }

        public List<string> Mountain_instruction
        {
            get => mountain_instruction;
            set
            {
                 List<string> temp_instruction = new List<string>();

                foreach(string mountain_insctruction in value)
                {
                    //if instruction still composed of the letter : M
                    if (mountain_insctruction.Split(" - ").Length == 3 && char.Parse(mountain_insctruction.Split(" - ")[0]) == 'M')
                    {
                        //keep just the map instructions
                        temp_instruction.Add(mountain_insctruction.Split(" - ")[1] + " - " + mountain_insctruction.Split(" - ")[2]) ;
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

                mountain_instruction = temp_instruction;
            }
        }
        public List<string> Treasure_instruction
        {
            get => treasure_instruction;
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

                treasure_instruction = temp_instruction;

            }
        }

        public string Adventurer_instruction
        {
            get => adventurer_instruction;
            set
            {
                //if instruction still composed of the letter : A
                if (value.Split(" - ").Length == 6 && char.Parse(value.Split(" - ")[0]) == 'A')
                {
                    //keep just the map instructions
                    adventurer_instruction = value.Split(" - ")[1]
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
                        adventurer_instruction = value;
                    }
                    else
                    {
                        if (value == "")
                        {
                            adventurer_instruction = "";
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
            if (String.IsNullOrEmpty(map_instruction) || String.IsNullOrEmpty(adventurer_instruction) || !mountain_instruction.Any() || !treasure_instruction.Any())
            {
                return true;
            }
            else{
                return false;
            }
            
        }

  
    }
}
