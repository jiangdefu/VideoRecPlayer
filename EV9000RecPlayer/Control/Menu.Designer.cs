namespace EV9000RecPlayer
{
    partial class Menu
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
            this.openfile = new System.Windows.Forms.Label();
            this.addfile = new System.Windows.Forms.Label();
            this.opendir = new System.Windows.Forms.Label();
            this.winhelp = new System.Windows.Forms.Label();
            this.about = new System.Windows.Forms.Label();
            this.sepator = new System.Windows.Forms.Label();
            this.clip = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // openfile
            // 
            this.openfile.AutoSize = true;
            this.openfile.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.openfile.ForeColor = System.Drawing.Color.Gainsboro;
            this.openfile.Location = new System.Drawing.Point(11, 9);
            this.openfile.Name = "openfile";
            this.openfile.Size = new System.Drawing.Size(53, 12);
            this.openfile.TabIndex = 0;
            this.openfile.Text = "打开文件";
            this.openfile.MouseLeave += new System.EventHandler(this.openfile_MouseLeave);
            this.openfile.Click += new System.EventHandler(this.openfile_Click);
            this.openfile.MouseEnter += new System.EventHandler(this.openfile_MouseEnter);
            // 
            // addfile
            // 
            this.addfile.AutoSize = true;
            this.addfile.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.addfile.ForeColor = System.Drawing.Color.Gainsboro;
            this.addfile.Location = new System.Drawing.Point(11, 32);
            this.addfile.Name = "addfile";
            this.addfile.Size = new System.Drawing.Size(53, 12);
            this.addfile.TabIndex = 1;
            this.addfile.Text = "添加文件";
            this.addfile.MouseLeave += new System.EventHandler(this.openfile_MouseLeave);
            this.addfile.Click += new System.EventHandler(this.addfile_Click);
            this.addfile.MouseEnter += new System.EventHandler(this.openfile_MouseEnter);
            // 
            // opendir
            // 
            this.opendir.AutoSize = true;
            this.opendir.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.opendir.ForeColor = System.Drawing.Color.Gainsboro;
            this.opendir.Location = new System.Drawing.Point(10, 76);
            this.opendir.Name = "opendir";
            this.opendir.Size = new System.Drawing.Size(65, 12);
            this.opendir.TabIndex = 2;
            this.opendir.Text = "添加文件夹";
            this.opendir.MouseLeave += new System.EventHandler(this.openfile_MouseLeave);
            this.opendir.Click += new System.EventHandler(this.opendir_Click);
            this.opendir.MouseEnter += new System.EventHandler(this.openfile_MouseEnter);
            // 
            // winhelp
            // 
            this.winhelp.AutoSize = true;
            this.winhelp.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.winhelp.ForeColor = System.Drawing.Color.Gainsboro;
            this.winhelp.Location = new System.Drawing.Point(14, 111);
            this.winhelp.Name = "winhelp";
            this.winhelp.Size = new System.Drawing.Size(29, 12);
            this.winhelp.TabIndex = 3;
            this.winhelp.Text = "帮助";
            this.winhelp.MouseLeave += new System.EventHandler(this.openfile_MouseLeave);
            this.winhelp.Click += new System.EventHandler(this.winhelp_Click);
            this.winhelp.MouseEnter += new System.EventHandler(this.openfile_MouseEnter);
            // 
            // about
            // 
            this.about.AutoSize = true;
            this.about.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.about.ForeColor = System.Drawing.Color.Gainsboro;
            this.about.Location = new System.Drawing.Point(14, 134);
            this.about.Name = "about";
            this.about.Size = new System.Drawing.Size(29, 12);
            this.about.TabIndex = 4;
            this.about.Text = "关于";
            this.about.MouseLeave += new System.EventHandler(this.openfile_MouseLeave);
            this.about.Click += new System.EventHandler(this.about_Click);
            this.about.MouseEnter += new System.EventHandler(this.openfile_MouseEnter);
            // 
            // sepator
            // 
            this.sepator.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.sepator.Location = new System.Drawing.Point(0, 100);
            this.sepator.Name = "sepator";
            this.sepator.Size = new System.Drawing.Size(107, 1);
            this.sepator.TabIndex = 5;
            // 
            // clip
            // 
            this.clip.AutoSize = true;
            this.clip.ForeColor = System.Drawing.Color.Gainsboro;
            this.clip.Location = new System.Drawing.Point(11, 54);
            this.clip.Name = "clip";
            this.clip.Size = new System.Drawing.Size(53, 12);
            this.clip.TabIndex = 6;
            this.clip.Text = "录像剪辑";
            this.clip.MouseLeave += new System.EventHandler(this.openfile_MouseLeave);
            this.clip.Click += new System.EventHandler(this.clip_Click);
            this.clip.MouseEnter += new System.EventHandler(this.openfile_MouseEnter);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Controls.Add(this.clip);
            this.Controls.Add(this.sepator);
            this.Controls.Add(this.about);
            this.Controls.Add(this.winhelp);
            this.Controls.Add(this.opendir);
            this.Controls.Add(this.addfile);
            this.Controls.Add(this.openfile);
            this.Name = "Menu";
            this.Size = new System.Drawing.Size(107, 162);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label openfile;
        private System.Windows.Forms.Label addfile;
        private System.Windows.Forms.Label opendir;
        private System.Windows.Forms.Label winhelp;
        private System.Windows.Forms.Label about;
        private System.Windows.Forms.Label sepator;
        private System.Windows.Forms.Label clip;
    }
}
