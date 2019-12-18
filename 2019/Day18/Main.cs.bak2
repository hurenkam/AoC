using System;
using System.Collections.Generic;
using System.Numerics;
using System.IO;
using System.Text;
using System.Threading;

namespace AoC2019.Day18
{
    public struct Position
    {
        public int X;
        public int Y;
    }

    public class Node
    {
        public char id;
        public Position pos;
        public Dictionary<Node, int> connections;
    }

    public class FindNodeResult
    {
        public Node node;
        public int length;
    }

    public class Main : Base
    {
        public List<StringBuilder> Map = new List<StringBuilder>();
        public Position Start = new Position() { X = -1, Y = -1 };

        public List<Position> Headings = new List<Position>() {
            new Position() { X =  0, Y = -1 }, // North
            new Position() { X =  1, Y =  0 }, // East
            new Position() { X =  0, Y =  1 }, // South
            new Position() { X = -1, Y =  0 }, // West
        };

        public const byte North = 0;
        public const byte East = 1;
        public const byte South = 2;
        public const byte West = 3;

        protected override void Part1()
        {
            InitialiseMap();
            Start = FindStartPosition();

            // Make sure that the maze is also narrow at start position
            // should not affect outcome
            Map[Start.Y][Start.X - 1] = '#';
            Map[Start.Y][Start.X + 1] = '#';

            DrawMap();

            var node = GetNode(Start);
            BuildGraph(node,new List<Node>());
        }

        public void BuildGraph(Node start, List<Node> visited)
        {
            var x = start.pos.X;
            var y = start.pos.Y;
            var c = Map[start.pos.Y][start.pos.X];

            for (int heading = 0; heading < 4; heading++)
            {
                var p = new Position() { X = x + Headings[heading].X, Y = y + Headings[heading].Y };
                FindNodes(start.connections, p, heading, 1);
            }
        }

        public void FindNodes(Dictionary<Node,int> results, Position p, int heading, int length)
        {
            var x = p.X;
            var y = p.Y;
            var c = Map[p.Y][p.X];

            if (IsWall(p))
            {
                DrawChar(x, y, c, ConsoleColor.DarkGray);
                return;
            }

            if (IsKnown(p))
            {
                results[NodesPerPosition[p]] = length;
            }

            if (IsDoor(p))
            {
                DrawChar(x, y, c, ConsoleColor.Red);
                var node = GetNode(p);
                results[node] = length;
                return;
            }

            if (IsKey(p))
            {
                DrawChar(x, y, c, ConsoleColor.Green);
                var node = GetNode(p);
                results[node] = length;
                return;
            }

            DrawChar(x, y, c, ConsoleColor.DarkBlue);

            var left = (heading - 1) >= 0 ? heading - 1 : 3;
            var right = (heading + 1) < 4 ? heading + 1 : 0;

            var pl = new Position() { X = p.X + Headings[left].X, Y = p.Y + Headings[left].Y };
            var ph = new Position() { X = p.X + Headings[heading].X, Y = p.Y + Headings[heading].Y };
            var pr = new Position() { X = p.X + Headings[right].X, Y = p.Y + Headings[right].Y };

            FindNodes(results, pr, right, length + 1);
            FindNodes(results, ph, heading, length + 1);
            FindNodes(results, pl, left, length + 1);
        }

        public void InitialiseMap()
        {
            Map = new List<StringBuilder>();
            var lines = Input.Data.Split('\n');
            foreach (var line in lines)
            {
                Map.Add(new StringBuilder(line));
            }
        }

        public Position FindStartPosition()
        {
            for (var y = 0; y < Map.Count; y++)
                for (var x = 0; x < Map[0].Length; x++)
                    if (Map[y][x] == '@') { return new Position() { X = x, Y = y }; };

            return new Position() { X = -1, Y = -1 };
        }

        private const int xoffset = 40;
        private const int yoffset = 0;
        public void DrawMap()
        {
            Console.ReadLine();
            for (var y = 0; y < Map.Count; y++)
            {
                Console.SetCursorPosition(xoffset, yoffset + y);
                Console.Write(Map[y]);
            }
        }

        public void DrawChar(int x, int y, char c, ConsoleColor color = ConsoleColor.White)
        {
            var saved = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.SetCursorPosition(xoffset + x, yoffset + y);
            Console.Write(c);
            Console.ForegroundColor = saved;
        }

        public Dictionary<Position, Node> NodesPerPosition = new Dictionary<Position, Node>();
        public Dictionary<char, Node> NodesPerChar = new Dictionary<char, Node>();
        public String DoorChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public String KeyChars = "abcdefghijklmnopqrstuvwxyz";

        public Node GetNode(Position p)
        {
            if (NodesPerPosition.ContainsKey(p))
            {
                //return NodesPerPosition[p];
                throw new Exception("Node already listed");
            }
            var node = new Node() { id = Map[p.Y][p.X], pos = p, connections = new Dictionary<Node, int>() };

            //Console.WriteLine("GetNode(): Creating node with id '{0}' and position {1},{2}", node.id, node.pos.X, node.pos.Y);
            NodesPerPosition[p] = node;
            if ((DoorChars.Contains("" + node.id)) || (KeyChars.Contains("" + node.id)))
                NodesPerChar[node.id] = node;

            return node;
        }

        public bool IsKnown(Position p)
        {
            return NodesPerPosition.ContainsKey(p);
        }

        public bool IsWall(Position p)
        {
            return Map[p.Y][p.X] == '#';
        }

        public bool IsDoor(Position p)
        {
            return DoorChars.Contains("" + Map[p.Y][p.X]);
        }

        public bool IsKey(Position p)
        {
            return KeyChars.Contains("" + Map[p.Y][p.X]);
        }

        public bool IsPath(Position p)
        {
            if (Map[p.Y][p.X] == '.') return true;
            if (IsDoor(p)) return true;
            if (IsKey(p)) return true;

            return false;
        }

        protected override void Part2()
        {
        }
    }

/*
    public class _Main : Base
    {
        public List<StringBuilder> Map = new List<StringBuilder>();
        public Position Start = new Position() { X = -1, Y = -1 };

        public List<Position> Headings = new List<Position>() {
            new Position() { X =  0, Y = -1 }, // North
            new Position() { X =  1, Y =  0 }, // East
            new Position() { X =  0, Y =  1 }, // South
            new Position() { X = -1, Y =  0 }, // West
        };

        public const byte North = 0;
        public const byte East = 1;
        public const byte South = 2;
        public const byte West = 3;

        protected override void Part1()
        {
            InitialiseMap();
            Start = FindStartPosition();

            // Make sure that the maze is also narrow at start position
            // should not affect outcome
            Map[Start.Y][Start.X - 1] = '#';
            Map[Start.Y][Start.X + 1] = '#';

            DrawMap();

            var node = GetNode(Start);
            BuildGraph(node);

            //var keys = new List<Node>();
            //var doors = new List<Node>();
            //FindReachableKeysAndDoors(node, new List<Node>(), keys, doors);

            //Console.Write("Result: ");
            //foreach (var key in keys)
            //{
            //    Console.WriteLine("Key  '{0}': {1}", key.id, FindShortestPath(node, NodesPerChar[key.id], new List<Node>()));
            //}

            var paths = GeneratePaths("@", node);
            foreach (var path in paths)
            {
                var result = CalculatePathLength(path);
            }
        }

        public List<String> GeneratePaths(String prefix, Node from)
        {
            var result = new List<String>();

            var keys = new List<Node>();
            var doors = new List<Node>();

            FindReachableKeysAndDoors(prefix, from, new List<Node>(), keys, doors);
            var chars = new List<char>();
            foreach (var key in keys) chars.Add(key.id);

            foreach (var door in doors)
            {
                var s = "";
                //chars.Add(door.id);
                foreach (var c in chars)
                {

                }
            }

            if (from.id != '.') prefix += from.id;

            return result;
        }

        public int CalculatePathLength(String path)
        {
            return 0;
        }


        public void InitialiseMap()
        {
            Map = new List<StringBuilder>();
            var lines = Input.Data.Split('\n');
            foreach (var line in lines)
            {
                Map.Add(new StringBuilder(line));
            }
        }

        public Position FindStartPosition()
        {
            for (var y = 0; y < Map.Count; y++)
                for (var x = 0; x < Map[0].Length; x++)
                    if (Map[y][x] == '@') { return new Position() { X = x, Y = y }; };

            return new Position() { X = -1, Y = -1 };
        }

        private const int xoffset = 40;
        private const int yoffset = 0;
        public void DrawMap()
        {
            for (var y = 0; y < Map.Count; y++)
            {
                //Console.SetCursorPosition(xoffset, yoffset+y);
                //Console.Write(Map[y]);
            }
        }

        public void DrawChar(int x, int y, char c, ConsoleColor color=ConsoleColor.White)
        {
            //var saved = Console.ForegroundColor;
            //Console.ForegroundColor = color;
            //Console.SetCursorPosition(xoffset + x, yoffset + y);
            //Console.Write(c);
            //Console.ForegroundColor = saved;
        }

        public Dictionary<Position, Node> NodesPerPosition = new Dictionary<Position, Node>();
        public Dictionary<char, Node> NodesPerChar = new Dictionary<char, Node>();

        public void BuildGraph(Node start)
        {
            var x = start.pos.X;
            var y = start.pos.Y;
            var c = Map[start.pos.Y][start.pos.X];
            //Console.WriteLine("BuildGraph({0},{1}): '{2}'", x, y, c);

            for (int heading = 0; heading < 4; heading++)
            {
                var p = new Position() { X = x + Headings[heading].X, Y = y + Headings[heading].Y };
                var cp = Map[p.Y][p.X];

                if (cp != '#')
                {
                    var result = FindNode(p, heading, 1);
                    if (result != null) start.connections[result.node] = result.length;
                }
            }
        }

        public FindNodeResult FindNode(Position p, int heading, int length)
        {
            var x = p.X;
            var y = p.Y;
            var c = Map[p.Y][p.X];

            DrawChar(x, y, c, ConsoleColor.DarkBlue);
            if (c == '#')
            {
                throw new Exception("Should not query borders");
            }

            //Console.WriteLine("FindNode({0},{1},{2},{3}): '{4}'", x, y, heading, length, c);
            if (IsKnown(p))
            {
                //Console.WriteLine("FindNode({0},{1},{2},{3}): '{4}' is already mapped.", x, y, heading, length, c);
                return new FindNodeResult() { node = NodesPerPosition[p], length = length };
            }

            if (IsDoor(p))
            {
                DrawChar(x, y, c, ConsoleColor.Red);
                //Console.WriteLine("FindNode({0},{1},{2},{3}): '{4}' is new door.", x, y, heading, length, c);
                var node = GetNode(p);
                BuildGraph(node);
                return new FindNodeResult() { node = node, length = length };
            }

            if (IsKey(p))
            {
                DrawChar(x, y, c, ConsoleColor.Green);
                //Console.WriteLine("FindNode({0},{1},{2},{3}): '{4}' is new key.", x, y, heading, length, c);
                var node = GetNode(p);
                BuildGraph(node);
                return new FindNodeResult() { node = node, length = length };
            }

            if (IsIntersection(p))
            {
                DrawChar(x, y, c, ConsoleColor.Blue);
                //Console.WriteLine("FindNode({0},{1},{2},{3}): '{4}' is new intersection.", x, y, heading, length, c);
                var node = GetNode(p);
                BuildGraph(node);
                return new FindNodeResult() { node = node, length = length };
            }

            //Console.WriteLine("FindNode({0},{1},{2},{3}): '{4}' continue on path.", x, y, heading, length, c);

            var left = (heading - 1) >= 0 ? heading - 1 : 3;
            var right = (heading + 1) < 4 ? heading + 1 : 0;

            var pl = new Position() { X = p.X + Headings[left].X,    Y = p.Y + Headings[left].Y };
            var ph = new Position() { X = p.X + Headings[heading].X, Y = p.Y + Headings[heading].Y };
            var pr = new Position() { X = p.X + Headings[right].X,   Y = p.Y + Headings[right].Y };

            if (IsPath(pr))
            {
                return FindNode(pr, right, length + 1);
            }
            else if (IsPath(ph))
            {
                return FindNode(ph, heading, length + 1);
            }
            else if (IsPath(pl))
            {
                return FindNode(pl, left, length + 1);
            }

            // deadend
            DrawChar(x, y, c, ConsoleColor.Cyan);

            return null;
            //return new FindNodeResult() { node = GetNode(p), length= length };
        }

        public Node GetNode(Position p)
        {
            if (NodesPerPosition.ContainsKey(p))
            {
                //return NodesPerPosition[p];
                throw new Exception("Node already listed");
            }
            var node = new Node() { id = Map[p.Y][p.X], pos = p, connections = new Dictionary<Node, int>() };

            //Console.WriteLine("GetNode(): Creating node with id '{0}' and position {1},{2}", node.id, node.pos.X, node.pos.Y);
            NodesPerPosition[p] = node;
            if ((DoorChars.Contains("" + node.id)) || (KeyChars.Contains("" + node.id)))
                NodesPerChar[node.id] = node;

            return node;
        }

        public bool IsKnown(Position p)
        {
            return NodesPerPosition.ContainsKey(p);
        }

        public bool IsIntersection(Position p)
        {
            int count = 0;
            for (int i = 0; i < 4; i++)
            {
                if (Map[p.Y + Headings[i].Y][p.X + Headings[i].X] != '#') count++;
            }
            return (count > 2);
        }

        public String DoorChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public bool IsDoor(Position p)
        {
            return DoorChars.Contains("" + Map[p.Y][p.X]);
        }

        public String KeyChars = "abcdefghijklmnopqrstuvwxyz";
        public bool IsKey(Position p)
        {
            return KeyChars.Contains("" + Map[p.Y][p.X]);
        }

        public bool IsPath(Position p)
        {
            if (Map[p.Y][p.X] == '.') return true;
            if (IsDoor(p)) return true;
            if (IsKey(p)) return true;

            return false;
        }

        public void FindReachableKeysAndDoors(String prefix, Node start, List<Node> visited, List<Node> keys, List<Node> doors)
        {
            visited.Add(start);

            foreach (var node in start.connections.Keys)
            {
                if (!visited.Contains(node))
                {
                    if (DoorChars.Contains("" + node.id))
                    {
                        //Console.WriteLine("Door '{0}' with distance {1}", node.id, start.connections[node]);
                        visited.Add(node);

                        doors.Add(node);
                    }
                    else
                    {
                        if (KeyChars.Contains("" + node.id))
                        {
                            //Console.WriteLine("Key  '{0}' with distance {1}", node.id, start.connections[node]);
                            keys.Add(node);
                        }

                        FindReachableKeysAndDoors(prefix, node, visited, keys, doors);
                    }
                }
            }
        }

        public int FindShortestPath(Node from, Node to, List<Node> visited)
        {
            if (visited.Contains(from)) return -1;
            visited.Add(from);

            if (from == to) return 0;

            var results = new List<int>();
            foreach (var node in from.connections.Keys)
            {
                var result = FindShortestPath(node, to, visited);
                if (result >= 0) results.Add(result+from.connections[node]);
            }

            results.Sort();
            if (results.Count > 0) return results[0];

            return -2;
        }

        protected override void Part2()
        {
        }
    }
*/
}
