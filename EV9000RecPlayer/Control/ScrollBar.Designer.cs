namespace EV9000RecPlayer.Control
{
    partial class ScrollBar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScrollBar));
            this.scrollmove_panel = new EV9000RecPlayer.Control.EV9000Panel();
            this.move_panel = new EV9000RecPlayer.Control.EV9000Panel();
            this.SuspendLayout();
            // 
            // scrollmove_panel
            // 
            this.scrollmove_panel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("scrollmove_panel.BackgroundImage")));
            this.scrollmove_panel.Location = new System.Drawing.Point(0, 0);
            this.scrollmove_panel.Name = "scrollmove_panel";
            this.scrollmove_panel.Size = new System.Drawing.Size(200, 20);
            this.scrollmove_panel.TabIndex = 0;
            this.scrollmove_panel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ScrollBar_MouseMove);
            // 
            // move_panel
            // 
            this.move_panel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("move_panel.BackgroundImage")));
            this.move_panel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.move_panel.Location = new System.Drawing.Point(202, 0);
            this.move_panel.Name = "move_panel";
            this.move_panel.Size = new System.Drawing.Size(20, 20);
            this.move_panel.TabIndex = 1;
            this.move_panel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ScrollBar_MouseMove);
            this.move_panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.move_panel_MouseDown);
            this.move_panel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.move_panel_MouseUp);
            // 
            // ScrollBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.scrollmove_panel);
            this.Controls.Add(this.move_panel);
            this.Name = "ScrollBar";
            this.Size = new System.Drawing.Size(713, 20);
            this.Load += new System.EventHandler(this.ScrollBar_Load);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ScrollBar_MouseMove);
            this.SizeChanged += new System.EventHandler(this.ScrollBar_SizeChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private EV9000Panel scrollmove_panel;
        private EV9000Panel move_panel;

    }
}
