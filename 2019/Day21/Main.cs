using System;
using System.Collections.Generic;
using System.Numerics;
using System.IO;
using System.Text;
using System.Threading;
using System.Linq;

namespace AoC2019.Day21
{
    public class Main : Base
    {
        protected override void Part1()
        {
            var springdroid = new SpringDroid();

            List<String> lines = new List<string>() {
                "NOT A T",
                "NOT B J",
                "OR T J",
                "NOT C T",
                "OR T J",
                "AND D J",
                "WALK"
            };
            String LF = "\n";
            String script = "";
            foreach (var line in lines)
                script = script + line + LF;

            springdroid.ExecuteScript(script);

            Console.SetCursorPosition(0, 1);
            Console.WriteLine("Result: {0}", springdroid.LastOutput);
        }

        protected override void Part2()
        {
            var springdroid = new SpringDroid();

            List<String> lines = new List<string>() {
                "NOT A J",
                "NOT C T",
                "AND H T",
                "OR T J",
                "NOT B T",
                "AND A T",
                "AND C T",
                "OR T J",
                "AND D J",
                "RUN"
            };
            String LF = "\n";
            String script = "";
            foreach (var line in lines)
                script = script + line + LF;

            springdroid.ExecuteScript(script);

            Console.SetCursorPosition(0, 3);
            Console.WriteLine("Result: {0}", springdroid.LastOutput);
        }
    }
}
