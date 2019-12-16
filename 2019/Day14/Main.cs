using System;
using System.Collections.Generic;
using System.Numerics;
using System.IO;
using System.Text;
using System.Threading;

namespace AoC2019.Day14
{
    public struct Record
    {
        public String Name;
        public int Amount;
        public Dictionary<String, int> Inputs;
    }

    public class Main : Base
    {
        public Dictionary<String, Record> Map= new Dictionary<string, Record>();

        public Main()
        {
        }

        protected override void Part1()
        {
            BuildMap(Input.Data);
            //var result = WalkPath(Map["FUEL"]);
            GetSubstance("FUEL", 1);
            Console.WriteLine("Result: {0}", OreUsed);
        }

        private void BuildMap(String data)
        {
            var lines = data.Split('\n');
            foreach (var line in lines)
            {
                //Console.WriteLine("Line: {0}", line);
                var parts = line.Split('=');
                var inputs = parts[0].Split(',');
                var output = parts[1].Substring(2);
                var outputparts = output.Split(' ');

                var result = new Record() { Name = outputparts[1].Trim(), Amount = int.Parse(outputparts[0]), Inputs = new Dictionary<string, int>() };
                foreach (var item in inputs)
                {
                    var inputparts = item.Trim().Split(' ');
                    result.Inputs[inputparts[1]] = int.Parse(inputparts[0]);
                }
                Map[result.Name] = result;
            }
        }

        public BigInteger OreUsed = 0;
        public Dictionary<String, BigInteger> Stock = new Dictionary<string, BigInteger>();

        public void GetSubstance(string substance, BigInteger amount)
        {
            //Console.WriteLine("GetSubstance({0},{1})", substance, amount);
            if (substance == "ORE")
            {
                OreUsed += amount;
                //var x = Console.CursorLeft;
                //var y = Console.CursorTop;
                //Console.SetCursorPosition(0, 25);
                //Console.WriteLine("OreUsed: {0}        ", OreUsed);
                //Console.SetCursorPosition(x, y);
            }
            else
            {
                if (!Stock.ContainsKey(substance)) Stock[substance] = 0;
                //while (Stock[substance] < amount)
                {
                    ProduceSubstance(substance,amount-Stock[substance]);
                }

                Stock[substance] -= amount;
            }
        }

        public void ProduceSubstance(string substance, BigInteger minamount)
        {
            //Console.WriteLine("ProduceSubstance({0},{1})", substance, minamount);
            var amount = Map[substance].Amount;
            var mincount = minamount / amount;
            if (minamount % amount != 0) mincount++;

            var components = Map[substance].Inputs;
            foreach (var component in components.Keys)
            {
                GetSubstance(component, components[component] * mincount);
            }
            Stock[substance] += amount * mincount;
        }

        protected override void Part2()
        {
            BigInteger trillion = 1000000000000;

            BuildMap(Input.Data);
            BigInteger min = trillion / 378929;
            BigInteger value = min;
            BigInteger delta = min;
            while (CalculateOreUsedForFuel(value+1) < trillion)
            {
                if (CalculateOreUsedForFuel(value + delta) > trillion)
                {
                    delta = delta / 2;
                    //if (delta == 0) delta = 1;
                }
                else
                {
                    value += delta;
                }
            }

            Console.WriteLine("Result: {0}", value);
        }

        private BigInteger CalculateOreUsedForFuel(BigInteger fuel)
        {
            ClearStock();
            GetSubstance("FUEL", fuel);
            //Console.WriteLine("CalculateOreUsedForFuel({0}): {1}", fuel, OreUsed);
            return OreUsed;
        }

        private void ClearStock()
        {
            OreUsed = 0;
            Stock = new Dictionary<string, BigInteger>();
        }
    }
}
