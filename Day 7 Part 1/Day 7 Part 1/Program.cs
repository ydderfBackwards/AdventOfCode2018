using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day_7_Part_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Anwser part 1 is: {0}", RunChallangePart1());
            Console.ReadKey();
        }


        static string RunChallangePart1()
        {
            var dict = new Dictionary<int, MySteps>();

            Console.WriteLine("************* ReadData *****************");
            dict = ReadData();


            Console.WriteLine("************* HandleData *****************");
            string anwser = HandleData(dict);

            return  anwser;
        }


        //****************************************************************************************//
        //************************************* HANDLE DATA **************************************//
        //****************************************************************************************//
        static string HandleData(Dictionary<int, MySteps> dict)
        {
            var returnValue = new MyRetunValue();
            string anwser = "";

            CheckNext:;

            Console.WriteLine("************* HandleData --> HandleDataOneScan *****************");
            returnValue = HandleDataOneScan(dict);
            anwser += returnValue.Anwser.ToString();

            Console.WriteLine("************* HandleData --> RemoveLine *****************");
            dict = RemoveLine(dict, returnValue);



            if (dict.Count > 0)
            {
                //Console.ReadKey();
                goto CheckNext;
            }
            else
            {
                anwser += returnValue.SecondStep.ToString();
            }


            return anwser;
        }


        //****************************************************************************************//
        //************************************* REMOVE LINE **************************************//
        //****************************************************************************************//
        static Dictionary<int, MySteps> RemoveLine(Dictionary<int, MySteps> dict, MyRetunValue lastAnwser)
        {

            var items = from pair in dict
                        orderby pair.Value.FirstStep ascending
                        select pair;

            //Remove line from dictonary
            foreach (KeyValuePair<int, MySteps> pair in items)
            {
                if (pair.Value.FirstStep == lastAnwser.FirstStep )//&& pair.Value.SecondStep == lastAnwser.SecondStep)
                {
                    Console.WriteLine(" Remove: {0}: {1} : {2}", pair.Key, pair.Value.FirstStep, pair.Value.SecondStep);

                    dict.Remove(pair.Key);
                }

            }

            return dict;
        }



        //****************************************************************************************//
        //************************************* READ DATA ****************************************//
        //****************************************************************************************//
        static Dictionary<int, MySteps> ReadData()
        {

            var dict = new Dictionary<int, MySteps>();
            int i = 0;
            string[] fileData;
            string[] delimiterStrings = { "Step ", " must be finished before step ", " can begin." };
            string[] lineParts;

            fileData = File.ReadLines(@"D:\Prive\Projecten\C#\AdventOfCode2018\Day 7 Part 1\input.txt", Encoding.UTF8).ToArray();
             

            //Read each line --> write to array
            foreach (string line in fileData)
            {
                //Split line
                lineParts = line.Split(delimiterStrings, System.StringSplitOptions.RemoveEmptyEntries);
                Console.WriteLine("Data is: {0} {1}", lineParts[0], lineParts[1]);
                dict.Add(i, new MySteps { FirstStep = Convert.ToChar(lineParts[0]), SecondStep = Convert.ToChar(lineParts[1]) });
                i += 1;
            }

            Console.WriteLine("dict is {0}", dict.Count);

            return dict;
        }



        //****************************************************************************************//
        //************************** HANDLE DATA ONE SCAN ****************************************//
        //****************************************************************************************//
        static MyRetunValue HandleDataOneScan(Dictionary<int, MySteps> dict)
        {
            var returnValue = new MyRetunValue();

            bool itemFound;
            int i, j;
            char[,] itemsFound = new char[100, 2];
            char nextLetter = new char();
            char firstLetter = new char();


            //Order by firststeps
            var items = from pair in dict
                        orderby pair.Value.FirstStep ascending
                        select pair;

            i = 0;

            //loop to dictonary
            foreach (KeyValuePair<int, MySteps> pair in items)
            {
                itemFound = false;

                //loop to diconary
                foreach (KeyValuePair<int, MySteps> pair2 in items)
                {
                    //check if value exist in second colum
                    if (pair.Value.FirstStep == pair2.Value.SecondStep)
                    {
                        itemFound = true;
                    }
                }

                //If not found in second colum
                if (itemFound == false)
                {
                    itemsFound[i, 0] = pair.Value.FirstStep;
                    itemsFound[i, 1] = pair.Value.SecondStep;
                    i += 1;
                }
            }

            nextLetter = 'Z';

            //Determine first and next letter 
            for (j = 0; j < 1; j++)
            {
                Console.WriteLine("     Option found: {0}, {1}", itemsFound[j,0], itemsFound[j,1]);
                if (itemsFound[j, 1] <=   nextLetter)
                {
                    firstLetter = itemsFound[j, 0];
                    nextLetter = itemsFound[j, 1];
                }

            }

            returnValue.Anwser = firstLetter;
            returnValue.FirstStep = firstLetter;
            returnValue.SecondStep = nextLetter;

            Console.WriteLine("     Return: Firstletter: {0}, Secondletter: {1}, Anwser: {2}", returnValue.FirstStep, returnValue.SecondStep, returnValue.Anwser);


            return returnValue;
        }


    }

    class MySteps
    {
        public char FirstStep { get; set; }
        public char SecondStep { get; set; }
    }

    class MyRetunValue
    {
        public char Anwser { get; set; }
        public char FirstStep { get; set; }
        public char SecondStep { get; set; }

    }

}
