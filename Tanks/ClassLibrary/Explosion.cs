using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ClassLibrary
{
    public class Explosion
    {
        public int position_y;
        public int position_x;
        public int size;
        public int explocionCount;

        public Explosion (Tank myTank)
        {
            position_y = myTank.position_y;
            position_x = myTank.position_x;
            explocionCount = 0;
            size = 15;
        }
    }
}