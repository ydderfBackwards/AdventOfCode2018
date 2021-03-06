using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day_4_Part_1
{

    class Program
    {
        static void Main(string[] args)
        {

            char[] delimiterChars = { ' ', '[', ']', '-', ':' };
            string[] lineParts, stringParts;
            ulong year, month, day, hour, minute;
            int[,] a = new int[1000, 1000];
            ulong dateTime;
            string[] fileData;
            var dictionary = new Dictionary<ulong, string>(1100);
            string infoText;
            int guardNr = new int();
            int[,,] guardData = new int[4000 ,60 ,1 ]; //Guardnr; minutes; count
            int minuteSleep, minuteWake;
            //bool guardSleep;
            int x, y;
            int minutesASleep = new int();
            int minutesASleepMax = new int();
            int minutesASleepMaxGuardNr = new int();
            int minuteASleepMost = new int(); 
            int minuteASleepMostCount;


            fileData = File.ReadLines(@"D:\Prive\Projecten\C#\AdventOfCode2018\Day 4 Part 1\input.txt", Encoding.UTF8).ToArray();


            //Read each line --> write to array
            foreach (string line in fileData)
            {
                //Split line
                lineParts = line.Split(delimiterChars);


                Console.WriteLine("Data is: {0} {1} {2} {3} {4} {5} {6} {7} {8} ", lineParts[0], lineParts[1], lineParts[2], lineParts[3], lineParts[4], lineParts[5], lineParts[6], lineParts[7], lineParts[8]);

                //Console.WriteLine("Data is: {0} ", lineParts[1]);

                
                year = Convert.ToUInt64(lineParts[1]);
                month = Convert.ToUInt64(lineParts[2]);
                day = Convert.ToUInt64(lineParts[3]);
                hour = Convert.ToUInt64(lineParts[4]);
                minute = Convert.ToUInt64(lineParts[5]);

                dateTime = minute + hour * 100 + (day * 10000) + (month * 1000000) + (year * 100000000);
                Console.WriteLine("DateTime is {0}", dateTime);

                infoText = lineParts[7] + lineParts[8]; 

                dictionary.Add(dateTime, infoText);


            }


            //Source: https://www.dotnetperls.com/sort-dictionary
            //Sort dictionary by date ascending
            var items = from pair in dictionary
                        orderby pair.Key ascending
                        select pair;

            minuteSleep = 0;
            minuteWake = 0;


            // Handle results.
            foreach (KeyValuePair<ulong, string> pair in items)
            {
                Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
                //guardSleep = false;


                if (pair.Value.StartsWith("Guard") == true)
                {   
                    //Split string
                    stringParts = pair.Value.Split('#');
                    
                    guardNr = Convert.ToInt16(stringParts[1]);

                    minuteSleep = 0;
                    minuteWake = 0;



                    Console.WriteLine("Guard");
                    Console.WriteLine("Data is: {0} {1}", stringParts[0], stringParts[1]);
                    Console.WriteLine("Guardnr = {0}", guardNr);

                }

                if (String.Compare(pair.Value, "fallsasleep") == 0)
                {
                    
                    minuteSleep = Convert.ToInt32(pair.Key % 100);
                    Console.WriteLine("Fall a sleep {0}", minuteSleep);

                }

                if (String.Compare(pair.Value, "wakesup") == 0 )
                {
                    
                    minuteWake = Convert.ToInt32(pair.Key % 100);
                    Console.WriteLine("Wake up {0}",minuteWake);

                }

                if( minuteSleep < minuteWake)
                {
                    Console.WriteLine("saving data");
                    //Save data
                    for( x = minuteSleep; x < minuteWake; x++)
                    {
                        guardData[guardNr, x, 0] = guardData[guardNr, x, 0] + 1;

                    }
                }
            }

            //Count number of minutes sleep for each guard
            for(x = 0; x < 4000; x++)
            {
                //Start with 0 minutes
                minutesASleep = 0;

                //For each minute
                for(y = 0; y <60; y++)
                {
                    //Add
                    minutesASleep += guardData[x, y, 0];
                }

                //Check max
                if( minutesASleep > minutesASleepMax)
                {
                    //save guardnr with max sleeping minutes
                    minutesASleepMax = minutesASleep;
                    minutesASleepMaxGuardNr = x;
                }

            }

            Console.WriteLine("Max minutes = {0} for guardnr: {1}", minutesASleepMax, minutesASleepMaxGuardNr);


            minuteASleepMostCount = 0;
            //Check minute most likely to be a sleep
            //For each minute
            for (y = 0; y < 60; y++)
            {
                
                if ( guardData[minutesASleepMaxGuardNr, y, 0] > minuteASleepMostCount)
                {
                    minuteASleepMostCount = guardData[minutesASleepMaxGuardNr, y, 0];
                    minuteASleepMost = y;
                }
            }

            Console.WriteLine("Minute most a sleep is {0}", minuteASleepMost);

            Console.WriteLine("Anwser part 1 is {0} * {1} = {2}", minutesASleepMaxGuardNr, minuteASleepMost, (minutesASleepMaxGuardNr * minuteASleepMost));

            Console.ReadKey();

        }
    }


}
