using System;
using System.Collections.Generic;
using System.Numerics;
using System.IO;
using System.Text;
using System.Threading;

namespace AoC2019.Day16
{
    public class Main : Base
    {
        private int[] _pattern = { 0, 1, 0, -1 };

        protected override void Part1()
        {
            char[] buf = new char[8];
            StringBuilder input = new StringBuilder(Input.Data);
            StringBuilder output = null;
            for (int i = 0; i < 100; i++)
            {
                output = CalculatePhase(input);
                input = output;
                //Console.WriteLine("Result {0}: {1}", i,output);
            }
            Console.WriteLine("Result: {0}", output.ToString().Substring(0,8));
        }

        public StringBuilder CalculatePhase(StringBuilder input)
        {
            StringBuilder output = new StringBuilder("");

            for (var i = 0; i < input.Length; i++)
            {
                output.Append(String.Format("{0}",CalculateOutputValue(input, i)));
            }

            return output;
        }

        private int GetInputValue(StringBuilder input, int i)
        {
            return int.Parse("" + input[i]);
        }

        private int CalculateOutputValue(StringBuilder input, int i)
        {
            int output = 0;
            //Console.Write("CalculateOutputValue(): ");
            for (var j = 0; j < input.Length; j++)
            {
                var m = DetermineMultiplier(input, i, j);
                var v = GetInputValue(input, j);
                var r = m * v;
                //Console.Write("{0} * {1}  ", v, m);
                output += r;
            }
            output = Math.Abs(output%10);
            //Console.WriteLine("= {0}", output);
            return output;
        }

        private int DetermineMultiplier(StringBuilder input, int i, int j)
        {
            int pos = (j + 1) % (_pattern.Length * (i + 1));
            var p2 = pos % (4*(i + 1));
            var p3 = p2 / (i + 1);
            return _pattern[p3];
        }

        protected override void Part2()
        {
            // index (N): 5976963 (length = 6500000, so index is close to the end)

            // Matrix we are multiplying by is upper triangular so we can ignore the any input before N. 
            // Now, the submatrix we are left with is just an upper unitriangular matrix where all upper triangular part is one (because we never reach the third 0 in the "base pattern"). 
            // Therefore, the linear operation induced by the submatrix is just adding from ith term to the end, to get ith new term. This can be run in linear time.

            // If the index from which you have read your message is larger than half the length of the entire list, you can just sum numbers from the end of the list to update the list every time.

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < 10000; i++)
                builder.Append(Input.Data);

            String input = builder.ToString();
            int length = input.Length;
            int offset = int.Parse(input.Substring(0, 7));

            int[] values = new int[length];
            for (int i = 0; i < length; i++)
            {
                values[i] = input[i] - 48;
            }

            for (int phase = 0; phase < 100; phase++)
            {
                for (int repeat = length - 2; repeat > offset - 5; repeat--)
                {
                    int number = values[repeat + 1] + values[repeat];
                    values[repeat] = Math.Abs(number % 10);
                }
            }

            String s = "";
            for (int i = offset; i < offset + 8; i++)
            {
                s += values[i];
            }

            Console.WriteLine("Result: {0}", s);
        }
    }
}
