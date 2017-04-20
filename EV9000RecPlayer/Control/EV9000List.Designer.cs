namespace EV9000RecPlayer.Control
{
    partial class EV9000List
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
            this.components = new System.ComponentModel.Container();
            this.carmera = new System.Windows.Forms.PictureBox();
            this.filename = new System.Windows.Forms.Label();
            this.filename2 = new System.Windows.Forms.Label();
            this.ttip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.carmera)).BeginInit();
            this.SuspendLayout();
            // 
            // carmera
            // 
            this.carmera.BackColor = System.Drawing.Color.Transparent;
            this.carmera.Image = global::EV9000RecPlayer.Properties.Resources.carmera;
            this.carmera.Location = new System.Drawing.Point(11, 9);
            this.carmera.Name = "carmera";
            this.carmera.Size = new System.Drawing.Size(16, 16);
            this.carmera.TabIndex = 0;
            this.carmera.TabStop = false;
            this.carmera.DoubleClick += new System.EventHandler(this.EV9000List_DoubleClick);
            this.carmera.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.EV9000List_MouseWheel);
            this.carmera.Click += new System.EventHandler(this.filename_Click);
            // 
            // filename
            // 
            this.filename.BackColor = System.Drawing.Color.Transparent;
            this.filename.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.filename.ForeColor = System.Drawing.Color.Gainsboro;
            this.filename.Location = new System.Drawing.Point(33, 10);
            this.filename.Name = "filename";
            this.filename.Size = new System.Drawing.Size(114, 16);
            this.filename.TabIndex = 1;
            this.filename.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.EV9000List_MouseWheel);
            this.filename.DoubleClick += new System.EventHandler(this.EV9000List_DoubleClick);
            this.filename.Click += new System.EventHandler(this.filename_Click);
            // 
            // filename2
            // 
            this.filename2.BackColor = System.Drawing.Color.Transparent;
            this.filename2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.filename2.ForeColor = System.Drawing.Color.Gainsboro;
            this.filename2.Location = new System.Drawing.Point(36, 30);
            this.filename2.Name = "filename2";
            this.filename2.Size = new System.Drawing.Size(111, 20);
            this.filename2.TabIndex = 2;
            this.filename2.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.EV9000List_MouseWheel);
            this.filename2.DoubleClick += new System.EventHandler(this.EV9000List_DoubleClick);
            this.filename2.Click += new System.EventHandler(this.filename_Click);
            // 
            // EV9000List
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::EV9000RecPlayer.Properties.Resources.listbg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.filename2);
            this.Controls.Add(this.filename);
            this.Controls.Add(this.carmera);
            this.Name = "EV9000List";
            this.Size = new System.Drawing.Size(150, 57);
            this.Load += new System.EventHandler(this.EV9000List_Load);
            this.DoubleClick += new System.EventHandler(this.EV9000List_DoubleClick);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.EV9000List_MouseWheel);
            this.Click += new System.EventHandler(this.filename_Click);
            this.Resize += new System.EventHandler(this.EV9000List_Resize);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EV9000List_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.carmera)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox carmera;
        private System.Windows.Forms.Label filename;
        private System.Windows.Forms.Label filename2;
        private System.Windows.Forms.ToolTip ttip;
    }
}
