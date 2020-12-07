using System;
using System.Collections.Generic;
using System.Linq;

namespace СompressionApp
{
    public static class ArrayExtention
    {
        public static void ConsolePrint(this Array sender)
        {
            var arrayString = string.Empty;
            foreach (var item in sender)
            {
                // if (double.Parse(item.ToString() ?? "0") != 0)
                    arrayString += $"{item}, ";
            }

            arrayString = arrayString.Substring(0, arrayString.Length - 2);
            Console.WriteLine(arrayString);
        }

        public static void ConsolePrint(this double[,] sender)
        {
            for (var i = 0; i < sender.GetLength(0); i++)
            {
                for (var j = 0; j < sender.GetLength(1); j++)
                {
                    Console.Write($"{sender[i, j]}\t");
                }
                Console.Write("\n");
            }
        }
    }
}