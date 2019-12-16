using System;
using System.Collections.Generic;
using System.Numerics;
using System.IO;

namespace AoC2019.Day10
{
    public struct RC
    {
        public int X;
        public int Y;
    };

    public class Position
    {
        public int X;
        public int Y;
    };

    public class Main : Base
    {
        private Position _selected = new Position() { X = -1, Y = -1 };

        protected override void Part1()
        {
            var data = Input.Data;
            var width = data[0].Length;
            var height = data.Count;
            var max = 0;

            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    if (data[y][x] == '#')
                    {
                        var p = new Position() { X = x, Y = y };
                        var result = CountAsteroidsInDirectLineOfSight(p);
                        if (result > max)
                        {
                            max = result;
                            _selected = p;
                        }
                    }
            Console.WriteLine("Result: {0}", max);
        }

        private int CountAsteroidsInDirectLineOfSight(Position Asteroid)
        {
            var map = MapAsteroidsInDirectLineOfSight(Asteroid);
            return map.Keys.Count;
        }

        private Dictionary<RC,List<Position>> MapAsteroidsInDirectLineOfSight(Position Asteroid)
        {
            var data = Input.Data;
            var width = data[0].Length;
            var height = data.Count;

            Dictionary<RC, List<Position>> map = new Dictionary<RC, List<Position>>();

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (data[y][x] == '#')
                    {
                        var p = new Position() { X = x, Y = y };

                        if (!((p.X == Asteroid.X) && (p.Y == Asteroid.Y)))
                        {
                            var rc = CalculateRC(Asteroid, p);
                            if (!map.ContainsKey(rc)) map[rc] = new List<Position>();
                            map[rc].Add(p);
                        }
                    }
                }
            }

            return map;
        }

        private RC CalculateRC(Position from, Position to)
        {
            var rc = new RC() { Y = (to.Y - from.Y), X = (to.X - from.X) };
            rc = SimplifyRC(rc);
            return rc;
        }

        private RC SimplifyRC(RC rc)
        {
            if (rc.X == 0) { rc.Y = rc.Y/Math.Abs(rc.Y); return rc; }
            if (rc.Y == 0) { rc.X = rc.X/Math.Abs(rc.X); return rc; }

            int d = gcd(rc.X, rc.Y);
            if (d == 0) return rc;

            if (d < 0) d = d * -1;
            rc.X = rc.X / d;
            rc.Y = rc.Y / d;

            return rc;
        }

        private int gcd(int a, int b)
        {
            if (a < 0) a = a * -1;
            if (b < 0) b = b * -1;

            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }

            return a == 0 ? b : a;
        }

        protected override void Part2()
        {
            // Laser starts Up and rotates Clockwise
            var map = MapAsteroidsInDirectLineOfSight(_selected);

            // Map RC's to angle, starting from 0 (Up) and rotate clockwise.
            Dictionary<double, List<Position>> map2 = new Dictionary<double, List<Position>>();
            foreach (var key in map.Keys)
            {
                double angle = CalculateAngle(key);
                map2[angle] = map[key];
            }

            List<double> keys = new List<double>(map2.Keys);
            keys.Sort();

            // Assumes >= 200 entries in map2
            var asteroid = map2[keys[199]][0];

            int result = asteroid.X * 100 + asteroid.Y;
            Console.WriteLine("Result: {0}", result);
        }

        private double CalculateAngle(RC rc)
        {
            if (rc.X == 0) return (rc.Y > 0) ? 180.0 : 0.0;

            double w = rc.X;
            double h = rc.Y * -1; // y coordinates are upside down

            var atan = Math.Atan(h / w) / Math.PI * 180.0;
            if (w < 0 || h < 0)
                atan += 180.0;
            if (w > 0 && h < 0)
                atan -= 180.0;

            // adjust because we need to count clockwise & start from up position
            atan = (-1 * atan) + 90.0;

            if (atan < 0)
                atan += 360.0;

            return atan % 360;
        }
    }
}
