using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Bullet : Movable
    {
        public Tank myOwner;

        public Bullet(Tank myTank, directions tankDirection)
        {
            direction = tankDirection;
            myOwner = myTank;

            switch (direction)
            {
                case directions.up:
                    position_y = myTank.position_y;
                    position_x = myTank.position_x + myTank.size_x / 2;
                    break;
                case directions.down:
                    position_y = myTank.position_y + myTank.size_y;
                    position_x = myTank.position_x + myTank.size_x / 2;
                    break;
                case directions.left:
                    position_y = myTank.position_y + myTank.size_y / 2;
                    position_x = myTank.position_x;
                    break;
                case directions.right:
                    position_y = myTank.position_y + myTank.size_y / 2;
                    position_x = myTank.position_x + myTank.size_x;
                    break;
                default:
                    break;
            }
        }
    }
}
