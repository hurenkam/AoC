using AoC2019.Intcode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AoC2019.Day19
{
    public class Position
    {
        public int X;
        public int Y;
    }

    public class Area
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    public class Drone
    {
        private Computer _computer;

        public Drone()
        {
            _computer = new Computer();
            _computer.LoadProgram(Input.Data);
        }

        public List<Position> ProbeArea(Area area)
        {
            List<Position> map = new List<Position>();
            for (int x = area.Left; x <= area.Right; x++)
                for (int y = area.Top; y <= area.Bottom; y++)
                {
                    var pos = new Position() { X = x, Y = y };
                    if (Probe(pos) > 0) map.Add(pos);
                }
            return map;
        }

        public List<Position> ProbeCorners(Area area)
        {
            //Console.WriteLine("ProbeCorners({0},{1},{2},{3}))", area.Left,area.Top,area.Right,area.Bottom);
            List<Position> map = new List<Position>();
            foreach(int x in new int[] { area.Left, area.Right } )
                foreach (int y in new int[] { area.Top, area.Bottom })
                {
                    var pos = new Position() { X = x, Y = y };
                    if (Probe(pos) > 0) map.Add(pos);
                }
            return map;
        }

        public int Probe(Position pos)
        {
            //Console.WriteLine("Probe()");
            int result = -1;

            var q = new Queue<BigInteger>();
            q.Enqueue(pos.X);
            q.Enqueue(pos.Y);
            _computer.Reset();
            _computer.input = () => { return q.Dequeue(); };
            _computer.output = (value) => { result = (int)value; };
            _computer.Run();

            return result;
        }
    }
}
