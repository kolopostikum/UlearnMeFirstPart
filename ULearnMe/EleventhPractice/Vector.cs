using System;

namespace GeometryTasks
{
    public class Vector
    {
        public double X;
        public double Y;
    }

    public class Geometry
    {
        public static double GetLength(Vector A)
        {
            var x = A.X;
            var y = A.Y;
            var vectorLength = Math.Sqrt( x * x + y * y );

            return vectorLength;
        }

        public static Vector Add(Vector A, Vector B)
        {
            var summX = A.X + B.X;
            var summY = A.Y + B.Y;
            var resultVector = new Vector { X = summX, Y = summY};
            
            return resultVector;
        }
    }
}
