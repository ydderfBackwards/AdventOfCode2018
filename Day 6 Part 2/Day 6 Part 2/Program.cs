using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day_6_Part_2
{
    class Program
    {
        static void Main(string[] args)
        {
            int i, x, y;
            int[,] coordinates = new int[300, 2];
            char[] delimiterChars = { ',' };
            string[] lineParts;
            string[] fileData;
            int FieldSize = 400; //400 for real, 10 for test;
            int maxDistance = 10000; //10000 for real, 32 for test
            int[,] playingField = new int[FieldSize, FieldSize];


            fileData = File.ReadLines(@"D:\Prive\Projecten\C#\AdventOfCode2018\Day 6 Part 2\input.txt", Encoding.UTF8).ToArray();

            i = 1;

            //Read each line --> write to array
            foreach (string line in fileData)
            {
                //Split line
                lineParts = line.Split(delimiterChars);


                Console.WriteLine("Data (x y) is: {0} {1} ", lineParts[0], lineParts[1]);

                coordinates[i, 0] = Convert.ToInt16(lineParts[0]);
                coordinates[i, 1] = Convert.ToInt16(lineParts[1]);

                i += 1;

            }


            int nrOfCoordinates = i;

            //Fill array
            for (x = 0; x < FieldSize; x++)
            {
                for (y = 0; y < FieldSize; y++)
                {

                    playingField[x, y] = DetermineWithinLimits(x, y, coordinates, nrOfCoordinates, maxDistance);
                }
            }
            

            //Print for debug. 
            /*
            for (y = 0; y < FieldSize; y++)
            {
                Console.WriteLine("");
                for (x = 0; x < FieldSize; x++)
                {

                    Console.Write("{0},", playingField[x, y]);

                }
            }
            */
            Console.WriteLine("Largest is {0}", CheckArea(playingField, FieldSize));

            Console.ReadKey();
        }

        public static int CheckArea(int[,] playingField, int fieldSize)
        {
            int x, y;
            int area = new int();
            
            area = 0;
            for (x = 0; x < fieldSize; x++)
            {
                for (y = 0; y < fieldSize; y++)
                {
                    if (playingField[x, y] == 1)
                    {
                        area += 1;

                    }
                }
            }

            return area;
        }


   




        public static int DetermineWithinLimits(int sourceX, int sourceY, int[,] coordinates, int nrOfCoordinates, int maxDistance)
        {
            int i;
            int totalDistance, currentDistance;

            totalDistance = 0;

            for (i = 1; i < nrOfCoordinates; i++)
            {
              
                currentDistance = CalculateDistance(sourceX, sourceY, coordinates[i, 0], coordinates[i, 1]);
                totalDistance += currentDistance;

            }
            //Console.WriteLine("TotalDistance is {0}", totalDistance);

            if( totalDistance < maxDistance)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public static int CalculateDistance(int sourceX, int sourceY, int destinationX, int destinationY)
        {
            int distance;

            distance = Math.Abs((sourceX - destinationX)) + Math.Abs((sourceY - destinationY));

            //Console.WriteLine("source = {0},{1}, destination = {2},{3}, distance = {4} ", sourceX, sourceY, destinationX, destinationY, distance);

            return distance;

        }

    }
}
