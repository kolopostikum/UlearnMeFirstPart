using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mazes
{
    public class EmptyMazeTask
	{
		public static void MoveOut(Robot robot, int width, int height)
		{
            GoRight(robot, width - 3);

            GoDown(robot, height - 3);
		}

        private static void GoDown(Robot robot, int height)
        {
            for (int j = 0; j < height; j++)
            {
                robot.MoveTo(Direction.Down);
            }
        }

        private static void GoRight(Robot robot, int width)
        {
            for (int j = 0; j < width; j++)
            {
                robot.MoveTo(Direction.Right);
            }
        }
    }
}