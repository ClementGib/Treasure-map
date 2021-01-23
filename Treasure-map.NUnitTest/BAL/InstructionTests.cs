using NUnit.Framework;
using System;
using System.IO;
using BAL;

namespace UnitTests.IO
{
    [TestFixture]
    public class InstructionTests
    {

        private InstructionTests _fileManager;

        protected string path = null;
        


        [SetUp]
        public void Setup()
        {

            string currentPath = Directory.GetCurrentDirectory();
            //set path to current directory files
            path = Path.GetFullPath(Path.Combine(currentPath, @"..\..\..\BAL"));
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
            //Assert.Throws<FileNotFoundException>(() => _fileManager.readFile(P_fileName));
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




    }
}