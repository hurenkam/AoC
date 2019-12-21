using AoC2019.Intcode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AoC2019.Day21
{
    public struct Position
    {
        public int X;
        public int Y;
    }

    public class SpringDroid
    {
        private Position _current = new Position() { X = 0, Y = 0 };

        private Queue<BigInteger> _input = new Queue<BigInteger>();
        public BigInteger ExecuteScript(String script)
        {
            foreach (char c in script)
                _input.Enqueue((BigInteger)c);

            var computer = new Computer();
            computer.LoadProgram(Input.Data);
            computer.input = InputProvider;
            computer.output = OutputHandler;
            computer.Run();

            return LastOutput;
        }

        private BigInteger InputProvider()
        {
            BigInteger result = 0;
            if (_input.Count > 0)
                result = _input.Dequeue();

            return result;
        }

        public BigInteger LastOutput = 0;
        private void OutputHandler(BigInteger value)
        {
            LastOutput = value;
            if (value == 10)
            {
                _current.Y += 1;
                _current.X = 0;
                return;
            }

            if ((value < 32) || (value > 127)) return;

            var c = (char)value;
            DrawChar(_current.X, _current.Y, c);
            _current.X += 1;
        }

        public void DrawChar(int x, int y, char c)
        {
            Console.SetCursorPosition(x + 25, y + 0);
            Console.Write(c);
        }
    }
}
