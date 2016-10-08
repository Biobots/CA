using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Tao.OpenGl;
using System.Windows.Forms;

namespace CA
{
    public static class Sys
    {
        public static Map currentMap;
        public static int halfWidth;
        public static int halfHeight;

        public static float preX = 1;
        public static float preY = 1;

        public static int carCount = 1500;

        public static Mode mode = Mode.Cars;

        public static List<Crossing> crossings = new List<Crossing>();

        public static int round;
        public static int maxSpeed;

        public static Dictionary<int, List<int>> queue = new Dictionary<int, List<int>>();
        public static List<float> speed = new List<float>();

        public static void LoadMap()
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "文本文件|*.txt";
            fd.ShowReadOnly = true;
            DialogResult r = fd.ShowDialog();
            if (r == DialogResult.OK)
            {
                string tempLine;
                FileStream fs = new FileStream(fd.SafeFileName, FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                
                int width;
                int height;
                tempLine = sr.ReadLine();
                width = tempLine.Count();
                height = 1;
                while ((tempLine = sr.ReadLine()) != null)
                {
                    height++;
                }
                currentMap = new Map(width, height);
                for (int x = 0; x < width + 2; x++)
                {
                    for (int y = 0; y < height + 2; y++)
                    {
                        currentMap.map[x, y] = new Pixel();
                        currentMap.map[x, y].x = x;
                        currentMap.map[x, y].y = y;
                        currentMap.map[x, y].isBlocked = true;
                        //currentMap.map[x, y].isBlocked = false;
                        currentMap.map[x, y].isNSAvailable = true;
                        currentMap.map[x, y].isWEAvailable = true;
                    }
                }
                sr.Dispose();
                fs.Close();
                fs = new FileStream(fd.SafeFileName, FileMode.Open);
                StreamReader fill = new StreamReader(fs);
                int w = 1;
                List<string> list = new List<string>();
                while ((tempLine = fill.ReadLine()) != null)
                {
                    list.Add(tempLine);
                }
                while (list.Count != 0)
                {
                    tempLine = list.Last();
                    list.RemoveAt(list.Count - 1);
                    for (int x = 0; x < tempLine.Count(); x++)
                    {
                        char tempC = tempLine[x];
                        
                        switch (tempC)
                        {
                            case '*':
                                currentMap.map[x + 1, w].isBlocked = true;
                                break;
                            case '6':
                                currentMap.map[x + 1, w].isBlocked = false;
                                currentMap.map[x + 1, w].direction = currentMap.map[x + 1, w].direction.Add(Direction.East);
                                break;
                            case '4':
                                currentMap.map[x + 1, w].isBlocked = false;
                                currentMap.map[x + 1, w].direction = currentMap.map[x + 1, w].direction.Add(Direction.West);
                                break;
                            case '8':
                                currentMap.map[x + 1, w].isBlocked = false;
                                currentMap.map[x + 1, w].direction = currentMap.map[x + 1, w].direction.Add(Direction.North);
                                break;
                            case '2':
                                currentMap.map[x + 1, w].isBlocked = false;
                                currentMap.map[x + 1, w].direction = currentMap.map[x + 1, w].direction.Add(Direction.South);
                                break;
                            case '7':
                                currentMap.map[x + 1, w].isBlocked = false;
                                currentMap.map[x + 1, w].direction = currentMap.map[x + 1, w].direction.Add(Direction.North | Direction.West);
                                break;
                            case '9':
                                currentMap.map[x + 1, w].isBlocked = false;
                                currentMap.map[x + 1, w].direction = currentMap.map[x + 1, w].direction.Add(Direction.North | Direction.East);
                                break;
                            case '1':
                                currentMap.map[x + 1, w].isBlocked = false;
                                currentMap.map[x + 1, w].direction = currentMap.map[x + 1, w].direction.Add(Direction.South | Direction.West);
                                break;
                            case '3':
                                currentMap.map[x + 1, w].isBlocked = false;
                                currentMap.map[x + 1, w].direction = currentMap.map[x + 1, w].direction.Add(Direction.South | Direction.East);
                                break;
                            case 'Q':
                                currentMap.map[x + 1, w].isBlocked = false;
                                currentMap.map[x + 1, w].direction = currentMap.map[x + 1, w].direction.Add(Direction.South | Direction.West);
                                if (crossings.Count == 0)
                                {
                                    Crossing t = new Crossing();
                                    t.UL = currentMap.map[x + 1, w];
                                    t.UR = currentMap.map[x + 2, w];
                                    t.DL = currentMap.map[x + 1, w - 1];
                                    t.DR = currentMap.map[x + 2, w - 1];
                                    crossings.Add(t);
                                }
                                else
                                {
                                    bool exist = false;
                                    foreach (Crossing c in crossings)
                                    {
                                        if (c.UL == currentMap.map[x + 1, w]) { exist = true; }
                                    }
                                    if (!exist)
                                    {
                                        Crossing t = new Crossing();
                                        t.UL = currentMap.map[x + 1, w];
                                        t.UR = currentMap.map[x + 2, w];
                                        t.DL = currentMap.map[x + 1, w - 1];
                                        t.DR = currentMap.map[x + 2, w - 1];
                                        crossings.Add(t);
                                    }
                                }
                                break;
                            case 'W':
                                currentMap.map[x + 1, w].isBlocked = false;
                                currentMap.map[x + 1, w].direction = currentMap.map[x + 1, w].direction.Add(Direction.North | Direction.West);
                                if (crossings.Count == 0)
                                {
                                    Crossing t = new Crossing();
                                    t.UR = currentMap.map[x + 1, w];
                                    t.UL = currentMap.map[x, w];
                                    t.DR = currentMap.map[x + 1, w - 1];
                                    t.DL = currentMap.map[x, w - 1];
                                    crossings.Add(t);
                                }
                                else
                                {
                                    bool exist = false;
                                    foreach (Crossing c in crossings)
                                    {
                                        if (c.UR == currentMap.map[x + 1, w]) { exist = true; }
                                    }
                                    if (!exist)
                                    {
                                        Crossing t = new Crossing();
                                        t.UR = currentMap.map[x + 1, w];
                                        t.UL = currentMap.map[x, w];
                                        t.DR = currentMap.map[x + 1, w - 1];
                                        t.DL = currentMap.map[x, w - 1];
                                        crossings.Add(t);
                                    }
                                }
                                break;
                            case 'A':
                                currentMap.map[x + 1, w].isBlocked = false;
                                currentMap.map[x + 1, w].direction = currentMap.map[x + 1, w].direction.Add(Direction.South | Direction.East);
                                if (crossings.Count == 0)
                                {
                                    Crossing t = new Crossing();
                                    t.DL = currentMap.map[x + 1, w];
                                    t.UR = currentMap.map[x +2, w + 1];
                                    t.DR = currentMap.map[x + 2, w];
                                    t.UL = currentMap.map[x + 1, w + 1];
                                    crossings.Add(t);
                                }
                                else
                                {
                                    bool exist = false;
                                    foreach (Crossing c in crossings)
                                    {
                                        if (c.DL == currentMap.map[x + 1, w]) { exist = true; }
                                    }
                                    if (!exist)
                                    {
                                        Crossing t = new Crossing();
                                        t.DL = currentMap.map[x + 1, w];
                                        t.UR = currentMap.map[x + 2, w + 1];
                                        t.DR = currentMap.map[x + 2, w];
                                        t.UL = currentMap.map[x + 1, w + 1];
                                        crossings.Add(t);
                                    }
                                }
                                break;
                            case 'S':
                                currentMap.map[x + 1, w].isBlocked = false;
                                currentMap.map[x + 1, w].direction = currentMap.map[x + 1, w].direction.Add(Direction.North | Direction.East);
                                if (crossings.Count == 0)
                                {
                                    Crossing t = new Crossing();
                                    t.DR = currentMap.map[x + 1, w];
                                    t.UR = currentMap.map[x + 1, w + 1];
                                    t.DL = currentMap.map[x, w];
                                    t.UL = currentMap.map[x, w + 1];
                                    crossings.Add(t);
                                }
                                else
                                {
                                    bool exist = false;
                                    foreach (Crossing c in crossings)
                                    {
                                        if (c.DR == currentMap.map[x + 1, w]) { exist = true; }
                                    }
                                    if (!exist)
                                    {
                                        Crossing t = new Crossing();
                                        t.DR = currentMap.map[x + 1, w];
                                        t.UR = currentMap.map[x + 1, w + 1];
                                        t.DL = currentMap.map[x, w];
                                        t.UL = currentMap.map[x, w + 1];
                                        crossings.Add(t);
                                    }
                                }
                                break;
                        }
                    }
                    w++;
                }
            }
            
        }
        public static void InitializeMap()
        {
            Random rdmX = new Random();
            int count = 0;
            while (count< carCount)
            {
                int x = rdmX.Next(currentMap.map.GetLength(0));
                int y = rdmX.Next(currentMap.map.GetLength(1));
                if (!currentMap.map[x,y].isBlocked && !currentMap.map[x,y].hasCar)
                {
                    currentMap.map[x, y].hasCar = true;
                    count++;
                }
            }
        }
    }
}
