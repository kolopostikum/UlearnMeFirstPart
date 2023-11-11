using System;
using NUnit.Framework;

namespace Manipulation
{
    public class TriangleTask
    {
        /// <summary>
        /// Возвращает угол (в радианах) между сторонами a и b в треугольнике со сторонами a, b, 
        /// </summary>
        public static double GetABAngle(double a, double b, double c)
        {
            if ((a + b >= c) && (a + c >= b) && (b + c >= a))
            {
                double cosAB = (a * a + b * b - c * c) / (2 * a * b);
                return Math.Acos(cosAB);
            }
            else
                return double.NaN;
        }
    }

    [TestFixture]
    public class TriangleTask_Tests
    {
        [TestCase(3, 4, 5, Math.PI / 2)]
        [TestCase(1, 1, 1, Math.PI / 3)]
        [TestCase(0, 1, 1, double.NaN)]
        [TestCase(3, 4, 5, 1.5707963267949d)]


        // добавьте ещё тестовых случаев!
        public void TestGetABAngle(double a, double b, double c, double expectedAngle)
        {
            Assert.AreEqual(expectedAngle, TriangleTask.GetABAngle(a, b, c), 1e-5);
        }
    }
}