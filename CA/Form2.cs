using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CA
{
    public partial class Form2 : Form
    {
        List<Pixel> l;
        public Form2()
        {
            InitializeComponent();
        }

        private void Traffic_Click(object sender, EventArgs e)
        {
            Sys.mode = Mode.Traffic;
        }

        private void Crossing_Click(object sender, EventArgs e)
        {
            Sys.mode = Mode.Crossing;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            l = new List<Pixel>();
            foreach (Crossing c in Sys.crossings)
            {
                l.Add(Sys.currentMap.map[c.UL.x, c.UL.y]);
                l.Add(Sys.currentMap.map[c.UR.x, c.UR.y]);
                l.Add(Sys.currentMap.map[c.DL.x, c.DL.y]);
                l.Add(Sys.currentMap.map[c.DR.x, c.DR.y]);
            }
            saveFileDialog1.Filter = "文本文件|*.txt";
            saveFileDialog1.ShowDialog();
            
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            Dictionary<Pixel, int> list = new Dictionary<Pixel, int>();
            foreach (Pixel p in l)
            {
                if (!p.isBlocked)
                {
                    list.Add(p, p.traffic);
                }
            }
            FileStream fs = new FileStream(saveFileDialog1.FileName, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            while (list.Count!=0)
            {
                sw.WriteLine(list.First().Key.x + " " + list.First().Key.y + " " + list.First().Value);
                list.Remove(list.First().Key);
            }
            sw.Close();
            fs.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            saveFileDialog2.Filter = "文本文件|*.txt";
            saveFileDialog2.ShowDialog();
            Dictionary<Pixel, int> list = new Dictionary<Pixel, int>();
            foreach (Pixel p in Sys.currentMap.map)
            {
                if (!p.isBlocked)
                {
                    list.Add(p, p.delay);
                }
            }
            FileStream fs = new FileStream(saveFileDialog2.FileName, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            while (list.Count != 0)
            {
                sw.WriteLine(list.First().Key.x + " " + list.First().Key.y + " " + list.First().Value);
                list.Remove(list.First().Key);
            }
            sw.Close();
            fs.Close();
        }

        private void saveFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            
        }
    }
}
