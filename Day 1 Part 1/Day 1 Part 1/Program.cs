using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day_1_Part_1
{
    class Program
    {
         static void Main(string[] args)
        {
            int CalculatedValue = 0;

            foreach (string line in File.ReadLines(@"D:\Prive\Projecten\C#\AdventOfCode2018\Day 1 Part 1\input.txt", Encoding.UTF8))
            {
                // process the line
                Console.WriteLine("Value is {0}", line);
                CalculatedValue += Convert.ToInt32(line);

            }

            Console.WriteLine("Result is {0}", CalculatedValue);

            Console.ReadKey();


        }
    }
}
