using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day_6_Part_1
{
    class Program
    {
        static void Main(string[] args)
        {
            int distance = new int();
            int i, x, y;
            int[,] coordinates = new int[300, 2];
            char[] delimiterChars = { ',' };
            string[] lineParts;
            string[] fileData;
            int FieldSize = 400;
            int[,] playingField = new int[FieldSize, FieldSize];


            fileData = File.ReadLines(@"D:\Prive\Projecten\C#\AdventOfCode2018\Day 6 Part 1\input.txt", Encoding.UTF8).ToArray();

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

                    playingField[x, y] = DetermineClosest(x, y, coordinates, nrOfCoordinates);
                }
            }

            //Remove numbers who have infinitive size
            playingField = RemoveInfinitive(playingField,FieldSize);

            

            //Print for debug.
            for (y = 0; y < FieldSize; y++)
            {
                Console.WriteLine("");
                for (x = 0; x < FieldSize; x++)
                {

                    Console.Write("{0},", playingField[x, y]);

                }
            }


            Console.WriteLine("Distance is {0}", distance);
            Console.WriteLine("Largest is {0}", CheckLargest(playingField,FieldSize, nrOfCoordinates));

            Console.ReadKey();
        }

        public static int CheckLargest(int[,] playingField, int fieldSize, int nrOfCoordinates)
        {
            int i, x, y;
            int Largest = new int();
            int currentSize = new int();

            Largest = 0;

            for (i = 1; i < nrOfCoordinates; i++)
            {
                currentSize = 0;
                for (x = 0; x < fieldSize; x++)
                {
                    for (y = 0; y < fieldSize; y++)
                    {
                        if( playingField[x,y] == i)
                        {
                            currentSize += 1;

                        }
                    }
                }

                if (currentSize > Largest)
                {
                    Largest = currentSize;
                }
            }

            return Largest;
        }


        public static int[,] RemoveInfinitive(int[,] playingField, int fieldSize)
        {
            int x, y;
            int i, j;
            int removeNumber = new int();

            for(x=0; x<fieldSize;x++)
            {
                for (y = 0; y < fieldSize; y++)
                {
                    if (x == 0 || y == 0 || x == (fieldSize - 1) || y == (fieldSize - 1))
                    {
                        removeNumber = playingField[x, y];


                        for (i = 0; i < fieldSize; i++)
                        {
                            for (j = 0; j < fieldSize; j++)
                            {
                                if( playingField[i,j] == removeNumber)
                                {
                                    playingField[i, j] = 0;
                                }
                            }
                        }
                    }
                }
            }


            return playingField;
        }





        public static int DetermineClosest(int sourceX, int sourceY, int[,] coordinates, int nrOfCoordinates)
        {
            int i;
            int closestNr = new int();
            int closestDistance, currentDistance;

            closestDistance = 99999;

            for(i = 1; i < nrOfCoordinates; i++)
            {
                //Console.WriteLine("coordinate is {0};{1}", coordinates[i, 0], coordinates[i, 1]);

                currentDistance = CalculateDistance(sourceX, sourceY, coordinates[i, 0], coordinates[i, 1]);
                
                if( currentDistance <= closestDistance)
                {
                    if (currentDistance == closestDistance)
                    {
                        closestNr = 0;
                    }
                    else
                    {
                        closestNr = i;
                    }
                    closestDistance = currentDistance;             
                }
                
                /*
                if(sourceX == coordinates[i,0] && sourceY == coordinates[i,1])
                {
                    closestNr = i;
                }
                else
                {
                    //closestNr = 0;
                }
                */
            }
            Console.WriteLine("Closest is {0}", closestNr);
            return closestNr;
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
