using System;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace RefactorMe
{
    class Drawer
    {
        static float x, y;
        static Graphics graphic;
        public static void Initialize(Graphics newGraphics)
        {
            graphic = newGraphics;
            graphic.SmoothingMode = SmoothingMode.None;
            graphic.Clear(Color.Black);
        }

        public static void Set_position(float x0, float y0)
        {
            x = x0;
            y = y0;
        }

        public static void MakeIt(Pen pen, double length, double corner)
        {
            //Делает шаг длиной length в направлении corner и рисует пройденную траекторию
            var x1 = (float)(x + length * Math.Cos(corner));
            var y1 = (float)(y + length * Math.Sin(corner));
            graphic.DrawLine(pen, x, y, x1, y1);
            x = x1;
            y = y1;
        }

        public static void Change(double length, double corner)
        {
            x = (float)(x + length * Math.Cos(corner));
            y = (float)(y + length * Math.Sin(corner));
        }
    }

    public class ImpossibleSquare
    {
        public static void Draw(int width, int height, double rotationAngle, Graphics graph)
        {
            // rotationAngle пока не используется, но будет использоваться в будущем
            Drawer.Initialize(graph);

            var size = Math.Min(width, height);

            var diagonalLength = Math.Sqrt(2) * (size * 0.375f + size * 0.04f) / 2;
            var x0 = (float)(diagonalLength * Math.Cos(Math.PI / 4 + Math.PI)) + width / 2f;
            var y0 = (float)(diagonalLength * Math.Sin(Math.PI / 4 + Math.PI)) + height / 2f;

            Drawer.Set_position(x0, y0);

            //Рисуем 1-ую сторону
            FirstSide(size);

            //Рисуем 2-ую сторону
            SecondSide(size);


            //Рисуем 3-ю сторону
            ThirdSide(size);

            //Рисуем 4-ую сторону
            FourthSide(size);
        }

        public static void FirstSide(int size)
        {
            Drawer.MakeIt(Pens.Yellow, size * 0.375f, 0);
            Drawer.MakeIt(Pens.Yellow, size * 0.04f * Math.Sqrt(2), Math.PI / 4);
            Drawer.MakeIt(Pens.Yellow, size * 0.375f, Math.PI);
            Drawer.MakeIt(Pens.Yellow, size * 0.375f - size * 0.04f, Math.PI / 2);

            Drawer.Change(size * 0.04f, -Math.PI);
            Drawer.Change(size * 0.04f * Math.Sqrt(2), 3 * Math.PI / 4);
        }

        public static void SecondSide(int size)
        {
            Drawer.MakeIt(Pens.Yellow, size * 0.375f, -Math.PI / 2);
            Drawer.MakeIt(Pens.Yellow, size * 0.04f * Math.Sqrt(2), -Math.PI / 2 + Math.PI / 4);
            Drawer.MakeIt(Pens.Yellow, size * 0.375f, -Math.PI / 2 + Math.PI);
            Drawer.MakeIt(Pens.Yellow, size * 0.375f - size * 0.04f, -Math.PI / 2 + Math.PI / 2);

            Drawer.Change(size * 0.04f, -Math.PI / 2 - Math.PI);
            Drawer.Change(size * 0.04f * Math.Sqrt(2), -Math.PI / 2 + 3 * Math.PI / 4);
        }

        public static void ThirdSide(int size)
        {
            Drawer.MakeIt(Pens.Yellow, size * 0.375f, Math.PI);
            Drawer.MakeIt(Pens.Yellow, size * 0.04f * Math.Sqrt(2), Math.PI + Math.PI / 4);
            Drawer.MakeIt(Pens.Yellow, size * 0.375f, Math.PI + Math.PI);
            Drawer.MakeIt(Pens.Yellow, size * 0.375f - size * 0.04f, Math.PI + Math.PI / 2);

            Drawer.Change(size * 0.04f, Math.PI - Math.PI);
            Drawer.Change(size * 0.04f * Math.Sqrt(2), Math.PI + 3 * Math.PI / 4);
        }

        public static void FourthSide(int size)
        {
            Drawer.MakeIt(Pens.Yellow, size * 0.375f, Math.PI / 2);
            Drawer.MakeIt(Pens.Yellow, size * 0.04f * Math.Sqrt(2), Math.PI / 2 + Math.PI / 4);
            Drawer.MakeIt(Pens.Yellow, size * 0.375f, Math.PI / 2 + Math.PI);
            Drawer.MakeIt(Pens.Yellow, size * 0.375f - size * 0.04f, Math.PI / 2 + Math.PI / 2);

            Drawer.Change(size * 0.04f, Math.PI / 2 - Math.PI);
            Drawer.Change(size * 0.04f * Math.Sqrt(2), Math.PI / 2 + 3 * Math.PI / 4);
        }
    }
}