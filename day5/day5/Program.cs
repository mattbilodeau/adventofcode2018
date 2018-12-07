using System;

namespace day5
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            // load data
            string[] arg = System.IO.File.ReadAllLines("/Users/kingpin/Projects/adventofcode/day5/day5/data.txt");

            string poly = arg[0];

            RS rs = new RS();

            // part1
            poly = rs.ReactString(poly);

            //Console.WriteLine(poly);
            Console.WriteLine(poly.Length.ToString());


            //poly = arg[0];
            //part2
            char rch = 'a';

            char whch = 'a';
            int whlen = poly.Length + 1000;

            for (int i = 0; i < 26; i++) {
                char ichar = (char)i;
                char iplusa = (char)(rch + ichar);
                char iplusA = Char.ToUpper(iplusa);

                string tmpPoly = poly.Replace(iplusa.ToString(), String.Empty);
                tmpPoly = tmpPoly.Replace(iplusA.ToString(), String.Empty);

                int tlen = tmpPoly.Length;

                tmpPoly = rs.ReactString(tmpPoly);


                if (tmpPoly.Length < whlen)
                {
                    whlen = tmpPoly.Length;
                    whch = iplusa;
                }

                Console.WriteLine(iplusa + " " + tlen.ToString() + " " + tmpPoly.Length.ToString());
            }
            Console.WriteLine("-------------------");
            Console.WriteLine(whch);
            Console.WriteLine(whlen);
        }
    }

    class RS
    {
        public string ReactString(string poly1)
        {
            string poly = poly1;
            int changes = 10000;
            while (changes != 0)
            {
                changes = 0;
                for (int i = 0; i < (poly.Length - 1); i++)
                {
                    char ch = poly[i];
                    char nch = poly[(i + 1)];


                    char chT;
                    char nchT;
                    if ((char)'a' <= nch && nch <= (char)'z')
                    {
                        chT = (char)(ch + 32);
                    }
                    else
                    {
                        chT = (char)(ch - 32);
                    }
                    if ((char)'a' <= ch && ch <= (char)'z')
                    {
                        nchT = (char)(nch + 32);
                    }
                    else
                    {
                        nchT = (char)(nch - 32);
                    }

                    if (ch == nchT || nch == chT) {
                        poly = poly.Substring(0, i) + poly.Substring((i + 2), poly.Length - (i + 2));
                        changes++;
                        break;
                    }
                }
            }
            return poly;
        }
    }
}
