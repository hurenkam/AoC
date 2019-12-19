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
        private Queue<BigInteger> _inputqueue = new Queue<BigInteger>();
        private Queue<BigInteger> _outputqueue = new Queue<BigInteger>();
        private Semaphore _guardinput = new Semaphore(0, 2);
        private Semaphore _guardoutput = new Semaphore(0, 1);
        private List<BigInteger> _inputs = new List<BigInteger>() { 1, 4, 2, 3 };

        public Drone()
        {
        }

        public void Halt()
        {
            _inputqueue.Enqueue(0);
            _guardinput.Release();
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
            var computer = new Computer();
            computer.LoadProgram(Input.Data);
            computer.input = InputProvider;
            computer.output = OutputHandler;
            Thread t = new Thread(new ThreadStart(computer.Run));
            t.Start();

            //Console.WriteLine("Probe({0},{1}): Enqueuing ... ", pos.X, pos.Y);
            _inputqueue.Enqueue(pos.X);
            _inputqueue.Enqueue(pos.Y);
            _guardinput.Release(2);
            //Console.WriteLine("Probe({0},{1}): Waiting ... ", pos.X, pos.Y);
            _guardoutput.WaitOne();
            int result = (int)_outputqueue.Dequeue();
            //Console.WriteLine("Probe({0},{1}): Return {2}.", pos.X, pos.Y, result);
            return result;
        }

        private BigInteger InputProvider()
        {
            //Console.WriteLine("InputProvider(): Waiting ... ");
            _guardinput.WaitOne();
            var v = _inputqueue.Dequeue();
            //Console.WriteLine("InputProvider(): Return {0}.",v);
            return v;
        }

        private void OutputHandler(BigInteger value)
        {
            //Console.WriteLine("OutputHandler({0}): Enqueing ... ", value);
            _outputqueue.Enqueue(value);
            _guardoutput.Release();
            //Console.WriteLine("OutputHandler({0}): Released.", value);
        }
    }
}
