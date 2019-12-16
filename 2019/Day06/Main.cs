using System;
using System.Collections.Generic;
using System.IO;

namespace AoC2019.Day6
{
    public class Main: Base
    {
        public class ObjectInOrbit
        {
            public static bool Debug = false;
            public String Identifier { get; set; }
            public ObjectInOrbit Center { get; set; }

            public int CalculateTotalOrbits(ObjectInOrbit stop = null)
            {
                int result = 0;
                var next = this.Center;
                while (next != stop)
                {
                    result += 1;
                    next = next.Center;
                }
                return result;
            }

            public List<ObjectInOrbit> WalkPath(ObjectInOrbit stop = null)
            {
                if (Debug) Console.Write("WalkPath() {0}", Identifier);
                List<ObjectInOrbit> result = new List<ObjectInOrbit>();
                var next = this.Center;
                while (next != stop)
                {
                    if (Debug) Console.Write("->{0}", next.Identifier);
                    result.Add(next);
                    next = next.Center;
                }
                if (Debug) Console.WriteLine();
                return result;
            }
        }

        private Dictionary<String, ObjectInOrbit> objectsinorbit = new Dictionary<string, ObjectInOrbit>();

        public Main()
        {
            ObjectInOrbit.Debug = Debug;
            BuildTree();
        }

        private void BuildTree()
        {
            foreach (var item in Input.Data.Split('\n'))
            {
                if (item != "")
                {
                    var items = item.Split(')');
                    var center = items[0].Trim();
                    var orbiter = items[1].Trim();

                    if (Debug) Console.WriteLine("BuildTree()  Object {0} orbits {1}", orbiter, center);

                    if (!objectsinorbit.ContainsKey(center))
                    {
                        objectsinorbit[center] = new ObjectInOrbit() { Identifier = center, Center = null };
                    }

                    if (!objectsinorbit.ContainsKey(orbiter))
                    {
                        objectsinorbit[orbiter] = new ObjectInOrbit() { Identifier = orbiter, Center = null };
                    }

                    objectsinorbit[orbiter].Center = objectsinorbit[center];
                }
            }
        }

        protected override void Part1()
        {
            var total = 0;
            foreach (var key in objectsinorbit.Keys)
            {
                var pathlength = objectsinorbit[key].CalculateTotalOrbits();
                total += pathlength;
            }

            Console.WriteLine("Calculated total: {0}", total);
        }

        protected override void Part2()
        {
            List<ObjectInOrbit> you = objectsinorbit["YOU"].WalkPath();
            List<ObjectInOrbit> san = objectsinorbit["SAN"].WalkPath();

            ObjectInOrbit nearestcommon = FindNearestCommon(you, san);

            var dyou = objectsinorbit["YOU"].CalculateTotalOrbits(nearestcommon);
            var dsan = objectsinorbit["SAN"].CalculateTotalOrbits(nearestcommon);

            Console.WriteLine("Calculated diff: {0}", dyou + dsan);
        }

        private ObjectInOrbit FindNearestCommon(List<ObjectInOrbit> path1, List<ObjectInOrbit> path2)
        {
            foreach (var item in path1)
            {
                if (path2.Contains(item))
                    return item;
            }
            return null;
        }
    }
}
