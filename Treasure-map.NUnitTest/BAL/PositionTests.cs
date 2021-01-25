using NUnit.Framework;
using System;
using System.IO;
using BAL;

namespace UnitTests.BAL.PositionTests
{
    [TestFixture]
    public class PositionTests
    {
        private Position _position;

        [SetUp]
        public void Setup()
        {
            _position = new Position(10,10);
        }


        /// <summary>
        /// Compare Positions with Positions Comparer 
        /// </summary>
        /// <param name="P_x"></param>
        /// /// <param name="P_y"></param>

        [TestCase(10,10), Description("Compare two same positions")]
        public void EqualsPositions_HaveSameValues_IsTrue(int P_x,int P_y)
        {

            ////Arrange 
            Position otherPosition = new Position(P_x, P_y);


            ////Assert
            Assert.IsTrue(PositionComparer.EqualsPositions(_position, otherPosition));
        }


        [TestCase(10, 10), Description("Compare two same positions with HashCode")]
        public void EqualHashCodePositions_HaveSameValues_IsTrue(int P_x, int P_y)
        {

            ////Arrange 
            Position otherPosition = new Position(P_x, P_y);


            ////Assert
            Assert.IsTrue(PositionComparer.EqualHashCodePositions(_position, otherPosition));
        }




    }
}