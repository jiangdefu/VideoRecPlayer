using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace EV9000RecPlayer.Control
{
    public partial class EV9000Config : UserControl
    {
        public string rec_path;                 //录像地址
        public string cap_path;                 //抓图地址


        //窗体拖动代码
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        public EV9000Config()
        {
            //防止界面出现闪烁现象
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
            /*****************************************/
            //rec_path = "C:\\WISCOM\\EV9000Player\\Record";
            //cap_path="C:\\WISCOM\\EV9000Player\\Picture";
            rec_path = "C:\\S50SVRPlayer\\Record";
            cap_path = "C:\\S50SVRPlayer\\Picture";
            InitializeComponent();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public void initTBShow()
        {
            this.tb_cappath.Text = cap_path;
            this.tb_recpath.Text = rec_path;
        }
        /// <summary>
        /// 鼠标进入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picclose_MouseEnter(object sender, EventArgs e)
        {
            this.picclose.Image = Properties.Resources.close_on;
        }
        /// <summary>
        /// 鼠标移出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picclose_MouseLeave(object sender, EventArgs e)
        {
            this.picclose.Image = Properties.Resources.close;
        }
        /// <summary>
        /// 点击关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picclose_MouseClick(object sender, MouseEventArgs e)
        {
            this.Hide();
        }
        /// <summary>
        /// 窗体初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EV9000Config_Load(object sender, EventArgs e)
        {
            initTBShow();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EV9000Config_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tb_cappath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.ShowNewFolderButton = true;
            dlg.Description = "请选择文件保存路径";
            if (sender == this.tb_cappath)
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    this.cap_path=dlg.SelectedPath.ToString();
                }
            }
            else if (sender == this.tb_recpath)
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    this.rec_path = dlg.SelectedPath.ToString();

                }
            }
            initTBShow();
        }
    }
}
