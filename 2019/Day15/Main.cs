using System;
using System.Collections.Generic;
using System.Numerics;
using System.IO;
using System.Text;
using System.Threading;
using AoC2019.Intcode;

namespace AoC2019.Day15
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
            BuildMap();
            var length = FindShortestPathToOxygen(new Position() { X=0, Y=0 });
            Console.SetCursorPosition(0,1);
            Console.WriteLine("Result: {0}", length);
        }

        private List<Position> Directions = new List<Position>() {
            new Position() { X= 0, Y=-1 }, // North
            new Position() { X= 1, Y= 0 }, // East
            new Position() { X= 0, Y= 1 }, // South
            new Position() { X=-1, Y= 0 }, // West
        };

        private Dictionary<Position, char> Map = new Dictionary<Position, char>();
        private RepairDroid _droid = new RepairDroid();
        private Position location = new Position() { X = 0, Y = 0 };
        private byte _heading = 0;
        private Boolean _halt = false;
        private void BuildMap()
        {
            DrawChar(0, 0, 'S');
            Map[location] = 'S';
            while (!_halt)
            {
                BuildMapStep();
            }
            _droid.Halt();
        }

        private void BuildMapStep()
        {
            var l = LookAhead(_heading - 1);
            var h = LookAhead(_heading);
            var r = LookAhead(_heading + 1);

            if (r == ' ') TurnRight();
            else if (h == ' ') { /* continue ahead */ }
            else if (l == ' ') TurnLeft();
            else TurnBack();

            MoveDroid();
        }

        private char LookAhead(int direction)
        {
            if (direction < 0) direction += 4;
            if (direction > 3) direction -= 4;

            var x = (int)(location.X + Directions[direction].X);
            var y = (int)(location.Y + Directions[direction].Y);
            var r = (int)_droid.TryMove(direction);
            if (r == 1) _droid.TryMove(direction + 2);

            char c = GetChar(r);

            Map[new Position() { X = x, Y = y }] = c;
            DrawChar(x, y, c);
            return c;
        }

        public char GetChar(int r)
        {
            char c = '?';
            switch (r)
            {
                case 0:
                    c = '#';
                    break;
                case 1:
                    c = ' ';
                    break;
                case 2:
                    c = 'O';
                    _halt = true;
                    break;
                default:
                    throw new Exception("unexpected result");
                    break;
            }
            return c;
        }

        public void TurnRight()
        {
            _heading = (_heading < 3) ? (byte)(_heading + 1) : (byte)0;
        }

        public void TurnLeft()
        {
            _heading = (_heading > 0) ? (byte)(_heading - 1) : (byte)3;
        }

        public void TurnBack()
        {
            _heading = (_heading > 1) ? (byte)(_heading - 2) : (byte)(_heading + 2);
        }

        public void MoveDroid()
        {
            _droid.TryMove(_heading);
            location.X += Directions[_heading].X;
            location.Y += Directions[_heading].Y;
        }

        public void DrawChar(int x, int y, char c)
        {
            Console.SetCursorPosition(x + 45, y + 21);
            Console.Write(c);
        }

        public int FindShortestPathToOxygen(Position p)
        {
            List<int> lengths = new List<int>();
            if (!Map.ContainsKey(p)) return -1;

            if (Map[p] == '#') return -1;
            if (Map[p] == '.') return -1;
            if (Map[p] == 'O') return 0;

            Map[p] = '.';
            DrawChar((int)p.X, (int)p.Y, '.');

            for (int i = 0; i < 4; i++)
            {
                var x = p.X + Directions[i].X;
                var y = p.Y + Directions[i].Y;
                var v = FindShortestPathToOxygen(new Position() { X=x, Y=y });
                if (v != -1) lengths.Add(v);
            }
            if (lengths.Count < 1) return -1;

            lengths.Sort();
            return lengths[0]+1;
        }

        protected override void Part2()
        {
            Position p = new Position();
            foreach (var key in Map.Keys)
            {
                if (Map[key] == 'O') p = key;
            }
            var length = FindLongestPathForOxygen(p);
            Console.SetCursorPosition(0, 3);
            Console.WriteLine("Result: {0}", length - 1);
        }

        public int FindLongestPathForOxygen(Position p)
        {
            List<int> lengths = new List<int>();
            if (!Map.ContainsKey(p)) return 0;

            if (Map[p] == '#') return 0;
            if (Map[p] == 'o') return 0;

            Map[p] = 'o';
            DrawChar((int)p.X, (int)p.Y, 'o');

            for (int i = 0; i < 4; i++)
            {
                var x = p.X + Directions[i].X;
                var y = p.Y + Directions[i].Y;
                var v = FindLongestPathForOxygen(new Position() { X = x, Y = y });
                if (v != -1) lengths.Add(v);
            }
            if (lengths.Count < 1) return -1;

            lengths.Sort();
            return lengths[lengths.Count-1] + 1;
        }
    }
}
