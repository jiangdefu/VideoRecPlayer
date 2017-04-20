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
        /// ���Ŵ��ھ��
        /// </summary>
        public IntPtr play_hwnd = new IntPtr(0);                                    
        /// <summary>
        /// ���Ŵ��ڶ���
        /// </summary>
        public int playHandle = -1;                                                 
        /// <summary>
        /// �����ļ�����λ��
        /// </summary>
        public string savePath = "";                                                
        /// <summary>
        /// �ļ���С�ͽ�����ֵ������
        /// </summary>
        public double precent = 0;
        /// <summary>
        /// ��ǰ��Ƶ�ļ�
        /// </summary>
        public string vedioFile = "";                                               
        //�����϶�����
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
        /// ���ر༭���
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
        /// ���ù����������ֵ�Ͳ�����Ƶ
        /// </summary>
        /// <param name="value">��������ֵ</param>
        /// <param name="filepath">�ļ���ַ</param>
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
                        m_log.writeRunMsg("�����ļ�[" + filepath + "]�ɹ�");
                    }
                    else 
                    {
                        m_log.writeRunMsg("�����ļ�[" + filepath + "]ʧ��");
                    }
                    FileInfo movie = new FileInfo(filepath);
                    precent = movie.Length / 100;
                    this.speedBar.Minimum = 0;
                    this.speedBar.Maximum = 100;
                   }
            }
            catch (Exception ew)
            {
                m_log.writeRunErrorMsg("��ִ�к���[SetProgressBarValueAndFile()]ʱ�����쳣���쳣ԭ��:" + ew.Message);
            }

        }
        /// <summary>
        /// ���ñ�����ļ�λ��
        /// </summary>
        /// <param name="path"></param>
        public void SetSaveFilePath(String path)
        {
            this.savePath = path;
        }
        /// <summary>
        /// ���ڼ���
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
            //��ʼ��EV9000
            this.play_hwnd = this.clip_play_panel.Handle;
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
        }
        /// <summary>
        /// �����ƶ�
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
        /// ��ȡֵ
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
        /// ����������
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
        /// ��ɼ���
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
                        showInfo("������ɣ��ļ�λ�ã�" + clipfile);
                        this.clipFilepath.Text = clipfile;
                    }
                    else
                    {
                        showInfo("�����ļ�λ��δ����");  
                    }
                }
                catch (Exception ew)
                {
                    m_log.writeRunErrorMsg("�����ļ�ʱ�����쳣,�쳣ԭ��:"+ew.Message);
                }
            }
            else
            {
                showInfo("��ʼλ�ò��ܴ��ڽ���λ��");  
            }
        }
        /// <summary>
        /// ��ʾ��ʾ��Ϣ
        /// </summary>
        /// <param name="msg">��ʾ��ʾ��Ϣ</param>
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
