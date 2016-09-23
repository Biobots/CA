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
        }
        void GameLoop(double elapsedTime)
        {

        }
        private void Setup2DGraphics(double width, double height)
        {
            double halfWidth = width / 2;
            double halfHeight = height / 2;
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Gl.glOrtho(-halfWidth, halfWidth, -halfHeight, halfHeight, 0, 0);
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
        }
    }
}
