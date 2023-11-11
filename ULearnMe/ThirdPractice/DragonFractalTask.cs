using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using System;


namespace Fractals
{
	internal static class DragonFractalTask
	{
		public static void DrawDragonFractal(Pixels pixels, int iterationsCount, int seed)
		{
			var random = new Random(seed);
			var x  = 1.0;
			var y = 0.0;
			DrawFractal(random, x, y, iterationsCount, pixels);
		}

        private static void DrawFractal(Random random, double x, double y, int iterationsCount, Pixels pixels)
        {
			var x1 = x;
			var y1 = y;
			for (; iterationsCount > 0; iterationsCount--)
			{
				var nextNumber = random.Next(2);
				if (nextNumber == 0)
				{
					x1 = (x * Math.Cos(45 * Math.PI / 180) - y * Math.Sin(45 * Math.PI / 180)) / Math.Sqrt(2);
					y1 = (x * Math.Sin(45 * Math.PI / 180) + y * Math.Cos(45 * Math.PI / 180)) / Math.Sqrt(2);
				}
				else
				{
					x1 = (x * Math.Cos(135 * Math.PI / 180) - y * Math.Sin(135 * Math.PI / 180)) / Math.Sqrt(2) + 1;
					y1 = (x * Math.Sin(135 * Math.PI / 180) + y * Math.Cos(135 * Math.PI / 180)) / Math.Sqrt(2);
				}
				x = x1;
				y = y1;
				pixels.SetPixel(x1, y1);
			}
		}
    }
}