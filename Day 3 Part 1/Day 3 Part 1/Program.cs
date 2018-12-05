using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day_3_Part_1
{
    class Program
    {
        static void Main(string[] args)
        {
       
            char[] delimiterChars = { ' ', ',', ':', 'x', '#', '@' };
            string[] lineParts;
            int number, offsetHorizontal, offsetVertical, sizeHorizontal, sizeVertical;
            int[,] a = new int[1000, 1000];
            int x, y, numberDubble;


            //Read each line
            foreach (string line in File.ReadLines(@"D:\Prive\Projecten\C#\AdventOfCode2018\Day 3 Part 1\input.txt", Encoding.UTF8))
            {
                //Split line
                lineParts = line.Split(delimiterChars);

                //Save values
                number = Convert.ToInt32(lineParts[1]);
                offsetHorizontal = Convert.ToInt32(lineParts[4]);
                offsetVertical = Convert.ToInt32(lineParts[5]);
                sizeHorizontal = Convert.ToInt32(lineParts[7]);
                sizeVertical = Convert.ToInt32(lineParts[8]);
                
                //Console.WriteLine("Data is: {0} {1} {2} {3} {4}", number, offsetHorizontal, offsetVertical, sizeHorizontal, sizeVertical);

                //Write data to array
                for( x=offsetHorizontal; x < (offsetHorizontal+sizeHorizontal); x++)
                {
                    for (y = offsetVertical; y < (offsetVertical + sizeVertical); y++)
                    {
                        a[x, y] += 1;
                        //Console.WriteLine("a[{0},{1}] = {2}", x, y, a[x, y]);
                    }
                }


            }

            //Init value
            numberDubble = 0;

            //Check array for values > 1
            for (x = 0; x < 1000; x++)
            {

                for (y = 0; y < 1000; y++)
                {
                    if( a[x,y] > 1)
                    {
                        //Count
                        numberDubble += 1;
                    }
                 }
            }

            Console.WriteLine("Result is: {0}", numberDubble);


            Console.ReadKey();

        }
    }
}
