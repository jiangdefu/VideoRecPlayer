using System.Drawing;
namespace EV9000RecPlayer.Control
{
    partial class EV9000PlayList
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
            this.pic_down = new System.Windows.Forms.PictureBox();
            this.pic_up = new System.Windows.Forms.PictureBox();
            this.toolop_panel = new EV9000RecPlayer.Control.EV9000Panel();
            this.pic_clear = new System.Windows.Forms.PictureBox();
            this.pic_remove = new System.Windows.Forms.PictureBox();
            this.pic_add = new System.Windows.Forms.PictureBox();
            this.scroll_panel = new EV9000RecPlayer.Control.EV9000Panel();
            this.scroll = new EV9000RecPlayer.Control.EV9000Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pic_down)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_up)).BeginInit();
            this.toolop_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_clear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_remove)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_add)).BeginInit();
            this.scroll_panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // pic_down
            // 
            this.pic_down.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pic_down.Image = global::EV9000RecPlayer.Properties.Resources.arrow_down;
            this.pic_down.Location = new System.Drawing.Point(131, 350);
            this.pic_down.Name = "pic_down";
            this.pic_down.Size = new System.Drawing.Size(8, 8);
            this.pic_down.TabIndex = 1;
            this.pic_down.TabStop = false;
            this.pic_down.MouseLeave += new System.EventHandler(this.pic_add_MouseLeave);
            this.pic_down.MouseEnter += new System.EventHandler(this.pic_add_MouseEnter);
            // 
            // pic_up
            // 
            this.pic_up.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pic_up.Image = global::EV9000RecPlayer.Properties.Resources.arrow_up;
            this.pic_up.Location = new System.Drawing.Point(131, 0);
            this.pic_up.Name = "pic_up";
            this.pic_up.Size = new System.Drawing.Size(8, 8);
            this.pic_up.TabIndex = 0;
            this.pic_up.TabStop = false;
            this.pic_up.MouseLeave += new System.EventHandler(this.pic_add_MouseLeave);
            this.pic_up.MouseEnter += new System.EventHandler(this.pic_add_MouseEnter);
            // 
            // toolop_panel
            // 
            this.toolop_panel.BackColor = System.Drawing.Color.Transparent;
            this.toolop_panel.Controls.Add(this.pic_clear);
            this.toolop_panel.Controls.Add(this.pic_remove);
            this.toolop_panel.Controls.Add(this.pic_add);
            this.toolop_panel.Location = new System.Drawing.Point(0, 364);
            this.toolop_panel.Name = "toolop_panel";
            this.toolop_panel.Size = new System.Drawing.Size(141, 20);
            this.toolop_panel.TabIndex = 3;
            // 
            // pic_clear
            // 
            this.pic_clear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pic_clear.Image = global::EV9000RecPlayer.Properties.Resources.clear;
            this.pic_clear.Location = new System.Drawing.Point(121, 2);
            this.pic_clear.Name = "pic_clear";
            this.pic_clear.Size = new System.Drawing.Size(16, 16);
            this.pic_clear.TabIndex = 2;
            this.pic_clear.TabStop = false;
            this.pic_clear.MouseLeave += new System.EventHandler(this.pic_add_MouseLeave);
            this.pic_clear.Click += new System.EventHandler(this.pic_clear_Click);
            this.pic_clear.MouseEnter += new System.EventHandler(this.pic_add_MouseEnter);
            // 
            // pic_remove
            // 
            this.pic_remove.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pic_remove.Image = global::EV9000RecPlayer.Properties.Resources.remove;
            this.pic_remove.Location = new System.Drawing.Point(29, 2);
            this.pic_remove.Name = "pic_remove";
            this.pic_remove.Size = new System.Drawing.Size(16, 16);
            this.pic_remove.TabIndex = 1;
            this.pic_remove.TabStop = false;
            this.pic_remove.MouseLeave += new System.EventHandler(this.pic_add_MouseLeave);
            this.pic_remove.Click += new System.EventHandler(this.pic_remove_Click);
            this.pic_remove.MouseEnter += new System.EventHandler(this.pic_add_MouseEnter);
            // 
            // pic_add
            // 
            this.pic_add.BackColor = System.Drawing.Color.Transparent;
            this.pic_add.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pic_add.Image = global::EV9000RecPlayer.Properties.Resources.add;
            this.pic_add.Location = new System.Drawing.Point(2, 2);
            this.pic_add.Name = "pic_add";
            this.pic_add.Size = new System.Drawing.Size(16, 16);
            this.pic_add.TabIndex = 0;
            this.pic_add.TabStop = false;
            this.pic_add.MouseLeave += new System.EventHandler(this.pic_add_MouseLeave);
            this.pic_add.Click += new System.EventHandler(this.pic_add_Click);
            this.pic_add.MouseEnter += new System.EventHandler(this.pic_add_MouseEnter);
            // 
            // scroll_panel
            // 
            this.scroll_panel.BackgroundImage = global::EV9000RecPlayer.Properties.Resources.listscroll_bg;
            this.scroll_panel.Controls.Add(this.scroll);
            this.scroll_panel.Location = new System.Drawing.Point(132, 13);
            this.scroll_panel.Name = "scroll_panel";
            this.scroll_panel.Size = new System.Drawing.Size(8, 333);
            this.scroll_panel.TabIndex = 2;
            // 
            // scroll
            // 
            this.scroll.BackColor = System.Drawing.Color.Transparent;
            this.scroll.BackgroundImage = global::EV9000RecPlayer.Properties.Resources.listscrollbar;
            this.scroll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.scroll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.scroll.Location = new System.Drawing.Point(0, 0);
            this.scroll.Name = "scroll";
            this.scroll.Size = new System.Drawing.Size(8, 1);
            this.scroll.TabIndex = 0;
            this.scroll.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.EV9000PlayList_MouseWheel);
            this.scroll.MouseMove += new System.Windows.Forms.MouseEventHandler(this.EV9000PlayList_MouseMove);
            this.scroll.MouseDown += new System.Windows.Forms.MouseEventHandler(this.scroll_MouseDown);
            this.scroll.MouseUp += new System.Windows.Forms.MouseEventHandler(this.scroll_MouseUp);
            // 
            // EV9000PlayList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.Controls.Add(this.toolop_panel);
            this.Controls.Add(this.scroll_panel);
            this.Controls.Add(this.pic_down);
            this.Controls.Add(this.pic_up);
            this.Name = "EV9000PlayList";
            this.Size = new System.Drawing.Size(141, 384);
            this.Load += new System.EventHandler(this.EV9000PlayList_Load);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.EV9000PlayList_MouseMove);
            this.SizeChanged += new System.EventHandler(this.EV9000PlayList_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EV9000PlayList_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pic_down)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_up)).EndInit();
            this.toolop_panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_clear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_remove)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_add)).EndInit();
            this.scroll_panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pic_up;
        private System.Windows.Forms.PictureBox pic_down;
        private EV9000Panel scroll_panel;
        private EV9000Panel toolop_panel;
        private System.Windows.Forms.PictureBox pic_add;
        private System.Windows.Forms.PictureBox pic_clear;
        private System.Windows.Forms.PictureBox pic_remove;
        private EV9000Panel scroll;
    }
}
