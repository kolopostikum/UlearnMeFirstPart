using System;
using System.Drawing;
using NUnit.Framework;
using static System.Math;

namespace Manipulation
{
    public static class AnglesToCoordinatesTask
    {
        /// <summary>
        /// По значению углов суставов возвращает массив координат суставов
        /// в порядке new []{elbow, wrist, palmEnd}
        /// </summary>
        public static PointF[] GetJointPositions(double shoulder, double elbow, double wrist)
        {
            var elbowPos = CountPos(Manipulator.UpperArm, PI, shoulder, 0.0, 0.0);

            var wristPos = CountPos(Manipulator.Forearm, shoulder, elbow, elbowPos.X, elbowPos.Y);
            
            var angle2 = elbow + shoulder - PI;

            var palmEndPos = CountPos(Manipulator.Palm, angle2, wrist, wristPos.X, wristPos.Y);

            return new PointF[]
            {
                elbowPos,
                wristPos,
                palmEndPos
            };
        }

        private static PointF CountPos(float Arm, double firstAng, double secondAng, double x, double y)
        {
            var angle = firstAng + secondAng - PI;
            var PosX = Arm * (float)Cos(angle) + (float)x;
            var PosY = Arm * (float)Sin(angle) + (float)y;
            return new PointF(PosX, PosY);
        }
    }

    [TestFixture]
    public class AnglesToCoordinatesTask_Tests
    {
        // Доработайте эти тесты!
        // С помощью строчки TestCase можно добавлять новые тестовые данные.
        // Аргументы TestCase превратятся в аргументы метода.
        [TestCase(Math.PI / 2, Math.PI / 2, Math.PI, Manipulator.Forearm + Manipulator.Palm, Manipulator.UpperArm)]
        [TestCase(0, 0, 0, - Manipulator.Forearm + Manipulator.Palm + Manipulator.UpperArm, 0)]
        [TestCase(PI / 2, PI / 2, PI / 2, Manipulator.Forearm, Manipulator.UpperArm - Manipulator.Palm)]
        [TestCase(PI, PI, PI, -(Manipulator.Forearm + Manipulator.Palm + Manipulator.UpperArm), 0)]

        public void TestGetJointPositions(double shoulder, double elbow, double wrist, double palmEndX, double palmEndY)
        {
            var joints = AnglesToCoordinatesTask.GetJointPositions(shoulder, elbow, wrist);
            Assert.AreEqual(palmEndX, joints[2].X, 1e-5, "palm endX");
            Assert.AreEqual(palmEndY, joints[2].Y, 1e-5, "palm endY");
        }
    }
}