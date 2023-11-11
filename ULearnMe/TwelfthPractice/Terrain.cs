using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digger
{
    //Напишите здесь классы Player, Terrain и другие.
    public class Terrain : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand { DeltaX = 0, DeltaY = 0 };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return true;
        }

        public int GetDrawingPriority()
        {
            return 0;
        }

        public string GetImageFileName()
        {
            return $"Terrain.png";
        }
    }

    public class Player : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            var command = new CreatureCommand { DeltaX = 0, DeltaY = 0 };
            switch ((int)Game.KeyPressed)
            {
                case 38:
                    command.DeltaY -= 1;
                    break;
                case 40:
                    command.DeltaY += 1;
                    break;
                case 37:
                    command.DeltaX -= 1;
                    break;
                case 39:
                    command.DeltaX += 1;
                    break;
                default:
                    return command;
            }
            if (x + command.DeltaX < 0 || x + command.DeltaX >= Game.MapWidth || y + command.DeltaY < 0 ||
    y + command.DeltaY >= Game.MapHeight)
                return new CreatureCommand { DeltaX = 0, DeltaY = 0 };
            return command;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return false;
        }

        public int GetDrawingPriority()
        {
            return 0;
        }

        public string GetImageFileName()
        {
            return $"Digger.png";
        }
    }
}