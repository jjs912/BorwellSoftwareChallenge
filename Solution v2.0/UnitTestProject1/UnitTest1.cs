using Microsoft.VisualStudio.TestTools.UnitTesting;
using BorwellSoftwareChallenge_v2._0;
using System;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_01_IntegerInput()
        {
            // For input of width 2 m, depth 3 m, height 4 m
            // Floor area = 2 x 3 = 6 m^2
            // Paint volume = (2 x 4 x 2) + (2 x 4 x 3) = 16 + 24 = 40 => 4 litres
            // Room volume = 2 x 3 x 4 = 24 m^3
            Program.RoomResults expectedResults = new Program.RoomResults(6, 4, 24);
            Program.RoomResults testResults = Program.CalculateResults(2, 3, 4, new System.Collections.Generic.List<double[]>());
            Assert.AreEqual(expectedResults.FloorArea, testResults.FloorArea);
            Assert.AreEqual(expectedResults.PaintVolume, testResults.PaintVolume);
            Assert.AreEqual(expectedResults.RoomVolume, testResults.RoomVolume);
        }

        [TestMethod]
        public void Test_02_DecimalInput()
        {
            // For input of width 14.25 m, depth 19.5 m, height 3.5 m
            // Floor area = 14.25 x 19.5 = 277.875 m^2
            // Paint volume = (2 x 14.25 x 3.5) + (2 x 19.5 x 3.5) = 99.75 + 136.5 = 236.25 => 23.625 litres
            // Room volume = 14.25 x 19.5 x 3.5 = 972.5625 m^3
            Program.RoomResults expectedResults = new Program.RoomResults(277.875, 23.625, 972.5625);
            Program.RoomResults testResults = Program.CalculateResults(14.25, 19.5, 3.5, new System.Collections.Generic.List<double[]>());
            Assert.AreEqual(expectedResults.FloorArea, testResults.FloorArea);
            Assert.AreEqual(expectedResults.PaintVolume, testResults.PaintVolume);
            Assert.AreEqual(expectedResults.RoomVolume, testResults.RoomVolume);
        }

        [TestMethod]
        public void Test_03_NonNumericInput()
        {
            bool expectedFail = false;
            bool expectedPass = true;
            bool testFail = Program.CheckInput("three.2");
            bool testPass = Program.CheckInput("3.2");
            Assert.AreEqual(expectedFail, testFail);
            Assert.AreEqual(expectedPass, testPass);
        }

        [TestMethod]
        public void Test_04_InputRange()
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

        [TestMethod]
        public void Test_05_IntegerWindows()
        {
            // For input of width 2 m, depth 3 m, height 4 m
            // Windows: (2m x 1m), (3m x 2m)
            // Floor area = 2 x 3 = 6 m^2
            // Paint volume = (2 x 4 x 2) + (2 x 4 x 3) - (2 x 1) - (3 x 2) = 16 + 24 - 2 - 6 = 40 - 8 = 32 => 3.2 litres
            // Room volume = 2 x 3 x 4 = 24 m^3
            List<double[]> windows = new List<double[]>
            {
                new double[] {2, 1},
                new double[] {3, 2}
            };
            Program.RoomResults expectedResults = new Program.RoomResults(6, 3.2, 24);
            Program.RoomResults testResults = Program.CalculateResults(2, 3, 4, windows);
            Assert.AreEqual(expectedResults.FloorArea, testResults.FloorArea);
            Assert.AreEqual(expectedResults.PaintVolume, testResults.PaintVolume);
            Assert.AreEqual(expectedResults.RoomVolume, testResults.RoomVolume);
        }

        [TestMethod]
        public void Test_06_DecimalWindows()
        {
            // For input of width 14.25 m, depth 19.5 m, height 3.5 m
            // Windows: (3.75m x 2.125m), (5.5m x 2.625m)
            // Paint volume = (2 x 4 x 2) + (2 x 4 x 3) - (2 x 1) - (3 x 2) = 16 + 24 - 2 - 6 = 40 - 8 = 32 => 3.2 litres
            // Floor area = 14.25 x 19.5 = 277.875 m^2
            // Paint volume = (2 x 14.25 x 3.5) + (2 x 19.5 x 3.5) - (3.75 x 2.125) - (5.5 x 2.625) = 99.75 + 136.5 - 7.96875 - 14.4375 = 236.25 - 22.40625 = 213.84375 => 21.384375 litres
            // Room volume = 14.25 x 19.5 x 3.5 = 972.5625 m^3
            List<double[]> windows = new List<double[]>
            {
                new double[] {3.75, 2.125},
                new double[] {5.5, 2.625}
            };
            Program.RoomResults expectedResults = new Program.RoomResults(277.875, 21.384375, 972.5625);
            Program.RoomResults testResults = Program.CalculateResults(14.25, 19.5, 3.5, windows);
            Assert.AreEqual(expectedResults.FloorArea, testResults.FloorArea, 1e-10);
            Assert.AreEqual(expectedResults.PaintVolume, testResults.PaintVolume, 1e-10);
            Assert.AreEqual(expectedResults.RoomVolume, testResults.RoomVolume, 1e-10);
        }

        [TestMethod]
        public void Test_07_TwoDimensionalWindows()
        {
            bool expectedFail = false;
            bool expectedPass = true;
            bool testFail = (Program.CheckWindowInput("1 2 3", new List<double[]>(), 10, 10, 10)).ValidInput;
            bool testFail2 = (Program.CheckWindowInput("1", new List<double[]>(), 10, 10, 10)).ValidInput;
            bool testPass = (Program.CheckWindowInput("1 2", new List<double[]>(), 10, 10, 10)).ValidInput;
            Assert.AreEqual(expectedFail, testFail);
            Assert.AreEqual(expectedFail, testFail2);
            Assert.AreEqual(expectedPass, testPass);
        }

        [TestMethod]
        public void Test_08_NumericWindows()
        {
            bool expectedFail = false;
            bool expectedPass = true;
            bool testFail = (Program.CheckWindowInput("one two", new List<double[]>(), 10, 10, 10)).ValidInput;
            bool testPass = (Program.CheckWindowInput("1 2", new List<double[]>(), 10, 10, 10)).ValidInput;
            Assert.AreEqual(expectedFail, testFail);
            Assert.AreEqual(expectedPass, testPass);
        }

        [TestMethod]
        public void Test_09_ValidHeightWindows()
        {
            bool expectedFail = false;
            bool expectedPass = true;
            bool testFail = (Program.CheckWindowInput("1 11", new List<double[]>(), 10, 10, 10)).ValidInput;
            bool testPass = (Program.CheckWindowInput("1 2", new List<double[]>(), 10, 10, 10)).ValidInput;
            Assert.AreEqual(expectedFail, testFail);
            Assert.AreEqual(expectedPass, testPass);
        }

        [TestMethod]
        public void Test_10_ValidWidthWindows()
        {
            bool expectedFail = false;
            bool expectedPass = true;
            bool testFail = (Program.CheckWindowInput("50 1", new List<double[]>(), 10, 10, 10)).ValidInput;
            bool testFail2 = (Program.CheckWindowInput("20 1",
                new List<double[]>()
                    {
                        new double[] {10, 1 },
                        new double[] {10, 1 },
                        new double[] {10, 1 },
                        new double[] {10, 1 },
                    },
                10, 10, 10)).ValidInput;
            bool testPass = (Program.CheckWindowInput("1 2", new List<double[]>(), 10, 10, 10)).ValidInput;
            Assert.AreEqual(expectedFail, testFail);
            Assert.AreEqual(expectedFail, testFail2);
            Assert.AreEqual(expectedPass, testPass);
        }

        [TestMethod]
        public void Test_11_ValidInputRangeWindows()
        {
            bool expectedFail = false;
            bool expectedPass = true;
            bool testFail1 = Program.CheckWindowInput($"{double.MaxValue.ToString("e")} {double.MaxValue.ToString("e")}", new List<double[]>(), double.MaxValue, double.MaxValue, double.MaxValue).ValidInput;
            bool testFail2 = Program.CheckWindowInput($"{double.MinValue.ToString("e")} {double.MinValue.ToString("e")}", new List<double[]>(), double.MaxValue, double.MaxValue, double.MaxValue).ValidInput;
            bool testFail3 = Program.CheckWindowInput($"{double.Epsilon.ToString("e")} {double.Epsilon.ToString("e")}", new List<double[]>(), double.MaxValue, double.MaxValue, double.MaxValue).ValidInput;
            bool testPass1 = Program.CheckWindowInput($"{Math.Pow(double.MaxValue, 1.0/2.1).ToString("e")} {Math.Pow(double.MaxValue, 1.0/2.1).ToString("e")}", new List<double[]>(), double.MaxValue, double.MaxValue, double.MaxValue).ValidInput;
            bool testPass2 = Program.CheckWindowInput($"{Math.Pow(double.Epsilon, 1.0/2.1).ToString("e")} {Math.Pow(double.Epsilon, 1.0/2.1).ToString("e")}", new List<double[]>(), double.MaxValue, double.MaxValue, double.MaxValue).ValidInput;
            Assert.AreEqual(testFail1, expectedFail);
            Assert.AreEqual(testFail2, expectedFail);
            Assert.AreEqual(testPass1, expectedPass);
            Assert.AreEqual(testPass2, expectedPass);
        }
    }
}
