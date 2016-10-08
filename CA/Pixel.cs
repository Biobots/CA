using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;
using Tao.OpenGl;

namespace CA
{
    [Flags]
    public enum Direction
    {
        North = 0x01,
        South = 0x02,
        West = 0x04,
        East = 0x08
    }
    public enum Mode
    {
        Traffic,
        Crossing,
        Cars
    }
    public static class Extensions
    {
        public static Direction Add(this Direction left, Direction right)
        { return left | (right ^ (left & right)); }
        public static Direction Remove(this Direction left, Direction right)
        { return left ^ (left & right); }
    }
    public class Pixel : ICloneable
    {
        public int x;
        public int y;

        public bool isBlocked;
        public bool hasCar;
        public bool isNSAvailable;
        public bool isWEAvailable;

        public int traffic;
        public int delay;

        //private Pixel()
        //{
        //    x = 0;
        //    y = 0;
        //    hasCar = false;
        //    isBlocked = true;
        //    direction = Direction.Null;
        //}

        public Direction direction;

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class Crossing
    {
        public Pixel UL { get; set; }
        public Pixel UR { get; set; }
        public Pixel DL { get; set; }
        public Pixel DR { get; set; }

        public int time;

    }
}
