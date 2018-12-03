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

            bool TwiceFound, TripleFound;
            int Nr = 0, Nr2 = 0, Nr3 = 0, i;
            var list = new List<char>();
            var array_count = new int[40];
            

            //Read each line
            foreach (string line in File.ReadLines(@"D:\Prive\Projecten\C#\AdventOfCode2018\Day 2 Part 1\input.txt", Encoding.UTF8))
            {
                    // process the line
                    foreach ( char letter in line)
                    {
                        //If letter is in list --> keep count how often
                        if(list.Contains(letter))
                        {
                            //Convert char to int (= position in array)
                            Nr = char.ToUpper(letter) - 64;

                            //save in array;
                            array_count[Nr] += 1;

                        }
                        //Save found letter to array
                        list.Add(letter);
                    }

                    //Reset value
                    TwiceFound = false;
                    TripleFound = false;

                    //Check letters found twice and triple
                    for ( i = 0; i < 30; i++)
                    {
                        //Check if twice found
                        if (array_count[i] == 1)
                        {
                            if( TwiceFound == false)
                            {
                                Nr2 += 1;
                                TwiceFound = true;
                            }
                    }
                        //Check if triple found
                        if (array_count[i] == 2)
                        {
                            if( TripleFound==false)
                            {
                                Nr3 += 1;
                                TripleFound = true;
                            }
                        }
                        
                    }

                Array.Clear(array_count, 0, 30);
                list.Clear();
            }

            Console.WriteLine("Letter found twice {0}", Nr2);
            Console.WriteLine("Letter found triple {0}", Nr3);

            //Calculate total
            int total = Nr2 * Nr3;

            Console.WriteLine("Result is {0}", total);

            Console.ReadKey();

        }
    }
}
