using System;
using System.Collections.Generic;
using System.IO;

namespace AoC2019.Day4
{
    public class Main : Base
    {
        protected override void Part1()
        {
            var possiblepins = new List<int>();

            int pin = Input.Min;
            while (pin <= Input.Max)
            {
                if (PinMeetsCriteria(pin))
                {
                    possiblepins.Add(pin);
                }
                pin++;
            }

            Console.WriteLine("Result: {0}", possiblepins.Count);
        }

        public bool PinMeetsCriteria(int pin)
        {
            var s = String.Format("{0}", pin);
            int prev = 0;
            bool hasdouble = false;
            foreach (char c in s)
            {
                var sc = "" + c;
                int n = int.Parse(sc);
                if (n < prev) return false;
                if (n == prev) hasdouble = true;
                prev = n;
            }

            return hasdouble;
        }

        protected override void Part2()
        {
            var possiblepins = new List<int>();

            int pin = Input.Min;
            while (pin <= Input.Max)
            {
                if (PinMeetsCriteria2(pin))
                {
                    possiblepins.Add(pin);
                }
                pin++;
            }

            Console.WriteLine("Result: {0}", possiblepins.Count);
        }

        public bool PinMeetsCriteria2(int pin)
        {
            if (!PinMeetsCriteria(pin)) return false;

            var s = String.Format("{0}", pin);
            List<int> numbers = new List<int>();
            Dictionary<int, int> duplicates = new Dictionary<int, int>();
            foreach (var c in s)
            {
                var v = int.Parse("" + c);
                numbers.Add(v);
                if (!duplicates.ContainsKey(v)) duplicates[v] = 0;
                duplicates[v] += 1;
            }
            foreach (var key in duplicates.Keys)
            {
                if (duplicates[key] == 2) return true;
            }

            return false;
        }
    }
}
