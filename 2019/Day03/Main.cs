using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

namespace AoC2019.Day3
{
    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Steps { get; set; }
    }

    public class Main : Base
    {
        public const int SizeX = 30000;
        public const int SizeY = 30000;

        private Position _centralport = new Position() { X = SizeX / 2, Y = SizeY / 2 };
        protected override void Part1()
        {
            List<Position> crossings = new List<Position>() { };
            RunWires(crossings);

            List<int> distances = new List<int>();
            foreach (var pos in crossings)
            {
                distances.Add(CalculateManhattan(pos));
            }
            distances.Sort();

            Console.WriteLine("Result: {0}", distances[1]);
        }

        private void RunWires(List<Position> crossings)
        {
            char[,] matrix = new char[SizeX, SizeY];

            for (int i = 0; i < SizeX; i++)
                for (int j = 0; j < SizeY; j++)
                    matrix[i, j] = ' ';

            var wire1 = Input.Wire1.Split(',');
            var wire2 = Input.Wire2.Split(',');

            RunWire(matrix, crossings, wire1, '.', 'o');
            RunWire(matrix, crossings, wire2, 'o', '.');
        }

        public void RunWire(char[,] matrix, List<Position> crossings, String[] data, char c, char c2)
        {
            int posx = _centralport.X;
            int posy = _centralport.Y;

            foreach (var item in data)
            {
                int count = int.Parse(item.Substring(1));
                while (count > 0)
                {
                    if (matrix[posx, posy] == c2)
                    {
                        //Console.WriteLine("Found X at position {0},{1}.", posx, posy);
                        crossings.Add(new Position() { X = posx, Y = posy });
                    }

                    matrix[posx, posy] = (matrix[posx, posy] == c2) ? 'x' : c;
                    count--;
                    switch (item[0])
                    {
                        case 'R':
                            posx++;
                            break;
                        case 'L':
                            posx--;
                            break;
                        case 'U':
                            posy--;
                            break;
                        case 'D':
                            posy++;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public int CalculateManhattan(Position pos)
        {
            var dx = Math.Abs(pos.X - _centralport.X);
            var dy = Math.Abs(pos.Y - _centralport.Y);
            return dx + dy;
        }

        protected override void Part2()
        {
            List<Position> crossings = new List<Position>() { };
            RunWires(crossings);

            var positions = new Dictionary<int, Position>();
            var wire1 = Input.Wire1.Split(',');
            var wire2 = Input.Wire2.Split(',');

            foreach (var pos in crossings)
            {
                var l1 = CalculatePathLengthToCrossing(wire1, pos);
                var l2 = CalculatePathLengthToCrossing(wire2, pos);

                pos.Steps = l1 + l2;
                positions[pos.Steps] = pos;

                //Console.WriteLine("{0}+{1}={2} steps to position[{3},{4}].", l1, l2, l1 + l2, pos.X, pos.Y);
            }
            List<int> keylist = new List<int>(positions.Keys);
            keylist.Sort();

            Console.WriteLine("Result: {0}", keylist[1]);
        }

        public int CalculatePathLengthToCrossing(String[] wire, Position crossing)
        {
            int length = 0;

            var position = new Position() { X = _centralport.X, Y = _centralport.Y };
            foreach (var item in wire)
            {
                int count = int.Parse(item.Substring(1));
                while (count > 0)
                {
                    if ((position.X == crossing.X) && (position.Y == crossing.Y)) return length;

                    count--;
                    length++;
                    switch (item[0])
                    {
                        case 'R':
                            position.X++;
                            break;
                        case 'L':
                            position.X--;
                            break;
                        case 'U':
                            position.Y--;
                            break;
                        case 'D':
                            position.Y++;
                            break;
                        default:
                            break;
                    }
                }
            }

            throw new Exception("Crossing not found");
        }
    }
}
