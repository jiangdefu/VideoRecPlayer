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
        public EV9000Player.Util.RunLogReport     m_log;                                    //��־��¼
        public IntPtr           play_hwnd = new IntPtr(0);                                  //���Ŵ��ھ��
        public int              playHandle = -1;                                            //���Ŵ��ڶ���
        public bool             playHwndisMax = false;                                      //�жϵ�ǰ״̬�Ƿ�ȫ������
        public int              playWnd_width = 0;                                          //ȫ��֮ǰ���ڿ��
        public int              playWnd_height = 0;                                         //ȫ��֮ǰ���ڸ߶�

        //��ǰ����״̬
        public bool             isvideoplay = false;                                        //��ǰ����״̬�Ƿ����ڲ���
        public bool             isvideorecord = false;                                      //�Ƿ�����¼��
        public bool             isaduio = false;                                            //��ǰ�����Ƿ��

        //�����б��Ƿ�����
        public bool             ishidePlayList = false;                                     //�����б��Ƿ�����

      

        public int              controlSpace;                                               //��������֮��ľ���

        public Menu             openMenu;                                                   //���ļ��˵�

        public EV9000PlayList   playList;                                                   //�����б�   

        public CSEV9000APP      EV9000App;                                                  //EV9000APP�����

        public MaxPlayWindows   maxWindows;                                                 //ȫ������

        public EV9000_PLAYBACK_SPEED playSpeed;                                             //��ǰ��Ƶ�����ٶ�

        public ABOUUTEV9000 about;                                                          //���ڶԻ���

        public EV9000Config config;                                                         //���öԻ���

        public int scorllWidht = 0;                                                         //��������

        public List<string> fileTypeList;                                                   //��Ƶ�ļ�����

        public VideoClip clipVedio;                                                         //��Ƶ�������

        public string currentFilepath = "";                                                 //��ǰ���ŵ��ļ�
        //�����϶�����
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        /// <summary>
        /// ��ʼ������
        /// </summary>
        public S50SVRPlayer()
        {
            /*****************************************/
            //��ֹ���������˸����
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // ��ֹ��������.
            SetStyle(ControlStyles.DoubleBuffer, true); // ˫����
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
        //��дWindows ��Ϣ�����������ڲ�������Ĵ����ƶ�
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
        /// ���ڼ���
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
                    this.m_log.writeRunMsg("EV9000APP[EV9000AppInit()]��ʼ�������ɹ�");
                }
                else
                {
                    this.m_log.writeRunErrorMsg("EV9000APP[EV9000AppInit()]��ʼ�������ɹ�");
                }
            }
            catch (Exception ew)
            {
                this.m_log.writeRunErrorMsg("EV9000APP[EV9000AppInit()]�����쳣,�쳣ԭ��:" + ew.Message);
            }
            initTip();
            initAbout();
            clipVedio = new VideoClip();
            this.Controls.Add(clipVedio);
            clipVedio.Visible = false;
            ReadPlayListFromFile();
        }
        /// <summary>
        /// ��ʾ�༭���
        /// </summary>
        /// <param name="isShow">�Ƿ���ʾ</param>
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
        /// ��ʼ���������ؼ�
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
        /// ��ʼ���ؼ�
        /// </summary>
        public void ShowControl()
        {
            //�˵���
            this.Controls.Add(this.openMenu);
            this.openMenu.Visible = false;
            //�����б���
            this.play_list_panel.Controls.Add(this.playList);
            this.playList.Visible = true;
        }
        /// <summary>
        /// ��ʼ���رգ���󻯣���С���˵��������б����İ�ť
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
        /// ��ʼ����������ť
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
        /// ��ʼ�������б�
        /// </summary>
        public void initPlayList()
        {
            this.playList.Location = new Point(0,3);
            playList.KeyDown += new KeyEventHandler(this.IEV9000Player_KeyDown);
            playList.KeyPressDown += new EV9000RecPlayer.EventHander.KeyEVENTHander(this.IEV9000Player_KeyDown);
            this.playList.Size = new Size(this.play_list_panel.Size.Width,this.play_list_panel.Size.Height-6);
        }
        /// <summary>
        /// ��ʼ����ʾ��Ϣ
        /// </summary>
        public void initTip()
        {
            this.TipMain.SetToolTip(this.pic_capture,"ץͼ");
            this.TipMain.SetToolTip(this.pic_record,"¼��");
            this.TipMain.SetToolTip(this.pic_closeplay, "ֹͣ");
            this.TipMain.SetToolTip(this.pic_preview,"��һ��");
            this.TipMain.SetToolTip(this.pic_goback,"����");
            this.TipMain.SetToolTip(this.pic_play,"����");
            this.TipMain.SetToolTip(this.pic_forward, "���");
            this.TipMain.SetToolTip(this.pic_vol, "����");
            this.TipMain.SetToolTip(this.pic_set,"����");
            this.TipMain.SetToolTip(this.pic_max,"ȫ��");
        }
        /// <summary>
        /// ��ʼ�����ڶԻ���
        /// </summary>
        public void initAbout()
        {
            this.EV9000playpanel.Controls.Add(about);
            about.Visible = false;
            this.EV9000playpanel.Controls.Add(config);
            config.Visible = false;
        }
        /// <summary>
        /// �Ƿ���ʾ���ڶԻ���
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
        /// �����ƶ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IEV9000Player_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
        }
       
        /// <summary>
        /// ���ز����б�
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
        /// �ñ�ͼ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hide_panel_MouseEnter(object sender, EventArgs e)
        {
            if (this.ishidePlayList == false)
            {
                this.hide_panel.Cursor = Cursors.PanEast;
                this.TipMain.SetToolTip(this.hide_panel, "���ز����б�");
            }
            else
            {
                this.hide_panel.Cursor = Cursors.PanWest;
                this.TipMain.SetToolTip(this.hide_panel, "��ʾ�����б�");
            }
        }

        /// <summary>
        /// ���沥���б��ļ���Ĭ���ļ����ƣ�PlayList.lst��
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
                m_log.writeRunErrorMsg("���沥���б��ļ�ʱ�������󣬴���ԭ��:" + e.Message);
            }
        }
        /// <summary>
        /// ��ȡ�ļ������沥���б�Ĭ���ļ����ƣ�PlayList.lst��
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
                    m_log.writeRunErrorMsg("��ȡ�ļ��������б���󣬴���ԭ��" + e.Message);
                }
            }
        }
        /// <summary>
        /// ���ڴ�С�ı�
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
            //���²˵�λ��
            if (this.openMenu.Visible)
            {
                this.openMenu.Location = new Point(this.pic_openmenu.Location.X, this.pic_openmenu.Location.Y + this.pic_openmenu.Size.Height);
            }
        }
        /// <summary>
        /// ��������ť�͵����¼�
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
        /// ��������ť�˳��¼�
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
        /// ��������ť�����¼�
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
        /// ��������ť�����¼�
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
                    this.ShowInfo("ץͼ�ɹ�");
                }
                else
                {
                    this.ShowInfo("ץͼʧ��");
                }
            }
            else if (sender == this.pic_record)
            {
                if (isvideoplay)
                {
                    if (isvideorecord)
                    {
                        EV9000App.EV9000APPStopRecord(this.playHandle);
                        this.TipMain.SetToolTip(this.pic_record, "��ʼ¼��");
                        isvideorecord = false;
                    }
                    else
                    {
                        this.pic_record.Image = Properties.Resources.recording;
                        EV9000App.EV9000APPStartRecord(this.playHandle, config.rec_path);
                        this.TipMain.SetToolTip(this.pic_record, "ֹͣ¼��");
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
                        this.TipMain.SetToolTip(this.pic_goback, "2����");
                        this.TipMain.SetToolTip(this.pic_forward, "8����");
                    }
                    else if (playSpeed == EV9000_PLAYBACK_SPEED.EV9000_PLAYBACK_SPEED_4OF1)
                    {
                        playSpeed = EV9000_PLAYBACK_SPEED.EV9000_PLAYBACK_SPEED_2OF1;
                        this.TipMain.SetToolTip(this.pic_goback, "����");
                        this.TipMain.SetToolTip(this.pic_forward, "4����");
                    }
                    else if (playSpeed == EV9000_PLAYBACK_SPEED.EV9000_PLAYBACK_SPEED_2OF1)
                    {
                        playSpeed = EV9000_PLAYBACK_SPEED.EV9000_PLAYBACK_SPEED_NORMAL;
                        this.TipMain.SetToolTip(this.pic_goback, "1/2����");
                        this.TipMain.SetToolTip(this.pic_forward, "2����");
                    }
                    else if (playSpeed == EV9000_PLAYBACK_SPEED.EV9000_PLAYBACK_SPEED_NORMAL)
                    {
                        playSpeed = EV9000_PLAYBACK_SPEED.EV9000_PLAYBACK_SPEED_1OF2;
                        this.TipMain.SetToolTip(this.pic_goback, "1/4����");
                        this.TipMain.SetToolTip(this.pic_forward, "����");
                    }
                    else if (playSpeed == EV9000_PLAYBACK_SPEED.EV9000_PLAYBACK_SPEED_1OF2)
                    {
                        playSpeed = EV9000_PLAYBACK_SPEED.EV9000_PLAYBACK_SPEED_1OF4;
                        this.TipMain.SetToolTip(this.pic_goback, "1/8����");
                        this.TipMain.SetToolTip(this.pic_forward, "1/2����");
                    }
                    else if (playSpeed == EV9000_PLAYBACK_SPEED.EV9000_PLAYBACK_SPEED_1OF4)
                    {
                        playSpeed = EV9000_PLAYBACK_SPEED.EV9000_PLAYBACK_SPEED_8OF1;
                        this.TipMain.SetToolTip(this.pic_forward, "1/4����");
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
                        this.TipMain.SetToolTip(this.pic_play, "����");
                        StopPlayTimer();
                    }
                }
                else
                {
                    if (EV9000App.EV9000APPPlay(this.playHandle) >= 0)
                    {
                        isvideoplay = true;
                        this.pic_play.Image = Properties.Resources.play_on;
                        this.TipMain.SetToolTip(this.pic_play, "��ͣ");
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
                        this.TipMain.SetToolTip(this.pic_goback, "1/8����");
                        this.TipMain.SetToolTip(this.pic_forward, "1/2����");
                    }
                    else if (playSpeed == EV9000_PLAYBACK_SPEED.EV9000_PLAYBACK_SPEED_1OF4)
                    {
                        playSpeed = EV9000_PLAYBACK_SPEED.EV9000_PLAYBACK_SPEED_1OF2;
                        this.TipMain.SetToolTip(this.pic_goback, "1/4����");
                        this.TipMain.SetToolTip(this.pic_forward, "����");
                    }
                    else if (playSpeed == EV9000_PLAYBACK_SPEED.EV9000_PLAYBACK_SPEED_1OF2)
                    {
                        playSpeed = EV9000_PLAYBACK_SPEED.EV9000_PLAYBACK_SPEED_NORMAL;
                        this.TipMain.SetToolTip(this.pic_goback, "1/2����");
                        this.TipMain.SetToolTip(this.pic_forward, "2����");
                    }
                    else if (playSpeed == EV9000_PLAYBACK_SPEED.EV9000_PLAYBACK_SPEED_NORMAL)
                    {
                        playSpeed = EV9000_PLAYBACK_SPEED.EV9000_PLAYBACK_SPEED_2OF1;
                        this.TipMain.SetToolTip(this.pic_forward, "4����");
                        this.TipMain.SetToolTip(this.pic_goback, "����");
                    }
                    else if (playSpeed == EV9000_PLAYBACK_SPEED.EV9000_PLAYBACK_SPEED_2OF1)
                    {
                        playSpeed = EV9000_PLAYBACK_SPEED.EV9000_PLAYBACK_SPEED_4OF1;
                        this.TipMain.SetToolTip(this.pic_forward, "8����");
                        this.TipMain.SetToolTip(this.pic_goback, "2����");
                    }
                    else if (playSpeed == EV9000_PLAYBACK_SPEED.EV9000_PLAYBACK_SPEED_4OF1)
                    {
                        playSpeed = EV9000_PLAYBACK_SPEED.EV9000_PLAYBACK_SPEED_8OF1;
                        this.TipMain.SetToolTip(this.pic_goback, "4����");
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
                                this.m_log.writeRunErrorMsg("�ر�����ʧ�ܣ���ǰ���ž����" + this.playHandle+" ���������"+bRet);
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
                                this.m_log.writeRunErrorMsg("������ʧ�ܣ���ǰ���ž����" + this.playHandle + " ���������" + bRet);
                            }
                        }
                    }
                }
                catch (Exception ew)
                {
                    this.m_log.writeRunErrorMsg("�����������з����쳣���쳣ԭ��"+ew.Message);
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
        /// ��������ť�����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolControlMouseEnter(object sender, EventArgs e)
        {
            if (sender == this.pic_capture)
            {
                this.TipMain.SetToolTip(this.pic_capture,"ץͼ");
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
                this.TipMain.SetToolTip(this.pic_closeplay, "�ر�");
                this.pic_closeplay.Image = Properties.Resources.stop_on;
            }
            else if (sender == this.pic_preview)
            {
                this.TipMain.SetToolTip(this.pic_preview, "ǰһ��");
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
                    this.TipMain.SetToolTip(this.pic_play, "��ͣ");
                }
                else
                {
                    this.pic_play.Image = Properties.Resources.play_on;
                    this.TipMain.SetToolTip(this.pic_play, "����");
                }
            }
            else if (sender == this.pic_forward)
            {
                this.pic_forward.Image = Properties.Resources.forward_on;
            }
            else if (sender == this.pic_next)
            {
                this.TipMain.SetToolTip(this.pic_next, "��һ��");
                this.pic_next.Image = Properties.Resources.next_on;
            }
            else if (sender == this.pic_vol)
            {
                if (this.isaduio)
                {
                    this.pic_vol.Image = Properties.Resources.openvol_on;
                    this.TipMain.SetToolTip(this.pic_vol, "�ر�����");
                }
                else
                {
                    this.pic_vol.Image = Properties.Resources.novol_on;
                    this.TipMain.SetToolTip(this.pic_vol, "������");
                }
            }
            else if (sender == this.pic_set)
            {
                this.pic_set.Image = Properties.Resources.set_on;
                this.TipMain.SetToolTip(this.pic_set, "����");
            }
            else if (sender == this.pic_max)
            {
                if (playHwndisMax)
                {
                    this.pic_max.Image = Properties.Resources.wnd_normal_on;
                    this.TipMain.SetToolTip(this.pic_max, "�˳�ȫ��");
                }
                else
                {
                    
                    this.pic_max.Image = Properties.Resources.maxwnd_on;
                    this.TipMain.SetToolTip(this.pic_max, "ȫ��");
                }
            }
        }
        /// <summary>
        /// ��������ť�˳��¼�
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
        /// ���ù�����Ϊ��ʼֵ
        /// </summary>
        public void SetProgerssValue(long value)
        {
            this.play_progress.SetPos(0);
            this.play_progress.SetValue(value);
        }
        /// <summary>
        /// �������϶��¼�
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
        /// ���̰����¼�
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
        /// ��ʾ������Ϣ
        /// </summary>
        /// <param name="msg">��Ϣ����</param>
        public void ShowInfo(string msg)
        {
            this.info_label.Text = msg;
            this.info_label.Location = new Point((int)(this.EV9000playpanel.Size.Width - this.info_label.Size.Width) / 2, (int)(this.EV9000playpanel.Size.Height - this.info_label.Size.Height) / 2);
            this.info_label.Show();
            Thread.Sleep(2000);
            this.info_label.Hide();
        }
        /// <summary>
        /// ͨ���ļ�����
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
                            this.m_log.writeRunMsg("ͨ���ļ�����[PlayFileByFilePath(" + filepath + ")]�ɹ�");
                            this.isvideoplay = true;
                            currentFilepath = filepath;
                        }
                        else
                        {
                            this.isvideoplay = false;
                            this.m_log.writeRunErrorMsg("ͨ���ļ�����[PlayFileByFilePath(" + filepath + ")]ʧ��");
                        }
                    }
                    else
                    {
                        this.ShowInfo("�ļ���С����4G����ʧ��");
                    }
                }
            }
            catch(Exception ew)
            {
                this.m_log.writeRunErrorMsg("ͨ���ļ�����[PlayFileByFilePath(" + filepath + ")]�����쳣,�쳣ԭ��" + ew.Message);
            }
        }
        /// <summary>
        /// ������timer
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
                    Console.WriteLine("��ȡ���Ž���ֵ��"+pos);
                    this.play_progress.SetPos(pos);
                }
            }
        }
        /// <summary>
        /// ��������ʼ
        /// </summary>
        public void StartPlayTimer()
        {
            this.scroll_timer.Enabled = true;
        }
        /// <summary>
        /// ������ֹͣ
        /// </summary>
        public void StopPlayTimer()
        {
            this.scroll_timer.Enabled = false;
        }

        /// <summary>
        /// ����
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
        /// ��ͣ
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
        /// ȫ��
        /// </summary>
        public void FullScreen()
        {

            this.TipMain.SetToolTip(this.EV9000playpanel, "˫����ESC�˳�ȫ��");
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
        /// �˳�ȫ��
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
            this.TipMain.SetToolTip(this.EV9000playpanel, "˫��ȫ��");
        }
        /// <summary>
        /// ˫������ȫ��
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
        /// ���ڹر�
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
        /// �ļ��϶�
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
        /// �϶�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EV9000playpanel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;  
        }
        /// <summary>
        /// �϶��Ƴ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EV9000playpanel_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }
        /// <summary>
        /// �˵�����ʧ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EV9000playpanel_MouseClick(object sender, MouseEventArgs e)
        {
            this.openMenu.Visible = false;
        }
        /// <summary>
        /// �������϶�ͼ����갴���¼�
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
        /// �������϶�ͼ�����̧���¼�
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
                    this.m_log.writeRunMsg("ִ�й�������[EV9000APPSetPostion]�����ɹ�������ֵ��" + bRet + "����������" + scorllWidht);
                }
                else
                {
                    this.m_log.writeRunMsg("ִ�й�������[EV9000APPSetPostion]����ʧ�ܣ�����ֵ��" + bRet + "����������" + scorllWidht);
                }
                this.scroll_timer.Enabled = true;
            }
        }
        
    }
}