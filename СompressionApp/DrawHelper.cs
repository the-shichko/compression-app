using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Linq;

namespace СompressionApp
{
    public class DrawHelper
    {
        public static Bitmap Draw<T>(T[] arrayX, T[] arrayY, string name) where T : struct
        {
            var maxX = arrayX.Select(x => (int)Math.Abs(float.Parse(x.ToString() ?? "0"))).Max();
            var maxY = arrayY.Select(x => (int)Math.Abs(float.Parse(x.ToString() ?? "0"))).Max();
            var bitmap = new Bitmap((maxX + 1) * 30, (maxY + 1) * 30);

            using var graph = Graphics.FromImage(bitmap);
            graph.SmoothingMode = SmoothingMode.AntiAlias;
            graph.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            graph.InterpolationMode = InterpolationMode.High;

            var height = (maxY + 1) * 30;
            var width = (maxX + 1) * 30;
            var imageSize = new Rectangle(0, 0, (maxX + 1) * 30, (maxY + 1) * 30);
            graph.FillRectangle(Brushes.Azure, imageSize);

            for (var i = 0; i < arrayX.Length - 1; i++)
            {
                graph.DrawLine(new Pen(Brushes.Black), 
                    (float.Parse(arrayX[i].ToString() ?? "0")  * 10 + width / 2),
                    (float.Parse(arrayY[i].ToString() ?? "0") * -1  * 10 + height / 2),
                    (float.Parse(arrayX[i + 1].ToString() ?? "0")  * 10 + width / 2),
                    (float.Parse(arrayY[i + 1].ToString() ?? "0") * -1  * 10 + height / 2));
            }

            bitmap.Save($"{Directory.GetCurrentDirectory()}\\{name}.png");
            return bitmap;
        }
    }
}