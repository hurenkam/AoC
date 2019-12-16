using AoC2019.Intcode;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace AoC2019.Day5
{
    public class Main : Base
    {
        protected override void Part1()
        {
            BigInteger output = 0;

            var computer = new Computer();
            computer.LoadProgram(Input.Data);
            computer.input = () => { return 1; };
            computer.output = (value) => { output = value; };
            computer.Run();

            Console.WriteLine("Result: {0}", output);
        }

        protected override void Part2()
        {
            BigInteger output = 0;

            var computer = new Computer();
            computer.LoadProgram(Input.Data);
            computer.input = () => { return 5; };
            computer.output = (value) => { output = value; };
            computer.Run();

            Console.WriteLine("Result: {0}", output);
        }
    }
}
