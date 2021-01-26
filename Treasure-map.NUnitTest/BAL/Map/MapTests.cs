using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using DAL;
using BAL;

namespace UnitTests.BAL.MapTests
{
    [TestFixture]
    public class MapTests
    {

        private Instruction _instruction;
        protected string path = null;


        [SetUp]
        public void Setup()
        {

            Dictionary<int, string> temp_commentaries = new Dictionary<int, string>();
            temp_commentaries.Add(0, "# Map size definition");
            temp_commentaries.Add(2, "# Mountain position");
            temp_commentaries.Add(5, "# Treasure chest position");
            temp_commentaries.Add(8, "# Adventurer definition");

            string temp_map_instruction = "3 - 4";

            List<string> temp_mountain_instruction = new List<string>();
            temp_mountain_instruction.Add("1 - 0");
            temp_mountain_instruction.Add("2 - 1");

            List<string> temp_treasure_instruction = new List<string>();
            temp_treasure_instruction.Add("0 - 3 - 2");
            temp_treasure_instruction.Add("1 - 3 - 3");

            string temp_adventurer_instruction = "Lara - 1 - 1 - S - AADADAGGA";

            _instruction = new Instruction(temp_commentaries,
                temp_map_instruction,
                temp_mountain_instruction,
                temp_treasure_instruction,
                temp_adventurer_instruction
                );
        }



        /// <summary>
        /// Map is correctly defined
        /// </summary>
        [Test, Description("Map correctly defined.")]
        public void isInit_InstructionIsCorrectlyDefine_True()
        {
            //Arrange 
            Map _map;

            //Act
            _map = Map.GetInstance(_instruction);

            //Map is correctly define
            Assert.AreEqual(true, Map.isInit());
        }


        /// <summary>
        /// Map initialisation tests with wrong instructions
        /// </summary>

        [Test, Description("Instruction is incorrectly defined")]
        public void GetInstance_InstructionIncorrectlyDefined_ArgumentException()
        {
            //Arrange 
            Map _map;

            //Act 
            if (Map.isInit())
            {
                Map.clearMap();
            }
            //badly defined
            _instruction.Mountain_instruction.Add("M - 1 - 0");

            //Assert
            Assert.Throws<FormatException>(() => _map = Map.GetInstance(_instruction));



        }

        /// <summary>
        /// Map initialisation tests with empty instructions
        /// </summary>

        [Test, Description("Instruction is empty")]
        public void GetInstance_InstructionIsEmpty_ArgumentNullException()
        {
            //Arrange 
            Instruction empty_instruction = new Instruction();
            Map _map;

            //Act 
            if (Map.isInit())
            {
                Map.clearMap();
            }

            //Assert
            Assert.Throws<ArgumentNullException>(() => _map = Map.GetInstance(empty_instruction));



        }


        /// <summary>
        /// Map initialisation is too big 
        /// width min:0 - width max:125, 
        /// height min:0 - height max:250
        /// </summary>

        [Test, Description("Map instructions are too big")]
        public void GetInstance_InstructionSizeAreTooBig_ArgumentNullException()
        {
            //Arrange 
            Instruction empty_instruction = new Instruction();
            Map _map;

            //Act 
            if (Map.isInit())
            {
                Map.clearMap();
            }
            
            _instruction.Map_instruction = "128 - 251";

            //Assert
            Assert.Throws<ArgumentException>(() => _map = Map.GetInstance(_instruction));
        }



        /// <summary>
        /// Map not init tests 
        /// </summary>

        [Test, Description("Map is not initialise")]
        public void isInit_MapIsEmpty_IsFalse()
        {
            //Arrange 
            Map _map = Map.GetInstance(_instruction);

            //Act 
            if (Map.isInit())
            {
                Map.clearMap();
            }

            //Assert
            Assert.IsFalse(Map.isInit());


        }



        /// <summary>
        /// Map position is already set with the same key
        /// </summary>

        [Test, Description("Map position is already set with the same key, check with checkPositionIsNotDefined() ")]
        public void checkPositionIsNotDefined_MapPositionAlreadySet_IsFalse()
        {
            //Arrange 
            Map _map = Map.GetInstance(_instruction);

            //Act 
            Position temp_position = new Position(1, 0);

            //Assert
            Assert.IsFalse(_map.checkPositionIsDefined(temp_position));

        }



        /// <summary>
        /// Map position is not set with the same key
        /// </summary>

        [Test, Description("Map position is not set with the same key, check with checkPositionIsNotDefined() ")]
        public void checkPositionIsNotDefined_MapPositionNotSet_IsTrue()
        {
            //Arrange 
            Map _map = Map.GetInstance(_instruction);

            //Act 
            Position temp_position = new Position(2, 0);

            //Assert
            Assert.IsFalse(_map.checkPositionIsDefined(temp_position));

        }



        /// <summary>
        /// get value with position in the temp map
        /// </summary>

        [Test, Description("Map position is not set with the same key, check with checkPositionIsNotDefined() ")]
        public void getValueByPosition_MapPositionNotSet_IsTrue()
        {
            ////Arrange 
            //Map _map = Map.GetInstance(_instruction);

            //Dictionary<Position, Surface> temp_mapGrids = new Dictionary<Position, Surface>();
            //temp_mapGrids.Add(new Position(1,0), new Mountain());
            //temp_mapGrids.Add(new Position(1, 2), new Mountain());
            //temp_mapGrids.Add(new Position(0, 1), new Treasure(2));
            //temp_mapGrids.Add(new Position(1, 0), new Treasure(3));

            ////Act 
            //Surface returnPosition = _map.getValueByPosition(temp_mapGrids, new Position(0, 1));

            ////Assert
            //Assert.IsFalse(_map.checkPositionIsNotDefined(temp_position));

        }


        [Test, Description("Return the amount number of the treasures on map")]
        public void getNumberOfTreasure_NbOfTreasureIs5_IsTrue()
        {
            //Arrange 
            Map _map = Map.GetInstance(_instruction);

            //Act 
            int number = _map.getNumberOfTreasure();

            //Assert
            Assert.IsTrue(number==5);

        }


        



    }
    }