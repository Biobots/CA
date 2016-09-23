using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;

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
    public class Pixel : IGameObject
    {
        public bool isBlocked;
        public bool hasCar;

        public Direction direction;



        public void Render()
        {
            
            throw new NotImplementedException();
        }

        public void Update(double elapsedTime)
        {
            throw new NotImplementedException();
        }
    }
}
