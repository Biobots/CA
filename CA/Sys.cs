using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace CA
{
    public static class Sys
    {
        public static Map currentMap;
        public static void LoadMap()
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "*.txt";
            fd.ShowReadOnly = true;
            DialogResult r = fd.ShowDialog();
            if (r == DialogResult.OK)
            {
                string tempLine;
                FileStream fs = new FileStream(fd.SafeFileName, FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                StreamReader fill = new StreamReader(fs);
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
                int y = 1;
                while((tempLine = fill.ReadLine()) != null)
                {
                    for (int x=0;x<tempLine.Count();x++)
                    {
                        char tempC = tempLine[x];
                        switch(tempC)
                        {
                            case 'A':
                                currentMap.map[x + 1, y].isBlocked = true;
                                break;
                            case 'B':
                                currentMap.map[x + 1, y].isBlocked = false;
                                break;
                        }
                    }
                }
            }
        }
    }
}
