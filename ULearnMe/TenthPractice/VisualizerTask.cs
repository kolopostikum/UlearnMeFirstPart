using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Manipulation
{
	public static class VisualizerTask
	{
		public static double X = 220;
		public static double Y = -100;
		public static double Alpha = 0.05;
		public static double Wrist = 2 * Math.PI / 3;
		public static double Elbow = 3 * Math.PI / 4;
		public static double Shoulder = Math.PI / 2;

		public static Brush UnreachableAreaBrush = new SolidBrush(Color.FromArgb(255, 255, 230, 230));
		public static Brush ReachableAreaBrush = new SolidBrush(Color.FromArgb(255, 230, 255, 230));
		public static Pen ManipulatorPen = new Pen(Color.Black, 3);
		public static Brush JointBrush = Brushes.Gray;

		public static void KeyDown(Form form, KeyEventArgs key)
		{
            switch (key.KeyCode)
            {
                case Keys.Q:
					Shoulder += 0.1;
					Wrist = -Alpha - Shoulder - Elbow;
					break;
				case Keys.A:
					Shoulder -= 0.1;
					Wrist = -Alpha - Shoulder - Elbow;
					break;
				case Keys.W:
					Elbow += 0.1;
					Wrist = -Alpha - Shoulder - Elbow;
					break;
				case Keys.S:
					Elbow -= 0.1;
					Wrist = -Alpha - Shoulder - Elbow;
					break;
				default:
					break;
            }
			form.Invalidate(); // 
		}


		public static void MouseMove(Form form, MouseEventArgs e)
		{
			// TODO: Измените X и Y пересчитав координаты (e.X, e.Y) в логические.
			var logicE = GetShoulderPos(form);

			var PosX = (float)e.X;
			var PosY = (float)e.Y;
			var pointMouse =  new PointF(PosX, PosY);

			var mathPoint = ConvertWindowToMath(pointMouse, logicE);

			X = mathPoint.X; 
			Y = mathPoint.Y;

			UpdateManipulator();
			form.Invalidate();
		}

		public static void MouseWheel(Form form, MouseEventArgs e)
		{
			// TODO: Измените Alpha, используя e.Delta — размер прокрутки колеса мыши

			Alpha += e.Delta;

			UpdateManipulator();
			form.Invalidate();
		}

		public static void UpdateManipulator()
		{
			// Вызовите ManipulatorTask.MoveManipulatorTo и обновите значения полей Shoulder, Elbow и Wrist, 
			// если они не NaN. Это понадобится для последней задачи.
			var angles = ManipulatorTask.MoveManipulatorTo(X, Y, Alpha);

			if ((Shoulder != double.NaN) && ( Elbow != double.NaN) && ( Wrist != double.NaN))
            {
				Shoulder = angles[0];
				Elbow = angles[1];
				Wrist = angles[2];
			}
		}

		public static void DrawManipulator(Graphics graphics, PointF shoulderPos)
		{
			var joints = AnglesToCoordinatesTask.GetJointPositions(Shoulder, Elbow, Wrist);

			graphics.DrawString(
                $"X={X:0}, Y={Y:0}, Alpha={Alpha:0.00}", 
                new Font(SystemFonts.DefaultFont.FontFamily, 12), 
                Brushes.DarkRed, 
                10, 
                10);
			DrawReachableZone(graphics, ReachableAreaBrush, UnreachableAreaBrush, shoulderPos, joints);

			DrawManipulatorLines(graphics, joints, shoulderPos);

			// Нарисуйте сегменты манипулятора методом graphics.DrawLine используя ManipulatorPen.
			// Нарисуйте суставы манипулятора окружностями методом graphics.FillEllipse используя JointBrush.
			// Не забудьте сконвертировать координаты из логических в оконные
		}

        public static void DrawManipulatorLines(Graphics graphics, PointF[] joints, PointF shoulderPos)
        {
			var logicShoulder = ConvertMathToWindow(joints[0], shoulderPos);
			var logicElbow = ConvertMathToWindow(joints[1], shoulderPos);
			var logicWrist = ConvertMathToWindow(joints[2], shoulderPos);

			graphics.DrawLine(ManipulatorPen, shoulderPos.X, shoulderPos.Y, logicShoulder.X, logicShoulder.Y);
			graphics.DrawLine(ManipulatorPen, logicShoulder.X, logicShoulder.Y, logicElbow.X, logicElbow.Y);
			graphics.DrawLine(ManipulatorPen, logicElbow.X, logicElbow.Y, logicWrist.X, logicWrist.Y);

			var shoulderRectangle = new RectangleF(shoulderPos.X - (float)5, shoulderPos.Y - (float)5, (float)10, (float)10);
			var elbowRectangle = new RectangleF(logicShoulder.X - (float)5, logicShoulder.Y - (float)5, (float)10, (float)10);
			var wristRectangle = new RectangleF(logicElbow.X - (float)5, logicElbow.Y - (float)5, (float)10, (float)10);

			graphics.FillEllipse(JointBrush, shoulderRectangle);
			graphics.FillEllipse(JointBrush, elbowRectangle);
			graphics.FillEllipse(JointBrush, wristRectangle);
		}

        private static void DrawReachableZone(
            Graphics graphics, 
            Brush reachableBrush, 
            Brush unreachableBrush, 
            PointF shoulderPos, 
            PointF[] joints)
		{
			var rmin = Math.Abs(Manipulator.UpperArm - Manipulator.Forearm);
			var rmax = Manipulator.UpperArm + Manipulator.Forearm;
			var mathCenter = new PointF(joints[2].X - joints[1].X, joints[2].Y - joints[1].Y);
			var windowCenter = ConvertMathToWindow(mathCenter, shoulderPos);
			graphics.FillEllipse(reachableBrush, windowCenter.X - rmax, windowCenter.Y - rmax, 2 * rmax, 2 * rmax);
			graphics.FillEllipse(unreachableBrush, windowCenter.X - rmin, windowCenter.Y - rmin, 2 * rmin, 2 * rmin);
		}

		public static PointF GetShoulderPos(Form form)
		{
			return new PointF(form.ClientSize.Width / 2f, form.ClientSize.Height / 2f);
		}

		public static PointF ConvertMathToWindow(PointF mathPoint, PointF shoulderPos)
		{
			return new PointF(mathPoint.X + shoulderPos.X, shoulderPos.Y - mathPoint.Y);
		}

		public static PointF ConvertWindowToMath(PointF windowPoint, PointF shoulderPos)
		{
			return new PointF(windowPoint.X - shoulderPos.X, shoulderPos.Y - windowPoint.Y);
		}
	}
}
