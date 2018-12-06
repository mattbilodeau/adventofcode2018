using System;
using System.Collections.Generic;
using System.Drawing;

namespace day3 {
    class MainClass {
        public static void Main(string[] args) {
            string[] arg = System.IO.File.ReadAllLines("/Users/kingpin/Projects/adventofcode/day3/day3/data.txt");

            List<Order> orders = new List<Order>();
            int[,] fabric = new int[1000, 1000];

            foreach (string a in arg) {
                Order addme = new Order(a);

                for (int h = addme.rec.Top; h < (addme.rec.Top + addme.rec.Height); h++) {
                    for (int w = addme.rec.Left; w < (addme.rec.Left + addme.rec.Width); w++) {
                        fabric[h, w]++;
                    }
                }

                orders.Add(addme);
            }

                // part 1
            string row = "";
            int area = 0;
            for (int i = 0; i < 1000; i++) {
                row = "";
                for (int j = 0; j < 1000; j++) {
                    row += fabric[i, j];
                    if (fabric[i,j] > 1) {
                        area++;
                    }
                }
                //Console.WriteLine(row);

            }
            Console.WriteLine(area);


                //part 2

            bool part2 = true;
            foreach (Order addme in orders) {
                part2 = true;
                for (int h = addme.rec.Top; h < (addme.rec.Top + addme.rec.Height); h++) {
                    for (int w = addme.rec.Left; w < (addme.rec.Left + addme.rec.Width); w++) {
                        if (fabric[h, w] > 1) {
                            part2 = false;
                            }
                    }
                }
                if (part2 == true) {
                    Console.WriteLine(addme.ClaimID);
                }
            }

        }
    }

    class Order
    {
        public int ClaimID;
        public Rectangle rec;

        public Order(string raw) {
            // #1247 @ 436,777: 27x25
            raw = raw.Replace(':', ' ');
            // #1247 @ 436,777  27x25
            raw = raw.Replace('x', ',');
            // #1247 @ 436,777  27,25

            string[] el = raw.Split(' ');

            string claimIDStr = el[0];
            claimIDStr = claimIDStr.Replace('#', ' ');
            ClaimID = Int32.Parse(claimIDStr);
            //  1247 @ 436,777  27,25

            string lt = el[2];
            string wh = el[4];

            string[] ltA = lt.Split(',');
            string[] whA = wh.Split(',');

            int left = Int32.Parse(ltA[0]);
            int top = Int32.Parse(ltA[1]);
            int width = Int32.Parse(whA[0]);
            int height = Int32.Parse(whA[1]);

            rec = new Rectangle(left, top, width, height);

        }
    }
}
