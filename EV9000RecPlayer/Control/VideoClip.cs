using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using CSharpEV9000APP;
using System.IO;
using System.Threading;

namespace EV9000RecPlayer.Control
{
    public partial class VideoClip : UserControl
    {
        public long startPos = 0;
        public long endPos = 0;
        public RunLogReport m_log;
        public CSEV9000APP EV9000App;
        /// <summary>
        /// 播放窗口句柄
        /// </summary>
        public IntPtr play_hwnd = new IntPtr(0);                                    
        /// <summary>
        /// 播放窗口对象
        /// </summary>
        public int playHandle = -1;                                                 
        /// <summary>
        /// 剪辑文件保存位置
        /// </summary>
        public string savePath = "";                                                
        /// <summary>
        /// 文件大小和进度条值比例尺
        /// </summary>
        public double precent = 0;
        /// <summary>
        /// 当前视频文件
        /// </summary>
        public string vedioFile = "";                                               
        //窗体拖动代码
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        public VideoClip()
        {
            InitializeComponent();
            m_log = new RunLogReport("Clip", 30, false);
            EV9000App = new CSEV9000APP();
        }
        /// <summary>
        /// 隐藏编辑面板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pic_close_Click(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                this.Visible = false;
                if (this.playHandle >= 0)
                {
                    EV9000App.EV9000APPClosePlay(this.playHandle);
                }
            }
        }
        /// <summary>
        /// 设置滚动条的最大值和播放视频
        /// </summary>
        /// <param name="value">滚动条的值</param>
        /// <param name="filepath">文件地址</param>
        public void SetProgressBarValueAndFile(string filepath)
        {
            try
            {
                if (filepath != null && !filepath.Equals(""))
                {
                    this.vedioFile = filepath;
                    this.playHandle = EV9000App.EV9000OpenLocalRecord(0, filepath, this.play_hwnd);
                    if (this.playHandle>=0)
                    {
                        m_log.writeRunMsg("播放文件[" + filepath + "]成功");
                    }
                    else 
                    {
                        m_log.writeRunMsg("播放文件[" + filepath + "]失败");
                    }
                    FileInfo movie = new FileInfo(filepath);
                    precent = movie.Length / 100;
                    this.speedBar.Minimum = 0;
                    this.speedBar.Maximum = 100;
                   }
            }
            catch (Exception ew)
            {
                m_log.writeRunErrorMsg("在执行函数[SetProgressBarValueAndFile()]时发生异常，异常原因:" + ew.Message);
            }

        }
        /// <summary>
        /// 设置保存的文件位置
        /// </summary>
        /// <param name="path"></param>
        public void SetSaveFilePath(String path)
        {
            this.savePath = path;
        }
        /// <summary>
        /// 窗口加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VideoClip_Load(object sender, EventArgs e)
        {
            this.clip_play_panel.Location = new Point(3, 30);
            this.clip_play_panel.Size = new Size(this.Size.Width - 6, this.Size.Height - 60);

            this.speed_panel.Location = new Point(4,this.clip_play_panel.Size.Height-1);
            this.speed_panel.Size = new Size(this.Size.Width - 8,30);
            this.speed_panel.BringToFront();
            this.speed_panel.Visible = false;
            this.btn_start.ForeColor = Color.White;
            this.btn_end.ForeColor = Color.White;
            this.btn_start.Cursor = Cursors.Hand;
            this.btn_end.Cursor = Cursors.Hand;
            //初始化EV9000
            this.play_hwnd = this.clip_play_panel.Handle;
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
        }
        /// <summary>
        /// 窗口移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VideoClip_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public bool isBtnStartShow = false;
        private void btn_start_Click(object sender, EventArgs e)
        {
            this.speedBar.Value = 0;
            if (isBtnStartShow)
            {
                this.speed_panel.Visible = false;
                isBtnStartShow = false;
            }
            else
            {
                this.speed_panel.Visible = true;
                isBtnStartShow = true;
                isBtnEndShow = false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public bool isBtnEndShow = false;
        private void btn_end_Click(object sender, EventArgs e)
        {
            this.speedBar.Value = 0;
            if (isBtnEndShow)
            {
                this.speed_panel.Visible = false;
                isBtnEndShow = false;
            }
            else
            {
                this.speed_panel.Visible = true;
                isBtnEndShow = true;
                isBtnStartShow = false;
            }
        }
        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label_ok_Click(object sender, EventArgs e)
        {
            if (isBtnStartShow)
            {
                startPos = (long)(precent*this.speedBar.Value);
            }
            else if (isBtnEndShow)
            {
                endPos = (long)(precent*this.speedBar.Value);
            }
            isBtnStartShow = false;
            isBtnEndShow = false;
            this.speed_panel.Visible = false;
        }
        /// <summary>
        /// 滚动条滚动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void speedBar_Scroll(object sender, EventArgs e)
        {
            if (this.playHandle >= 0)
            {
                EV9000App.EV9000APPSetPostion(this.playHandle, this.speedBar.Value);

                if (isBtnStartShow)
                {
                    startPos = (long)(precent * this.speedBar.Value);
                }
                else if (isBtnEndShow)
                {
                    endPos = (long)(precent * this.speedBar.Value);
                }
            }
        }
        /// <summary>
        /// 完成剪辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ok_Click(object sender, EventArgs e)
        {
            if (startPos < endPos)
            {
                try
                {
                    if (!savePath.Equals(""))
                    {
                        if (!Directory.Exists(savePath))
                        {
                            Directory.CreateDirectory(savePath);
                        }
                        FileStream savefs = null;
                        String clipfile = savePath + "//" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss_clip.wvr");
                        savefs =  new FileStream(clipfile, FileMode.CreateNew);
                        FileStream readfs = new FileStream(this.vedioFile, FileMode.Open, FileAccess.Read, FileShare.Read);
                        byte[] buffer = new byte[1];
                        for (int i = 0; i < endPos; i++)
                        {
                            if (i < startPos)
                            {
                                readfs.Read(buffer, 0, 1);
                            }
                            else
                            {
                                readfs.Read(buffer, 0, 1);
                                savefs.Write(buffer, 0, 1);
                            }
                            
                        }
                        savefs.Flush();
                        readfs.Close();
                        savefs.Close();
                        showInfo("剪辑完成，文件位置：" + clipfile);
                        this.clipFilepath.Text = clipfile;
                    }
                    else
                    {
                        showInfo("保存文件位置未设置");  
                    }
                }
                catch (Exception ew)
                {
                    m_log.writeRunErrorMsg("剪辑文件时出现异常,异常原因:"+ew.Message);
                }
            }
            else
            {
                showInfo("开始位置不能大于结束位置");  
            }
        }
        /// <summary>
        /// 显示提示信息
        /// </summary>
        /// <param name="msg">显示提示信息</param>
        public void showInfo(string msg)
        { 
            this.info_label.Text = msg;
            this.info_label.Location = new Point((int)(this.clip_play_panel.Size.Width - this.info_label.Size.Width) / 2, (int)(this.clip_play_panel.Size.Height - this.info_label.Size.Height) / 2);
            this.info_label.Show();
            this.clip_play_panel.Refresh();
            Thread.Sleep(5000);
            this.info_label.Hide();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clip_play_panel_Click(object sender, EventArgs e)
        {
            if (this.speed_panel.Visible)
            {
                this.speed_panel.Visible = false;
            }
        }
    }
}
