using System;
using System.Collections.Generic;
using System.Numerics;
using System.IO;
using System.Text;
using System.Threading;

namespace AoC2019.Day19
{
    public class Main : Base
    {
        protected override void Part1()
        {
            var drone = new Drone();
            var map = drone.ProbeArea(new Area() { Left = 0, Top = 0, Right = 49, Bottom = 49 });
            drone.Halt();

            var area = GetArea(map);

            Console.WriteLine("Result: {0}", map.Count);
        }

        public Area GetArea(List<Position> map)
        {
            if (map.Count < 1) return null;

            var result = new Area() { Left = map[0].X, Right = map[0].X, Top = map[0].Y, Bottom = map[0].Y };
            foreach (var pos in map)
            {
                result.Left =   (pos.X < result.Left) ?   pos.X : result.Left;
                result.Right =  (pos.X > result.Right) ?  pos.X : result.Right;
                result.Top =    (pos.Y < result.Top) ?    pos.Y : result.Top;
                result.Bottom = (pos.Y > result.Bottom) ? pos.Y : result.Bottom;
            }
            return result;
        }

        protected override void Part2()
        {
            var drone = new Drone();
            bool halt = false;
            var x = 0;
            var y = -1;

            while (!halt)
            {
                // x and y are following the bottomleft corner of the beam
                y += 1;
                while (drone.Probe(new Position { X=x, Y=y+99 }) <1) { x++; };

                // determine if ship fits
                var area = new Area() { Left = x, Top = y, Right = x + 99, Bottom = y+99 };
                if (drone.ProbeCorners(area).Count >3) halt = true;
            }

            Console.WriteLine("Result:    {0}", x*10000+y);
        }
    }
}
