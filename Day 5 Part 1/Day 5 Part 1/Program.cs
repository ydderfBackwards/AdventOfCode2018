using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day_5_Part_1
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] data;
            string[] fileData;
            int x = new int();
            string checkLine;// = new string[];
            //string line;

            fileData = File.ReadLines(@"D:\Prive\Projecten\C#\AdventOfCode2018\Day 5 Part 1\Input.txt", Encoding.UTF8).ToArray();
            checkLine = fileData[0];

            reCheck:;
                
                //String to char array
                data = checkLine.ToCharArray();
       
                for (x = 0; x < (data.Length-1); x++)
                {
                    char.ToUpper(data[x]);
                    char.ToUpper(data[x + 1]);

                    if( char.ToUpper(data[x]).Equals(char.ToUpper(data[x+1])) && !data[x].Equals(data[x + 1]))
                    {
                        Console.WriteLine("Remove {0} en {1}", data[x], data[x + 1]);
                        
                        checkLine = checkLine.Remove(x, 2);
                        
                        goto reCheck;

                    }
                }

            //Console.WriteLine("Result string is {0}", checkLine);
            Console.WriteLine("Result number of char is {0}", checkLine.Length);

        }
    }
}
