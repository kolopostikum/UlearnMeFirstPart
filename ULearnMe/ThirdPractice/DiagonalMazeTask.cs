using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mazes
{
	public static class DiagonalMazeTask
	{
		public static void MoveOut(Robot robot, int width, int height)
		{
            if (height >= width)
            {
                MoveOutHeight(robot, height, width);
            }
            else 
            {
                MoveOutWidth(robot, height, width);
            }
        }

        private static void MoveOutWidth(Robot robot, int height, int width)
        {
            for (int i = 0; i < height - 3; i++)
            {
                GoRight(robot, width - 3, height - 3);
                GoDown(robot, height - 3, width - 3);
            }

            GoRight(robot, width - 3, height - 3);
        }

        private static void MoveOutHeight(Robot robot, int height, int width)
        {
            for (int i = 0; i < width - 3; i++)
            {
                GoDown(robot, height - 3, width - 3);
                GoRight(robot, width - 3, height - 3);
            }

            GoDown(robot, height - 3, width - 3);
        }

        private static void GoDown(Robot robot, int height, int width)
        {
            if (height>width)
            {
                for (int j = 0; j < height/width; j++)
                {
                    robot.MoveTo(Direction.Down);
                }
            }
            else
                robot.MoveTo(Direction.Down);
        }

        private static void GoRight(Robot robot, int width, int height)
        {
            if (width > height)
            {
                for (int j = 0; j < width / height; j++)
                {
                    robot.MoveTo(Direction.Right);
                }
            }
            else
                robot.MoveTo(Direction.Right);
        }
    }
}