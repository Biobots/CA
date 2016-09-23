namespace CA
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.Stage = new Tao.Platform.Windows.SimpleOpenGlControl();
            this.SuspendLayout();
            // 
            // Stage
            // 
            this.Stage.AccumBits = ((byte)(0));
            this.Stage.AutoCheckErrors = false;
            this.Stage.AutoFinish = false;
            this.Stage.AutoMakeCurrent = true;
            this.Stage.AutoSwapBuffers = true;
            this.Stage.BackColor = System.Drawing.Color.Black;
            this.Stage.ColorBits = ((byte)(32));
            this.Stage.DepthBits = ((byte)(16));
            this.Stage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Stage.Location = new System.Drawing.Point(0, 0);
            this.Stage.Name = "Stage";
            this.Stage.Size = new System.Drawing.Size(1285, 904);
            this.Stage.StencilBits = ((byte)(0));
            this.Stage.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1285, 904);
            this.Controls.Add(this.Stage);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Tao.Platform.Windows.SimpleOpenGlControl Stage;
    }
}

