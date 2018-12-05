using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day_2_Part_1
{
    class Program
    {
        static void Main(string[] args)
        {
            int i, numberFault;
            char[] charArray;
            char[] charArray2;
            char[] charArrayFound = new char[30];
            string[] fileData;


            fileData = File.ReadLines(@"D:\Prive\Projecten\C#\AdventOfCode2018\Day 2 Part 2\input.txt", Encoding.UTF8).ToArray() ;

            //Read each line
            foreach (string line in fileData)
            {
                //String to array of chars
                charArray = line.ToCharArray();

                //Read each line again (matches don't have to be directly below each other
                foreach (string line2 in fileData)
                {
                    //String to array of chars
                    charArray2 = line2.ToCharArray();

                    //Reset to zero at nex compare
                    numberFault = 0;

                    //For each char in array
                    for (i = 0; i < charArray.Length; i++)
                    {
                        //compare char
                        if (charArray[i] != charArray2[i])
                        {
                            //Not the same
                            charArrayFound[i] = ' ';
                            numberFault += 1;
                        }
                        else
                        {
                            //same --> remember char
                            charArrayFound[i] = charArray[i];
                        }
                    }

                    //check number of faults
                    if (numberFault == 1)
                    {
                        string lineFound = line;
                        goto gotoItemFound;
                    }


                }
            }


            gotoItemFound:

            //print result. Note: it wil allway print result even if no found.
            Console.WriteLine("Result is: ");
            for (i = 0; i < 30; i++)
            {
                if( charArrayFound[i] != ' ')
                {
                    Console.Write("{0}", charArrayFound[i]);
                }
                

            }

            Console.ReadKey();

        }
    }
}
