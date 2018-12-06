using System;
using System.Collections.Generic;
using System.Linq;


namespace day2
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            string[] arg = System.IO.File.ReadAllLines("/Users/kingpin/Projects/adventofcode/day2/day2/data.txt");

                // part1
            bool twoOfKind = false;
            bool threeOfKind = false;

            int twoMatches = 0;
            int threeMatches = 0;


            foreach (string s in arg) {
                string sorted = String.Concat(s.OrderBy(c => c));

                twoOfKind = false;
                threeOfKind = false;

                for (int i = 0; i < sorted.Length; i++) {
                    int lio = sorted.LastIndexOf(sorted[i]);
                    if (lio - i > 1) {
                        threeOfKind = true;
                    }
                    if (lio - i == 1)
                    {
                        twoOfKind = true;
                    }
                }

                if (threeOfKind == true){
                    threeMatches++;
                    Console.WriteLine(sorted + "3");
                }
                if (twoOfKind == true) {
                    twoMatches++;
                    Console.WriteLine(sorted + "2");
                }
            }


                // part two
            Console.WriteLine(threeMatches * twoMatches);

            for (int x = 0; x < arg.Length; x++) {
                string s1 = arg[x];

                for (int z = x+1; z< arg.Length; z++) {
                    string s2 = arg[z];
                    int diff = 0;
                    for (int c = 0; c < s1.Length; c++) {
                        if (s1[c] != s2[c]) {
                            diff++;
                        }
                        if (diff > 1) {
                            break;
                        }
                    }
                    if (diff == 1) {
                        Console.WriteLine(s1); 
                        Console.WriteLine(s2);
                    }
                }
            }
        }
    }
}
