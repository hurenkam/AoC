using System;
using System.Collections.Generic;
using System.Numerics;
using System.IO;
using AoC2019.Intcode;

namespace AoC2019.Day9
{
    public class Main : Base
    {
        protected override void Part1()
        {
            var computer = new Computer();
            computer.LoadProgram(Input.Data);
            computer.input = () => { return 1; };
            computer.output = (value) => { Console.WriteLine("Result: {0}", value); };
            computer.Run();
        }

        protected override void Part2()
        {
            var computer = new Computer();
            computer.LoadProgram(Input.Data);
            computer.input = () => { return 2; };
            computer.output = (value) => { Console.WriteLine("Result: {0}", value); };
            computer.Run();
        }
    }
}
