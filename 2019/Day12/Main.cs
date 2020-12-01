using System;
using System.Collections.Generic;
using System.Numerics;
using System.IO;
using System.Text;
using System.Linq;

namespace AoC2019.Day12
{
    public struct Vector3d
    {
        public int X;
        public int Y;
        public int Z;
    }

    public struct State
    {
        public int pX;
        public int pY;
        public int pZ;
        public int vX;
        public int vY;
        public int vZ;
    }

    public class Moon
    {
        public List<BigInteger> Location = new List<BigInteger>() { 0, 0, 0 };
        public List<BigInteger> Velocity = new List<BigInteger>() { 0, 0, 0 };

        public int PotentialEnergy { get { return Math.Abs((int) Location[0]) + Math.Abs((int) Location[1]) + Math.Abs((int) Location[2]); } }
        public int KineticEnergy   { get { return Math.Abs((int) Velocity[0]) + Math.Abs((int) Velocity[1]) + Math.Abs((int) Velocity[2]); } }

        public void UpdateVelocity(List<Moon> moons)
        {
            for (byte i = 0; i < 3; i++)
            {
                UpdateVelocity(moons, i);
            }
        }
        public void UpdateVelocity(List<Moon> moons, byte axis)
        {
            foreach (var moon in moons)
            {
                if (moon.Location[axis] > Location[axis]) Velocity[axis] += 1;
                if (moon.Location[axis] < Location[axis]) Velocity[axis] -= 1;
            }
        }

        public void UpdateLocation()
        {
            for (byte i = 0; i < 3; i++)
            {
                UpdateLocation(i);
            }
        }

        public void UpdateLocation(byte axis)
        {
            Location[axis] += Velocity[axis];
        }
    }

    public class Main : Base
    {
        public List<Moon> Moons = new List<Moon>()
        {
            new Moon() { Location = new List<BigInteger>() {   5,   4,   4 } },
            new Moon() { Location = new List<BigInteger>() { -11, -11,  -3 } },
            new Moon() { Location = new List<BigInteger>() {   0,   7,   0 } },
            new Moon() { Location = new List<BigInteger>() { -13,   2,  10 } },
        };

        public List<State> StatesFound = new List<State>();

        protected override void Part1()
        {
            Iterate(1000);
            var energy = CalculateEnergy();
            Console.WriteLine("Result: {0}", energy);
        }

        private void Iterate(int count)
        {
            for (var i = 0; i < count; i++)
            {
                foreach (var moon in Moons)
                {
                    moon.UpdateVelocity(Moons);
                }
                foreach (var moon in Moons)
                {
                    moon.UpdateLocation();
                }
            }
        }

        private int CalculateEnergy()
        {
            var result = 0;
            foreach (var moon in Moons)
            {
                result = result + moon.PotentialEnergy * moon.KineticEnergy;
            }
            return result;
        }

        protected override void Part2()
        {
            // since the axis don't interact with each other, their periods can be determined independently
            // so iterate over x y and z seperately
            // then calculate LCM over the resulting periods

            List<BigInteger> periods = new List<BigInteger>();
            for (byte i = 0; i < 3; i++)
            {
                var count = IterateSingleAxis(i);
                periods.Add(count);
                //Console.WriteLine("Found count {0}: {1}", i, count);
            }
            Console.WriteLine("Result: {0}", CalculateLcm(periods));
        }

        private BigInteger IterateSingleAxis(byte axis)
        {
            BigInteger result = 0;
            List<BigInteger> start = new List<BigInteger>();
            foreach (var moon in Moons)
            {
                start.Add(moon.Location[axis]);
                start.Add(moon.Velocity[axis]);
            }

            BigInteger count = 0;
            do
            {
                foreach (var moon in Moons)
                {
                    moon.UpdateVelocity(Moons,axis);
                }
                foreach (var moon in Moons)
                {
                    moon.UpdateLocation(axis);
                }

                count++;

                List<BigInteger> state = new List<BigInteger>();
                foreach (var moon in Moons)
                {
                    state.Add(moon.Location[axis]);
                    state.Add(moon.Velocity[axis]);
                }

                if (start.SequenceEqual(state)) return count;

            } while (true);
        }

        private BigInteger CalculateLcm(List<BigInteger> numbers)
        {
            BigInteger result = CalculateLcm(numbers[0], numbers[1]);
            if (numbers.Count == 2) return result;

            numbers.RemoveRange(0, 2);
            while (numbers.Count > 0)
            {
                result = CalculateLcm(result, numbers[0]);
                numbers.RemoveAt(0);
            }
            return result;
        }

        private BigInteger CalculateLcm(BigInteger a, BigInteger b)
        {
            return (a * b) / CalculateGcd(a, b);
        }

        private BigInteger CalculateGcd(BigInteger a, BigInteger b)
        {
            return b == 0 ? a : CalculateGcd(b, a % b);
        }
    }
}
