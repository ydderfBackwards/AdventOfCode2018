using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day_3_Part_2
{
    class Program
    {
        static void Main(string[] args)
        {
            
            char[] delimiterChars = { ' ', ',', ':', 'x', '#', '@' };
            string[] lineParts;
            int claimNumber, offsetHorizontal, offsetVertical, sizeHorizontal, sizeVertical;
            int[,] a = new int[1000, 1000];
            int x, y;
            string[] fileData;
            bool allGood;

            fileData = File.ReadLines(@"D:\Prive\Projecten\C#\AdventOfCode2018\Day 3 Part 2\input.txt", Encoding.UTF8).ToArray();

            claimNumber = 99999;

            //Read each line --> write to array
            foreach (string line in fileData)
            {
                //Split line
                lineParts = line.Split(delimiterChars);


                claimNumber = Convert.ToInt32(lineParts[1]);
                offsetHorizontal = Convert.ToInt32(lineParts[4]);
                offsetVertical = Convert.ToInt32(lineParts[5]);
                sizeHorizontal = Convert.ToInt32(lineParts[7]);
                sizeVertical = Convert.ToInt32(lineParts[8]);

                //Console.WriteLine("Data is: {0} {1} {2} {3} {4}", number, offsetHorizontal, offsetVertical, sizeHorizontal, sizeVertical);

                for (x = offsetHorizontal; x < (offsetHorizontal + sizeHorizontal); x++)
                {
                    for (y = offsetVertical; y < (offsetVertical + sizeVertical); y++)
                    {
                        a[x, y] += claimNumber;
                        //Console.WriteLine("a[{0},{1}] = {2}", x, y, a[x, y]);
                    }
                }


            }


            //Read each line --> check if still is in array
            foreach (string line in fileData)
            {
                //Split line
                lineParts = line.Split(delimiterChars);


                claimNumber = Convert.ToInt32(lineParts[1]);
                offsetHorizontal = Convert.ToInt32(lineParts[4]);
                offsetVertical = Convert.ToInt32(lineParts[5]);
                sizeHorizontal = Convert.ToInt32(lineParts[7]);
                sizeVertical = Convert.ToInt32(lineParts[8]);

                //Console.WriteLine("Data is: {0} {1} {2} {3} {4}", number, offsetHorizontal, offsetVertical, sizeHorizontal, sizeVertical);

                allGood = true;

                for (x = offsetHorizontal; x < (offsetHorizontal + sizeHorizontal); x++)
                {
                    for (y = offsetVertical; y < (offsetVertical + sizeVertical); y++)
                    {
                        if( a[x, y] != claimNumber)
                        {
                            allGood = false;
                        }
                        //Console.WriteLine("a[{0},{1}] = {2}", x, y, a[x, y]);
                    }
                }

                if( allGood == true)
                {
                    goto LabelFound;
                }
            }


            LabelFound:


            Console.WriteLine("Result is: {0}", claimNumber);


            Console.ReadKey();

        }
    }
}
