using System;

namespace Recognizer
{
	public static class ThresholdFilterTask
	{
		public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
		{
			var limit = original.Length * whitePixelsFraction;
			var result = new double[original.GetLength(0), original.GetLength(1)];
			var subArray = new double[original.GetLength(0) * original.GetLength(1)];
			int count = 0;
			
            foreach (var pixel in original)
            {
				subArray[count] = pixel;
				count++;
            }

			Array.Sort(subArray);
			var T = 0.0;
			
			if ((limit >= 0)&&(limit < 1))
				T = subArray[subArray.Length -1] + 1;
			else
			if (subArray.Length - (int)(limit) >= 1)
				T = subArray[subArray.Length - (int)(limit)];
			
			for (int i = 0; i < original.GetLength(0); i++)
            {
                for (int j = 0; j < original.GetLength(1); j++)
                {
                    if (original[i,j] < T)
						result[i, j] = 0.0;
					else
						result[i, j] = 1.0;
				}
			}

			return result;
		}
	}
}