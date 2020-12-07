using System;
using System.Collections.Generic;

namespace СompressionApp
{
    public static class CompressionModel
    {
        private static readonly int[] ArrayX = {2, 4, 5, 4, 4, 6, 9, 9, 12, 14, 16, 15, 12, 14, 12, 10, 9, 7, 5, 2};
        private static readonly int[] ArrayY = {1, 3, 5, 8, 11, 14, 14, 10, 11, 9, 9, 7, 6, 4, 2, 4, 7, 3, 1, 1};

        private const int Count = 20;
        private const int M = 16;

        private static readonly double[] ResultX = new double[Count];
        private static readonly double[] UnclenchingX = new double[Count];
        private static readonly double[] UnclenchingRoundX = new double[Count];

        private static readonly double[] ResultY = new double[Count];
        private static readonly double[] UnclenchingY = new double[Count];
        private static readonly double[] UnclenchingRoundY = new double[Count];
        private static readonly double[,] ArrayH = new double[Count, Count];

        public static void Start()
        {
            Console.WriteLine($"M (filter) = {M}");
            Console.WriteLine("Array X:");
            ArrayX.ConsolePrint();
            
            Console.WriteLine("Array Y:");
            ArrayY.ConsolePrint();
            
            CompressionArray();
            
            Console.WriteLine("Result X:");
            ResultX.ConsolePrint();
            
            Console.WriteLine("Result Y:");
            ResultY.ConsolePrint();
            
            Console.WriteLine("Array H:");
            ArrayH.ConsolePrint();

            UnclenchingArray();

            Console.WriteLine("---------\n" +
                              "Back to X:");
            UnclenchingX.ConsolePrint();
            
            Console.WriteLine("Back to Y:");
            UnclenchingY.ConsolePrint();

            Console.WriteLine("---------\n" +
                              "Back to X (round):");
            UnclenchingRoundX.ConsolePrint();

            Console.WriteLine("Back to Y (round):");
            UnclenchingRoundY.ConsolePrint();

            Console.WriteLine($"Error rate y (M = {M}): {GetErrorRate("f")}");
        }

        private static void CompressionArray()
        {
            for (var v = 0; v < Count; v++)
            {
                ResultX[v] = CompressionSumCas("x", v);
                ResultY[v] = CompressionSumCas("y", v);
            }

            GetResultByFilter(ResultX);
            GetResultByFilter(ResultY);
        }

        private static void UnclenchingArray()
        {
            for (var v = 0; v < Count; v++)
            {
                var x = UnclenchingSumCas("x", ResultX, v);
                UnclenchingX[v] = Math.Round(x, 3);
                UnclenchingRoundX[v] = Math.Round(x);

                var y = UnclenchingSumCas("y", ResultY, v);
                UnclenchingY[v] = Math.Round(y, 3);
                UnclenchingRoundY[v] = Math.Round(y);
            }
        }

        private static double CompressionSumCas(string type, int v)
        {
            double sum = 0;
            for (var n = 0; n < Count; n++)
            {
                var cas = Cas(v, n);
                sum += (type == "x" ? ArrayX[n] : ArrayY[n]) * cas;
                ArrayH[v, n] = Math.Round(cas, 2);
            }

            return Math.Round(sum, 3);
        }

        private static double UnclenchingSumCas(string type, double[] compArray, int v)
        {
            double sum = 0;
            for (var n = 0; n < Count; n++)
            {
                sum += compArray[n] * Cas(v, n);
            }

            return (double) 1 / Count * sum;
        }

        private static double Cas(int v, int n)
        {
            return Math.Cos(2 * Math.PI * v * n / Count) + Math.Sin(2 * Math.PI * v * n / Count);
        }

        private static void GetResultByFilter(IList<double> array)
        {
            var indexes = new List<int>();

            for (var i = 0; i < M; i++)
            {
                var index = -1;
                var max = double.MinValue;
                for (var j = 0; j < Count; j++)
                {
                    if (!(max < Math.Abs(array[j])) || indexes.Contains(j)) continue;

                    max = Math.Abs(array[j]);
                    index = j;
                }

                indexes.Add(index);
            }

            for (var i = 0; i < array.Count; i++)
            {
                if (!indexes.Contains(i))
                    array[i] = 0;
            }
        }

        private static double GetErrorRate(string type)
        {
            var q = 0.0;
            for (var i = 0; i < Count; i++)
            {
                q += Math.Pow(UnclenchingRoundX[i] - ArrayX[i], 2) + Math.Pow(UnclenchingRoundY[i] - ArrayY[i], 2);
            }

            return Math.Sqrt(q / (2 * Count));
        }
    }
}