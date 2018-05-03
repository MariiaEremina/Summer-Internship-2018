using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public abstract class Movable
    {
        public enum directions { up, down, left, right, none };
        public int position_x;
        public int position_y;
        public directions direction;

        public void Move()
        {
            switch (direction)
            {
                case directions.up:
                    position_y--;
                    break;
                case directions.down:
                    position_y++;
                    break;
                case directions.left:
                    position_x--;
                    break;
                case directions.right:
                    position_x++;
                    break;
                default:
                    break;
            }
        }
    }

}
