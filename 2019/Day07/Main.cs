using System;
using System.Collections.Generic;
using System.IO;

namespace AoC2019.Day7
{
    public class Main: Base
    {
        protected override void Part1()
        {
            List<List<int>> attempts = new List<List<int>>();
            CalculateValidSequences(new List<int>() { 0, 1, 2, 3, 4 }, new List<int>(), attempts);
            Console.WriteLine("Max Output: {0}", CalculateMax(attempts));
        }

        private void CalculateValidSequences(List<int> range, List<int> pre, List<List<int>> results)
        {
            if (range.Count == pre.Count)
            {
                results.Add(pre);
                return;
            }

            foreach (var i in range)
            {
                if (!pre.Contains(i))
                {
                    List<int> newpre = new List<int>(pre);
                    newpre.Add(i);
                    CalculateValidSequences(range, newpre, results);
                }
            }
        }

        private int CalculateMax(List<List<int>> attempts)
        {
            int max = 0;
            foreach (var attempt in attempts)
            {
                var A = new Amplifier(attempt[0]);
                var B = new Amplifier(attempt[1]);
                var C = new Amplifier(attempt[2]);
                var D = new Amplifier(attempt[3]);
                var E = new Amplifier(attempt[4]);

                A.Output += B.Input;
                B.Output += C.Input;
                C.Output += D.Input;
                D.Output += E.Input;
                E.Output += A.Input;

                E.Output += (value) => { max = (value > max) ? value : max; };

                A.Input(0);
            }
            return max;
        }

        protected override void Part2()
        {
            List<List<int>> attempts = new List<List<int>>();
            CalculateValidSequences(new List<int>() { 5, 6, 7, 8, 9 }, new List<int>(), attempts);
            Console.WriteLine("Max Output: {0}", CalculateMax(attempts));
        }
    }
}
