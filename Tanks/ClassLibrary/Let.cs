using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Let
    {
        public int position_x;
        public int position_y;
        public int type;

        public Let(int x, int y, char t)
        {
            position_x = x;
            position_y = y;
            int intT = Int32.Parse (t.ToString());
            type = intT - 1; 
    }
    }
    
}
