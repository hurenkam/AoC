using AoC2019.Intcode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AoC2019.Day17
{
    public struct Position
    {
        public int X;
        public int Y;
    }

    public class VacuumDroid
    {
        public Dictionary<Position, char> Map = new Dictionary<Position, char>();
        public Position TopLeft = new Position() { X = 0, Y = 0 };
        public Position BottomRight = new Position() { X = 0, Y = 0 };
        public Position VacuumDroidPosition = new Position() { X = 0, Y = 0 };
        public byte VacuumDroidDirection = 0;
        private Position _current = new Position() { X = 0, Y = 0 };

        public void UpdateMap()
        {
            var computer = new Computer();
            computer.LoadProgram(Input.Data);
            computer.input = InputProvider;
            computer.output = OutputHandler;
            _current = new Position() { X = 0, Y = 0 };
            computer.Run();
        }

        private Queue<BigInteger> _input = new Queue<BigInteger>();
        public BigInteger CollectDustFollowingPath(String path)
        {
            foreach (char c in path)
                _input.Enqueue((BigInteger)c);

            var computer = new Computer();
            computer.LoadProgram(Input.Data);
            computer.ram[0] = 2;
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
                UpdateCorners();
                return;
            }

            if ((value < 32) || (value > 127)) return;

            var c = (char)value;
            DrawChar(_current.X, _current.Y, c);

            if ("^><V".Contains(c))
            {
                VacuumDroidPosition = _current;
                VacuumDroidDirection = (byte) "^>v<".IndexOf(c);
                c = '#';
            }

            Map[_current] = c;
            _current.X += 1;
            UpdateCorners();
        }

        public void DrawChar(int x, int y, char c)
        {
            Console.SetCursorPosition(x + 5, y + 5);
            Console.Write(c);
        }

        private void UpdateCorners()
        {
            TopLeft.X = (TopLeft.X < _current.X) ? TopLeft.X : _current.X;
            TopLeft.X = (TopLeft.Y < _current.Y) ? TopLeft.Y : _current.Y;
            BottomRight.X = (BottomRight.X > _current.X) ? BottomRight.X : _current.X;
            BottomRight.Y = (BottomRight.Y > _current.Y) ? BottomRight.Y : _current.Y;
        }
    }
}
