using System;

using AoC2019.Day19;

namespace AoC2019
{
    public abstract class Base
    {
        public bool Debug = false;

        public void Run()
        {
            bool saveCursorVisibile = Console.CursorVisible;
            Console.WriteLine("Part1...");
            Part1();
            Console.SetCursorPosition(0, 2);
            Console.WriteLine("Part2...");
            Part2();
            Console.SetCursorPosition(0, 4);
            Console.WriteLine("Press enter to quit.");
            Console.ReadLine();
            Console.CursorVisible = saveCursorVisibile;
        }

        protected abstract void Part1();
        protected abstract void Part2();
    }

    class Program
    {
        static void Main(string[] args)
        {
            var p = new Main();
            p.Run();
        }
    }
}
