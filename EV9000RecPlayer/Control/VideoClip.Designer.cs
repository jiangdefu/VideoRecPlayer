namespace EV9000RecPlayer.Control
{
    partial class VideoClip
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
            this.clip_play_panel = new System.Windows.Forms.Panel();
            this.info_label = new System.Windows.Forms.Label();
            this.pic_close = new System.Windows.Forms.PictureBox();
            this.clip_title = new System.Windows.Forms.Label();
            this.btn_start = new System.Windows.Forms.Label();
            this.btn_end = new System.Windows.Forms.Label();
            this.clipFilepath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_ok = new System.Windows.Forms.Label();
            this.speed_panel = new System.Windows.Forms.Panel();
            this.label_ok = new System.Windows.Forms.Label();
            this.speedBar = new System.Windows.Forms.TrackBar();
            this.clip_play_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_close)).BeginInit();
            this.speed_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.speedBar)).BeginInit();
            this.SuspendLayout();
            // 
            // clip_play_panel
            // 
            this.clip_play_panel.BackColor = System.Drawing.Color.Gainsboro;
            this.clip_play_panel.Controls.Add(this.info_label);
            this.clip_play_panel.Location = new System.Drawing.Point(3, 31);
            this.clip_play_panel.Name = "clip_play_panel";
            this.clip_play_panel.Size = new System.Drawing.Size(637, 299);
            this.clip_play_panel.TabIndex = 0;
            this.clip_play_panel.Click += new System.EventHandler(this.clip_play_panel_Click);
            // 
            // info_label
            // 
            this.info_label.AutoSize = true;
            this.info_label.BackColor = System.Drawing.Color.Black;
            this.info_label.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.info_label.ForeColor = System.Drawing.Color.White;
            this.info_label.Location = new System.Drawing.Point(293, 127);
            this.info_label.Name = "info_label";
            this.info_label.Size = new System.Drawing.Size(0, 17);
            this.info_label.TabIndex = 0;
            // 
            // pic_close
            // 
            this.pic_close.BackColor = System.Drawing.Color.Transparent;
            this.pic_close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pic_close.Image = global::EV9000RecPlayer.Properties.Resources.close;
            this.pic_close.InitialImage = global::EV9000RecPlayer.Properties.Resources.close;
            this.pic_close.Location = new System.Drawing.Point(618, 7);
            this.pic_close.Name = "pic_close";
            this.pic_close.Size = new System.Drawing.Size(16, 16);
            this.pic_close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_close.TabIndex = 1;
            this.pic_close.TabStop = false;
            this.pic_close.Click += new System.EventHandler(this.pic_close_Click);
            // 
            // clip_title
            // 
            this.clip_title.AutoSize = true;
            this.clip_title.BackColor = System.Drawing.Color.Transparent;
            this.clip_title.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.clip_title.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.clip_title.Location = new System.Drawing.Point(6, 5);
            this.clip_title.Name = "clip_title";
            this.clip_title.Size = new System.Drawing.Size(65, 20);
            this.clip_title.TabIndex = 2;
            this.clip_title.Text = "录像剪辑";
            // 
            // btn_start
            // 
            this.btn_start.AutoSize = true;
            this.btn_start.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btn_start.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_start.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_start.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_start.Location = new System.Drawing.Point(10, 345);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(58, 19);
            this.btn_start.TabIndex = 3;
            this.btn_start.Text = "开始位置";
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // btn_end
            // 
            this.btn_end.AutoSize = true;
            this.btn_end.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btn_end.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_end.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_end.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_end.Location = new System.Drawing.Point(78, 345);
            this.btn_end.Name = "btn_end";
            this.btn_end.Size = new System.Drawing.Size(58, 19);
            this.btn_end.TabIndex = 4;
            this.btn_end.Text = "结束位置";
            this.btn_end.Click += new System.EventHandler(this.btn_end_Click);
            // 
            // clipFilepath
            // 
            this.clipFilepath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.clipFilepath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.clipFilepath.Location = new System.Drawing.Point(199, 345);
            this.clipFilepath.Name = "clipFilepath";
            this.clipFilepath.Size = new System.Drawing.Size(405, 21);
            this.clipFilepath.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(140, 349);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "文件位置";
            // 
            // btn_ok
            // 
            this.btn_ok.AutoSize = true;
            this.btn_ok.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btn_ok.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_ok.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_ok.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_ok.ForeColor = System.Drawing.Color.White;
            this.btn_ok.Location = new System.Drawing.Point(603, 346);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(34, 19);
            this.btn_ok.TabIndex = 8;
            this.btn_ok.Text = "完成";
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // speed_panel
            // 
            this.speed_panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.speed_panel.Controls.Add(this.label_ok);
            this.speed_panel.Controls.Add(this.speedBar);
            this.speed_panel.Location = new System.Drawing.Point(5, 290);
            this.speed_panel.Name = "speed_panel";
            this.speed_panel.Size = new System.Drawing.Size(631, 32);
            this.speed_panel.TabIndex = 0;
            // 
            // label_ok
            // 
            this.label_ok.AutoSize = true;
            this.label_ok.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_ok.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label_ok.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_ok.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label_ok.Location = new System.Drawing.Point(595, 8);
            this.label_ok.Name = "label_ok";
            this.label_ok.Size = new System.Drawing.Size(34, 19);
            this.label_ok.TabIndex = 7;
            this.label_ok.Text = "确定";
            this.label_ok.Click += new System.EventHandler(this.label_ok_Click);
            // 
            // speedBar
            // 
            this.speedBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.speedBar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.speedBar.Location = new System.Drawing.Point(-4, 6);
            this.speedBar.Maximum = 1000;
            this.speedBar.Name = "speedBar";
            this.speedBar.Size = new System.Drawing.Size(603, 45);
            this.speedBar.TabIndex = 6;
            this.speedBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.speedBar.Scroll += new System.EventHandler(this.speedBar_Scroll);
            // 
            // VideoClip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.speed_panel);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.clipFilepath);
            this.Controls.Add(this.btn_end);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.clip_title);
            this.Controls.Add(this.pic_close);
            this.Controls.Add(this.clip_play_panel);
            this.Name = "VideoClip";
            this.Size = new System.Drawing.Size(643, 370);
            this.Load += new System.EventHandler(this.VideoClip_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.VideoClip_MouseDown);
            this.clip_play_panel.ResumeLayout(false);
            this.clip_play_panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_close)).EndInit();
            this.speed_panel.ResumeLayout(false);
            this.speed_panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.speedBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel clip_play_panel;
        private System.Windows.Forms.PictureBox pic_close;
        private System.Windows.Forms.Label clip_title;
        private System.Windows.Forms.Label btn_start;
        private System.Windows.Forms.Label btn_end;
        private System.Windows.Forms.TextBox clipFilepath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label btn_ok;
        private System.Windows.Forms.Panel speed_panel;
        private System.Windows.Forms.TrackBar speedBar;
        private System.Windows.Forms.Label label_ok;
        private System.Windows.Forms.Label info_label;
    }
}
