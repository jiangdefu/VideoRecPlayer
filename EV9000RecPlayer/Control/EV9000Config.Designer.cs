namespace EV9000RecPlayer.Control
{
    partial class EV9000Config
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.path = new System.Windows.Forms.Label();
            this.picclose = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_cappath = new System.Windows.Forms.TextBox();
            this.tb_recpath = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.picclose)).BeginInit();
            this.SuspendLayout();
            // 
            // path
            // 
            this.path.AutoSize = true;
            this.path.BackColor = System.Drawing.Color.Transparent;
            this.path.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.path.ForeColor = System.Drawing.Color.White;
            this.path.Location = new System.Drawing.Point(7, 3);
            this.path.Name = "path";
            this.path.Size = new System.Drawing.Size(65, 20);
            this.path.TabIndex = 0;
            this.path.Text = "保存地址";
            this.path.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EV9000Config_MouseDown);
            // 
            // picclose
            // 
            this.picclose.BackColor = System.Drawing.Color.Transparent;
            this.picclose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picclose.Image = global::EV9000RecPlayer.Properties.Resources.close;
            this.picclose.Location = new System.Drawing.Point(226, 6);
            this.picclose.Name = "picclose";
            this.picclose.Size = new System.Drawing.Size(16, 16);
            this.picclose.TabIndex = 1;
            this.picclose.TabStop = false;
            this.picclose.MouseLeave += new System.EventHandler(this.picclose_MouseLeave);
            this.picclose.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picclose_MouseClick);
            this.picclose.MouseEnter += new System.EventHandler(this.picclose_MouseEnter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(16, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "截图";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(16, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "录像";
            // 
            // tb_cappath
            // 
            this.tb_cappath.Location = new System.Drawing.Point(59, 45);
            this.tb_cappath.Name = "tb_cappath";
            this.tb_cappath.Size = new System.Drawing.Size(168, 21);
            this.tb_cappath.TabIndex = 4;
            this.tb_cappath.Click += new System.EventHandler(this.tb_cappath_Click);
            // 
            // tb_recpath
            // 
            this.tb_recpath.Location = new System.Drawing.Point(59, 81);
            this.tb_recpath.Name = "tb_recpath";
            this.tb_recpath.Size = new System.Drawing.Size(168, 21);
            this.tb_recpath.TabIndex = 5;
            this.tb_recpath.Click += new System.EventHandler(this.tb_cappath_Click);
            // 
            // EV9000Config
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::EV9000RecPlayer.Properties.Resources.about_bg;
            this.Controls.Add(this.tb_recpath);
            this.Controls.Add(this.tb_cappath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picclose);
            this.Controls.Add(this.path);
            this.Name = "EV9000Config";
            this.Size = new System.Drawing.Size(248, 124);
            this.Load += new System.EventHandler(this.EV9000Config_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EV9000Config_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.picclose)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label path;
        private System.Windows.Forms.PictureBox picclose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_cappath;
        private System.Windows.Forms.TextBox tb_recpath;
    }
}
