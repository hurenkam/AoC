using AoC2019.Intcode;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

namespace AoC2019.Day2
{
    public class Main : Base
    {
        protected override void Part1()
        {
            var result = RunProgramWithNounAndVerb(12, 2);
            Console.WriteLine("Result: {0}", result);
        }

        private BigInteger RunProgramWithNounAndVerb(int noun, int verb)
        {
            var code = new List<BigInteger>(Input.Data);
            code[1] = noun;
            code[2] = verb;
            var computer = new Computer();
            computer.LoadProgram(code);
            computer.Run();
            return computer.ram[0];
        }

        protected override void Part2()
        {
            for (int noun = 0; noun < 99; noun++)
            {
                for (int verb = 0; verb < 99; verb++)
                {
                    var result = RunProgramWithNounAndVerb(noun, verb);
                    if (result == 19690720)
                    {
                        Console.WriteLine("Result: {0}", noun*100+ verb);
                        return;
                    }
                }
            }
        }
    }
}
