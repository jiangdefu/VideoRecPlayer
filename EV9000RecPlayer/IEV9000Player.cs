using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using EV9000Player.Util;
using EV9000RecPlayer.Control;
using EV9000RecPlayer.Event;
using CSharpEV9000APP;
using System.Threading;
namespace EV9000RecPlayer
{
    public partial class S50SVRPlayer : Form
    {
        public EV9000Player.Util.RunLogReport     m_log;                                    //日志记录
        public IntPtr           play_hwnd = new IntPtr(0);                                  //播放窗口句柄
        public int              playHandle = -1;                                            //播放窗口对象
        public bool             playHwndisMax = false;                                      //判断当前状态是否全屏播放
        public int              playWnd_width = 0;                                          //全屏之前窗口宽度
        public int              playWnd_height = 0;                                         //全屏之前窗口高度

        //当前播放状态
        public bool             isvideoplay = false;                                        //当前播放状态是否正在播放
        public bool             isvideorecord = false;                                      //是否正在录像
        public bool             isaduio = false;                                            //当前声音是否打开

        //播放列表是否隐藏
        public bool             ishidePlayList = false;                                     //播放列表是否隐藏

      

        public int              controlSpace;                                               //各个操作之间的距离

        public Menu             openMenu;                                                   //打开文件菜单

        public EV9000PlayList   playList;                                                   //播放列表   

        public CSEV9000APP      EV9000App;                                                  //EV9000APP库对象

        public MaxPlayWindows   maxWindows;                                                 //全屏窗口

        public EV9000_PLAYBACK_SPEED playSpeed;                                             //当前视频播放速度

        public ABOUUTEV9000 about;                                                          //关于对话框

        public EV9000Config config;                                                         //配置对话框

        public int scorllWidht = 0;                                                         //滚动距离

        public List<string> fileTypeList;                                                   //视频文件类型

        public VideoClip clipVedio;                                                         //视频剪辑面板

        public string currentFilepath = "";                                                 //当前播放的文件
        //窗体拖动代码
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        /// <summary>
        /// 初始化对象
        /// </summary>
        public S50SVRPlayer()
        {
            /*****************************************/
            //防止界面出现闪烁现象
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
            /*****************************************/
            
            m_log = new EV9000Player.Util.RunLogReport("EV9000Player",30,false);

            fileTypeList = new List<string>();
            fileTypeList.Add("wvr");
            fileTypeList.Add("avi");
            fileTypeList.Add("h264");
            controlSpace = 15;
            openMenu = new Menu(this);
            playList = new EV9000PlayList(this);
            playList.MouseClick += new MouseEventHandler(EV9000playpanel_MouseClick);
            EV9000App = new CSEV9000APP();
            maxWindows = new MaxPlayWindows(this);
            playSpeed = EV9000_PLAYBACK_SPEED.EV9000_PLAYBACK_SPEED_NORMAL;
            about = new ABOUUTEV9000();
            config = new EV9000Config();
            InitializeComponent();
        }
        ///////////////////////////////////////////////////////////////////////
        //重写Windows 消息处理函数，用于不带标题的窗口移动
        private const int WM_NCHITTEST = 0x0084;
        private const int HTLEFT = 10;
        private const int HTRIGHT = 11;
        private const int HTTOP = 12;
        private const int HTTOPLEFT = 13;
        private const int HTTOPRIGHT = 14;
        private const int HTBOTTOM = 15;
        private const int HTBOTTOMLEFT = 0x10;
        private const int HTBOTTOMRIGHT = 17;
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    Point vPoint = new Point((int)m.LParam & 0xFFFF,
                        (int)m.LParam >> 16 & 0xFFFF);
                    vPoint = PointToClient(vPoint);
                    if (vPoint.X <= 5)
                        if (vPoint.Y <= 5)
                            m.Result = (IntPtr)HTTOPLEFT;
                        else if (vPoint.Y >= ClientSize.Height - 5)
                            m.Result = (IntPtr)HTBOTTOMLEFT;
                        else m.Result = (IntPtr)HTLEFT;
                    else if (vPoint.X >= ClientSize.Width - 5)
                        if (vPoint.Y <= 5)
                            m.Result = (IntPtr)HTTOPRIGHT;
                        else if (vPoint.Y >= ClientSize.Height - 5)
                            m.Result = (IntPtr)HTBOTTOMRIGHT;
                        else m.Result = (IntPtr)HTRIGHT;
                    else if (vPoint.Y <= 5)
                        m.Result = (IntPtr)HTTOP;
                    else if (vPoint.Y >= ClientSize.Height - 5)
                        m.Result = (IntPtr)HTBOTTOM;
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 窗口加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IEV9000Player_Load(object sender, EventArgs e)
        {
            this.Size = new Size(Screen.PrimaryScreen.Bounds.Width * 3 / 4, Screen.PrimaryScreen.Bounds.Height * 3 / 4);
            initMainControlSize();
            initWindowControl();
            initToolBarControl();
            ShowControl();
            initPlayList();
            this.play_hwnd = this.EV9000playpanel.Handle;
            try
            {
                if (0 == EV9000App.EV9000AppInit())
                {
                    this.m_log.writeRunMsg("EV9000APP[EV9000AppInit()]初始化函数成功");
                }
                else
                {
                    this.m_log.writeRunErrorMsg("EV9000APP[EV9000AppInit()]初始化函数成功");
                }
            }
            catch (Exception ew)
            {
                this.m_log.writeRunErrorMsg("EV9000APP[EV9000AppInit()]发生异常,异常原因:" + ew.Message);
            }
            initTip();
            initAbout();
            clipVedio = new VideoClip();
            this.Controls.Add(clipVedio);
            clipVedio.Visible = false;
            ReadPlayListFromFile();
        }
        /// <summary>
        /// 显示编辑面板
        /// </summary>
        /// <param name="isShow">是否显示</param>
        public void initClipVedio(bool isShow)
        {
            clipVedio.Location = new Point((int)((this.Size.Width - clipVedio.Size.Width) / 2), (int)((this.Size.Height - clipVedio.Size.Height) / 2));
            clipVedio.SetSaveFilePath(config.rec_path);
            if (isShow)
            {
                clipVedio.BringToFront();
                clipVedio.Visible = true;
            }
            else
            {
                clipVedio.Visible = false;
            }

        }
        /// <summary>
        /// 初始化窗体主控件
        /// </summary>
        public void initMainControlSize()
        {
            this.top_panel.Size = new Size(this.Size.Width-2,this.top_panel.Size.Height);
            this.top_panel.Location = new Point(1, 1);
            this.tool_panel.Size = new Size(this.Size.Width-2,65);
            this.tool_panel.Location = new Point(1,this.Size.Height-this.tool_panel.Size.Height+this.top_panel.Location.Y-2);
            this.play_progress.Size = new Size(this.tool_panel.Size.Width-12, 20);
            this.play_progress.Location = new Point(9, 5);
            if (this.ishidePlayList)
            {
                this.play_list_panel.Size = new Size(0, this.Size.Height-2 - this.top_panel.Size.Height - this.tool_panel.Size.Height);
                //this.hide_panel.Size = new Size(3, this.play_list_panel.Size.Height);
                this.play_panel.Size = new Size(this.Size.Width/* - this.hide_panel.Size.Width */- this.play_list_panel.Size.Width-2, this.play_list_panel.Size.Height);
                this.play_panel.Location = new Point(1, this.top_panel.Size.Height + this.top_panel.Location.X);
                this.hide_panel.Location = new Point(this.play_panel.Location.X + this.play_panel.Size.Width, this.top_panel.Size.Height+this.top_panel.Location.X);
                this.play_list_panel.Location = new Point(this.play_panel.Size.Width/*this.hide_panel.Location.X + this.hide_panel.Size.Width*/, this.top_panel.Size.Height + this.top_panel.Location.X);
             
            }
            else
            {
                this.play_list_panel.Size = new Size((int)this.Size.Width/7, this.Size.Height-2 - this.top_panel.Size.Height - this.tool_panel.Size.Height);
                //this.hide_panel.Size = new Size(3, this.play_list_panel.Size.Height);
                this.play_panel.Size = new Size(this.Size.Width /*- this.hide_panel.Size.Width*/ - this.play_list_panel.Size.Width - 2, this.play_list_panel.Size.Height);
                this.play_panel.Location = new Point(1, this.top_panel.Size.Height + this.top_panel.Location.X);
                //this.hide_panel.Location = new Point(this.play_panel.Location.X + this.play_panel.Size.Width, this.top_panel.Size.Height + this.top_panel.Location.X);
                this.play_list_panel.Location = new Point(this.play_panel.Size.Width/*this.hide_panel.Location.X + this.hide_panel.Size.Width*/, this.top_panel.Size.Height + this.top_panel.Location.X);               
            }
            this.EV9000playpanel.Location = new Point(2, 3);
            this.EV9000playpanel.Size = new Size(this.play_panel.Size.Width-6,this.play_panel.Size.Height-5);
        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        public void ShowControl()
        {
            //菜单栏
            this.Controls.Add(this.openMenu);
            this.openMenu.Visible = false;
            //播放列表栏
            this.play_list_panel.Controls.Add(this.playList);
            this.playList.Visible = true;
        }
        /// <summary>
        /// 初始化关闭，最大化，最小化菜单，隐藏列表栏的按钮
        /// </summary>
        public void initWindowControl()
        {
            this.pic_close.Location = new Point(this.Size.Width - pic_close.Size.Width - controlSpace, 10);
            this.pic_normalmax.Location = new Point(this.Size.Width - pic_close.Size.Width * 2 - controlSpace * 2, 10);
            this.pic_min.Location = new Point(this.Size.Width - pic_close.Size.Width * 3 - controlSpace * 3, 10);
            this.pic_separate.Location = new Point(this.Size.Width - pic_close.Size.Width * 4 - controlSpace*4, 10);
            this.pic_hideplaylist.Location = new Point(this.Size.Width - pic_close.Size.Width * 5 - controlSpace * 5, 10);
            this.pic_openmenu.Location = new Point(this.Size.Width - pic_close.Size.Width * 6 - controlSpace * 6, 10);
        }
        /// <summary>
        /// 初始化工具栏按钮
        /// </summary>
        public void initToolBarControl()
        {
            int picwidth = pic_capture.Size.Width;
            this.pic_capture.Location = new Point(this.controlSpace, this.play_progress.Height+this.play_progress.Location.X);
            this.pic_record.Location = new Point(this.controlSpace * 2 + picwidth, this.play_progress.Height + this.play_progress.Location.X);
            this.pic_closeplay.Location = new Point(this.controlSpace * 3 + picwidth * 2, this.play_progress.Height + this.play_progress.Location.X);

            this.pic_max.Location = new Point(this.tool_panel.Size.Width - picwidth - this.controlSpace, this.play_progress.Height + this.play_progress.Location.X);
            this.pic_set.Location = new Point(this.tool_panel.Size.Width - picwidth * 2 - this.controlSpace * 2, this.play_progress.Height + this.play_progress.Location.X);
            this.pic_vol.Location = new Point(this.tool_panel.Size.Width - picwidth * 3 - this.controlSpace * 3, this.play_progress.Height + this.play_progress.Location.X);

            this.pic_play.Location = new Point((this.Size.Width - picwidth) / 2, this.play_progress.Height + this.play_progress.Location.X);
            this.pic_goback.Location = new Point(this.pic_play.Location.X - picwidth - controlSpace, this.play_progress.Height + this.play_progress.Location.X);
            this.pic_preview.Location = new Point(this.pic_goback.Location.X - picwidth - controlSpace, this.play_progress.Height + this.play_progress.Location.X);
            this.pic_forward.Location = new Point(this.pic_play.Location.X + picwidth + controlSpace,this.play_progress.Height + this.play_progress.Location.X);
            this.pic_next.Location = new Point(this.pic_forward.Location.X + picwidth + controlSpace,this.play_progress.Height + this.play_progress.Location.X);
        }
        /// <summary>
        /// 初始化播放列表
        /// </summary>
        public void initPlayList()
        {
            this.playList.Location = new Point(0,3);
            playList.KeyDown += new KeyEventHandler(this.IEV9000Player_KeyDown);
            playList.KeyPressDown += new EV9000RecPlayer.EventHander.KeyEVENTHander(this.IEV9000Player_KeyDown);
            this.playList.Size = new Size(this.play_list_panel.Size.Width,this.play_list_panel.Size.Height-6);
        }
        /// <summary>
        /// 初始化提示信息
        /// </summary>
        public void initTip()
        {
            this.TipMain.SetToolTip(this.pic_capture,"抓图");
            this.TipMain.SetToolTip(this.pic_record,"录像");
            this.TipMain.SetToolTip(this.pic_closeplay, "停止");
            this.TipMain.SetToolTip(this.pic_preview,"上一个");
            this.TipMain.SetToolTip(this.pic_goback,"慢放");
            this.TipMain.SetToolTip(this.pic_play,"播放");
            this.TipMain.SetToolTip(this.pic_forward, "快放");
            this.TipMain.SetToolTip(this.pic_vol, "声音");
            this.TipMain.SetToolTip(this.pic_set,"设置");
            this.TipMain.SetToolTip(this.pic_max,"全屏");
        }
        /// <summary>
        /// 初始化关于对话框
        /// </summary>
        public void initAbout()
        {
            this.EV9000playpanel.Controls.Add(about);
            about.Visible = false;
            this.EV9000playpanel.Controls.Add(config);
            config.Visible = false;
        }
        /// <summary>
        /// 是否显示关于对话框
        /// </summary>
        /// <param name="ishow"></param>
        public void ShowAbout(bool ishow)
        {
            if (ishow)
            {
                about.Location = new Point((int)(this.EV9000playpanel.Size.Width - about.Size.Width) / 2, (int)(EV9000playpanel.Size.Height - about.Size.Height) / 2);
                about.Visible = true;
                about.BringToFront();
            }
            else
            {
                about.Visible =false;
            }
        }
        /// <summary>
        /// 窗口移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IEV9000Player_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
        }
       
        /// <summary>
        /// 隐藏播放列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hide_panel_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.ishidePlayList == false)
            {
                this.ishidePlayList = true;
            }
            else
            {
                this.ishidePlayList = false;
            }
            this.initMainControlSize();
        }
        /// <summary>
        /// 该表图标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hide_panel_MouseEnter(object sender, EventArgs e)
        {
            if (this.ishidePlayList == false)
            {
                this.hide_panel.Cursor = Cursors.PanEast;
                this.TipMain.SetToolTip(this.hide_panel, "隐藏播放列表");
            }
            else
            {
                this.hide_panel.Cursor = Cursors.PanWest;
                this.TipMain.SetToolTip(this.hide_panel, "显示播放列表");
            }
        }

        /// <summary>
        /// 保存播放列表到文件（默认文件名称：PlayList.lst）
        /// </summary>
        public void SavePlayListToFile()
        {
            try
            {
                if (File.Exists(Application.StartupPath + "\\PlayList.lst") == true)
                {
                    File.Delete(Application.StartupPath + "\\PlayList.lst");
                }
                FileStream fs = new FileStream(Application.StartupPath + "\\PlayList.lst", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs, Encoding.Default);
                foreach(EV9000List list in this.playList.filelist)
                {
                    sw.WriteLine(list.filepath);
                }
                sw.Close();
                fs.Close();
            }
            catch (Exception e)
            {
                m_log.writeRunErrorMsg("保存播放列表文件时发生错误，错误原因:" + e.Message);
            }
        }
        /// <summary>
        /// 读取文件到保存播放列表（默认文件名称：PlayList.lst）
        /// </summary>
        public void ReadPlayListFromFile()
        {
            if (File.Exists(Application.StartupPath + "\\PlayList.lst") == true)
            {
                try
                {
                    FileStream fs = new FileStream(Application.StartupPath + "\\PlayList.lst", FileMode.Open);
                    StreamReader sw = new StreamReader(fs, Encoding.Default);
                    string str = sw.ReadLine();
                    while (str != null)
                    {
                        if (File.Exists(str))
                        {
                            this.playList.AddFileToPlayList(str);
                        }
                        str = sw.ReadLine();
                    }
                    sw.Close();
                    fs.Close();
                }
                catch (Exception e)
                {
                    m_log.writeRunErrorMsg("读取文件到播放列表错误，错误原因：" + e.Message);
                }
            }
        }
        /// <summary>
        /// 窗口大小改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IEV9000Player_SizeChanged(object sender, EventArgs e)
        {
            /*if (this.Size.Width <= 459 && this.Size.Height > 300)
            {
                this.Size = new Size(459, this.Size.Height);
            }
            else if (this.Size.Width > 459 && this.Size.Height <= 300)
            {
                this.Size = new Size(this.Size.Width, 300);
            }
            else if (this.Size.Width <= 459 && this.Size.Height <= 300)
            {
                this.Size = new Size(459, 300);
            }*/
            initMainControlSize();
            initWindowControl();
            initToolBarControl();
            initPlayList();
            //更新菜单位置
            if (this.openMenu.Visible)
            {
                this.openMenu.Location = new Point(this.pic_openmenu.Location.X, this.pic_openmenu.Location.Y + this.pic_openmenu.Size.Height);
            }
        }
        /// <summary>
        /// 顶部栏按钮就单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pic_close_Click(object sender, EventArgs e)
        {
            if (sender == this.pic_close)
            {
                this.Close();
            }
            else if (sender == this.pic_normalmax)
            {
                if (this.WindowState == FormWindowState.Normal)
                {
                    this.controlSpace = 20;
                    this.WindowState = FormWindowState.Maximized;
                    this.pic_normalmax.Image = Properties.Resources.window_normal;
                }
                else
                {
                    this.controlSpace = 15;
                    this.WindowState = FormWindowState.Normal;
                    this.pic_normalmax.Image = Properties.Resources.window_max;
                }
            }
            else if (sender == this.pic_min)
            {
                this.WindowState = FormWindowState.Minimized;
            }
            else if (sender == this.pic_hideplaylist)
            {
                hide_panel_MouseClick(sender,null);
            }
            else if (sender == this.pic_openmenu)
            {
                if (this.openMenu.Visible)
                {
                    this.openMenu.Visible = false;
                }
                else
                {
                    this.openMenu.Location = new Point(this.pic_openmenu.Location.X, this.pic_openmenu.Location.Y + this.pic_openmenu.Size.Height);
                    this.openMenu.BringToFront();
                    this.openMenu.Visible = true;
                }
            }
        }
        /// <summary>
        /// 顶部栏按钮退出事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pic_close_MouseLeave(object sender, EventArgs e)
        {
            if (sender == this.pic_close)
            {
                this.pic_close.Image = Properties.Resources.close;
            }
            else if (sender == this.pic_normalmax)
            {
                if (this.WindowState == FormWindowState.Maximized)
                {
                    this.pic_normalmax.Image = Properties.Resources.window_normal;
                }
                else
                {
                    this.pic_normalmax.Image = Properties.Resources.window_max;
                }
            }
            else if (sender == this.pic_min)
            {
                this.pic_min.Image = Properties.Resources.window_min;
            }
            else if (sender == this.pic_hideplaylist)
            {
                this.pic_hideplaylist.Image = Properties.Resources.hide_playlist;
            }
            else if (sender == this.pic_openmenu)
            {
                this.pic_openmenu.Image = Properties.Resources.openmeun;
            }
        }
        /// <summary>
        /// 顶部栏按钮进入事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pic_close_MouseEnter(object sender, EventArgs e)
        {
            if (sender == this.pic_close)
            {
                this.pic_close.Image = Properties.Resources.close_on;
            }
            else if (sender == this.pic_normalmax)
            {
                if (this.WindowState == FormWindowState.Maximized)
                {
                    this.pic_normalmax.Image = Properties.Resources.window_normal_on;
                }
                else
                {
                    this.pic_normalmax.Image = Properties.Resources.window_max_on;
                }
            }
            else if (sender == this.pic_min)
            {
                this.pic_min.Image = Properties.Resources.window_min_on;
            }
            else if (sender == this.pic_hideplaylist)
            {
                this.pic_hideplaylist.Image = Properties.Resources.hide_playlist_on;
            }
            else if (sender == this.pic_openmenu)
            {
                this.pic_openmenu.Image = Properties.Resources.openmenu_on;
            }
        }
        /// <summary>
        /// 工具栏按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolControlClick(object sender, MouseEventArgs e)
        {
            if(sender==this.pic_capture)
            {
                int bRet = EV9000App.EV9000APPCapPicture(this.playHandle,config.cap_path);
                if (bRet >= 0)
                {
                    this.ShowInfo("抓图成功");
                }
                else
                {
                    this.ShowInfo("抓图失败");
                }
            }
            else if (sender == this.pic_record)
            {
                if (isvideoplay)
                {
                    if (isvideorecord)
                    {
                        EV9000App.EV9000APPStopRecord(this.playHandle);
                        this.TipMain.SetToolTip(this.pic_record, "开始录像");
                        isvideorecord = false;
                    }
                    else
                    {
                        this.pic_record.Image = Properties.Resources.recording;
                        EV9000App.EV9000APPStartRecord(this.playHandle, config.rec_path);
                        this.TipMain.SetToolTip(this.pic_record, "停止录像");
                        isvideorecord = true;
                    }
                }
            }
            else if (sender == this.pic_closeplay)
            {
                if (isvideoplay && this.playHandle >= 0)
                {
                    EV9000App.EV9000APPClosePlay(this.playHandle);
                    this.isvideoplay = false;
                    this.playHandle = -1;
                    this.currentFilepath = "";
                    this.EV9000playpanel.Refresh();
                }
            }
            else if (sender == this.pic_preview)
            {
                int index = playList.GetCurrentIndex();
                if (index > 0&&index<playList.filelist.Count)
                {
                    this.playList.PlayVideoByFilePath(playList.filelist[index - 1].filepath, playList.filelist[index - 1].filepath.Substring(playList.filelist[index - 1].filepath.LastIndexOf(".")+1).ToLower());
                }
            }
            else if (sender == this.pic_goback)
            {
                if (isvideoplay)
                {
                    if (playSpeed == EV9000_PLAYBACK_SPEED.EV9000_PLAYBACK_SPEED_8OF1)
                    {
                        playSpeed = EV9000_PLAYBACK_SPEED.EV9000_PLAYBACK_SPEED_4OF1;
                        this.TipMain.SetToolTip(this.pic_goback, "2倍速");
                        this.TipMain.SetToolTip(this.pic_forward, "8倍速");
                    }
                    else if (playSpeed == EV9000_PLAYBACK_SPEED.EV9000_PLAYBACK_SPEED_4OF1)
                    {
                        playSpeed = EV9000_PLAYBACK_SPEED.EV9000_PLAYBACK_SPEED_2OF1;
                        this.TipMain.SetToolTip(this.pic_goback, "常速");
                        this.TipMain.SetToolTip(this.pic_forward, "4倍速");
                    }
                    else if (playSpeed == EV9000_PLAYBACK_SPEED.EV9000_PLAYBACK_SPEED_2OF1)
                    {
                        playSpeed = EV9000_PLAYBACK_SPEED.EV9000_PLAYBACK_SPEED_NORMAL;
                        this.TipMain.SetToolTip(this.pic_goback, "1/2倍速");
                        this.TipMain.SetToolTip(this.pic_forward, "2倍速");
                    }
                    else if (playSpeed == EV9000_PLAYBACK_SPEED.EV9000_PLAYBACK_SPEED_NORMAL)
                    {
                        playSpeed = EV9000_PLAYBACK_SPEED.EV9000_PLAYBACK_SPEED_1OF2;
                        this.TipMain.SetToolTip(this.pic_goback, "1/4倍速");
                        this.TipMain.SetToolTip(this.pic_forward, "常速");
                    }
                    else if (playSpeed == EV9000_PLAYBACK_SPEED.EV9000_PLAYBACK_SPEED_1OF2)
                    {
                        playSpeed = EV9000_PLAYBACK_SPEED.EV9000_PLAYBACK_SPEED_1OF4;
                        this.TipMain.SetToolTip(this.pic_goback, "1/8倍速");
                        this.TipMain.SetToolTip(this.pic_forward, "1/2倍速");
                    }
                    else if (playSpeed == EV9000_PLAYBACK_SPEED.EV9000_PLAYBACK_SPEED_1OF4)
                    {
                        playSpeed = EV9000_PLAYBACK_SPEED.EV9000_PLAYBACK_SPEED_8OF1;
                        this.TipMain.SetToolTip(this.pic_forward, "1/4倍速");
                    }
                    EV9000App.EV9000APPSetPlaySpeed(this.playHandle, playSpeed);
                }
            }
            else if (sender == this.pic_play)
            {
                if (isvideoplay)
                {
                    if (EV9000App.EV9000APPPause(this.playHandle) >= 0)
                    {
                        isvideoplay = false;
                        this.pic_play.Image = Properties.Resources.pause_on;
                        this.TipMain.SetToolTip(this.pic_play, "播放");
                        StopPlayTimer();
                    }
                }
                else
                {
                    if (EV9000App.EV9000APPPlay(this.playHandle) >= 0)
                    {
                        isvideoplay = true;
                        this.pic_play.Image = Properties.Resources.play_on;
                        this.TipMain.SetToolTip(this.pic_play, "暂停");
                        StartPlayTimer();
                    }
                }
            }
            else if (sender == this.pic_forward)
            {
                if (isvideoplay)
                {
                    if (playSpeed == EV9000_PLAYBACK_SPEED.EV9000_PLAYBACK_SPEED_1OF8)
                    {
                        playSpeed = EV9000_PLAYBACK_SPEED.EV9000_PLAYBACK_SPEED_1OF4;
                        this.TipMain.SetToolTip(this.pic_goback, "1/8倍速");
                        this.TipMain.SetToolTip(this.pic_forward, "1/2倍速");
                    }
                    else if (playSpeed == EV9000_PLAYBACK_SPEED.EV9000_PLAYBACK_SPEED_1OF4)
                    {
                        playSpeed = EV9000_PLAYBACK_SPEED.EV9000_PLAYBACK_SPEED_1OF2;
                        this.TipMain.SetToolTip(this.pic_goback, "1/4倍速");
                        this.TipMain.SetToolTip(this.pic_forward, "常速");
                    }
                    else if (playSpeed == EV9000_PLAYBACK_SPEED.EV9000_PLAYBACK_SPEED_1OF2)
                    {
                        playSpeed = EV9000_PLAYBACK_SPEED.EV9000_PLAYBACK_SPEED_NORMAL;
                        this.TipMain.SetToolTip(this.pic_goback, "1/2倍速");
                        this.TipMain.SetToolTip(this.pic_forward, "2倍速");
                    }
                    else if (playSpeed == EV9000_PLAYBACK_SPEED.EV9000_PLAYBACK_SPEED_NORMAL)
                    {
                        playSpeed = EV9000_PLAYBACK_SPEED.EV9000_PLAYBACK_SPEED_2OF1;
                        this.TipMain.SetToolTip(this.pic_forward, "4倍速");
                        this.TipMain.SetToolTip(this.pic_goback, "常速");
                    }
                    else if (playSpeed == EV9000_PLAYBACK_SPEED.EV9000_PLAYBACK_SPEED_2OF1)
                    {
                        playSpeed = EV9000_PLAYBACK_SPEED.EV9000_PLAYBACK_SPEED_4OF1;
                        this.TipMain.SetToolTip(this.pic_forward, "8倍速");
                        this.TipMain.SetToolTip(this.pic_goback, "2速速");
                    }
                    else if (playSpeed == EV9000_PLAYBACK_SPEED.EV9000_PLAYBACK_SPEED_4OF1)
                    {
                        playSpeed = EV9000_PLAYBACK_SPEED.EV9000_PLAYBACK_SPEED_8OF1;
                        this.TipMain.SetToolTip(this.pic_goback, "4速速");
                    }
                    EV9000App.EV9000APPSetPlaySpeed(this.playHandle, playSpeed);
                }
            }
            else if (sender == this.pic_next)
            {
                int index = playList.GetCurrentIndex();
                if (index >= 0 && index < playList.filelist.Count-1)
                {
                    this.playList.PlayVideoByFilePath(playList.filelist[index + 1].filepath, playList.filelist[index + 1].filepath.Substring(playList.filelist[index + 1].filepath.LastIndexOf(".")+1).ToLower());
                }
            }
            else if (sender == this.pic_vol)
            {
                try
                {
                    if (this.isaduio)
                    {
                        if (isvideoplay && this.playHandle >= 0)
                        {
                            int bRet = EV9000App.EV9000APPCloseAudio(this.playHandle);
                            if (bRet >= 0)
                            {
                                this.pic_vol.Image = Properties.Resources.novol;
                                this.isaduio = false;
                            }
                            else
                            {
                                this.m_log.writeRunErrorMsg("关闭声音失败，当前播放句柄：" + this.playHandle+" 操作句柄："+bRet);
                            }
                        }
                    }
                    else
                    {
                        if (isvideoplay && this.playHandle >= 0)
                        {
                            int bRet = EV9000App.EV9000APPOpenAudio(this.playHandle);
                            if (bRet >= 0)
                            {
                                this.pic_vol.Image = Properties.Resources.openvol;
                                this.isaduio = true;
                            }
                            else
                            {
                                this.m_log.writeRunErrorMsg("打开声音失败，当前播放句柄：" + this.playHandle + " 操作句柄：" + bRet);
                            }
                        }
                    }
                }
                catch (Exception ew)
                {
                    this.m_log.writeRunErrorMsg("在声音操作中发生异常，异常原因："+ew.Message);
                }
            }
            else if (sender == this.pic_set)
            {
                if (config.Visible)
                {
                    config.Visible = false;
                }
                else
                {
                    config.Location = new Point((int)(this.EV9000playpanel.Size.Width - about.Size.Width) / 2, (int)(EV9000playpanel.Size.Height - about.Size.Height) / 2);
                    config.Visible = true;
                    config.BringToFront();
                }
            }
            else if (sender == this.pic_max)
            {
                if (playHwndisMax)
                {
                    this.ExitFullScreen();
                    this.pic_max.Image = Properties.Resources.maxwnd;
                    this.playHwndisMax = false;
                }
                else
                {
                    FullScreen();
                    this.pic_max.Image = Properties.Resources.wnd_normal;
                    this.playHwndisMax = true;
                }
            }
        }
        /// <summary>
        /// 工具栏按钮进入事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolControlMouseEnter(object sender, EventArgs e)
        {
            if (sender == this.pic_capture)
            {
                this.TipMain.SetToolTip(this.pic_capture,"抓图");
                this.pic_capture.Image = Properties.Resources.capture_on;
            }
            else if (sender == this.pic_record)
            {
                if (!isvideorecord)
                {
                    this.pic_record.Image = Properties.Resources.record_on;
                }
            }
            else if (sender == this.pic_closeplay)
            {
                this.TipMain.SetToolTip(this.pic_closeplay, "关闭");
                this.pic_closeplay.Image = Properties.Resources.stop_on;
            }
            else if (sender == this.pic_preview)
            {
                this.TipMain.SetToolTip(this.pic_preview, "前一个");
                this.pic_preview.Image = Properties.Resources.preview_on;
            }
            else if (sender == this.pic_goback)
            {
                this.pic_goback.Image = Properties.Resources.goback_on;
            }
            else if (sender == this.pic_play)
            {
                if (isvideoplay)
                {
                    this.pic_play.Image = Properties.Resources.pause_on;
                    this.TipMain.SetToolTip(this.pic_play, "暂停");
                }
                else
                {
                    this.pic_play.Image = Properties.Resources.play_on;
                    this.TipMain.SetToolTip(this.pic_play, "播放");
                }
            }
            else if (sender == this.pic_forward)
            {
                this.pic_forward.Image = Properties.Resources.forward_on;
            }
            else if (sender == this.pic_next)
            {
                this.TipMain.SetToolTip(this.pic_next, "下一个");
                this.pic_next.Image = Properties.Resources.next_on;
            }
            else if (sender == this.pic_vol)
            {
                if (this.isaduio)
                {
                    this.pic_vol.Image = Properties.Resources.openvol_on;
                    this.TipMain.SetToolTip(this.pic_vol, "关闭声音");
                }
                else
                {
                    this.pic_vol.Image = Properties.Resources.novol_on;
                    this.TipMain.SetToolTip(this.pic_vol, "打开声音");
                }
            }
            else if (sender == this.pic_set)
            {
                this.pic_set.Image = Properties.Resources.set_on;
                this.TipMain.SetToolTip(this.pic_set, "设置");
            }
            else if (sender == this.pic_max)
            {
                if (playHwndisMax)
                {
                    this.pic_max.Image = Properties.Resources.wnd_normal_on;
                    this.TipMain.SetToolTip(this.pic_max, "退出全屏");
                }
                else
                {
                    
                    this.pic_max.Image = Properties.Resources.maxwnd_on;
                    this.TipMain.SetToolTip(this.pic_max, "全屏");
                }
            }
        }
        /// <summary>
        /// 工具栏按钮退出事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolControlMouseLeave(object sender, EventArgs e)
        {
            if (sender == this.pic_capture)
            {
                this.pic_capture.Image = Properties.Resources.capture;
            }
            else if (sender == this.pic_record)
            {
                if (!isvideorecord)
                {
                    this.pic_record.Image = Properties.Resources.record;
                }
            }
            else if (sender == this.pic_closeplay)
            {
                this.pic_closeplay.Image = Properties.Resources.stop;
            }
            else if (sender == this.pic_preview)
            {
                this.pic_preview.Image = Properties.Resources.preview;
            }
            else if (sender == this.pic_goback)
            {
                this.pic_goback.Image = Properties.Resources.goback;
            }
            else if (sender == this.pic_play)
            {
                if (isvideoplay)
                {
                    this.pic_play.Image = Properties.Resources.play;
                }
                else
                { 
                   this.pic_play.Image = Properties.Resources.pause; 
                }
            }
            else if (sender == this.pic_forward)
            {
                this.pic_forward.Image = Properties.Resources.forward;
            }
            else if (sender == this.pic_next)
            {
                this.pic_next.Image = Properties.Resources.next;
            }
            else if (sender == this.pic_vol)
            {
                if (this.isaduio)
                {
                    this.pic_vol.Image = Properties.Resources.openvol;
                }
                else
                {
                    this.pic_vol.Image = Properties.Resources.novol;
                }
            }
            else if (sender == this.pic_set)
            {
                this.pic_set.Image = Properties.Resources.set;
            }
            else if (sender == this.pic_max)
            {
                if (playHwndisMax)
                {
                    this.pic_max.Image = Properties.Resources.wnd_normal;
                }
                else
                {
                    this.pic_max.Image = Properties.Resources.maxwnd;
                }
            }
        }
        /// <summary>
        /// 设置滚动条为初始值
        /// </summary>
        public void SetProgerssValue(long value)
        {
            this.play_progress.SetPos(0);
            this.play_progress.SetValue(value);
        }
        /// <summary>
        /// 进度条拖动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void play_progress_Scroll(object sender, EV9000Event e)
        {
            if (isvideoplay)
            {
                scorllWidht = (int)(((double)e.value / (this.play_progress.Size.Width - 20)) * 100);
            }
        }
        /// <summary>
        /// 键盘按下事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IEV9000Player_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine(e.KeyValue);
            if (e.KeyValue == 17)
            {
                this.playList.isCtrlIsDown = true;
            }
            else if (e.KeyValue == 32)
            {
                if (isvideoplay)
                {
                    this.pic_play.Image = Properties.Resources.pause;
                    EV9000App.EV9000APPPause(this.playHandle);
                    isvideoplay = false;
                    StopPlayTimer();
                }
                else
                {
                    this.pic_play.Image = Properties.Resources.play;
                    EV9000App.EV9000APPPlay(this.playHandle);
                    isvideoplay = true;
                    StartPlayTimer();
                }
            }
            else if(e.KeyValue==37)
            {
                int index = playList.GetCurrentIndex();
                if (index > 0 && index < playList.filelist.Count)
                {
                    this.playList.PlayVideoByFilePath(playList.filelist[index - 1].filepath, playList.filelist[index - 1].filepath.Substring(playList.filelist[index - 1].filepath.LastIndexOf(".")+1).ToLower());
                }
            }
            else if(e.KeyValue==39)
            {
                int index = playList.GetCurrentIndex();
                if (index >= 0 && index < playList.filelist.Count - 1)
                {
                    this.playList.PlayVideoByFilePath(playList.filelist[index + 1].filepath, playList.filelist[index + 1].filepath.Substring(playList.filelist[index + 1].filepath.LastIndexOf(".") + 1).ToLower());
                }
            }
        }
        /// <summary>
        /// 显示操作消息
        /// </summary>
        /// <param name="msg">消息内容</param>
        public void ShowInfo(string msg)
        {
            this.info_label.Text = msg;
            this.info_label.Location = new Point((int)(this.EV9000playpanel.Size.Width - this.info_label.Size.Width) / 2, (int)(this.EV9000playpanel.Size.Height - this.info_label.Size.Height) / 2);
            this.info_label.Show();
            Thread.Sleep(2000);
            this.info_label.Hide();
        }
        /// <summary>
        /// 通过文件播放
        /// </summary>
        /// <param name="filepath"></param>
        public void PlayFileByFilePath(string filepath,String fileType)
        {
            try
            {
                if (isvideoplay)
                {
                    EV9000App.EV9000APPClosePlay(this.playHandle);
                }
                else if (this.playHandle >= 0)
                {
                    EV9000App.EV9000APPClosePlay(this.playHandle);
                }
                if (File.Exists(filepath))
                {
                    FileInfo movie = new FileInfo(filepath);
                    long maxFilesize = 4;
                    maxFilesize=maxFilesize * 1000*1000*1000;
                    if (movie.Length < maxFilesize)
                    {
                        SetProgerssValue(movie.Length);
                        if (this.fileTypeList.Contains(fileType))
                        {
                            this.playHandle = EV9000App.EV9000OpenLocalRecord(EV9000App.GetLoginHandle(), filepath, play_hwnd);
                        }
                        else
                        {

                        }
                        if (this.playHandle >= 0)
                        {
                            this.m_log.writeRunMsg("通过文件播放[PlayFileByFilePath(" + filepath + ")]成功");
                            this.isvideoplay = true;
                            currentFilepath = filepath;
                        }
                        else
                        {
                            this.isvideoplay = false;
                            this.m_log.writeRunErrorMsg("通过文件播放[PlayFileByFilePath(" + filepath + ")]失败");
                        }
                    }
                    else
                    {
                        this.ShowInfo("文件大小超过4G播放失败");
                    }
                }
            }
            catch(Exception ew)
            {
                this.m_log.writeRunErrorMsg("通过文件播放[PlayFileByFilePath(" + filepath + ")]发生异常,异常原因：" + ew.Message);
            }
        }
        /// <summary>
        /// 进度条timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scroll_timer_Tick(object sender, EventArgs e)
        {
            if (isvideoplay)
            {
                long pos = EV9000App.EV9000APPGetPostion(this.playHandle);
                if (pos > 0)
                {
                    Console.WriteLine("获取播放进度值："+pos);
                    this.play_progress.SetPos(pos);
                }
            }
        }
        /// <summary>
        /// 进度条开始
        /// </summary>
        public void StartPlayTimer()
        {
            this.scroll_timer.Enabled = true;
        }
        /// <summary>
        /// 进度条停止
        /// </summary>
        public void StopPlayTimer()
        {
            this.scroll_timer.Enabled = false;
        }

        /// <summary>
        /// 播放
        /// </summary>
        public void Play()
        {
            if (!isvideoplay)
            {
                EV9000App.EV9000APPPlay(this.playHandle);
                isvideoplay = true;
                pic_play.Image = Properties.Resources.play_on;
            }
        }
        /// <summary>
        /// 暂停
        /// </summary>
        public void Pause()
        {
            if (isvideoplay)
            {
                EV9000App.EV9000APPPause(this.playHandle);
                isvideoplay = false;
                pic_play.Image = Properties.Resources.pause_on;
            }
        }
        /// <summary>
        /// 全屏
        /// </summary>
        public void FullScreen()
        {

            this.TipMain.SetToolTip(this.EV9000playpanel, "双击或按ESC退出全屏");
            this.playWnd_width = this.EV9000playpanel.Size.Width;
            this.playWnd_height = this.EV9000playpanel.Size.Height;
            maxWindows.Size = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            this.play_hwnd = this.EV9000playpanel.Handle;
            Panel maxPanel = (Panel)Panel.FromHandle(this.play_hwnd);
            
            maxWindows.Controls.Add(maxPanel);
            maxPanel.Size = new Size(maxWindows.Size.Width, maxWindows.Size.Height);
            maxPanel.Location = new Point(0, 0);
            maxWindows.Show();
            this.Hide();
            playHwndisMax = true;  
        }
        /// <summary>
        /// 退出全屏
        /// </summary>
        public void ExitFullScreen()
        {
            /****************************************************************************/
            this.EV9000playpanel = (EV9000Panel)Panel.FromHandle(this.play_hwnd);
            this.EV9000playpanel.Location = new Point(2, 3);
            this.EV9000playpanel.Size = new Size(this.playWnd_width, this.playWnd_height);
            this.play_panel.Controls.Add(this.EV9000playpanel);
            /***************************************************************************/
            //this.Controls.Add(this.tool_panel);
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.controlSpace = 30;
            }
            else
            {
                this.controlSpace = 15;
            }
            /***************************************************************************/
            maxWindows.Hide();
            this.Show();
            this.TopMost = true;
            playHwndisMax = false;
            this.TipMain.SetToolTip(this.EV9000playpanel, "双击全屏");
        }
        /// <summary>
        /// 双击进入全屏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EV9000playpanel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (playHwndisMax)
            {
                this.ExitFullScreen();
            }
            else
            {
                this.FullScreen();
            }
        }
        /// <summary>
        /// 窗口关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IEV9000Player_FormClosing(object sender, FormClosingEventArgs e)
        {
            EV9000App.EV9000AppFini();
            this.currentFilepath = "";
            this.SavePlayListToFile();
        }
        /// <summary>
        /// 文件拖动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EV9000playpanel_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                String[] files = (String[])e.Data.GetData(DataFormats.FileDrop);
                if (files != null && files.Length > 0)
                {
                    string filetype = files[0].Substring(files[0].LastIndexOf(".") + 1).ToLower();
                    if (fileTypeList.Contains(filetype))
                    {
                        this.playList.PlayVideoByFilePath(files[0],filetype);
                        this.playList.AddFileToPlayList(files[0]);
                    }

                }
            }  
        }
        /// <summary>
        /// 拖动进入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EV9000playpanel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;  
        }
        /// <summary>
        /// 拖动移出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EV9000playpanel_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }
        /// <summary>
        /// 菜单栏消失
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EV9000playpanel_MouseClick(object sender, MouseEventArgs e)
        {
            this.openMenu.Visible = false;
        }
        /// <summary>
        /// 滚动条拖动图标鼠标按下事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void play_progress_ScrollBarMouseDown(object sender, MouseEventArgs e)
        {
            if (isvideoplay)
            {
                this.scroll_timer.Enabled = false;
            }
        }
        /// <summary>
        /// 滚动条拖动图标鼠标抬起事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void play_progress_ScrollBarMouseUp(object sender, MouseEventArgs e)
        {
            if (isvideoplay)
            {
                int bRet = EV9000App.EV9000APPSetPostion(this.playHandle, scorllWidht);
                if (bRet >= 0)
                {
                    this.m_log.writeRunMsg("执行滚动函数[EV9000APPSetPostion]函数成功，返回值：" + bRet + "，滚动距离" + scorllWidht);
                }
                else
                {
                    this.m_log.writeRunMsg("执行滚动函数[EV9000APPSetPostion]函数失败，返回值：" + bRet + "，滚动距离" + scorllWidht);
                }
                this.scroll_timer.Enabled = true;
            }
        }
        
    }
}