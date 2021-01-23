using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using BAL;

namespace UnitTests.IO
{
    [TestFixture]
    public class MapTests
    {

        private Map _map;

        protected string path = null;
        Instruction instructions;


        [SetUp]
        public void Setup(string map_instruction)
        {
            //TestContext.WriteLine(path);
            instructions = new Instruction();
        }


        /// <summary>
        /// Instructions tests
        /// </summary>

        [Test, Description("Instruction empty")]
        public void Map_InstructionIsEmpty_ArgumentNullException()
        {

            //Arrange 
            
            if (Map.isInit())
            {
                Map.clearMap();
            }

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => _map = Map.GetInstance(instructions));

  
        }



        [Test, Description("Good instruction map")]
        public void Map_InstructionIsGood_ArgumentNullException()
        {


            //Arrange 
            if (Map.isInit())
            {
                Map.clearMap();
            }

            instructions.Map_instruction = "C - 3 - 4";
            instructions.Adventurer_instruction = "A - Lara - 1 - 1 - S - AADADAGGA";

            List<string> L_mountain_instruction = new List<string>();
            L_mountain_instruction.Add("1 - 0");
            L_mountain_instruction.Add("2 - 1");
            instructions.Mountain_instruction = L_mountain_instruction;

            List<string> L_treasure_instruction = new List<string>();
            L_treasure_instruction.Add("0 - 3 - 2");
            L_treasure_instruction.Add("1 - 3 - 3");

            instructions.Treasure_instruction = L_treasure_instruction;

            //Act
            //bool returnValue = _fileManager.readFile(basicPath+P_fileName);

            //Assert -> 1 return code signify that the file is Okay
            //Assert.AreEqual(true, returnValue);
        }



        /// <summary>
        /// file format Okay
        /// </summary>
        /// <param name="P_fileName"></param>

        [TestCase("basicFile.txt"), Description("Good input files")]
        [TestCase("advancedFile.txt")]
        public void readFile_IfFileFormatOkay_ReturnTrue(string P_fileName)
        {
            TestContext.WriteLine(path);

            //Arrange 
            string basicPath = path + "\\Basic\\";


            //Act
            //bool returnValue = _fileManager.readFile(basicPath+P_fileName);

            //Assert -> 1 return code signify that the file is Okay
            //Assert.AreEqual(true, returnValue);
        }


        /// <summary>
        /// wrong attribute test
        /// </summary>
        /// <param name="P_fileName"></param>

        [TestCase("wrongAttribute.txt"), Description("Wrong attribute definition")]
        public void readFile_IfWrongAttribute_ReturnFormatException(string P_fileName)
        {

            //Arrange 
            string basicPath = path + "\\Basic\\";

            //Act & Assert 
            //Assert.Throws<FormatException>(() => _fileManager.readFile(basicPath + P_fileName));
        }


       

    }
}