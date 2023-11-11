using System.Linq;
using System;

namespace Recognizer
{
    internal static class MedianFilterTask
    {
        /* 
		 * Для борьбы с пиксельным шумом, подобным тому, что на изображении,
		 * обычно применяют медианный фильтр, в котором цвет каждого пикселя, 
		 * заменяется на медиану всех цветов в некоторой окрестности пикселя.
		 * https://en.wikipedia.org/wiki/Median_filter
		 * 
		 * Используйте окно размером 3х3 для не граничных пикселей,
		 * Окно размером 2х2 для угловых и 3х2 или 2х3 для граничных.
		 */
        public static double[,] MedianFilter(double[,] original)
        {
            var result = new double[original.GetLength(0), original.GetLength(1)];
            result = GetMedianCentral(original, result);

            if ((original.GetLength(0) > 1) && (original.GetLength(1) > 1))
            {
                result = GetMedianAngl(original, result);
                result = GetMedianEdge(original, result);
            }
            else
            {
                result = GetMedianAnglLine(original, result);
                result = GetMedianCentralLine(original, result);
            }

            return result;
        }

        private static double[,] GetMedianCentralLine(double[,] original, double[,] result)
        {
            var x = original.GetLength(0);
            var y = original.GetLength(1);
            var centralArr = new double[3];

            if ((x == 1) && (y == 1))
                return original;
            if ((x == 1) && (y > 2))
            {
                for (int i = 1; i < y - 1; i++)
                {
                    centralArr[0] = original[0, i - 1];
                    centralArr[1] = original[0, i];
                    centralArr[2] = original[0, i + 1];

                    Array.Sort(centralArr);

                    result[0, i] = centralArr[1];
                }
                return result;
            }
            if ((y == 1) && (x > 2))
            {
                for (int i = 1; i < x - 1; i++)
                {
                    centralArr[0] = original[i - 1, 0];
                    centralArr[1] = original[i, 0];
                    centralArr[2] = original[i + 1, 0];

                    Array.Sort(centralArr);

                    result[i, 0] = centralArr[1];
                }
                return result;
            }
            return result;
        }

        private static double[,] GetMedianAnglLine(double[,] original, double[,] result)
        {
            var x = original.GetLength(0);
            var y = original.GetLength(1);

            if ((x == 1) && (y == 1))
                return original;
            if (x == 1)
            {
                var anglArr = new double[] { original[0, 0], original[0, 1] };
                result[0, 0] = (anglArr[0] + anglArr[1]) / 2;
                anglArr = new double[] { original[0, y - 1], original[0, y - 2] };
                result[0, y - 1] = (anglArr[0] + anglArr[1]) / 2;
                return result;
            }
            if (y == 1)
            {
                var anglArr = new double[] { original[0, 0], original[1, 0] };
                result[0, 0] = (anglArr[0] + anglArr[1]) / 2;
                anglArr = new double[] { original[x - 1, 0], original[x - 2, 0] };
                result[x - 1, 0] = (anglArr[0] + anglArr[1]) / 2;
                return result;
            }
            return result;
        }

        private static double[,] GetMedianEdge(double[,] original, double[,] result)
        {
            if ((original.GetLength(1) > 2) && (original.GetLength(0) > 1))
            {
                result = GetLeftSide(original, result);

                result = GetRigthSide(original, result);
            }

            if ((original.GetLength(0) > 2) && (original.GetLength(1) > 1))
            {
                result = GetLowerSide(original, result);

                result = GetUpperSide(original, result);
            }

            return result;
        }

        private static double[,] GetLowerSide(double[,] original, double[,] result)
        {
            var centralArr = new double[6];
            var x = original.GetLength(0);

            for (int i = 1; i < x - 1; i++)
            {
                centralArr[0] = original[i - 1, 0];
                centralArr[1] = original[i, 0];
                centralArr[2] = original[i + 1, 0];

                centralArr[3] = original[i - 1, 1];
                centralArr[4] = original[i, 1];
                centralArr[5] = original[i + 1, 1];

                Array.Sort(centralArr);

                result[i, 0] = (centralArr[2] + centralArr[3]) / 2;
            }
            return result;
        }

        private static double[,] GetUpperSide(double[,] original, double[,] result)
        {
            var centralArr = new double[6];

            var x = original.GetLength(0);
            var y = original.GetLength(1);

            for (int i = 1; i < x - 1; i++)
            {
                centralArr[0] = original[i - 1, y - 1];
                centralArr[1] = original[i, y - 1];
                centralArr[2] = original[i + 1, y - 1];

                centralArr[3] = original[i - 1, y - 2];
                centralArr[4] = original[i, y - 2];
                centralArr[5] = original[i + 1, y - 2];

                Array.Sort(centralArr);

                result[i, y - 1] = (centralArr[2] + centralArr[3]) / 2;
            }
            return result;
        }

        private static double[,] GetRigthSide(double[,] original, double[,] result)
        {
            var centralArr = new double[6];

            var x = original.GetLength(0);
            var y = original.GetLength(1);

            for (int i = 1; i < y - 1; i++)
            {
                centralArr[0] = original[x - 1, i - 1];
                centralArr[1] = original[x - 1, i];
                centralArr[2] = original[x - 1, i + 1];

                centralArr[3] = original[x - 2, i - 1];
                centralArr[4] = original[x - 2, i];
                centralArr[5] = original[x - 2, i + 1];

                Array.Sort(centralArr);

                result[x - 1, i] = (centralArr[2] + centralArr[3]) / 2;
            }
            return result;
        }

        private static double[,] GetLeftSide(double[,] original, double[,] result)
        {
            var centralArr = new double[6];

            var x = original.GetLength(0);
            var y = original.GetLength(1);

            for (int i = 1; i < y - 1; i++)
            {
                centralArr[0] = original[0, i - 1];
                centralArr[1] = original[0, i];
                centralArr[2] = original[0, i + 1];

                centralArr[3] = original[1, i - 1];
                centralArr[4] = original[1, i];
                centralArr[5] = original[1, i + 1];

                Array.Sort(centralArr);

                result[0, i] = (centralArr[2] + centralArr[3]) / 2;
            }
            return result;
        }

        private static double[,] GetMedianAngl(double[,] original, double[,] result)
        {
            var x = original.GetLength(0);
            var y = original.GetLength(1);

            var anglArr = new double[] { original[0, 0], original[0, 1], original[1, 0], original[1, 1] };
            Array.Sort(anglArr);
            result[0, 0] = (anglArr[1] + anglArr[2]) / 2;

            anglArr = new double[] { original[0, y - 1], original[0, y - 2],
                                     original[1, y - 1], original[1, y - 2] };
            Array.Sort(anglArr);
            result[0, y - 1] = (anglArr[1] + anglArr[2]) / 2;

            anglArr = new double[] { original[x - 1, y - 1], original[x - 1, y - 2],
                                     original[x - 2, y - 1], original[x - 2, y - 2] };
            Array.Sort(anglArr);
            result[x - 1, y - 1] = (anglArr[1] + anglArr[2]) / 2;

            anglArr = new double[] { original[x - 1, 0], original[x - 1, 1],
                                     original[x - 2, 0], original[x - 2, 1] };
            Array.Sort(anglArr);
            result[x - 1, 0] = (anglArr[1] + anglArr[2]) / 2;

            return result;
        }

        private static double[,] GetMedianCentral(double[,] original, double[,] result)
        {
            var centralArr = new double[9];

            for (int x = 1; x < original.GetLength(0) - 1; x++)
            {
                for (int y = 1; y < original.GetLength(1) - 1; y++)
                {
                    centralArr[0] = original[x - 1, y - 1];
                    centralArr[1] = original[x - 1, y];
                    centralArr[2] = original[x - 1, y + 1];

                    centralArr[3] = original[x, y - 1];
                    centralArr[4] = original[x, y];
                    centralArr[5] = original[x, y + 1];

                    centralArr[6] = original[x + 1, y - 1];
                    centralArr[7] = original[x + 1, y];
                    centralArr[8] = original[x + 1, y + 1];

                    Array.Sort(centralArr);

                    result[x, y] = centralArr[4];
                }
            }
            return result;
        }
    }
}