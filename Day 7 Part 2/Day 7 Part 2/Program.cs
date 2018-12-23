using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day_7_Part_2
{
    class Program
    {
        public static int[] workersTimer = new int[6];
        public static char[] workersChar = new char[6];
        public static int stopwatch = 0;
        public static char[,,,,] dataLevels = new char[30,30,30,30,30];
        public static string anwser;
        public static int nrWorkers;

        static void Main(string[] args)
        {
            Console.WriteLine("Anwser part 1 is: {0}", RunChallangePart2());
            Console.ReadKey();
        }


        static string RunChallangePart2()
        {
            var dict = new Dictionary<int, MySteps>();

            Console.WriteLine("************* ReadData *****************");
            dict = ReadData();


            Console.WriteLine("************* HandleData *****************");
            anwser = HandleData(dict);

            return anwser;
        }


        //****************************************************************************************//
        //************************************* HANDLE DATA **************************************//
        //****************************************************************************************//
        static string HandleData(Dictionary<int, MySteps> dict)
        {
            var returnValue = new MyRetunValue();
            anwser = "";
            nrWorkers = 5;

            CheckNext:;
            ReCheck:;

//            Console.WriteLine("************* HandleData --> HandleDataOneScan *****************");
            returnValue = HandleDataOneScan(dict);

            if ( char.IsLetter(returnValue.Anwser))
            {
                goto ReCheck; //If letter found --> check if more letters can be started in this second.
            }

            //            Console.WriteLine("************* HandleData --> RemoveLine *****************");
            UpdateStopwacht(); //All timers - 1 second

            dict = CheckStopwacht(dict); //Check if worker is done with a letter

            Console.WriteLine("Infochar: {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}", stopwatch, workersChar[0], workersChar[1], workersChar[2], workersChar[3], workersChar[4], workersChar[5], anwser);

            //Console.WriteLine("Infotime: {0}, {1}, {2}, {3}, {4}, {5}, {6}", stopwatch, workersTimer[0], workersTimer[1], workersTimer[2], workersTimer[3], workersTimer[4], workersTimer[5], anwser);

            stopwatch += 1; //Total timer

            //If still letters in dictonary
            if (dict.Count > 0)
            {
                //Console.ReadKey();
                goto CheckNext;
            }

            //If one of more workers are busy
            if (char.IsLetter(workersChar[0]) || char.IsLetter(workersChar[1]) || char.IsLetter(workersChar[2]) || char.IsLetter(workersChar[3]) || char.IsLetter(workersChar[4]) || char.IsLetter(workersChar[5]))
                goto ReCheck;


            Console.WriteLine("letters is: {0}", anwser);
            //Console.WriteLine("stopwatch is: {0}", stopwatch);
            return stopwatch.ToString();
        }



        //****************************************************************************************//
        //************************************* Update stopwatch***********************************//
        //****************************************************************************************//
        static void UpdateStopwacht()
        {
            int i;

            //for all workers
            for (i = 0; i <= nrWorkers; i++)
            {
                workersTimer[i] -= 1;
            }
        }



        //****************************************************************************************//
        //************************************* Check stopwatch***********************************//
        //****************************************************************************************//
        static Dictionary<int, MySteps> CheckStopwacht(Dictionary<int, MySteps> dict)
        {
            int i;

            var items = from pair in dict
                        orderby pair.Value.FirstStep ascending
                        select pair;

            //for all workers
            for (i=0;i<= nrWorkers; i++)
            {
                if(workersTimer[i] <=0 && char.IsLetter( workersChar[i]))
                {
                    anwser += workersChar[i];

                    //Remove line from dictonary
                    foreach (KeyValuePair<int, MySteps> pair in items)
                    {
                        if (pair.Value.FirstStep == workersChar[i])//&& pair.Value.SecondStep == lastAnwser.SecondStep)
                        {
                            //Console.WriteLine(" Remove: {0}: {1} : {2}", pair.Key, pair.Value.FirstStep, pair.Value.SecondStep);
                            
                            if(dict.Count == 1)
                            {
                                Console.WriteLine("Last letter is: {0}", pair.Value.SecondStep);

                                workersChar[i] = ' ';
                                dict.Remove(pair.Key);

                                AssignWorker(pair.Value.SecondStep);
                                goto LastLineDone;
                            }

                            dict.Remove(pair.Key);

                        }

                    }
                    workersChar[i] = ' ';
                }

            }
            LastLineDone:;
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

            fileData = File.ReadLines(@"D:\Prive\Projecten\C#\AdventOfCode2018\Day 7 Part 2\input.txt", Encoding.UTF8).ToArray();


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
                    if (pair.Value.FirstStep == pair2.Value.SecondStep 
                        || pair.Value.FirstStep == workersChar[0] 
                        || pair.Value.FirstStep == workersChar[1] 
                        || pair.Value.FirstStep == workersChar[2] 
                        || pair.Value.FirstStep == workersChar[3] 
                        || pair.Value.FirstStep == workersChar[4]
                    //    || ( char.IsLetter(workersChar[0]) && char.IsLetter(workersChar[1])  ))
                        || (char.IsLetter(workersChar[0]) && char.IsLetter(workersChar[1]) && char.IsLetter(workersChar[2]) && char.IsLetter(workersChar[3]) && char.IsLetter(workersChar[4])))
                    
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
                //Console.WriteLine("     Option found: {0}, {1}", itemsFound[j, 0], itemsFound[j, 1]);
                if (itemsFound[j, 1] <= nextLetter )
                {
                    firstLetter = itemsFound[j, 0];
                    nextLetter = itemsFound[j, 1];
                }

            }

   

            returnValue.Anwser = firstLetter;
            returnValue.FirstStep = firstLetter;
            returnValue.SecondStep = nextLetter;


           // Console.WriteLine("     Return: Firstletter: {0}, Secondletter: {1}, Anwser: {2}", returnValue.FirstStep, returnValue.SecondStep, returnValue.Anwser);

            if ( char.IsLetter(firstLetter))
            {
                AssignWorker(firstLetter);

            }

           // Console.WriteLine("workers is: {0}, {1}, {2}, {3}, {4} ", workersChar[0], workersChar[1], workersChar[2], workersChar[3], workersChar[4]);
           // Console.ReadKey();

            return returnValue;
        }

        //****************************************************************************************//
        //************************************* ASSIGN WORKER ************************************//
        //****************************************************************************************//
        static void AssignWorker(char letter)
        {
            int i;
            int baseOffset = 60;
            int nrOffWorkers = nrWorkers;                 

            for (i = 0; i <= nrOffWorkers; i++)
            {

                if( workersChar[i] == letter)
                {
                    goto AllreadyHandled;
                }

            }


            for (i=0; i<= nrOffWorkers; i++)
            {
                if( workersTimer[i] <= 0)
                {
                    workersChar[i] = letter;
                    workersTimer[i] = baseOffset + Convert.ToInt32(letter) - 64 ; 
                    Console.WriteLine("Letter is: {0}, timer = {1}", letter, workersTimer[i]);
                    goto done;
                }
            }

            AllreadyHandled:;
            done:;
        }


    }

    //****************************************************************************************//
    //******************************* USER DEFINE DATA TYPES *********************************//
    //****************************************************************************************//
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
