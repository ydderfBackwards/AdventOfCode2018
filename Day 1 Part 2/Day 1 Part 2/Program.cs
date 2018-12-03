using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day_1_Part_2
{
    class Program
    {
        static void Main(string[] args)
        {
            int CalculatedValue = 0;
            bool DubbelGevonden = false;
            var list = new List<int>();

            while (DubbelGevonden==false)
            {

                foreach (string line in File.ReadLines(@"D:\Prive\Projecten\C#\AdventOfCode2018\Day 1 Part 1\input.txt", Encoding.UTF8))
                {
                    // process the line
                   // Console.WriteLine("Value is {0}", line);
                    CalculatedValue += Convert.ToInt32(line);
                    
                    if(list.Contains(CalculatedValue) && DubbelGevonden==false)
                    {
                        Console.WriteLine("Dubbel gevonden is {0}", CalculatedValue);
                        DubbelGevonden = true;
                    }

                    list.Add(CalculatedValue);

                }


            }
            Console.ReadKey();


        }
    }
}
