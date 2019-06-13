using Microsoft.VisualStudio.TestTools.UnitTesting;
using BorwellSoftwareChallenge_v1._0;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test1_IntegerInput()
        {
            // For input of width 2 m, depth 3 m, height 4 m
            // Floor area = 2 x 3 = 6 m^2
            // Paint volume = (2 x 4 x 2) + (2 x 4 x 3) = 16 + 24 = 40 => 4 litres
            // Room volume = 2 x 3 x 4 = 24 m^3
            Program.RoomResults expectedResults = new Program.RoomResults(6, 4, 24);
            Program.RoomResults testResults =  Program.CalculateResults(2, 3, 4);
            Assert.AreEqual(expectedResults.FloorArea, testResults.FloorArea);
            Assert.AreEqual(expectedResults.PaintVolume, testResults.PaintVolume);
            Assert.AreEqual(expectedResults.RoomVolume, testResults.RoomVolume);
        }

        [TestMethod]
        public void Test2_DecimalInput()
        {
            // For input of width 14.25 m, depth 19.5 m, height 3.5 m
            // Floor area = 14.25 x 19.5 = 277.875 m^2
            // Paint volume = (2 x 14.25 x 3.5) + (2 x 19.5 x 3.5) = 99.75 + 136.5 = 236.25 => 23.625 litres
            // Room volume = 14.25 x 19.5 x 3.5 = 972.5625 m^3
            Program.RoomResults expectedResults = new Program.RoomResults(277.875, 23.625, 972.5625);
            Program.RoomResults testResults = Program.CalculateResults(14.25, 19.5, 3.5);
            Assert.AreEqual(expectedResults.FloorArea, testResults.FloorArea);
            Assert.AreEqual(expectedResults.PaintVolume, testResults.PaintVolume);
            Assert.AreEqual(expectedResults.RoomVolume, testResults.RoomVolume);
        }

        [TestMethod]
        public void Test3_NonNumericInput()
        {
            bool expectedFail = false;
            bool expectedPass = true;
            bool testFail = Program.CheckInput("three.2");
            bool testPass = Program.CheckInput("3.2");
            Assert.AreEqual(expectedFail, testFail);
            Assert.AreEqual(expectedPass, testPass);
        }

        [TestMethod]
        public void Test4_InputRange()
        {
            bool expectedFail = false;
            bool expectedPass = true;
            bool testFail1 = Program.CheckInput(double.MaxValue.ToString("e"));
            bool testFail2 = Program.CheckInput(double.Epsilon.ToString("e"));
            bool testPass1 = Program.CheckInput(Math.Pow(double.MaxValue, 1.0 / 3.0).ToString("e"));
            bool testPass2 = Program.CheckInput(Math.Pow(double.Epsilon, 1.0 / 3.0).ToString("e"));
            Assert.AreEqual(testFail1, expectedFail);
            Assert.AreEqual(testFail2, expectedFail);
            Assert.AreEqual(testPass1, expectedPass);
            Assert.AreEqual(testPass2, expectedPass);
        }

    }
}
