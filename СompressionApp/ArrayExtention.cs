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
                if (double.Parse(item.ToString() ?? "0") != 0)
                    arrayString += $"{item}, ";
            }

            arrayString = arrayString.Substring(0, arrayString.Length - 2);
            Console.WriteLine(arrayString);
        }
    }
}