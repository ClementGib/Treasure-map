using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BAL
{
    public sealed class Instruction
    {
        private Dictionary<string, int> commentaries = new Dictionary<string, int>();
        private string map_instruction;
        private string adventurer_instruction;
        private List<string> mountain_instruction = new List<string>();
        private List<string> treasure_instruction = new List<string>();



        public Instruction()
        {

        }



 

        public Dictionary<string, int> Commentaries{
            get => commentaries;
            set => commentaries = value;
        }
        public string Map_instruction
        {
            get => map_instruction;
            set => map_instruction = value;
        }
        public string Adventurer_instruction
        {
            get => adventurer_instruction;
            set => adventurer_instruction = value;
        }
        public List<string> Mountain_instruction
        {
            get => mountain_instruction;
            set => mountain_instruction = value;
        }
        public List<string> Treasure_instruction
        {
            get => treasure_instruction;
            set => treasure_instruction = value;
        }

  
    }
}
