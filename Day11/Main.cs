using System;
using System.Collections.Generic;
using System.Numerics;
using System.IO;
using System.Text;
using AoC2019.Intcode;

namespace AoC2019.Day11
{
    public struct Position
    {
        public int X;
        public int Y;
    }

    public class Main : Base
    {
        private List<Position> Directions = new List<Position>()
        {
            new Position() { X =  0, Y = -1 },
            new Position() { X =  1, Y =  0 },
            new Position() { X =  0, Y =  1 },
            new Position() { X = -1, Y =  0 },
        };

        protected override void Part1()
        {
            var map = new Dictionary<Position, BigInteger>();
            PaintRegistration(map);
            Console.WriteLine("Result: {0}", map.Keys.Count);
        }

        private void PaintRegistration(Dictionary<Position, BigInteger> map)
        {
            var pos = new Position() { X = 0, Y = 0 };
            int dir = 0;

            var computer = new Computer();
            computer.LoadProgram(Input.Data);
            computer.input = () => {
                if (map.ContainsKey(pos))
                { return map[pos]; }
                else
                { return 0; }
            };

            var output = new List<BigInteger>();
            computer.output = (value) => {
                output.Add(value);
                if (output.Count >= 2)
                {
                    var color = (byte)output[0];
                    var turn = (byte)output[1];

                    map[pos] = color;
                    switch (turn)
                    {
                        case 0:
                            dir -= 1;
                            if (dir < 0) dir = 3;
                            break;
                        case 1:
                            dir += 1;
                            if (dir > 3) dir = 0;
                            break;
                    }
                    pos.X = pos.X + Directions[dir].X;
                    pos.Y = pos.Y + Directions[dir].Y;

                    output.RemoveRange(0, 2);
                }
            };

            computer.Run();
        }

        protected override void Part2()
        {
            var map = new Dictionary<Position, BigInteger>();

            // Start panel must be white
            map[new Position() { X = 0, Y = 0 }] = 1;

            PaintRegistration(map);

            Console.WriteLine("Result: ");
            PrintMap(map);
            // LPZKLGHR
        }

        private void PrintMap(Dictionary<Position, BigInteger> map)
        {
            Position TopLeft = new Position() { X = 0, Y = 0 };
            Position BottomRight = new Position() { X = 0, Y = 0 };

            foreach (var key in map.Keys)
            {
                if (key.X < TopLeft.X) TopLeft.X = key.X;
                if (key.Y < TopLeft.Y) TopLeft.Y = key.Y;
                if (key.X > BottomRight.X) BottomRight.X = key.X;
                if (key.Y > BottomRight.Y) BottomRight.Y = key.Y;
            }

            int width = BottomRight.X - TopLeft.X + 1;
            int height = BottomRight.Y - TopLeft.Y + 1;
            List<StringBuilder> lines = new List<StringBuilder>();
            for (int i = 0; i < height; i++)
            {
                var line = new StringBuilder();
                line.Append(' ', width);
                lines.Add(line);
            }

            foreach (var key in map.Keys)
            {
                int x = key.X - TopLeft.X;
                int y = key.Y - TopLeft.Y;
                char c = (map[key] == 1) ? '#' : ' ';
                lines[y][x] = c;
            }

            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }
        }
    }
}
