using System;
using System.Collections.Generic;
using System.Numerics;
using System.IO;
using System.Text;
using System.Threading;

namespace AoC2019.Day17
{
    public class Main : Base
    {
        protected override void Part1()
        {
            var vacuum = new VacuumDroid();
            vacuum.UpdateMap();
            DrawMap(vacuum.Map, vacuum.TopLeft, vacuum.BottomRight);
            DrawChar(vacuum.VacuumDroidPosition.X, vacuum.VacuumDroidPosition.Y, "^>v<"[vacuum.VacuumDroidDirection]);
            var result = CalculateSumOfIntersections(vacuum.Map,vacuum.TopLeft,vacuum.BottomRight);
            Console.SetCursorPosition(0, 1);
            Console.WriteLine("Result: {0}", result);
        }

        private int CalculateSumOfIntersections(Dictionary<Position, char> map, Position topleft, Position bottomright)
        {
            int result = 0;
            for (int x = topleft.X + 1; x < bottomright.X - 1; x++)
                for (int y = topleft.Y + 1; y < bottomright.Y - 1; y++)
                    if (   (map[new Position() { X = x, Y = y }] == '#')
                        && (map[new Position() { X = x - 1, Y = y }] == '#')
                        && (map[new Position() { X = x + 1, Y = y }] == '#')
                        && (map[new Position() { X = x, Y = y - 1 }] == '#')
                        && (map[new Position() { X = x, Y = y + 1 }] == '#'))
                    {
                        result += x * y;
                    }

            return result;
        }

        private void DrawMap(Dictionary<Position, char> map, Position topleft, Position bottomright)
        {
            foreach (var key in map.Keys)
                DrawChar(key.X, key.Y, map[key]);
        }

        public void DrawChar(int x, int y, char c)
        {
            Console.SetCursorPosition(x + 5, y + 5);
            Console.Write(c);
        }

        protected override void Part2()
        {
            String Main = "A,B,A,B,C,C,B,A,B,C";
            String A = "L,12,L,6,L,8,R,6";
            String B = "L,8,L,8,R,4,R,6,R,6";
            String C = "L,12,R,6,L,8";
            //String LF = "\x10";
            String LF = "\n";
            String Feed = "n";
            String input = Main + LF + A + LF + B + LF + C + LF + Feed + LF;

            var vacuum = new VacuumDroid();
            var result = vacuum.CollectDustFollowingPath(input);
            Console.SetCursorPosition(0, 3);
            Console.WriteLine("Result: {0}", result);
        }
    }
}
