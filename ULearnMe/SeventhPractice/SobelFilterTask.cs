using System;

namespace Recognizer
{
    public static class SobelFilterTask
    {
        public static double[,] SobelFilter(double[,] g, double[,] sx)
        {
            var width = g.GetLength(0);
            var height = g.GetLength(1);
            var frame = (int)(sx.GetLength(0) / 2);
            var result = new double[width, height];
            if ((width < sx.GetLength(0)) || (height < sx.GetLength(1)))
            {
                return new double[,] {{ 0.0 }};
            }
            for (int x = frame; x < width - frame; x++)
                for (int y = frame; y < height - frame; y++)
                {
                    double gradSX = ConvolutionX(x, y, g, sx, frame);

                    double gradSY = ConvolutionY(x, y, g, sx, frame);

                    result[x, y] = Math.Sqrt(gradSX * gradSX + gradSY * gradSY);
                }
            return result;
        }

        private static double ConvolutionY(int x, int y, double[,] g, double[,] sx, int frame)
        {
            var result = 0.0;
            var side = sx.GetLength(0);

            for (int i = 0; i < side; i++)
            {
                for (int j = 0; j < side; j++)
                {
                    result += sx[i, j]
                        * g[x - frame + i, y - frame + j];
                }
            }

            return result;
        }

        private static double ConvolutionX(int x, int y, double[,] g, double[,] sx, int frame)
        {
            var result = 0.0;
            var side = sx.GetLength(0);

            for (int i = 0; i < side; i++)
            {
                for (int j = 0; j < side; j++)
                {
                    result += sx[j, i]
                        * g[x - frame + i, y - frame + j];
                }
            }

            return result;
        }
    }
}