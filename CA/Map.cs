using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;
using Tao.OpenGl;

namespace CA
{
    public class Map : IGameObject
    {
        public Pixel[,] map;
        public Pixel[,] temp;
        public double time;
        public Map(int width, int height)
        {
            map = new Pixel[width + 2, height + 2];
        }

        public void Render()
        {
            Gl.glPointSize(2.0f);

            foreach (Pixel p in map)
            {
                if (p.isBlocked)
                {
                    Gl.glBegin(Gl.GL_POINTS);
                    {
                        Gl.glColor3f(0.0f, 0.0f, 0.0f);
                        Gl.glVertex2f(p.x - Sys.halfWidth / Sys.preX, p.y - Sys.halfHeight / Sys.preY);
                    }
                    Gl.glEnd();
                }
                else if (p.hasCar)
                {
                    Gl.glBegin(Gl.GL_POINTS);
                    {
                        Gl.glColor3f(0.0f, 0.0f, 1.0f);
                        Gl.glVertex2f(p.x - Sys.halfWidth / Sys.preX, p.y - Sys.halfHeight / Sys.preY);
                    }
                    Gl.glEnd();
                }
                else
                {
                    Gl.glBegin(Gl.GL_POINTS);
                    {
                        Gl.glColor3f(1, 1 - p.traffic / 2000f, 1 - p.traffic / 2000f);
                        Gl.glVertex2f(p.x - Sys.halfWidth / Sys.preX, p.y - Sys.halfHeight / Sys.preY);
                    }
                    Gl.glEnd();
                }
            }
        }

        public void Update(double elapsedTime)
        {
            if (true)
            {
                //temp = (Pixel[,])map.Clone();
                temp = new Pixel[map.GetLength(0), map.GetLength(1)];
                for (int x = 0; x < temp.GetLength(0); x++)
                {
                    for (int y = 0; y < temp.GetLength(1); y++)
                    {
                        temp[x, y] = (Pixel)map[x, y].Clone();
                    }
                }
                Random rdm = new Random();
                for (int x = 1; x < map.GetLength(0) - 1; x++)
                {
                    for (int y = 1; y < map.GetLength(1) - 1; y++)
                    {
                        if (map[x, y].hasCar)
                        {
                            temp[x, y].traffic++;
                            List<Direction> d = new List<Direction>();
                            if (!map[x + 1, y].isBlocked && !map[x + 1, y].hasCar && !temp[x + 1, y].hasCar && map[x, y].direction.HasFlag(Direction.East) && temp[x + 1, y].isWEAvailable)
                            {
                                d.Add(Direction.East);
                            }
                            if (!map[x - 1, y].isBlocked && !map[x - 1, y].hasCar && !temp[x - 1, y].hasCar && map[x, y].direction.HasFlag(Direction.West) && temp[x - 1, y].isWEAvailable)
                            {
                                d.Add(Direction.West);
                            }
                            if (!map[x, y + 1].isBlocked && !map[x, y + 1].hasCar && !temp[x, y + 1].hasCar && map[x, y].direction.HasFlag(Direction.North) && temp[x, y + 1].isNSAvailable)
                            {
                                d.Add(Direction.North);
                            }
                            if (!map[x, y - 1].isBlocked && !map[x, y - 1].hasCar && !temp[x, y - 1].hasCar && map[x, y].direction.HasFlag(Direction.South) && temp[x, y - 1].isNSAvailable)
                            {
                                d.Add(Direction.South);
                            }
                            if (d.Count == 0)
                            {
                                temp[x, y].delay += 1;
                                Sys.maxSpeed -= 1;
                            }
                            else
                            {
                                Direction di = d[rdm.Next(d.Count)];
                                int i;
                                int count = 0;
                                switch (di)
                                {
                                    case Direction.East:
                                        i = 1;
                                        count = 0;
                                        while (map[x - i, y].hasCar)
                                        {
                                            count++;
                                            i++;
                                        }
                                        temp[x + 1, y].hasCar = true;
                                        break;
                                    case Direction.West:
                                        i = 1;
                                        count = 0;
                                        while (map[x + i, y].hasCar)
                                        {
                                            count++;
                                            i++;
                                        }
                                        temp[x - 1, y].hasCar = true;
                                        break;
                                    case Direction.North:
                                        i = 1;
                                        count = 0;
                                        while (map[x, y - i].hasCar)
                                        {
                                            count++;
                                            i++;
                                        }
                                        temp[x, y + 1].hasCar = true;
                                        break;
                                    case Direction.South:
                                        i = 1;
                                        count = 0;
                                        while (map[x, y + i].hasCar)
                                        {
                                            count++;
                                            i++;
                                        }
                                        temp[x, y - 1].hasCar = true;
                                        break;
                                }
                                if (count > 0)
                                {
                                    Sys.queue[Sys.round].Add(count + 1);
                                }
                                
                                temp[x, y].hasCar = false;
                            }
                        }
                    }
                }
                //map = (Pixel[,])temp.Clone();
                for (int x = 0; x < temp.GetLength(0); x++)
                {
                    for (int y = 0; y < temp.GetLength(1); y++)
                    {
                        map[x, y] = (Pixel)temp[x, y].Clone();
                    }
                }
                //map = new Pixel[map.GetLength(0), map.GetLength(1)];
                //for (int x = 0; x < map.GetLength(0); x++)
                //{
                //    for (int y = 0; y < map.GetLength(1); y++)
                //    {
                //        map[x, y] = new Pixel();
                //        map[x, y].x = x;
                //        map[x, y].y = y;
                //        map[x, y].isBlocked = temp[x, y].isBlocked;
                //        map[x, y].hasCar = temp[x, y].hasCar;
                //    }
                //}
            }
            
        }

        private void InitializeCrossings()
        {

        }
    }
}
