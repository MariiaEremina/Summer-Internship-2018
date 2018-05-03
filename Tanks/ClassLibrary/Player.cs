using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Player : Tank
    {
        public Player (int x, int y)
        {

            position_x = x;
            position_y = y;
            
            direction = directions.up;
        }
    }
}
