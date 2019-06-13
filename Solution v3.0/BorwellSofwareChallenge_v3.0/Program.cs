using System;
using System.Collections.Generic;
using System.Linq;

namespace BorwellSofwareChallenge_v3._0
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Outputs Needed:
            // 1. Area of the floor
            // 2. Amount of paint required to paint the walls
            // 3. Volume of the room

            startProgram:

            string userInputWidth, userInputDepth, userInputHeight, userInputWindow, userResponse, userType, userInputCoord;
            double width, depth, height, perimeter = 0;
            bool rectangular;
            List<double[]> coordList = new List<double[]>();

            Console.WriteLine("**** Borwell Software Challenge v3.0 ****");
            Console.WriteLine("**** Basic Rectangular Room / Custom Shape Room with Windows/Doors ****");
            Console.WriteLine();

            getRoomType:
            Console.WriteLine("Enter 'R' for Rectangular Room, Enter 'C' for Custom Room, Enter 'X' to Exit Program:");
            userType = Console.ReadLine();
            if (userType.ToUpper() == "R")
            {
                rectangular = true;
            }
            else if (userType.ToUpper() == "C")
            {
                rectangular = false;
            }
            else if(userType.ToUpper() == "X")
            {
                return;
            }
            else { goto getRoomType; }

            if(rectangular)
            {
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
            }
            else
            {
                // Get & Check User Input for Room Coordinates
                coordList = new List<double[]>();
                Console.WriteLine("Enter coordinates of corners in room.\r\n" +
                    "(e.g.)\r\n" +
                    "0 0\r\n" +
                    "0 3.5\r\n" +
                    "4.5 6\r\n" +
                    "4.5 0\r\n" +
                    "F\r\n" +
                    "Enter each in metres as X Y then enter 'F' when finished:");
                coordInput:
                userInputCoord = Console.ReadLine();
                if (userInputCoord.ToUpper() != "F")
                {
                    CoordResults cr = CheckCoordInput(userInputCoord);
                    if (cr.ValidInput)
                    {
                        coordList.Add(cr.Coords);
                    }
                    goto coordInput;
                }
                else
                {
                    if(coordList.Count < 3)
                    {
                        Console.WriteLine("Room must have at least 3 corners.");
                        goto coordInput;
                    }
                }

                // Get & Check User Input for Height
                roomHeightCustom:
                Console.WriteLine("Enter Room Height (in metres):");
                userInputHeight = Console.ReadLine();
                if (!CheckInput(userInputHeight)) { goto roomHeightCustom; }
                else { double.TryParse(userInputHeight, out height); }

                // Calculate wall lengths
                double[] wallLengths = new double[coordList.Count];
                for (int i = 0; i < wallLengths.Length; i++)
                {
                    wallLengths[i] = Math.Sqrt(Math.Pow(coordList[(i + 1) % wallLengths.Length][0] - coordList[i][0], 2) + Math.Pow(coordList[(i + 1) % wallLengths.Length][1] - coordList[i][1], 2));
                }
                perimeter = wallLengths.Sum();
                width = perimeter / 4;
                depth = width;
            }

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
            for (int i = 0; i < windowList.Count; i++)
            {
                windowInfo += $"{i + 1}. {windowList[i][0]} m x {windowList[i][1]} m\r\n";
            }
            if (windowInfo.Length == 0) { windowInfo = "None."; }

            if(rectangular)
            {
                // Get Calculation Results
                RoomResults calcResults = CalculateResults(width, depth, height, windowList);

                Console.WriteLine($"\r\n\r\n*** Results *** \r\n" +
                $"Based on these dimensions: \r\n" +
                $"Width: {width} m, Depth: {depth} m, Height: {height} m \r\n\r\n" +
                $"Windows:\r\n" + windowInfo + "\r\n" +
                $"1. Floor Area: {calcResults.FloorArea} m^2 \r\n" +
                $"2. Volume of Paint Needed to Cover Walls: {calcResults.PaintVolume} litres \r\n" +
                $"3. Volume of Room: {calcResults.RoomVolume} m^3 \r\n\r\n");
            }
            else
            {
                // Get Calculation Results
                RoomResults calcCustomResults = CalculateCustomResults(coordList, perimeter, height, windowList);

                string coordInfo = "";
                for (int i = 0; i < coordList.Count; i++)
                {
                    coordInfo += $"{i + 1}. ({coordList[i][0]}, {coordList[i][1]})\r\n";
                }

                Console.WriteLine($"\r\n\r\n*** Results *** \r\n" +
                $"Based on these coordinates: \r\n" +
                coordInfo +
                $"Windows:\r\n" + windowInfo + "\r\n" +
                $"1. Floor Area: {calcCustomResults.FloorArea} m^2 \r\n" +
                $"2. Volume of Paint Needed to Cover Walls: {calcCustomResults.PaintVolume} litres \r\n" +
                $"3. Volume of Room: {calcCustomResults.RoomVolume} m^3 \r\n\r\n");
            }

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

        public class CoordResults
        {
            public bool ValidInput;
            public double[] Coords;

            public CoordResults(bool validInput, double[] coords)
            {
                ValidInput = validInput;
                Coords = coords;
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
            for (int i = 0; i < windows.Count; i++)
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

        public static RoomResults CalculateCustomResults(List<double[]> coords, double perimeter, double height, List<double[]> windows)
        {
            // Calculate Floor Area
            // Area from Shoelace Algorithm
            double floorArea = 0;
            int lastPoint = coords.Count - 1;
            for(int i = 0; i < coords.Count; i++)
            {
                floorArea += ((coords[lastPoint][0] + coords[i][0]) * (coords[lastPoint][1] - coords[i][1])) / 2;
                lastPoint = i;
            }
            floorArea = Math.Abs(floorArea);

            // Calculate Volume of Paint Needed
            // Note: Based on 1 litre per 10 square metres
            double wallArea = height * perimeter;
            double windowArea = 0;
            for (int i = 0; i < windows.Count; i++)
            {
                windowArea += (windows[i][0] * windows[i][1]);
            }
            double paintVolume = (wallArea - windowArea) * 0.1;

            // Calculate Room Volume
            // Volume = Width x Depth x Height
            double roomVolume = floorArea * height;

            RoomResults results = new RoomResults(floorArea, paintVolume, roomVolume);

            return results;
        }

        public static bool CheckInput(string inputAsString)
        {
            bool isNumeric = double.TryParse(inputAsString, out double inputAsDouble);
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
            if (splitInput.Length != 2)
            {
                validInput = false;
                Console.WriteLine("Must give 2 dimensions separated by a single space.");
                goto returnResults;
            }


            double minValue = Math.Pow(double.Epsilon, 1.0 / 2.0);
            double maxValue = Math.Pow(double.MaxValue, 1.0 / 2.0);

            for (int i = 0; i < 2; i++)
            {
                if (!double.TryParse(splitInput[i], out inputAsDouble))
                {
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
            if (totalWindowWidth > perimeter)
            {
                validInput = false;
                Console.WriteLine("Total window width can't be greater than room perimeter.");
                goto returnResults;
            }

            returnResults:
            return new WindowResults(validInput, windowDimensions);
        }

        public static CoordResults CheckCoordInput(string inputAsString)
        {
            double[] coords = new double[2];
            bool validInput = true;

            string[] splitInput = inputAsString.Split(' ');
            if (splitInput.Length != 2)
            {
                validInput = false;
                Console.WriteLine("Must give 2 values separated by a single space.");
                goto returnResults;
            }

            double maxValue = Math.Pow(double.MaxValue, 1.0 / 2.0) / 2;
			double minValue = -maxValue;

            for (int i = 0; i < 2; i++)
            {
                if (!double.TryParse(splitInput[i], out double value))
                {
                    validInput = false;
                    Console.WriteLine("Values must be numeric.");
                    goto returnResults;
                }
                if (value < minValue)
                {
                    validInput = false;
                    Console.WriteLine("Input too small.");
                    goto returnResults;
                }
                else if (value > maxValue)
                {
                    validInput = false;
                    Console.WriteLine("Input too large.");
                    goto returnResults;
                }
                coords[i] = value;
            }

            returnResults:
            return new CoordResults(validInput, coords);
        }
    }
}
