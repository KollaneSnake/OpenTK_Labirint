using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labirint_Kiir
{
    class Collusion
    {
        public static bool collide(int posX, int posY, Level level)
        {

            for (int x = 0; x < level.Width; x++)
            {
                for (int y = 0; y < level.Height; y++)
                {
                    if (level[x, y].Type == BlockType.Ladder || level[x, y].Type == BlockType.LadderPlatform)
                    {
                        if (posY> x*64 && posY<(x+1)*64 && posX>y*64 && posX<(y+1)*64)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public static bool gravity(int posX, int posY, Level level)
        {
            for (int x = 0; x < level.Width; x++)
            {
                for (int y = 0; y < level.Height; y++)
                {
                    if (level[x, y].Type == BlockType.Empty )
                    {
                        if (posY > x * 64 && posY < (x + 1) * 64 && posX > y * 64 && posX < (y + 1) * 64)
                        {
                            return true;
                        }
                    }
                    if (level[x, y].Type == BlockType.Solid)
                    {
                        if (posY > x * 64 && posY < (x + 1) * 64 && posX > y * 64 && posX < (y + 1) * 64)
                        {
                            return false;
                        }
                    }
                }
            }
            return false;
        }
    }
}
