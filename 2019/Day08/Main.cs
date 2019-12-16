using System;
using System.Collections.Generic;
using System.IO;

namespace AoC2019.Day8
{
    public class Main: Base
    {
        protected override void Part1()
        {
            var layers = Input.GetLayers(Input.ImageData);
            var layer = FindLayerWithFewestZeros(layers);

            var onecount = CountCharsInLayer(layers[layer], '1');
            var twocount = CountCharsInLayer(layers[layer], '2');
            Console.WriteLine("Result: {0}", onecount * twocount);
        }

        public int FindLayerWithFewestZeros(List<char[,]> layers)
        {
            int foundlayer = -1;
            int mincount = layers[0].GetLength(0) * layers[0].GetLength(1) + 1;
            int currentlayer = 0;

            foreach (var layer in layers)
            {
                var count = CountCharsInLayer(layer, '0');
                if (count < mincount)
                {
                    mincount = count;
                    foundlayer = currentlayer;
                }
                currentlayer++;
            }

            return foundlayer;
        }

        public int CountCharsInLayer(char[,] layer, char c)
        {
            var count = 0;
            for (var w = 0; w < layer.GetLength(0); w++)
            {
                for (int h = 0; h < layer.GetLength(1); h++)
                {
                    if (layer[w, h] == c) count += 1;
                }
            }
            return count;
        }

        protected override void Part2()
        {
            var layers = Input.GetLayers(Input.ImageData);
            var picture = CalculateCombinedImage(layers);
            ShowLayer(picture);
        }

        private char[,] CalculateCombinedImage(List<char[,]> layers)
        {
            int width = layers[0].GetLength(0);
            int height = layers[0].GetLength(1);
            char[,] result = GetEmptyLayer(width,height);

            foreach (var layer in layers)
            {
                for (int w = 0; w < width; w++)
                {
                    for (int h = 0; h < height; h++)
                    {
                        if (result[w, h] == '2')
                            result[w, h] = layer[w, h];
                    }
                }
            }

            return result;
        }

        private char[,] GetEmptyLayer(int width, int height)
        {
            char[,] result = new char[width, height];
            for (int w = 0; w < width; w++)
                for (int h = 0; h < height; h++)
                    result[w, h] = '2';
            return result;
        }

        public void ShowLayer(char[,] layer)
        {
            for (int h = 0; h < layer.GetLength(1); h++)
            {
                for (int w = 0; w < layer.GetLength(0); w++)
                {
                    var c = layer[w, h];
                    Console.Write(c == '0' ? ' ' : c);
                }
                Console.WriteLine();
            }
        }
    }
}
