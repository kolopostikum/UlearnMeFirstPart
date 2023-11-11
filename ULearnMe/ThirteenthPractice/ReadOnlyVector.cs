using System;

namespace ReadOnlyVectorTask
{
    public class ReadOnlyVector
    {
        public readonly double X;
        public readonly double Y;

        public ReadOnlyVector(double x, double y)
        {
            X = x;
            Y = y;
        }

        public ReadOnlyVector Add(ReadOnlyVector other)
        {
            double summX = other.X + this.X;
            double summY = other.Y + this.Y;
            return new ReadOnlyVector(summX, summY);
        }

        public ReadOnlyVector WithX(double newValue)
        {
            return new ReadOnlyVector(newValue, this.Y);
        }

        public ReadOnlyVector WithY(double newValue)
        {
            return new ReadOnlyVector(this.X, newValue);
        }
    }
}