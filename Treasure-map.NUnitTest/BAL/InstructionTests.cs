using NUnit.Framework;
using System;
using System.IO;
using BAL;

namespace UnitTests.BAL.InstructionsTests
{
    [TestFixture]
    public class InstructionTests
    {
        private Instruction _instruction;

        [SetUp]
        public void Setup()
        {
            _instruction = new Instruction();
        }


        /// <summary>
        /// commentaries tests same value
        /// </summary>
        /// <param name="com_instruction"></param>

        [TestCase("# Map size definition"), Description("Set the same value to commentaries Dictionnary")]
        public void Commentaries_SetSameValue_IsEqualTo(string com_instruction)
        {

            //Arrange 
            Instruction temp_instruction1 = new Instruction();
            Instruction temp_instruction2 = new Instruction();
            _instruction = new Instruction();


            //Act
            temp_instruction1.Commentaries.Add(0, com_instruction);
            temp_instruction2.Commentaries.Add(1,com_instruction);

            _instruction.Commentaries = temp_instruction1.Commentaries;

            //set second time
            _instruction.Commentaries = temp_instruction2.Commentaries;


            //Assert
            Assert.AreEqual(2, _instruction.Commentaries.Count);
        }


        /// <summary>
        /// commentaries tests same key
        /// </summary>
        /// <param name="com_instruction"></param>

        [TestCase("# Map size definition"), Description("Set the same key to commentaries Dictionnary")]
        public void Commentaries_SetSameKey_IsEqualTo(string com_instruction)
        {

            //Arrange 
            Instruction temp_instruction1 = new Instruction();
            Instruction temp_instruction2 = new Instruction();
            _instruction = new Instruction();


            //Act
            temp_instruction1.Commentaries.Add(0, com_instruction);
            temp_instruction2.Commentaries.Add(0, com_instruction);

            _instruction.Commentaries = temp_instruction1.Commentaries;

            //set second time
            


            //Assert
            Assert.Throws<ArgumentException>(() => _instruction.Commentaries = temp_instruction2.Commentaries);
        }

        /// <summary>
        /// commentaries tests
        /// </summary>
        /// <param name="com_instruction"></param>

        [TestCase("# Map size definition"), Description("Set and check commentaries value")]
        public void Commentaries_SetValue_IsEqualTo(string com_instruction)
        {

            //Arrange 
            _instruction = new Instruction();


            //Act
            _instruction.Commentaries.Add(0,com_instruction);
            _instruction.Commentaries.Add(4,com_instruction);
            _instruction.Commentaries.Add(8,com_instruction);


            //Assert
            Assert.AreEqual(3, _instruction.Commentaries.Count);
        }


        /// <summary>
        /// map tests
        /// </summary>
        /// <param name="map_instruction"></param>

        [TestCase("C - 3 - 4"), Description("Set and check map value")]
        public void Map_SetValue_IsEqualTo(string map_instruction)
        {

            //Arrange 
            _instruction = new Instruction();


            //Act
            _instruction.Map_instruction = map_instruction;

            //Assert
            StringAssert.AreEqualIgnoringCase("3 - 4", _instruction.Map_instruction );
        }



        /// <summary>
        /// map tests wrong value
        /// </summary>
        /// <param name="map_instruction"></param>

        [TestCase("C - 3 - 4 - 4"), Description("Set wrong value to map")]
        public void Map_SetWrongValue_ArgumentException(string map_instruction)
        {

            //Arrange 
            _instruction = new Instruction();


            //Act & Assert
            Assert.Throws<ArgumentException>(() => _instruction.Map_instruction = map_instruction);
        }



        /// <summary>
        /// mountain tests
        /// </summary>
        /// <param name="mountain_instruction"></param>

        [TestCase("M - 1 - 0"), Description("Set and check mountain value")]
        public void Mountain_SetValue_IsEqualTo(string mountain_instruction)
        {

            //Arrange 
            Instruction temp_instruction = new Instruction();
            _instruction = new Instruction();


            //Act
            temp_instruction.Mountain_instruction.Add(mountain_instruction);
            _instruction.Mountain_instruction = temp_instruction.Mountain_instruction;

            //Assert
            StringAssert.AreEqualIgnoringCase("1 - 0", _instruction.Mountain_instruction[0]);
        }


        /// <summary>
        /// mountain tests wrong value
        /// </summary>
        /// <param name="mountain_instruction"></param>

        [TestCase("C - 3 - 4"), Description("Set wrong value to mountain")]
        public void Mountain_SetWrongValue_ArgumentException(string mountain_instruction)
        {

            //Arrange
            _instruction = new Instruction();
            Instruction temp_instruction = new Instruction();

            temp_instruction.Mountain_instruction.Add(mountain_instruction);



            //Act & Assert
            Assert.Throws<ArgumentException>(() => _instruction.Mountain_instruction = temp_instruction.Mountain_instruction);
        }



        /// <summary>
        /// treasure tests
        /// </summary>
        /// <param name="treasure_instruction"></param>

        [TestCase("T - 0 - 3 - 2"), Description("Set and check treasure value")]
        public void Treasure_SetValue_IsEqualTo(string treasure_instruction)
        {

            //Arrange 
            Instruction temp_instruction = new Instruction();
            _instruction = new Instruction();


            //Act
            temp_instruction.Treasure_instruction.Add(treasure_instruction);
            _instruction.Treasure_instruction = temp_instruction.Treasure_instruction;


            //Assert
            StringAssert.AreEqualIgnoringCase("0 - 3 - 2", _instruction.Treasure_instruction[0]);
        }

        /// <summary>
        /// treasure tests wrong value
        /// </summary>
        /// <param name="treasure_instruction"></param>

        [TestCase("M - 2 - 1"), Description("Set wrong value to treasure")]
        public void Treasure_SetWrongValue_ArgumentException(string treasure_instruction)
        {

            //Arrange
            _instruction = new Instruction();
            Instruction temp_instruction = new Instruction();

            temp_instruction.Treasure_instruction.Add(treasure_instruction);
            


            //Act & Assert
            Assert.Throws<ArgumentException>(() => _instruction.Treasure_instruction = temp_instruction.Treasure_instruction);
        }



        /// <summary>
        /// adventurer tests
        /// </summary>
        /// <param name="adventurer_instruction"></param>

        [TestCase("A - Lara - 1 - 1 - S - AADADAGGA"), Description("Set and check adventurer value")]
        public void Adventurer_SetValue_IsEqualTo(string adventurer_instruction)
        {

            //Arrange 
            _instruction = new Instruction();


            //Act
            _instruction.Adventurer_instruction = adventurer_instruction;

            //Assert
            StringAssert.AreEqualIgnoringCase("Lara - 1 - 1 - S - AADADAGGA", _instruction.Adventurer_instruction);
        }

        /// <summary>
        /// adventurer tests wrong value
        /// </summary>
        /// <param name="adventurer_instruction"></param>

        [TestCase("T - 0 - 3 - 2"), Description("Set wrong value to adventurer")]
        public void Adventurer_SetWrongValue_ArgumentException(string adventurer_instruction)
        {

            //Arrange 
            _instruction = new Instruction();


            //Act & Assert
            Assert.Throws<ArgumentException>(() => _instruction.Adventurer_instruction = adventurer_instruction);
        }




        /// <summary>
        /// Instruction is not null
        /// </summary>
        /// <param name="com_instruction"></param>
        /// <param name="map_instruction"></param>
        /// <param name="mountain_instruction"></param>
        /// <param name="treasure_instruction"></param>
        /// <param name="adventurer_instruction"></param>
        [TestCase("# Map size definition",
            "C - 3 - 4",
            "M - 1 - 0",
            "T - 0 - 3 - 2",
            "A - Lara - 1 - 1 - S - AADADAGGA"),
            Description("All instructions are set in the Instruction Object")]
        public void isNull_IfInstructionsAreDefined_ReturnFalse(
            string com_instruction,
            string map_instruction,
            string mountain_instruction,
            string treasure_instruction,
            string adventurer_instruction)
        {
            //TestContext.WriteLine(path);

            //Arrange
            _instruction = new Instruction();
            Instruction temp_instruction = new Instruction();

            //Act
            _instruction.Commentaries.Add(0,com_instruction);
            _instruction.Map_instruction = map_instruction;
            temp_instruction.Mountain_instruction.Add(mountain_instruction);
            _instruction.Mountain_instruction = temp_instruction.Mountain_instruction;
            temp_instruction.Treasure_instruction.Add(treasure_instruction);
            _instruction.Treasure_instruction = temp_instruction.Treasure_instruction;
            _instruction.Adventurer_instruction = adventurer_instruction;


            Assert.IsFalse(_instruction.isNull());
        }


        /// <summary>
        /// Instruction is null
        /// </summary>
        /// <param name="com_instruction"></param>
        /// <param name="map_instruction"></param>
        /// <param name="mountain_instruction"></param>
        /// <param name="treasure_instruction"></param>
        /// <param name="adventurer_instruction"></param>
        [TestCase("# Map size definition",
            "",
            "M - 1 - 0",
            "T - 0 - 3 - 2",
            "A - Lara - 1 - 1 - S - AADADAGGA"),
            Description("Map instruction is not set in the Instruction Object")]
        public void isNull_IfMapInstructionsIsNotDefined_ReturnTrue(
            string com_instruction,
            string map_instruction,
            string mountain_instruction,
            string treasure_instruction,
            string adventurer_instruction)
        {
            //TestContext.WriteLine(path);

            //Arrange
            _instruction = new Instruction();
            Instruction temp_instruction = new Instruction();

            //Act
            _instruction.Commentaries.Add(0,com_instruction);
            _instruction.Map_instruction = map_instruction;
            temp_instruction.Mountain_instruction.Add(mountain_instruction);
            _instruction.Mountain_instruction = temp_instruction.Mountain_instruction;
            temp_instruction.Treasure_instruction.Add(treasure_instruction);
            _instruction.Treasure_instruction = temp_instruction.Treasure_instruction;
            _instruction.Adventurer_instruction = adventurer_instruction;


            Assert.IsTrue(_instruction.isNull());
        }




        /// <summary>
        /// Instruction is null
        /// </summary>
        /// <param name="com_instruction"></param>
        /// <param name="map_instruction"></param>
        /// <param name="mountain_instruction"></param>
        /// <param name="treasure_instruction"></param>
        /// <param name="adventurer_instruction"></param>
        [TestCase("",
            "C - 3 - 4",
            "M - 1 - 0",
            "T - 0 - 3 - 2",
            "A - Lara - 1 - 1 - S - AADADAGGA"),
            Description("All instructions are set in the Instruction Object but without commentary")]
        public void isNull_IfCommentIsNotDefined_ReturnFalse(
            string com_instruction,
            string map_instruction,
            string mountain_instruction,
            string treasure_instruction,
            string adventurer_instruction)
        {
            //TestContext.WriteLine(path);

            //Arrange
            _instruction = new Instruction();
            Instruction temp_instruction = new Instruction();

            //Act
            _instruction.Commentaries.Add(0,com_instruction);
            _instruction.Map_instruction = map_instruction;
            temp_instruction.Mountain_instruction.Add(mountain_instruction);
            _instruction.Mountain_instruction = temp_instruction.Mountain_instruction;
            temp_instruction.Treasure_instruction.Add(treasure_instruction);
            _instruction.Treasure_instruction = temp_instruction.Treasure_instruction;
            _instruction.Adventurer_instruction = adventurer_instruction;


            Assert.IsFalse(_instruction.isNull());
        }



    }
}