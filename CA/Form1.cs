using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Engine;
using System.IO;
using Tao.OpenGl;

namespace CA
{
    public partial class Form1 : Form
    {
        FastLoop _fastLoop;
        StateSystem _system;

        

        public Form1()
        {
            _fastLoop = new FastLoop(GameLoop);
            _system = new StateSystem();

            InitializeComponent();
            Stage.InitializeContexts();
            Setup2DGraphics(ClientSize.Width, ClientSize.Height);
            Sys.LoadMap();
            Sys.InitializeMap();

            Sys.halfHeight = ClientSize.Height / 2;
            Sys.halfWidth = ClientSize.Width / 2;

            Sys.preX = ClientSize.Width / Sys.currentMap.map.GetLength(0);
            Sys.preY = ClientSize.Height / Sys.currentMap.map.GetLength(1);

            Gl.glScalef(0.02f / Sys.preX, 0.02f / Sys.preY, 1);

            _system.AddState("map", Sys.currentMap);
            _system.ChangeState("map");

            Form2 frm = new Form2();
            frm.Show();

            timer1.Enabled = true;
            //Gl.glTranslatef(-ClientSize.Width / 2, -ClientSize.Height / 2, 0);
        }
        void GameLoop(double elapsedTime)
        {
            Gl.glClearColor(1.0f, 1.0f, 1.0f, 1.0f);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            //AutoZoom();

            if (Sys.currentMap.map != null)
            {
                //_system.Update(elapsedTime);
                _system.Render();
            }
            Gl.glFinish();
            Stage.SwapBuffers();
        }
        private void Setup2DGraphics(double width, double height)
        {
            double halfWidth = width / 2;
            double halfHeight = height / 2;
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Gl.glOrtho(-halfWidth, halfWidth, -halfHeight, halfHeight, 0, 0);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
        }
        
        public void AutoZoom()
        {
            Gl.glScalef(1/Sys.preX, 1/Sys.preY, 1);
            float x = ClientSize.Width / Sys.currentMap.map.GetLength(0);
            float y = ClientSize.Height / Sys.currentMap.map.GetLength(1);
            Gl.glScalef(x, y, 1);
            Sys.preX = x;
            Sys.preY = y;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            Sys.halfHeight = ClientSize.Height / 2;
            Sys.halfWidth = ClientSize.Width / 2;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Sys.round < 2000)
            {
                Sys.speed.Add(Sys.maxSpeed / (float)Sys.carCount);
                Sys.maxSpeed = Sys.carCount;
                Sys.round += 1;
                label1.Text = Sys.round.ToString();
                List<int> t = new List<int>();
                Sys.queue.Add(Sys.round, t);
                UpdateCrossings();
                _system.Update(0.0);
            }
            //else
            //{
            //    timer1.Enabled = false;
            //    saveFileDialog1.Filter = "文本文件 | *.txt";
            //    saveFileDialog1.ShowDialog();
            //    FileStream fs = new FileStream(saveFileDialog1.FileName, FileMode.Create);
            //    StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            //    for (int i = 1; i <= 2000; i++)
            //    {
            //        int num = 0;
            //        foreach (int l in Sys.queue[i])
            //        {
            //            num += l;
            //        }
            //        num = num / Sys.queue[i].Count;
            //        sw.WriteLine(i.ToString() + " " + Sys.speed[i - 1].ToString() + " " + Sys.queue[i].Count + " " + num.ToString());
            //    }
            //    sw.Dispose();
            //    sw.Close();
            //    fs.Dispose();
            //    fs.Close();
            //}
        }
        private void UpdateCrossings()
        {
            foreach (Crossing c in Sys.crossings)
            {
                c.time += 1;
                if (c.time == 60)
                {
                    c.time = 0;
                    Sys.currentMap.map[c.DR.x,c.DR.y].isNSAvailable = false;
                    Sys.currentMap.map[c.DR.x,c.DR.y].isWEAvailable = true;
                    Sys.currentMap.map[c.DL.x,c.DL.y].isNSAvailable = true;
                    Sys.currentMap.map[c.DL.x,c.DL.y].isWEAvailable = false;
                    Sys.currentMap.map[c.UL.x,c.UL.y].isNSAvailable = false;
                    Sys.currentMap.map[c.UL.x,c.UL.y].isWEAvailable = true;
                    Sys.currentMap.map[c.UR.x,c.UR.y].isNSAvailable = true;
                    Sys.currentMap.map[c.UR.x,c.UR.y].isWEAvailable = false;
                }
                else if (c.time>=1 && c.time <=25)
                {
                    Sys.currentMap.map[c.DR.x,c.DR.y].isNSAvailable = true;
                    Sys.currentMap.map[c.DR.x,c.DR.y].isWEAvailable = true;
                    Sys.currentMap.map[c.DL.x,c.DL.y].isNSAvailable = true;
                    Sys.currentMap.map[c.DL.x,c.DL.y].isWEAvailable = false;
                    Sys.currentMap.map[c.UL.x,c.UL.y].isNSAvailable = true;
                    Sys.currentMap.map[c.UL.x,c.UL.y].isWEAvailable = true;
                    Sys.currentMap.map[c.UR.x,c.UR.y].isNSAvailable = true;
                    Sys.currentMap.map[c.UR.x,c.UR.y].isWEAvailable = false;
                }
                else if (c.time>=26 && c.time<=30)
                {
                    Sys.currentMap.map[c.DR.x,c.DR.y].isNSAvailable = false;
                    Sys.currentMap.map[c.DR.x,c.DR.y].isWEAvailable = true;
                    Sys.currentMap.map[c.DL.x,c.DL.y].isNSAvailable = true;
                    Sys.currentMap.map[c.DL.x,c.DL.y].isWEAvailable = false;
                    Sys.currentMap.map[c.UL.x,c.UL.y].isNSAvailable = false;
                    Sys.currentMap.map[c.UL.x,c.UL.y].isWEAvailable = true;
                    Sys.currentMap.map[c.UR.x,c.UR.y].isNSAvailable = true;
                    Sys.currentMap.map[c.UR.x,c.UR.y].isWEAvailable = false;
                }
                else if (c.time >= 31 && c.time <= 55)
                {
                    Sys.currentMap.map[c.DR.x,c.DR.y].isNSAvailable = false;
                    Sys.currentMap.map[c.DR.x,c.DR.y].isWEAvailable = true;
                    Sys.currentMap.map[c.DL.x,c.DL.y].isNSAvailable = true;
                    Sys.currentMap.map[c.DL.x,c.DL.y].isWEAvailable = true;
                    Sys.currentMap.map[c.UL.x,c.UL.y].isNSAvailable = false;
                    Sys.currentMap.map[c.UL.x,c.UL.y].isWEAvailable = true;
                    Sys.currentMap.map[c.UR.x,c.UR.y].isNSAvailable = true;
                    Sys.currentMap.map[c.UR.x, c.UR.y].isWEAvailable = true;
                }
                else if (c.time >= 56 && c.time < 60)
                {
                    Sys.currentMap.map[c.DR.x,c.DR.y].isNSAvailable = false;
                    Sys.currentMap.map[c.DR.x,c.DR.y].isWEAvailable = true;
                    Sys.currentMap.map[c.DL.x,c.DL.y].isNSAvailable = true;
                    Sys.currentMap.map[c.DL.x,c.DL.y].isWEAvailable = false;
                    Sys.currentMap.map[c.UL.x,c.UL.y].isNSAvailable = false;
                    Sys.currentMap.map[c.UL.x,c.UL.y].isWEAvailable = true;
                    Sys.currentMap.map[c.UR.x,c.UR.y].isNSAvailable = true;
                    Sys.currentMap.map[c.UR.x,c.UR.y].isWEAvailable = false;
                }
            }
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}
