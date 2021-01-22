using NUnit.Framework;
using System;
using System.IO;
using IO;

namespace UnitTests.IO
{
    [TestFixture]
    public class IOManagerTests
    {

        private IOManager _fileManager;

        protected string path = null;
        


        [SetUp]
        public void Setup()
        {
            _fileManager = IOManager.GetInstance;

            string currentPath = Directory.GetCurrentDirectory();
            //set path to current directory files
            path = Path.GetFullPath(Path.Combine(currentPath, @"..\..\..\IO"));
        }


        /// <summary>
        /// files configuration tests
        /// </summary>
        /// <param name="P_fileName"></param>

        [TestCase("notthere.txt"), Description("File not found")]
        public void readFile_IfFileNotExists_FileNotFoundException(string P_fileName)
        {
            TestContext.WriteLine("File name : " + P_fileName);
            //Act & Assert
            Assert.Throws<FileNotFoundException>(() => _fileManager.readFile(P_fileName));
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
            bool returnValue = _fileManager.readFile(basicPath+P_fileName);

            //Assert -> 1 return code signify that the file is Okay
            Assert.AreEqual(true, returnValue);
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
            Assert.Throws<FormatException>(() => _fileManager.readFile(basicPath + P_fileName));
        }


        /// <summary>
        /// MAP definition tests
        /// </summary>
        /// <param name="P_fileName"></param>

        [TestCase("MapAbsentElementFile.txt"), Description("Wrong map definition")]
        [TestCase("MapMultiInstanceOf.txt")]
        [TestCase("MapWrongNumberOfArguments.txt")]
        [TestCase("MapWrongSizeDefinition.txt")]
        public void readFile_IfWrongMapDefinition_ReturnFormatException(string P_fileName)
        {

            //Arrange 
            string basicPath = path + "\\Map\\";

            //Act & Assert 
            Assert.Throws<FormatException>(() => _fileManager.readFile(basicPath + P_fileName));
        }


        /// <summary>
        /// Wrong MOUNTAIN definition tests
        /// </summary>
        /// <param name="P_fileName"></param>

        [TestCase("MountainAbsentElementFile.txt"), Description("Wrong mountain definition")]
        [TestCase("MountainWrongNumberOfArguments.txt")]
        [TestCase("MountainWrongPositionDefinition.txt")]
        public void readFile_IfWrongMountainDefinition_ReturnFormatException(string P_fileName)
        {

            //Arrange 
            string basicPath = path + "\\Mountain\\";

            //Act & Assert 
            Assert.Throws<FormatException>(() => _fileManager.readFile(basicPath + P_fileName));
        }

        /// <summary>
        /// Good MOUNTAIN definition tests
        /// </summary>
        /// <param name="P_fileName"></param>

        [TestCase("MountainMultiInstance.txt"), Description("Good mountain definition")]
        [TestCase("MountainSamePosition.txt")]
        public void readFile_IfGoodMountainDefinition_ReturnTrue(string P_fileName)
        {

            //Arrange 
            string basicPath = path + "\\Mountain\\";

            //Act
            bool returnValue = _fileManager.readFile(basicPath + P_fileName);

            //Assert -> 1 return code signify that the file is Okay
            Assert.AreEqual(true, returnValue);
        }

        /// <summary>
        /// Wrong TREASURE definition tests
        /// </summary>
        /// <param name="P_fileName"></param>

        [TestCase("TreasureAbsentElementFile.txt"), Description("Wrong treasure definition")]
        [TestCase("TreasureWrongNumberOfArguments.txt")]
        [TestCase("TreasureWrongPositionDefinition.txt")]
        public void readFile_IfWrongTreasureDefinition_ReturnFormatException(string P_fileName)
        {

            //Arrange 
            string basicPath = path + "\\Treasure\\";

            //Act & Assert 
            Assert.Throws<FormatException>(() => _fileManager.readFile(basicPath + P_fileName));
        }

        /// <summary>
        /// Good TREASURE definition tests
        /// </summary>
        /// <param name="P_fileName"></param>

        [TestCase("TreasureMultiInstance.txt"), Description("Good treasure definition")]
        [TestCase("TreasureSamePosition.txt")]
        public void readFile_IfGoodTreasureDefinition_ReturnTrue(string P_fileName)
        {

            //Arrange 
            string basicPath = path + "\\Treasure\\";

            //Act
            bool returnValue = _fileManager.readFile(basicPath + P_fileName);

            //Assert -> 1 return code signify that the file is Okay
            Assert.AreEqual(true, returnValue);
        }


        /// <summary>
        /// ADVENTURER definition tests
        /// </summary>
        /// <param name="P_fileName"></param>

        [TestCase("AdventurerAbsentElementFile.txt"), Description("Wrong adventurer definition")]
        [TestCase("AdventurerMultiInstance.txt")]
        [TestCase("AdventurerOrientationDefinition.txt")]
        [TestCase("AdventurerPositionDefinition.txt")]
        [TestCase("AdventurerWrongMovementDefinition.txt")]
        [TestCase("AdventurerWrongNumberOfArguments.txt")]
        public void readFile_IfWrongAdventurerDefinition_ReturnFormatException(string P_fileName)
        {

            //Arrange 
            string basicPath = path + "\\Adventurer\\";

            //Act & Assert 
            Assert.Throws<FormatException>(() => _fileManager.readFile(basicPath + P_fileName));
        }



    }
}