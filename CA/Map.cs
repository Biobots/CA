using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA
{
    public class Map
    {
        public Pixel[,] map;
        public Map(int width, int height)
        {
            map = new Pixel[width + 2, height + 2];
        }
    }
}
