using System;

namespace GeometryTasks
{
    public class Vector
    {
        public double X;
        public double Y;
    }

    public class Segment
    {
        public Vector Begin;
        public Vector End;
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

        public static double GetLength(Segment A)
        {
            var x = A.Begin.X - A.End.X;
            var y = A.Begin.Y - A.End.Y;
            var segmentLength = Math.Sqrt(x * x + y * y);

            return segmentLength;
        }

        public static bool IsVectorInSegment(Vector point, Segment A)
        {
            var lengthSegment = GetLength(A);
            
            var beginSegmentPoint = new Segment { Begin = A.Begin , End = point };

            var endSegmentPoint = new Segment { Begin = point, End = A.End };

            var pointSegmentLength = GetLength(beginSegmentPoint) + GetLength(endSegmentPoint);

            return lengthSegment == pointSegmentLength;
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