using EV9000RecPlayer.Control;
using EV9000RecPlayer.EventHander;
namespace EV9000RecPlayer
{
    partial class S50SVRPlayer
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(S50SVRPlayer));
            this.TipMain = new System.Windows.Forms.ToolTip(this.components);
            this.scroll_timer = new System.Windows.Forms.Timer(this.components);
            this.hide_panel = new EV9000RecPlayer.Control.EV9000Panel();
            this.play_list_panel = new EV9000RecPlayer.Control.EV9000Panel();
            this.play_panel = new EV9000RecPlayer.Control.EV9000Panel();
            this.EV9000playpanel = new EV9000RecPlayer.Control.EV9000Panel();
            this.info_label = new System.Windows.Forms.Label();
            this.tool_panel = new EV9000RecPlayer.Control.EV9000Panel();
            this.pic_max = new System.Windows.Forms.PictureBox();
            this.pic_set = new System.Windows.Forms.PictureBox();
            this.pic_vol = new System.Windows.Forms.PictureBox();
            this.pic_play = new System.Windows.Forms.PictureBox();
            this.pic_next = new System.Windows.Forms.PictureBox();
            this.pic_forward = new System.Windows.Forms.PictureBox();
            this.pic_goback = new System.Windows.Forms.PictureBox();
            this.pic_preview = new System.Windows.Forms.PictureBox();
            this.pic_closeplay = new System.Windows.Forms.PictureBox();
            this.pic_record = new System.Windows.Forms.PictureBox();
            this.pic_capture = new System.Windows.Forms.PictureBox();
            this.play_progress = new EV9000RecPlayer.Control.ScrollBar();
            this.top_panel = new EV9000RecPlayer.Control.EV9000Panel();
            this.title = new System.Windows.Forms.Label();
            this.pic_logo = new System.Windows.Forms.PictureBox();
            this.pic_openmenu = new System.Windows.Forms.PictureBox();
            this.pic_hideplaylist = new System.Windows.Forms.PictureBox();
            this.pic_separate = new System.Windows.Forms.PictureBox();
            this.pic_normalmax = new System.Windows.Forms.PictureBox();
            this.pic_min = new System.Windows.Forms.PictureBox();
            this.pic_close = new System.Windows.Forms.PictureBox();
            this.play_panel.SuspendLayout();
            this.EV9000playpanel.SuspendLayout();
            this.tool_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_max)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_set)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_vol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_play)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_next)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_forward)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_goback)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_preview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_closeplay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_record)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_capture)).BeginInit();
            this.top_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_logo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_openmenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_hideplaylist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_separate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_normalmax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_min)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_close)).BeginInit();
            this.SuspendLayout();
            // 
            // scroll_timer
            // 
            this.scroll_timer.Interval = 500;
            this.scroll_timer.Tick += new System.EventHandler(this.scroll_timer_Tick);
            // 
            // hide_panel
            // 
            this.hide_panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.hide_panel.Location = new System.Drawing.Point(797, 45);
            this.hide_panel.Name = "hide_panel";
            this.hide_panel.Size = new System.Drawing.Size(10, 413);
            this.hide_panel.TabIndex = 4;
            this.hide_panel.Visible = false;
            this.hide_panel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.hide_panel_MouseClick);
            this.hide_panel.MouseEnter += new System.EventHandler(this.hide_panel_MouseEnter);
            this.hide_panel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.IEV9000Player_KeyDown);
            // 
            // play_list_panel
            // 
            this.play_list_panel.BackColor = System.Drawing.Color.Black;
            this.play_list_panel.Location = new System.Drawing.Point(809, 45);
            this.play_list_panel.Name = "play_list_panel";
            this.play_list_panel.Size = new System.Drawing.Size(200, 416);
            this.play_list_panel.TabIndex = 3;
            this.play_list_panel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.IEV9000Player_KeyDown);
            // 
            // play_panel
            // 
            this.play_panel.BackColor = System.Drawing.Color.Black;
            this.play_panel.Controls.Add(this.EV9000playpanel);
            this.play_panel.Location = new System.Drawing.Point(3, 45);
            this.play_panel.Name = "play_panel";
            this.play_panel.Size = new System.Drawing.Size(790, 416);
            this.play_panel.TabIndex = 2;
            this.play_panel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.IEV9000Player_KeyDown);
            // 
            // EV9000playpanel
            // 
            this.EV9000playpanel.AllowDrop = true;
            this.EV9000playpanel.Controls.Add(this.info_label);
            this.EV9000playpanel.Location = new System.Drawing.Point(30, 31);
            this.EV9000playpanel.Name = "EV9000playpanel";
            this.EV9000playpanel.Size = new System.Drawing.Size(716, 362);
            this.EV9000playpanel.TabIndex = 0;
            this.EV9000playpanel.DragOver += new System.Windows.Forms.DragEventHandler(this.EV9000playpanel_DragOver);
            this.EV9000playpanel.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.EV9000playpanel_MouseDoubleClick);
            this.EV9000playpanel.DragDrop += new System.Windows.Forms.DragEventHandler(this.EV9000playpanel_DragDrop);
            this.EV9000playpanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.EV9000playpanel_MouseClick);
            this.EV9000playpanel.DragEnter += new System.Windows.Forms.DragEventHandler(this.EV9000playpanel_DragEnter);
            // 
            // info_label
            // 
            this.info_label.AutoSize = true;
            this.info_label.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.info_label.ForeColor = System.Drawing.Color.White;
            this.info_label.Location = new System.Drawing.Point(304, 133);
            this.info_label.Name = "info_label";
            this.info_label.Size = new System.Drawing.Size(0, 20);
            this.info_label.TabIndex = 0;
            // 
            // tool_panel
            // 
            this.tool_panel.BackColor = System.Drawing.Color.Transparent;
            this.tool_panel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tool_panel.BackgroundImage")));
            this.tool_panel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tool_panel.Controls.Add(this.pic_max);
            this.tool_panel.Controls.Add(this.pic_set);
            this.tool_panel.Controls.Add(this.pic_vol);
            this.tool_panel.Controls.Add(this.pic_play);
            this.tool_panel.Controls.Add(this.pic_next);
            this.tool_panel.Controls.Add(this.pic_forward);
            this.tool_panel.Controls.Add(this.pic_goback);
            this.tool_panel.Controls.Add(this.pic_preview);
            this.tool_panel.Controls.Add(this.pic_closeplay);
            this.tool_panel.Controls.Add(this.pic_record);
            this.tool_panel.Controls.Add(this.pic_capture);
            this.tool_panel.Controls.Add(this.play_progress);
            this.tool_panel.Location = new System.Drawing.Point(0, 459);
            this.tool_panel.Name = "tool_panel";
            this.tool_panel.Size = new System.Drawing.Size(1012, 84);
            this.tool_panel.TabIndex = 1;
            this.tool_panel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.IEV9000Player_KeyDown);
            // 
            // pic_max
            // 
            this.pic_max.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pic_max.Image = ((System.Drawing.Image)(resources.GetObject("pic_max.Image")));
            this.pic_max.Location = new System.Drawing.Point(971, 38);
            this.pic_max.Name = "pic_max";
            this.pic_max.Size = new System.Drawing.Size(24, 24);
            this.pic_max.TabIndex = 11;
            this.pic_max.TabStop = false;
            this.pic_max.MouseLeave += new System.EventHandler(this.ToolControlMouseLeave);
            this.pic_max.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ToolControlClick);
            this.pic_max.MouseEnter += new System.EventHandler(this.ToolControlMouseEnter);
            this.pic_max.KeyDown += new System.Windows.Forms.KeyEventHandler(this.IEV9000Player_KeyDown);
            // 
            // pic_set
            // 
            this.pic_set.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pic_set.Image = ((System.Drawing.Image)(resources.GetObject("pic_set.Image")));
            this.pic_set.Location = new System.Drawing.Point(924, 38);
            this.pic_set.Name = "pic_set";
            this.pic_set.Size = new System.Drawing.Size(24, 24);
            this.pic_set.TabIndex = 10;
            this.pic_set.TabStop = false;
            this.pic_set.MouseLeave += new System.EventHandler(this.ToolControlMouseLeave);
            this.pic_set.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ToolControlClick);
            this.pic_set.MouseEnter += new System.EventHandler(this.ToolControlMouseEnter);
            this.pic_set.KeyDown += new System.Windows.Forms.KeyEventHandler(this.IEV9000Player_KeyDown);
            // 
            // pic_vol
            // 
            this.pic_vol.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pic_vol.Image = ((System.Drawing.Image)(resources.GetObject("pic_vol.Image")));
            this.pic_vol.Location = new System.Drawing.Point(878, 38);
            this.pic_vol.Name = "pic_vol";
            this.pic_vol.Size = new System.Drawing.Size(24, 24);
            this.pic_vol.TabIndex = 9;
            this.pic_vol.TabStop = false;
            this.pic_vol.MouseLeave += new System.EventHandler(this.ToolControlMouseLeave);
            this.pic_vol.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ToolControlClick);
            this.pic_vol.MouseEnter += new System.EventHandler(this.ToolControlMouseEnter);
            this.pic_vol.KeyDown += new System.Windows.Forms.KeyEventHandler(this.IEV9000Player_KeyDown);
            // 
            // pic_play
            // 
            this.pic_play.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pic_play.Image = ((System.Drawing.Image)(resources.GetObject("pic_play.Image")));
            this.pic_play.Location = new System.Drawing.Point(470, 38);
            this.pic_play.Name = "pic_play";
            this.pic_play.Size = new System.Drawing.Size(24, 24);
            this.pic_play.TabIndex = 8;
            this.pic_play.TabStop = false;
            this.pic_play.MouseLeave += new System.EventHandler(this.ToolControlMouseLeave);
            this.pic_play.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ToolControlClick);
            this.pic_play.MouseEnter += new System.EventHandler(this.ToolControlMouseEnter);
            this.pic_play.KeyDown += new System.Windows.Forms.KeyEventHandler(this.IEV9000Player_KeyDown);
            // 
            // pic_next
            // 
            this.pic_next.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pic_next.Image = ((System.Drawing.Image)(resources.GetObject("pic_next.Image")));
            this.pic_next.Location = new System.Drawing.Point(561, 38);
            this.pic_next.Name = "pic_next";
            this.pic_next.Size = new System.Drawing.Size(24, 24);
            this.pic_next.TabIndex = 7;
            this.pic_next.TabStop = false;
            this.pic_next.MouseLeave += new System.EventHandler(this.ToolControlMouseLeave);
            this.pic_next.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ToolControlClick);
            this.pic_next.MouseEnter += new System.EventHandler(this.ToolControlMouseEnter);
            this.pic_next.KeyDown += new System.Windows.Forms.KeyEventHandler(this.IEV9000Player_KeyDown);
            // 
            // pic_forward
            // 
            this.pic_forward.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pic_forward.Image = ((System.Drawing.Image)(resources.GetObject("pic_forward.Image")));
            this.pic_forward.Location = new System.Drawing.Point(514, 38);
            this.pic_forward.Name = "pic_forward";
            this.pic_forward.Size = new System.Drawing.Size(24, 24);
            this.pic_forward.TabIndex = 6;
            this.pic_forward.TabStop = false;
            this.pic_forward.MouseLeave += new System.EventHandler(this.ToolControlMouseLeave);
            this.pic_forward.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ToolControlClick);
            this.pic_forward.MouseEnter += new System.EventHandler(this.ToolControlMouseEnter);
            this.pic_forward.KeyDown += new System.Windows.Forms.KeyEventHandler(this.IEV9000Player_KeyDown);
            // 
            // pic_goback
            // 
            this.pic_goback.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pic_goback.Image = ((System.Drawing.Image)(resources.GetObject("pic_goback.Image")));
            this.pic_goback.Location = new System.Drawing.Point(430, 38);
            this.pic_goback.Name = "pic_goback";
            this.pic_goback.Size = new System.Drawing.Size(24, 24);
            this.pic_goback.TabIndex = 5;
            this.pic_goback.TabStop = false;
            this.pic_goback.MouseLeave += new System.EventHandler(this.ToolControlMouseLeave);
            this.pic_goback.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ToolControlClick);
            this.pic_goback.MouseEnter += new System.EventHandler(this.ToolControlMouseEnter);
            this.pic_goback.KeyDown += new System.Windows.Forms.KeyEventHandler(this.IEV9000Player_KeyDown);
            // 
            // pic_preview
            // 
            this.pic_preview.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pic_preview.Image = ((System.Drawing.Image)(resources.GetObject("pic_preview.Image")));
            this.pic_preview.Location = new System.Drawing.Point(390, 38);
            this.pic_preview.Name = "pic_preview";
            this.pic_preview.Size = new System.Drawing.Size(24, 24);
            this.pic_preview.TabIndex = 4;
            this.pic_preview.TabStop = false;
            this.pic_preview.MouseLeave += new System.EventHandler(this.ToolControlMouseLeave);
            this.pic_preview.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ToolControlClick);
            this.pic_preview.MouseEnter += new System.EventHandler(this.ToolControlMouseEnter);
            this.pic_preview.KeyDown += new System.Windows.Forms.KeyEventHandler(this.IEV9000Player_KeyDown);
            // 
            // pic_closeplay
            // 
            this.pic_closeplay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pic_closeplay.Image = ((System.Drawing.Image)(resources.GetObject("pic_closeplay.Image")));
            this.pic_closeplay.Location = new System.Drawing.Point(110, 41);
            this.pic_closeplay.Name = "pic_closeplay";
            this.pic_closeplay.Size = new System.Drawing.Size(24, 24);
            this.pic_closeplay.TabIndex = 3;
            this.pic_closeplay.TabStop = false;
            this.pic_closeplay.MouseLeave += new System.EventHandler(this.ToolControlMouseLeave);
            this.pic_closeplay.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ToolControlClick);
            this.pic_closeplay.MouseEnter += new System.EventHandler(this.ToolControlMouseEnter);
            this.pic_closeplay.KeyDown += new System.Windows.Forms.KeyEventHandler(this.IEV9000Player_KeyDown);
            // 
            // pic_record
            // 
            this.pic_record.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pic_record.Image = global::EV9000RecPlayer.Properties.Resources.record;
            this.pic_record.Location = new System.Drawing.Point(63, 41);
            this.pic_record.Name = "pic_record";
            this.pic_record.Size = new System.Drawing.Size(24, 24);
            this.pic_record.TabIndex = 2;
            this.pic_record.TabStop = false;
            this.pic_record.MouseLeave += new System.EventHandler(this.ToolControlMouseLeave);
            this.pic_record.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ToolControlClick);
            this.pic_record.MouseEnter += new System.EventHandler(this.ToolControlMouseEnter);
            this.pic_record.KeyDown += new System.Windows.Forms.KeyEventHandler(this.IEV9000Player_KeyDown);
            // 
            // pic_capture
            // 
            this.pic_capture.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pic_capture.Image = ((System.Drawing.Image)(resources.GetObject("pic_capture.Image")));
            this.pic_capture.Location = new System.Drawing.Point(17, 41);
            this.pic_capture.Name = "pic_capture";
            this.pic_capture.Size = new System.Drawing.Size(24, 24);
            this.pic_capture.TabIndex = 1;
            this.pic_capture.TabStop = false;
            this.pic_capture.MouseLeave += new System.EventHandler(this.ToolControlMouseLeave);
            this.pic_capture.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ToolControlClick);
            this.pic_capture.MouseEnter += new System.EventHandler(this.ToolControlMouseEnter);
            this.pic_capture.KeyDown += new System.Windows.Forms.KeyEventHandler(this.IEV9000Player_KeyDown);
            // 
            // play_progress
            // 
            this.play_progress.BackColor = System.Drawing.Color.Transparent;
            this.play_progress.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("play_progress.BackgroundImage")));
            this.play_progress.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.play_progress.Location = new System.Drawing.Point(3, 9);
            this.play_progress.Name = "play_progress";
            this.play_progress.Size = new System.Drawing.Size(1003, 20);
            this.play_progress.TabIndex = 0;
            this.play_progress.ScrollBarMouseUp += new System.Windows.Forms.MouseEventHandler(this.play_progress_ScrollBarMouseUp);
            this.play_progress.ScrollBarMouseDown += new System.Windows.Forms.MouseEventHandler(this.play_progress_ScrollBarMouseDown);
            this.play_progress.Scroll += new EV9000RecPlayer.EventHander.EVENTHander(this.play_progress_Scroll);
            this.play_progress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.IEV9000Player_KeyDown);
            // 
            // top_panel
            // 
            this.top_panel.BackColor = System.Drawing.Color.Transparent;
            this.top_panel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("top_panel.BackgroundImage")));
            this.top_panel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.top_panel.Controls.Add(this.title);
            this.top_panel.Controls.Add(this.pic_logo);
            this.top_panel.Controls.Add(this.pic_openmenu);
            this.top_panel.Controls.Add(this.pic_hideplaylist);
            this.top_panel.Controls.Add(this.pic_separate);
            this.top_panel.Controls.Add(this.pic_normalmax);
            this.top_panel.Controls.Add(this.pic_min);
            this.top_panel.Controls.Add(this.pic_close);
            this.top_panel.Location = new System.Drawing.Point(0, 0);
            this.top_panel.Name = "top_panel";
            this.top_panel.Size = new System.Drawing.Size(1006, 45);
            this.top_panel.TabIndex = 0;
            this.top_panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.IEV9000Player_MouseDown);
            this.top_panel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.IEV9000Player_KeyDown);
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.title.ForeColor = System.Drawing.Color.White;
            this.title.Location = new System.Drawing.Point(46, 9);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(143, 25);
            this.title.TabIndex = 7;
            this.title.Text = "S50SVR Player";
            this.title.KeyDown += new System.Windows.Forms.KeyEventHandler(this.IEV9000Player_KeyDown);
            // 
            // pic_logo
            // 
            this.pic_logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pic_logo.Image = global::EV9000RecPlayer.Properties.Resources.HwAlarmSvr;
            this.pic_logo.Location = new System.Drawing.Point(12, 7);
            this.pic_logo.Name = "pic_logo";
            this.pic_logo.Size = new System.Drawing.Size(30, 30);
            this.pic_logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_logo.TabIndex = 6;
            this.pic_logo.TabStop = false;
            this.pic_logo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.IEV9000Player_KeyDown);
            // 
            // pic_openmenu
            // 
            this.pic_openmenu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pic_openmenu.Image = ((System.Drawing.Image)(resources.GetObject("pic_openmenu.Image")));
            this.pic_openmenu.Location = new System.Drawing.Point(797, 10);
            this.pic_openmenu.Name = "pic_openmenu";
            this.pic_openmenu.Size = new System.Drawing.Size(20, 20);
            this.pic_openmenu.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_openmenu.TabIndex = 5;
            this.pic_openmenu.TabStop = false;
            this.pic_openmenu.MouseLeave += new System.EventHandler(this.pic_close_MouseLeave);
            this.pic_openmenu.Click += new System.EventHandler(this.pic_close_Click);
            this.pic_openmenu.MouseEnter += new System.EventHandler(this.pic_close_MouseEnter);
            this.pic_openmenu.KeyDown += new System.Windows.Forms.KeyEventHandler(this.IEV9000Player_KeyDown);
            // 
            // pic_hideplaylist
            // 
            this.pic_hideplaylist.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pic_hideplaylist.Image = ((System.Drawing.Image)(resources.GetObject("pic_hideplaylist.Image")));
            this.pic_hideplaylist.Location = new System.Drawing.Point(836, 10);
            this.pic_hideplaylist.Name = "pic_hideplaylist";
            this.pic_hideplaylist.Size = new System.Drawing.Size(20, 20);
            this.pic_hideplaylist.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_hideplaylist.TabIndex = 4;
            this.pic_hideplaylist.TabStop = false;
            this.pic_hideplaylist.MouseLeave += new System.EventHandler(this.pic_close_MouseLeave);
            this.pic_hideplaylist.Click += new System.EventHandler(this.pic_close_Click);
            this.pic_hideplaylist.MouseEnter += new System.EventHandler(this.pic_close_MouseEnter);
            this.pic_hideplaylist.KeyDown += new System.Windows.Forms.KeyEventHandler(this.IEV9000Player_KeyDown);
            // 
            // pic_separate
            // 
            this.pic_separate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pic_separate.Image = ((System.Drawing.Image)(resources.GetObject("pic_separate.Image")));
            this.pic_separate.Location = new System.Drawing.Point(873, 10);
            this.pic_separate.Name = "pic_separate";
            this.pic_separate.Size = new System.Drawing.Size(20, 20);
            this.pic_separate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_separate.TabIndex = 3;
            this.pic_separate.TabStop = false;
            this.pic_separate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.IEV9000Player_KeyDown);
            // 
            // pic_normalmax
            // 
            this.pic_normalmax.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pic_normalmax.Image = ((System.Drawing.Image)(resources.GetObject("pic_normalmax.Image")));
            this.pic_normalmax.Location = new System.Drawing.Point(941, 10);
            this.pic_normalmax.Name = "pic_normalmax";
            this.pic_normalmax.Size = new System.Drawing.Size(20, 20);
            this.pic_normalmax.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_normalmax.TabIndex = 2;
            this.pic_normalmax.TabStop = false;
            this.pic_normalmax.MouseLeave += new System.EventHandler(this.pic_close_MouseLeave);
            this.pic_normalmax.Click += new System.EventHandler(this.pic_close_Click);
            this.pic_normalmax.MouseEnter += new System.EventHandler(this.pic_close_MouseEnter);
            this.pic_normalmax.KeyDown += new System.Windows.Forms.KeyEventHandler(this.IEV9000Player_KeyDown);
            // 
            // pic_min
            // 
            this.pic_min.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pic_min.Image = ((System.Drawing.Image)(resources.GetObject("pic_min.Image")));
            this.pic_min.Location = new System.Drawing.Point(909, 10);
            this.pic_min.Name = "pic_min";
            this.pic_min.Size = new System.Drawing.Size(20, 20);
            this.pic_min.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_min.TabIndex = 1;
            this.pic_min.TabStop = false;
            this.pic_min.MouseLeave += new System.EventHandler(this.pic_close_MouseLeave);
            this.pic_min.Click += new System.EventHandler(this.pic_close_Click);
            this.pic_min.MouseEnter += new System.EventHandler(this.pic_close_MouseEnter);
            this.pic_min.KeyDown += new System.Windows.Forms.KeyEventHandler(this.IEV9000Player_KeyDown);
            // 
            // pic_close
            // 
            this.pic_close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pic_close.Image = ((System.Drawing.Image)(resources.GetObject("pic_close.Image")));
            this.pic_close.Location = new System.Drawing.Point(973, 10);
            this.pic_close.Name = "pic_close";
            this.pic_close.Size = new System.Drawing.Size(20, 20);
            this.pic_close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_close.TabIndex = 0;
            this.pic_close.TabStop = false;
            this.pic_close.MouseLeave += new System.EventHandler(this.pic_close_MouseLeave);
            this.pic_close.Click += new System.EventHandler(this.pic_close_Click);
            this.pic_close.MouseEnter += new System.EventHandler(this.pic_close_MouseEnter);
            this.pic_close.KeyDown += new System.Windows.Forms.KeyEventHandler(this.IEV9000Player_KeyDown);
            // 
            // S50SVRPlayer
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1009, 541);
            this.Controls.Add(this.hide_panel);
            this.Controls.Add(this.play_list_panel);
            this.Controls.Add(this.play_panel);
            this.Controls.Add(this.tool_panel);
            this.Controls.Add(this.top_panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "S50SVRPlayer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "S50SVR Player";
            this.TransparencyKey = System.Drawing.SystemColors.Info;
            this.Load += new System.EventHandler(this.IEV9000Player_Load);
            this.SizeChanged += new System.EventHandler(this.IEV9000Player_SizeChanged);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.IEV9000Player_MouseDown);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.IEV9000Player_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.IEV9000Player_KeyDown);
            this.play_panel.ResumeLayout(false);
            this.EV9000playpanel.ResumeLayout(false);
            this.EV9000playpanel.PerformLayout();
            this.tool_panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_max)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_set)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_vol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_play)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_next)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_forward)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_goback)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_preview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_closeplay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_record)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_capture)).EndInit();
            this.top_panel.ResumeLayout(false);
            this.top_panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_logo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_openmenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_hideplaylist)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_separate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_normalmax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_min)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_close)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private EV9000Panel top_panel;
        private EV9000Panel tool_panel;
        private EV9000Panel play_list_panel;
        private EV9000Panel hide_panel;
        private System.Windows.Forms.ToolTip TipMain;
        private ScrollBar play_progress;
        private System.Windows.Forms.PictureBox pic_close;
        private System.Windows.Forms.PictureBox pic_normalmax;
        private System.Windows.Forms.PictureBox pic_min;
        private System.Windows.Forms.PictureBox pic_openmenu;
        private System.Windows.Forms.PictureBox pic_hideplaylist;
        private System.Windows.Forms.PictureBox pic_separate;
        private System.Windows.Forms.PictureBox pic_capture;
        private System.Windows.Forms.PictureBox pic_record;
        private System.Windows.Forms.PictureBox pic_closeplay;
        private System.Windows.Forms.PictureBox pic_preview;
        private System.Windows.Forms.PictureBox pic_next;
        private System.Windows.Forms.PictureBox pic_forward;
        private System.Windows.Forms.PictureBox pic_goback;
        private System.Windows.Forms.PictureBox pic_play;
        private System.Windows.Forms.PictureBox pic_max;
        private System.Windows.Forms.PictureBox pic_set;
        private System.Windows.Forms.PictureBox pic_vol;
        private System.Windows.Forms.PictureBox pic_logo;
        private System.Windows.Forms.Label title;
        private EV9000Panel play_panel;
        private EV9000Panel EV9000playpanel;
        private System.Windows.Forms.Label info_label;
        private System.Windows.Forms.Timer scroll_timer;
    }
}

