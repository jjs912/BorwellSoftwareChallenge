using System;

namespace BorwellSoftwareChallenge_v1._0
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

            string userInputWidth, userInputDepth, userInputHeight, userResponse;
            double width, depth, height;

            Console.WriteLine("**** Borwell Software Challenge v1.0 ****");
            Console.WriteLine("**** Basic Rectangular Room ****");
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
            if(!CheckInput(userInputDepth)) { goto roomDepth; }
            else { double.TryParse(userInputDepth, out depth); }

            // Get & Check User Input for Height
            roomHeight:
            Console.WriteLine("Enter Room Height (in metres):");
            userInputHeight = Console.ReadLine();
            if (!CheckInput(userInputHeight)) { goto roomHeight; }
            else { double.TryParse(userInputHeight, out height); }

            // Get Calculation Results
            RoomResults calcResults = CalculateResults(width, depth, height);

            Console.WriteLine($"*** Results *** \r\n" +
                $"Based on these dimensions: \r\n" +
                $"Width: {width} m, Depth: {depth} m, Height: {height} m \r\n\r\n" +
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

        public static RoomResults CalculateResults(double width, double depth, double height)
        {
            // Calculate Floor Area
            // Area = Width x Depth
            double floorArea = width * depth;

            // Calculate Volume of Paint Needed
            // Note: Based on 1 litre per 10 square metres
            double wallArea = 2 * height * (width + depth);
            double paintVolume = wallArea * 0.1;

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
            if(!isNumeric)
            {
                Console.WriteLine("Input must be numeric.");
                return false;
            }
            double minValue = Math.Pow(double.Epsilon, 1.0 / 3.0);
            double maxValue = Math.Pow(double.MaxValue, 1.0 / 3.0);
            if(inputAsDouble < minValue)
            {
                Console.WriteLine("Input too small.");
                return false;
            }
            else if(inputAsDouble > maxValue)
            {
                Console.WriteLine("Input too large.");
                return false;
            }
            return true;
        }
    }
}
