using System;
using System.Collections.Generic;
using System.IO;

namespace AoC2019.Day1
{
    public class Main : Base
    {
        protected override void Part1()
        {
            int total = 0;
            foreach (var number in Input.Numbers)
            {
                total += CalculateFuelForWeight(number);
            }
            Console.WriteLine("Total fuel needed: {0}", total);
        }

        public int CalculateFuelForWeight(int weight)
        {
            var fuel = weight / 3 - 2;
            return (fuel > 0) ? fuel : 0;
        }

        protected override void Part2()
        {
            int total = 0;
            foreach (var number in Input.Numbers)
            {
                total += CalculateFuelForWeightAndAddedFuel(number);
            }
            Console.WriteLine("Total fuel needed: {0}", total);
        }

        public int CalculateFuelForWeightAndAddedFuel(int weight)
        {
            var fuel = CalculateFuelForWeight(weight);

            var delta = fuel;
            while (delta != 0)
            {
                delta = CalculateFuelForWeight(delta);
                fuel += delta;
            }

            return fuel;
        }
    }
}
