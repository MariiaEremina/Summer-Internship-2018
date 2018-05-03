using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Enemy : Tank
    {
        public int time;
        public Enemy(int x, int y)
        {
            position_x = x;
            position_y = y;
            direction = directions.down;
            time = 0;
        }
    }
}
