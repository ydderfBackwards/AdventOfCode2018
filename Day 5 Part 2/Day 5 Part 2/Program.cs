using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day_5_Part_2
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] data;
            string[] fileData;
            int x = new int();
            int y;
            string checkLine;
            char[] letters = {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            int numberLeftMax = new int();
            char charRemoved = new char();
            string newCheckLine;

            fileData = File.ReadLines(@"D:\Prive\Projecten\C#\AdventOfCode2018\Day 5 Part 1\Input.txt", Encoding.UTF8).ToArray();
            checkLine = fileData[0];
            numberLeftMax = 99999999;
            
            
            //for all letters in alphabet
            for (y = 0; y < letters.Length; y++)
            {
                Console.WriteLine("Handeling letter {0}", letters[y]);

                //Remove letter (lower and upper case)
                newCheckLine = checkLine.Replace(letters[y].ToString(), string.Empty);
                newCheckLine = newCheckLine.Replace(char.ToUpper(letters[y]).ToString(), string.Empty);
                

                reCheck:;

                //String to char array
                data = newCheckLine.ToCharArray();

                for (x = 0; x < (data.Length - 1); x++)
                {
                    char.ToUpper(data[x]);
                    char.ToUpper(data[x + 1]);

                    //Check if combi exist (eg. aA or Aa)
                    if (char.ToUpper(data[x]).Equals(char.ToUpper(data[x + 1])) && !data[x].Equals(data[x + 1]))
                    {
                        //Remove lettes
                        newCheckLine = newCheckLine.Remove(x, 2);

                        goto reCheck;

                    }
                }

                //Check is this is best result
                if( numberLeftMax > newCheckLine.Length)
                {
                    //Save result
                    numberLeftMax = newCheckLine.Length;
                    charRemoved = letters[y];
                }

            }

            Console.WriteLine("Result number left is {0}, best letter is {1}", numberLeftMax, charRemoved );

        }
    }
}
