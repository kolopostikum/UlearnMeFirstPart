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

            command = CheckMovePlayer(command, x, y);
            return command;
        }

        private CreatureCommand CheckMovePlayer(CreatureCommand command, int x, int y)
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
            {
                return true;
            }

            if (conflictedObject.GetImageFileName() == "Monster.png")
            {
                return true;
            }

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
                else if ((Game.Map[x, y + 1].GetImageFileName() == "Monster.png") && CountMove > 0)
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

    public class Monster : ICreature
    {
        public int PlayerCoordinatX = 0;
        public int PlayerCoordinatY = 0;
        public bool IsAlife = true;

        public CreatureCommand Act(int x, int y)
        {
            SearchPlayer();
            if (IsAlife == true)
                return MoveMonster(x, y);
            return new CreatureCommand { DeltaX = 0, DeltaY = 0 };
        }

        private CreatureCommand MoveMonster( int x, int y)
        {
            if (HaventMove(new CreatureCommand { DeltaX = 0, DeltaY = 0 }, x, y))
                return new CreatureCommand { DeltaX = 0, DeltaY = 0 };
            
            SearchPlayer();

            if (PlayerCoordinatX - x > 0) 
            {
                if (CheckMoveMonster(new CreatureCommand { DeltaX = 1, DeltaY = 0 }, x, y))
                {
                    return new CreatureCommand { DeltaX = 1, DeltaY = 0 };
                }
            }

            else if (PlayerCoordinatX - x < 0)
            {
                if (CheckMoveMonster(new CreatureCommand { DeltaX = -1, DeltaY = 0 }, x, y))
                {
                    return new CreatureCommand { DeltaX = -1, DeltaY = 0 };
                }
            }

            else if (PlayerCoordinatY  - y < 0)
            {
                if (CheckMoveMonster(new CreatureCommand { DeltaX = 0, DeltaY = -1 }, x, y))
                {
                    return new CreatureCommand { DeltaX = 0, DeltaY = -1 };
                }
            }

            else if (PlayerCoordinatY - y > 0)
            {
                if (CheckMoveMonster(new CreatureCommand { DeltaX = 0, DeltaY = 1 }, x, y))
                {
                    return new CreatureCommand { DeltaX = 0, DeltaY = 1 };
                }
            }

            return new CreatureCommand { DeltaX = 0, DeltaY = 0 };
        }

        private void SearchPlayer()
        {
            for (int i = 0; i < Game.MapWidth; i++)
            {
                for (int j = 0; j < Game.MapHeight ; j++)
                {
                    if (Game.Map[i, j] == null)
                    {
                        ;
                    }
                    else if(Game.Map[i , j].GetImageFileName() == "Digger.png")
                    {
                        PlayerCoordinatX = i;
                        PlayerCoordinatY = j;
                        IsAlife = true;
                        return;
                    }
                }
            }
            IsAlife = false;
        }

        private bool HaventMove(CreatureCommand command, int x, int y)
        {
            for (int i = -1; i < 2; i++)
            {
                if (CheckMoveMonster(new CreatureCommand { DeltaX = i, DeltaY = 0 }, x, y))
                    return false;
                if (CheckMoveMonster(new CreatureCommand { DeltaX = 0, DeltaY = i }, x, y))
                    return false;
            }
            return true;
        }

        private bool CheckMoveMonster(CreatureCommand command, int x, int y)
        {
            if (x + command.DeltaX < 0 || x + command.DeltaX >= Game.MapWidth || y + command.DeltaY < 0 ||
y + command.DeltaY >= Game.MapHeight)
                return false;

            if (Game.Map[x + command.DeltaX, y + command.DeltaY] == null)
                return true;

            if (Game.Map[x + command.DeltaX, y + command.DeltaY].GetImageFileName() == "Sack.png")
                return false;

            if (Game.Map[x + command.DeltaX, y + command.DeltaY].GetImageFileName() == "Monster.png")
                return false;

            if (Game.Map[x + command.DeltaX, y + command.DeltaY].GetImageFileName() == "Terrain.png")
                return false;

            return true;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject.GetImageFileName() == "Sack.png")
                return true;
            if (conflictedObject.GetImageFileName() == "Monster.png")
                return true;
            return false;
        }

        public int GetDrawingPriority()
        {
            return 0;
        }

        public string GetImageFileName()
        {
            return $"Monster.png";
        }
    }
}