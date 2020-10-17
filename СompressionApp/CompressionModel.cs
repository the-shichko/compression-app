using System;

namespace СompressionApp
{
    public static class CompressionModel
    {
        private static readonly int[] ArrayX = {1, 1, 1, 2, 3, 4, 5, 6, 7, 7, 7, 6, 5, 4, 3, 2};
        private static readonly int[] ArrayY = {2, 3, 4, 5, 6, 7, 7, 6, 5, 4, 3, 2, 1, 1, 1, 1};

        private const int Count = 16;

        private static readonly double[] ResultX = new double[Count];
        private static readonly double[] UnclenchingX = new double[Count];
        
        private static readonly double[] ResultY = new double[Count];
        private static readonly double[] UnclenchingY = new double[Count];


        public static void Start()
        {
            Console.WriteLine("Array X:");
            ArrayX.ConsolePrint();
            
            Console.WriteLine("Array Y:");
            ArrayY.ConsolePrint();

            DrawHelper.Draw(ArrayX, ArrayY, "get");
            CompressionArray();
            
            Console.WriteLine("Result X:");
            ResultX.ConsolePrint();
            
            Console.WriteLine("Result Y:");
            ResultY.ConsolePrint();
            
            DrawHelper.Draw(ResultX, ResultY, "result");

            UnclenchingArray();
            
            Console.WriteLine("Back to X:");
            UnclenchingX.ConsolePrint();
            
            Console.WriteLine("Back to Y:");
            UnclenchingY.ConsolePrint();
        }

        private static void CompressionArray()
        {
            for (var v = 0; v < Count; v++)
            {
                ResultX[v] = CompressionSumCas("x", v);
                ResultY[v] = CompressionSumCas("y", v);
            }
        }
        
        private static void UnclenchingArray()
        {
            for (var v = 0; v < Count; v++)
            {
                UnclenchingX[v] = UnclenchingSumCas("x", ResultX, v);
                UnclenchingY[v] = UnclenchingSumCas("y", ResultY, v);
            }
        }

        private static double CompressionSumCas(string type, int v)
        {
            double sum = 0;
            for (var n = 0; n < Count; n++)
            {
                sum += (type == "x" ? ArrayX[n] : ArrayY[n]) * Cas(v, n);
            }

            return Math.Round(sum, 2);
        }
        
        private static double UnclenchingSumCas(string type, double[] compArray, int v)
        {
            double sum = 0;
            for (var n = 0; n < Count; n++)
            {
                sum += compArray[n] * Cas(v, n);
            }

            return  Math.Round((double)1 / Count * sum, 2);
        }

        private static double Cas(int v, int n)
        {
            return Math.Cos(2 * Math.PI * v * n / Count) + Math.Sin(2 * Math.PI * v * n / Count);
        }
    }
}