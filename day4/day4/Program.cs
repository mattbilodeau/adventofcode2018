using System;
using System.Collections.Generic;

namespace day4
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            // load data
            string[] arg = System.IO.File.ReadAllLines("/Users/kingpin/Projects/adventofcode/day4/day4/data.txt");

            List<LDate> shiftLog = new List<LDate>();
            List<Guard> guards = new List<Guard>();

            foreach (string log in arg)
            {
                if (log.IndexOf("Guard") != -1)
                {
                    string guardID = "";
                    string datestring = log.Substring(1, 16);
                    DateTime ldate = DateTime.Parse(datestring);

                    // we identify which guard is on
                    guardID = log.Substring(26, 9);
                    guardID = guardID.Substring(0, guardID.IndexOf(" "));

                    if (ldate.Hour > 20)
                    {
                        ldate = ldate.AddDays(1);
                    }

                    LDate addme = new LDate();
                    addme.guardID = guardID;
                    addme.date = ldate;
                    shiftLog.Add(addme);
                }
            }

            foreach (string log in arg)
            {
                if (log.IndexOf("Guard") == -1)
                {

                    string datestring = log.Substring(1, 16);
                    DateTime ldate = DateTime.Parse(datestring);

                    bool found = false;
                    int foundID = -1;
                    int id = 0;
                    foreach (LDate ld in shiftLog)
                    {
                        if (ld.date.Date == ldate.Date)
                        {
                            found = true;
                            foundID = id;
                            break;
                        }
                        id++;
                    }

                    if (log.IndexOf("falls") != -1)
                    {
                        // we identify which guard is on
                        if (found)
                        {
                            shiftLog[foundID].log[ldate.Minute] = 1;
                        }
                        else
                        {
                            LDate addme = new LDate();
                            addme.log[ldate.Minute] = 1;
                            addme.date = ldate;
                            shiftLog.Add(addme);
                        }
                    }

                    if (log.IndexOf("wakes") != -1)
                    {
                        // we identify which guard is on

                        if (found)
                        {
                            shiftLog[foundID].log[ldate.Minute] = 2;
                        }
                        else
                        {
                            LDate addme = new LDate();
                            addme.log[ldate.Minute] = 2;
                            addme.date = ldate;
                            shiftLog.Add(addme);
                        }
                    }
                }
            }
            foreach (LDate ld in shiftLog)
            {
                bool sleepflag = false;
                for (int i = 0; i < ld.log.Length; i++)
                {
                    if (ld.log[i] == 1)
                    {
                        sleepflag = true;
                    }
                    else if (sleepflag == true)
                    {
                        if (ld.log[i] == 2)
                        {
                            sleepflag = false;
                            ld.log[i] = 0;
                        }
                        else
                        {
                            ld.log[i] = 1;
                        }
                    }
                }
                string outp = "";
                int sleepmin = 0;
                foreach (int i in ld.log)
                {
                    outp += i.ToString();
                    sleepmin += i;
                }

                bool foundguard = false;
                int foundguardID = -1;
                int p = 0;
                foreach (Guard g in guards)
                {
                    if (g.guardID == ld.guardID)
                    {
                        foundguard = true;
                        foundguardID = p;
                        break;
                    }
                    p++;
                }

                if (foundguard)
                {
                    guards[foundguardID].sleeptime += sleepmin;
                    for (int j = 0; j < guards[foundguardID].intersect.Length; j++)
                    {
                        guards[foundguardID].intersect[j] += ld.log[j];
                    }


                }
                else
                {
                    Guard addme = new Guard();
                    addme.guardID = ld.guardID;
                    addme.sleeptime += sleepmin;
                    for (int j = 0; j < ld.log.Length; j++)
                    {
                        addme.intersect[j] += ld.log[j];
                    }
                    guards.Add(addme);
                }



                //Console.WriteLine(ld.date.ToString("yyyy-MM-dd") + " " + ld.guardID + " " + outp + " " + sleepmin);
            }

            int maxsleepy = 0;
            string maxsleepyguard = "";
            foreach (Guard g in guards)
            {
                if (g.sleeptime > maxsleepy)
                {
                    maxsleepyguard = g.guardID;
                    maxsleepy = g.sleeptime;
                }
                int highestSleepMinute = 0;
                for(int p = 0; p < g.intersect.Length; p++)
                {
                    if (g.intersect[p] > highestSleepMinute)
                    {
                        highestSleepMinute = g.intersect[p];
                    }
                }
                g.highSleepMinute = highestSleepMinute;
            }
            int[] intersectLog = new int[60];
            foreach (LDate ld in shiftLog)
            {
                if (maxsleepyguard == ld.guardID)
                {
                    string outp = "";
                    for (int i = 0; i < ld.log.Length; i++)
                    {
                        intersectLog[i] += ld.log[i];
                        outp += ld.log[i].ToString();
                    }

                    //Console.WriteLine(outp);
                }
            }

            //part 1
            string opt = "";
            int max = 0;
            int maxday = -1;
            int d = 0;
            foreach (int i in intersectLog)
            {
                if ( i > max)
                {
                    max = i;
                    maxday = d;
                }
                d++;
            }
            Console.WriteLine(maxsleepyguard);
            Console.WriteLine(maxday);

            //part2
            int hsleep = 0;
                string hgid = "";
            foreach (Guard g in guards)
            {
                if (g.highSleepMinute > hsleep)
                {
                    hsleep = g.highSleepMinute;
                    hgid = g.guardID;
                }
            }

            Console.WriteLine(hgid + " * " + hsleep + " ");
        }
    }

    class Guard
    {
        public string guardID;
        public int sleeptime;
        public int[] intersect = new int[60];
        public int highSleepMinute;
    }

    class LDate
    {
        public DateTime date;
        public int[] log = new int[60];
        public string guardID = "";


    }
}
