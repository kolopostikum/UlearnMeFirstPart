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

            command = CheckMove(command, x, y);
            return command;
        }

        private CreatureCommand CheckMove(CreatureCommand command, int x, int y)
        {
            if (x + command.DeltaX < 0 || x + command.DeltaX >= Game.MapWidth || y + command.DeltaY < 0 ||
y + command.DeltaY >= Game.MapHeight)
                return new CreatureCommand { DeltaX = 0, DeltaY = 0 };
     
            if (Game.Map[x + command.DeltaX, y + command.DeltaY] == null)
                return command;
     
            if (Game.Map[x + command.DeltaX, y + command.DeltaY].GetImageFileName() == "Sack.png")
                return new CreatureCommand { DeltaX = 0, DeltaY = 0 };

            return command;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject.GetImageFileName() == "Sack.png")
                return true;
            if (conflictedObject.GetImageFileName() == "Gold.png")
                Game.Scores += 10;
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

    public class Sack : ICreature
    {
        public int CountMove = 0;

        public CreatureCommand Act(int x, int y)
        {
            var command = new CreatureCommand { DeltaX = 0, DeltaY = 0 };
            if (y < Game.MapHeight - 1)
            {
                if (Game.Map[x, y + 1] == null)
                {
                    command.DeltaY += 1;
                    CountMove++;
                }
                else if ((Game.Map[x, y + 1].GetImageFileName() == "Digger.png") && CountMove > 0)
                {
                    command.DeltaY += 1;
                    CountMove++;
                }
                else
                {
                    if (CountMove > 1)
                    {
                        return new CreatureCommand() { DeltaX = 0, DeltaY = 0, TransformTo = new Gold() };
                    }
                    else
                        CountMove = 0;
                }
            }
            else
            {
                if (CountMove > 1)
                {
                    return new CreatureCommand() { DeltaX = 0, DeltaY = 0, TransformTo = new Gold() };
                }
            }
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
            return $"Sack.png";
        }
    }

    public class Gold : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            var command = new CreatureCommand { DeltaX = 0, DeltaY = 0 };
            return command;
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
            return $"Gold.png";
        }
    }
}