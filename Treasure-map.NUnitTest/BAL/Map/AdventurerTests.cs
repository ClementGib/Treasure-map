using NUnit.Framework;
using System;
using System.IO;
using BAL;

namespace UnitTests.BAL.AdventurerTests
{
    [TestFixture]
    public class AdventurerTests
    {

        private Adventurer _adventurer; 


        [SetUp]
        public void Setup()
        {


        }


        /// <summary>
        /// Adventurer WANT move to the next direction 
        /// </summary>
        /// <param name="adventurer_instruction"></param>

        [TestCase("Lara - 1 - 1 - S - AADADAGGA"), Description("adventurer move to the south")]
        public void wantMove_OrientationIsSAndMovementIsA_IsTrue(string adventurer_instruction)
        {
            //Arrange
            _adventurer = new Adventurer(adventurer_instruction);
            Position expectedPosition = new Position(1, 2);

            //Act
            Position nextPosition = _adventurer.wantMove();

            //Assert
            Assert.IsTrue(PositionComparer.EqualsPositions(nextPosition,expectedPosition));
        }

        [TestCase("Lara - 1 - 1 - N - AADADAGGA"), Description("adventurer move to the north")]
        public void wantMove_OrientationIsNAndMovementIsA_IsTrue(string adventurer_instruction)
        {
            //Arrange
            _adventurer = new Adventurer(adventurer_instruction);
            Position expectedPosition = new Position(1, 0);

            //Act
            Position nextPosition = _adventurer.wantMove();

            //Assert
            Assert.IsTrue(PositionComparer.EqualsPositions(nextPosition, expectedPosition));
        }

        [TestCase("Lara - 1 - 1 - E - AADADAGGA"), Description("adventurer move to the north")]
        public void wantMove_OrientationIsEAndMovementIsA_IsTrue(string adventurer_instruction)
        {
            //Arrange
            _adventurer = new Adventurer(adventurer_instruction);
            Position expectedPosition = new Position(2, 1);

            //Act
            Position nextPosition = _adventurer.wantMove();

            //Assert
            Assert.IsTrue(PositionComparer.EqualsPositions(nextPosition, expectedPosition));
        }

        [TestCase("Lara - 1 - 1 - W - AADADAGGA"), Description("adventurer move to the north")]
        public void wantMove_OrientationIsWAndMovementIsA_IsTrue(string adventurer_instruction)
        {
            //Arrange
            _adventurer = new Adventurer(adventurer_instruction);
            Position expectedPosition = new Position(0, 1);

            //Act
            Position nextPosition = _adventurer.wantMove();

            //Assert
            Assert.IsTrue(PositionComparer.EqualsPositions(nextPosition, expectedPosition));
        }


        [TestCase("Lara - 1 - 1 - S - DADAGGA")]
        public void wantMove_OrientationIsSAndMovementIsD_IsTrue(string adventurer_instruction)
        {
            //Arrange
            _adventurer = new Adventurer(adventurer_instruction);
            Position expectedPosition = new Position(1, 1);

            //Act
            Position nextPosition = _adventurer.wantMove();

            //Assert
            Assert.IsTrue(PositionComparer.EqualsPositions(nextPosition, expectedPosition));

        }


        [TestCase("Lara - 1 - 1 - S - GADAGGA")]
        public void wantMove_OrientationIsSAndMovementIsG_IsTrue(string adventurer_instruction)
        {
            //Arrange
            _adventurer = new Adventurer(adventurer_instruction);
            Position expectedPosition = new Position(1, 1);

            //Act
            Position nextPosition = _adventurer.wantMove();

            //Assert
            Assert.IsTrue(PositionComparer.EqualsPositions(nextPosition, expectedPosition));

        }


        /// <summary>
        /// Adventurer MOVE to the next direction 
        /// </summary>
        /// <param name="adventurer_instruction"></param>


        [TestCase("Lara - 1 - 1 - S - ADAGGA")]
        public void moveNextStep_OrientationIsSAndMovementIsA_IsTrue(string adventurer_instruction)
        {
            //Arrange
            _adventurer = new Adventurer(adventurer_instruction);
            Position expectedPosition = new Position(1, 2);

            //Act
            _adventurer.moveNextStep();

            //Assert
            Assert.IsTrue(PositionComparer.EqualsPositions(new Position(_adventurer.XPosition, _adventurer.YPosition), expectedPosition));

        }


        [TestCase("Lara - 1 - 1 - N - ADAGGA")]
        public void moveNextStep_OrientationIsNAndMovementIsA_IsTrue(string adventurer_instruction)
        {
            //Arrange
            _adventurer = new Adventurer(adventurer_instruction);
            Position expectedPosition = new Position(1, 0);

            //Act
            _adventurer.moveNextStep();

            //Assert
            Assert.IsTrue(PositionComparer.EqualsPositions(new Position(_adventurer.XPosition, _adventurer.YPosition), expectedPosition));

        }

        [TestCase("Lara - 1 - 1 - E - ADAGGA")]
        public void moveNextStep_OrientationIsEAndMovementIsA_IsTrue(string adventurer_instruction)
        {
            //Arrange
            _adventurer = new Adventurer(adventurer_instruction);
            Position expectedPosition = new Position(2, 1);

            //Act
            _adventurer.moveNextStep();

            //Assert
            Assert.IsTrue(PositionComparer.EqualsPositions(new Position(_adventurer.XPosition, _adventurer.YPosition), expectedPosition));

        }

        [TestCase("Lara - 1 - 1 - W - ADAGGA")]
        public void moveNextStep_OrientationIsWAndMovementIsA_IsTrue(string adventurer_instruction)
        {
            //Arrange
            _adventurer = new Adventurer(adventurer_instruction);
            Position expectedPosition = new Position(0, 1);

            //Act
            _adventurer.moveNextStep();

            //Assert
            Assert.IsTrue(PositionComparer.EqualsPositions(new Position(_adventurer.XPosition, _adventurer.YPosition), expectedPosition));

        }


        [TestCase("Lara - 1 - 1 - S - DADAGGA")]
        public void moveNextStep_OrientationIsSAndMovementIsD_IsTrue(string adventurer_instruction)
        {
            //Arrange
            _adventurer = new Adventurer(adventurer_instruction);
            Position expectedPosition = new Position(1, 1);

            //Act
            _adventurer.moveNextStep();

            //Assert
            Assert.IsTrue(PositionComparer.EqualsPositions(new Position(_adventurer.XPosition, _adventurer.YPosition), expectedPosition) && (_adventurer.Orientation == 'W'));

        }


        [TestCase("Lara - 1 - 1 - S - GADAGGA")]
        public void moveNextStep_OrientationIsSAndMovementIsG_IsTrue(string adventurer_instruction)
        {
            //Arrange
            _adventurer = new Adventurer(adventurer_instruction);
            Position expectedPosition = new Position(1, 1);

            //Act
            _adventurer.moveNextStep();

            //Assert
            Assert.IsTrue(PositionComparer.EqualsPositions(new Position(_adventurer.XPosition, _adventurer.YPosition), expectedPosition) && (_adventurer.Orientation == 'E'));

        }



        /// <summary>
        /// Adventurer steps
        /// </summary>
        /// <param name="adventurer_instruction"></param>


        [TestCase("Lara - 2 - 2 - S - ADAGA")]
        public void moveNextStep_MovementIsADAGA_IsTrue(string adventurer_instruction)
        {
            //Arrange
            _adventurer = new Adventurer(adventurer_instruction);
            Position expectedPosition = new Position(1,4);

            //Act
            _adventurer.moveNextStep();
            _adventurer.moveNextStep();
            _adventurer.moveNextStep();
            _adventurer.moveNextStep();
            _adventurer.moveNextStep();

            //Assert
            Assert.IsTrue(PositionComparer.EqualsPositions(new Position(_adventurer.XPosition, _adventurer.YPosition), expectedPosition));

        }

    }
}