using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mazes
{
	public static class SnakeMazeTask
	{
        public static void MoveOut(Robot robot, int width, int height)
        {
            for (int i = 0; i < (height-2)/4; i++)
            {
                GoCicleSnake(robot, width);
                GoDown(robot, 2);
            }
            
            GoCicleSnake(robot, width);
        }

        private static void GoCicleSnake(Robot robot, int width)
        {
            GoRight(robot, width - 3);

            GoDown(robot, 2);

            GoLeft(robot, width - 3);
        }

        private static void GoLeft(Robot robot, int width)
        {
            for (int j = 0; j < width; j++)
            {
                robot.MoveTo(Direction.Left);
            }
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