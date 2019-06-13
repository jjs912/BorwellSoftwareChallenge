using System;
using System.Collections.Generic;
using System.Linq;

namespace BorwellSoftwareChallenge_v2._0
{
    public class Program
    {
        public static void Main()
        {
            // Outputs Needed:
            // 1. Area of the floor
            // 2. Amount of paint required to paint the walls
            // 3. Volume of the room

            startProgram:

            string userInputWidth, userInputDepth, userInputHeight, userInputWindow, userResponse;
            double width, depth, height;

            Console.WriteLine("**** Borwell Software Challenge v2.0 ****");
            Console.WriteLine("**** Basic Rectangular Room with Windows/Doors ****");
            Console.WriteLine();

            // Get & Check User Input for Width
            roomWidth:
            Console.WriteLine("Enter Room Width (in metres):");
            userInputWidth = Console.ReadLine();
            if (!CheckInput(userInputWidth)) { goto roomWidth; }
            else { double.TryParse(userInputWidth, out width); }

            // Get & Check User Input for Depth
            roomDepth:
            Console.WriteLine("Enter Room Depth (in metres):");
            userInputDepth = Console.ReadLine();
            if (!CheckInput(userInputDepth)) { goto roomDepth; }
            else { double.TryParse(userInputDepth, out depth); }

            // Get & Check User Input for Height
            roomHeight:
            Console.WriteLine("Enter Room Height (in metres):");
            userInputHeight = Console.ReadLine();
            if (!CheckInput(userInputHeight)) { goto roomHeight; }
            else { double.TryParse(userInputHeight, out height); }

            // Get & Check User Input for Windows/Doors
            List<double[]> windowList = new List<double[]>();
            Console.WriteLine("Enter dimensions of windows/doors/areas not to be painted.\r\n" +
                "(e.g.)\r\n" +
                "0.7 1.2\r\n" +
                "1.1 1.2\r\n" +
                "1.5 2.2\r\n" +
                "F\r\n" +
                "Enter each in metres as WIDTH HEIGHT then enter 'F' when finished:");
            windowInput:
            userInputWindow = Console.ReadLine();
            if (userInputWindow.ToUpper() != "F")
            {
                WindowResults wr = CheckWindowInput(userInputWindow, windowList, width, depth, height);
                if (wr.ValidInput)
                {
                    windowList.Add(wr.WindowDimensions);
                }
                goto windowInput;
            }

            string windowInfo = "";
            for(int i = 0; i < windowList.Count; i++)
            {
                windowInfo += $"{i+1}. {windowList[i][0]} m x {windowList[i][1]} m\r\n";
            }
            if(windowInfo.Length == 0) { windowInfo = "None."; }

            // Get Calculation Results
            RoomResults calcResults = CalculateResults(width, depth, height, windowList);

            Console.WriteLine($"*** Results *** \r\n" +
                $"Based on these dimensions: \r\n" +
                $"Width: {width} m, Depth: {depth} m, Height: {height} m \r\n\r\n" +
                $"Windows:\r\n" + windowInfo + "\r\n" +
                $"1. Floor Area: {calcResults.FloorArea} m^2 \r\n" +
                $"2. Volume of Paint Needed to Cover Walls: {calcResults.PaintVolume} litres \r\n" +
                $"3. Volume of Room: {calcResults.RoomVolume} m^3 \r\n\r\n");

            Console.WriteLine("Enter 'R' to restart, or any other key to end program.");

            userResponse = Console.ReadLine();
            if (userResponse.ToUpper() == "R")
            {
                goto startProgram;
            }
            else
            {
                return;
            }

        }

        public class RoomResults
        {
            public double FloorArea, PaintVolume, RoomVolume;

            public RoomResults(double floorArea, double paintVolume, double roomVolume)
            {
                FloorArea = floorArea;
                PaintVolume = paintVolume;
                RoomVolume = roomVolume;
            }
        }

        public class WindowResults
        {
            public bool ValidInput;
            public double[] WindowDimensions;

            public WindowResults(bool validInput, double[] windowDimensions)
            {
                ValidInput = validInput;
                WindowDimensions = windowDimensions;
            }
        }

        public static RoomResults CalculateResults(double width, double depth, double height, List<double[]> windows)
        {
            // Calculate Floor Area
            // Area = Width x Depth
            double floorArea = width * depth;

            // Calculate Volume of Paint Needed
            // Note: Based on 1 litre per 10 square metres
            double wallArea = 2 * height * (width + depth);
            double windowArea = 0;
            for(int i = 0; i < windows.Count; i++)
            {
                windowArea += (windows[i][0] * windows[i][1]);
            }
            double paintVolume = (wallArea - windowArea) * 0.1;

            // Calculate Room Volume
            // Volume = Width x Depth x Height
            double roomVolume = width * depth * height;

            RoomResults results = new RoomResults(floorArea, paintVolume, roomVolume);

            return results;
        }

        public static bool CheckInput(string inputAsString)
        {
            double inputAsDouble;
            bool isNumeric = double.TryParse(inputAsString, out inputAsDouble);
            if (!isNumeric)
            {
                Console.WriteLine("Input must be numeric.");
                return false;
            }
            double minValue = Math.Pow(double.Epsilon, 1.0 / 3.0);
            double maxValue = Math.Pow(double.MaxValue, 1.0 / 3.0);
            if (inputAsDouble < minValue)
            {
                Console.WriteLine("Input too small.");
                return false;
            }
            else if (inputAsDouble > maxValue)
            {
                Console.WriteLine("Input too large.");
                return false;
            }
            return true;
        }



        public static WindowResults CheckWindowInput(string inputAsString, List<double[]> currentWindows, double width, double depth, double height)
        {
            double[] windowDimensions = new double[2];
            bool validInput = true;
            double inputAsDouble = new double();

            string[] splitInput = inputAsString.Split(' ');
            if(splitInput.Length != 2) {
                validInput = false;
                Console.WriteLine("Must give 2 dimensions separated by a single space.");
                goto returnResults;
            }


            double minValue = Math.Pow(double.Epsilon, 1.0 / 2.0);
            double maxValue = Math.Pow(double.MaxValue, 1.0 / 2.0);

            for (int i = 0; i < 2; i++)
            {
                if(!double.TryParse(splitInput[i], out inputAsDouble)) {
                    validInput = false;
                    Console.WriteLine("Dimensions must be numeric.");
                    goto returnResults;
                }
                if (inputAsDouble < minValue)
                {
                    validInput = false;
                    Console.WriteLine("Input too small.");
                    goto returnResults;
                }
                else if (inputAsDouble > maxValue)
                {
                    validInput = false;
                    Console.WriteLine("Input too large.");
                    goto returnResults;
                }
                windowDimensions[i] = inputAsDouble;
            }

            // Check window height < room height
            if (windowDimensions[1] > height)
            {
                validInput = false;
                Console.WriteLine("Window height can't be greater than room height.");
                goto returnResults;
            }

            // Check total window width < room perimeter
            double totalWindowWidth = currentWindows.Sum(window => window[0]) + windowDimensions[0];
            double perimeter = 2 * (width + depth);
            if(totalWindowWidth > perimeter)
            {
                validInput = false;
                Console.WriteLine("Total window width can't be greater than room perimeter.");
                goto returnResults;
            }
            
            returnResults:
            return new WindowResults(validInput, windowDimensions);
        }
    }
}
