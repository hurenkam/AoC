using System;
using System.Collections.Generic;
using System.Numerics;
using System.IO;
using System.Text;
using System.Threading;
using System.Linq;

namespace AoC2019.Day20
{
    public struct Position
    {
        public int X;
        public int Y;
    }

    public struct Vertex
    {
        public int Distance;
        public string Identifier;
    }

    public class Main : Base
    {
        private List<Position> Directions = new List<Position>() {
            new Position() { X= 0, Y=-1 }, // North
            new Position() { X= 1, Y= 0 }, // East
            new Position() { X= 0, Y= 1 }, // South
            new Position() { X=-1, Y= 0 }, // West
        };

        public List<String> Map;
        public Position AA;
        public Position ZZ;

        protected override void Part1()
        {
            int result = -1;

            Map = Input.Data.Split('\n').ToList();
            //Map = Input.Example.Split('\n').ToList();
            //Map = Input.LargeExample.Split('\n').ToList();
            MapPortals();
            //PrintPortals();

            foreach (var id in PortalsById.Keys)
                MapNeighborsForPortal(id);

            //DrawMap();

            result = FindShortestPath("AA","ZZ");

            Console.SetCursorPosition(0, 1);
            Console.WriteLine("Result: {0}", result);
        }

        // Dijkstra algorithm
        public int FindShortestPath(String from, String to)
        {
            if (from==to) return 0;

            Dictionary<String, int> visited = new Dictionary<string, int>() { { from, 0 } };
            Queue<string> pending = new Queue<string>();
            pending.Enqueue(from);
            while (pending.Count > 0)
            {
                var id = pending.Dequeue();
                var portal = PortalsById[id];
                var neighbors = NeighborTable[id];
                foreach (var child in neighbors.Keys)
                {
                    var distance = neighbors[child] + visited[id] +1;
                    if (!visited.ContainsKey(child))
                    {
                        pending.Enqueue(child);
                        visited[child] = distance;
                    }
                    else
                    {
                        visited[child] = (visited[child] < distance) ? visited[child] : distance;
                    }
                }
            }

            return visited[to] -1;
        }

        public Dictionary<String, Dictionary<String, int>> NeighborTable = new Dictionary<string, Dictionary<string, int>>();
        public void MapNeighborsForPortal(String id)
        {
            Dictionary<String, int> connections = new Dictionary<string, int>();
            foreach (var pos in PortalsById[id])
                FindConnections(connections,pos,0,new List<Position>());

            NeighborTable[id] = connections;
        }

        public String BorderChars = "#ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public void FindConnections(Dictionary<String, int> connections, Position p, int length, List<Position> visited)
        {
            DrawChar((int)p.X, (int)p.Y, Map[p.Y][p.X]);

            // borders
            if (BorderChars.Contains("" + Map[p.Y][p.X])) return;

            // visited
            if (visited.Contains(p)) return;
            visited.Add(p);
            DrawChar((int)p.X, (int)p.Y, '+');

            // portals
            if (PortalsByPosition.ContainsKey(p))
            {
                var id = PortalsByPosition[p];
                if (!connections.ContainsKey(id) || (connections[id] > length))
                {
                    connections[id] = length;
                }
            }

            for (int i = 0; i < 4; i++)
            {
                var x = p.X + Directions[i].X;
                var y = p.Y + Directions[i].Y;
                var pn = new Position() { X = x, Y = y };
                FindConnections(connections, pn, length+1, visited);
            }

            return;
        }

        public void DrawChar(int x, int y, char c)
        {
            Console.SetCursorPosition(x + 45, y + 21);
            Console.Write(c);
        }

        public void PrintPortals()
        {
            foreach (var id in PortalsById.Keys)
            {
                Console.Write("Portal {0}: ", id);
                foreach (var pos in PortalsById[id])
                {
                    Console.Write("({0},{1}) ", pos.X, pos.Y);
                }
                Console.WriteLine();
            }
            Console.WriteLine("Number of portals found: {0}", PortalsByPosition.Count);
        }

        public int DetermineWidth()
        {
            var valid = ".#";
            var x = Map[0].Length / 2;
            var y = 2;
            while (valid.Contains("" + Map[y][x])) y++;
            //Console.WriteLine("Found width: {0}", y - 2);
            return y - 2;
        }

        public void MapPortals()
        {
            var width = DetermineWidth();

            // outer top
            int top = 2;
            for (int x = 2; x < Map[top].Length - 2; x++)
                if (Map[top][x] == '.') AddPortal(x, top, "" + Map[top-2][x] + Map[top-1][x]);

            // inner top
            int itop = 2+width-1;
            for (int x = 2 + width; x < Map[itop].Length - 2 - width; x++)
                if (Map[itop][x] == '.') AddPortal(x, itop, "" + Map[itop + 1][x] + Map[itop + 2][x]);

            // outer bottom
            int bottom = Map.Count - 4;
            for (int x = 2; x < Map[bottom].Length - 2; x++)
                if (Map[bottom][x] == '.') AddPortal(x, bottom, "" + Map[bottom+1][x] + Map[bottom+2][x]);

            // inner bottom
            int ibottom = Map.Count - 3 - width;
            for (int x = 2 + width; x < Map[ibottom].Length - 2 -width; x++)
                if (Map[ibottom][x] == '.') AddPortal(x, ibottom, "" + Map[ibottom - 2][x] + Map[ibottom - 1][x]);

            //outer left
            int left = 2;
            for (int y = 2; y < Map.Count - 2; y++)
                if (Map[y][left] == '.') AddPortal(left, y, "" + Map[y][left - 2] + Map[y][left - 1]);

            //inner left
            int ileft = 2 + width - 1;
            for (int y = 2 + width; y < Map.Count - 2 - width; y++)
                if (Map[y][ileft] == '.') AddPortal(ileft, y, "" + Map[y][ileft + 1] + Map[y][ileft + 2]);

            // outer right
            int right = Map[top].Length - 4;
            for (int y = 2; y < Map.Count - 2; y++)
                if (Map[y][right] == '.') AddPortal(right, y, "" + Map[y][right + 1] + Map[y][right + 2]);

            // inner right
            int iright = Map[top].Length - 3 - width;
            for (int y = 2 + width; y < Map.Count - 2 - width; y++)
                if (Map[y][iright] == '.') AddPortal(iright, y, "" + Map[y][iright - 2] + Map[y][iright - 1]);
        }

        public Dictionary<Position, String> PortalsByPosition = new Dictionary<Position, string>();
        public Dictionary<String, List<Position>> PortalsById = new Dictionary<string, List<Position>>();
        public void AddPortal(int x, int y, string id)
        {
            //Console.WriteLine("Found Portal '{0}' at {1},{2}", id, x, y);

            if (!PortalsById.ContainsKey(id)) PortalsById[id] = new List<Position>();
            var pos = new Position() { X = x, Y = y };

            PortalsByPosition[pos] = id;
            PortalsById[id].Add(pos);
        }

        public Position FindCounterpart(Position p)
        {
            if (!PortalsByPosition.ContainsKey(p)) throw new Exception(String.Format("No portal at position {0},{1}", p.X, p.Y));
            var id = PortalsByPosition[p];
            var portals = new List<Position>(PortalsById[id]);

            portals.Remove(p);
            if (portals.Count == 0) throw new Exception(String.Format("Portal with id '{0}' at position {0},{1} has no counterpart.", id, p.X, p.Y));

            return portals[0];
        }

        protected override void Part2()
        {
        }
    }
}
