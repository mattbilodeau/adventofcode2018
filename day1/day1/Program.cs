using System;
using System.Collections.Generic;
using System.IO;


namespace day1
{
    class MainClass
    {

        public static void Main(string[] args)
        {
                // load data
            string[] arg = System.IO.File.ReadAllLines("/Users/kingpin/Projects/adventofcode/day1/day1/data.txt");

                // build list of ints
            List<int> intA = new List<int>();


            foreach (string a in arg) {
                intA.Add(Int32.Parse(a));
            }

                // part one
            int total = 0;
            foreach (int a in intA) {
                total += a;
            }
            Console.WriteLine(total.ToString());

                // part 2
            HashSet<int> frequencies = new HashSet<int>();
            int answer = 0;
            int index = 0;

           while (true) {
                if (!frequencies.Add(answer)) {
                    Console.WriteLine(answer);
                    break;
                }
                if (index >= intA.Count) {
                    index = 0;
                }
                answer += intA[index];
                index++;

            }

        }
    }
}
