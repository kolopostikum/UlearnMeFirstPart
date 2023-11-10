using System;

namespace DistanceTask
{
	public static class DistanceTask
	{
		// Расстояние от точки (x, y) до отрезка AB с координатами A(ax, ay), B(bx, by)
		public static double GetDistanceToSegment(double ax, double ay, double bx, double by, double x, double y)
		{
			if ((ax == x) && (ay == y)) return 0;
			if ((bx == x) && (by == y)) return 0;
            
			var distanceA = CountDistanceA(ax, ay, x, y);
			var distanceB = CountDistanceA(bx, by, x, y);
			var distanceC = CountDistanceA(ax, ay, bx, by);
			if ((ax == bx) && (ay == by)) return distanceA;
			var distanceLine = CountPerpendicular(distanceA, CountCorner(distanceB, distanceA, distanceC));
			if ((CountCorner(distanceA, distanceB, distanceC) * 180 / Math.PI > 90)
				|| (CountCorner(distanceB, distanceA, distanceC) * 180 / Math.PI > 90)) 
            {
				return Math.Min(distanceA, distanceB);
			}
			return distanceLine;
		}

		public static double CountCorner(double a, double b, double c)
		{
			var distance = (b*b + c*c - a*a)/(2*b*c);
			distance = Math.Acos(distance);
			return distance;
		}

		public static double CountDistanceA(double ax, double ay, double x, double y)
        {
			return Math.Sqrt((ax - x) * (ax - x) + (ay - y) * (ay - y)); 
		}

		public static double CountPerpendicular(double distanceA, double cornerA)
        {
			return distanceA * Math.Sin(cornerA);
        }
	}
}