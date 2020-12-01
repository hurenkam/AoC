using AoC2019.Intcode;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace AoC2019.Day13
{
    public struct Position
    {
        public BigInteger X;
        public BigInteger Y;
    }

    public class Main : Base
    {
        protected override void Part1()
        {
            Console.CursorVisible=false;

            var computer = new Computer();
            computer.LoadProgram(Input.Data);
            computer.input = InputProvider;
            computer.output = OutputHandler;
            computer.Run();
            //computer.Disassemble(Input.Data);

            Console.SetCursorPosition(0, 25);
            Console.WriteLine("Part1 Result: {0}", CountBlockTiles());
        }

        private List<BigInteger> logger = new List<BigInteger>() { };
        private BigInteger InputProvider()
        {
            if (Ball.X > Pad.X) return 1;
            if (Ball.X < Pad.X) return -1;
            return 0;
        }

        private Dictionary<Position, char> _screen = new Dictionary<Position, char>();
        private BigInteger _score = 0;
        private List<BigInteger> _output = new List<BigInteger>();
        private String _chars = " #*-o";
        private Position Ball = new Position() { X = 0, Y = 0 };
        private Position Pad = new Position() { X = 0, Y = 0 };
        private void OutputHandler(BigInteger value)
        {
            _output.Add(value);
            if (_output.Count > 2)
            {
                BigInteger x = _output[0];
                BigInteger y = _output[1];
                BigInteger s = _output[2];
                _output.RemoveRange(0, 3);

                if ((x == -1) && (y == 0))
                {
                    Console.SetCursorPosition(0, 0);
                    Console.Write("Score: {0}              ", s);
                    _score = s;
                }
                else
                {
                    char c = _chars[(int)s];
                    var p = new Position() { X = x, Y = y };
                    _screen[p] = c;
                    Console.SetCursorPosition((int)x,(int)y+1);
                    Console.Write(c);
                    if (c == 'o')
                        Ball = p;
                    if (c == '-')
                        Pad = p;
                }
            }
        }

        private int CountBlockTiles()
        {
            var result = 0;
            foreach (var key in _screen.Keys)
            {
                if (_screen[key] == '*') result += 1;
            }
            return result;
        }

        protected override void Part2()
        {
            Console.CursorVisible = false;
            Input.Data[0] = 2;

            var computer = new Computer();
            computer.LoadProgram(Input.Data);
            computer.input = InputProvider;
            computer.output = OutputHandler;
            computer.Run();

            Console.SetCursorPosition(0, 26);
            Console.WriteLine("Part2 Result: {0}", _score);
        }
    }
}
