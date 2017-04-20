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
        public string rec_path;                 //¼���ַ
        public string cap_path;                 //ץͼ��ַ


        //�����϶�����
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        public EV9000Config()
        {
            //��ֹ���������˸����
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // ��ֹ��������.
            SetStyle(ControlStyles.DoubleBuffer, true); // ˫����
            /*****************************************/
            //rec_path = "C:\\WISCOM\\EV9000Player\\Record";
            //cap_path="C:\\WISCOM\\EV9000Player\\Picture";
            rec_path = "C:\\S50SVRPlayer\\Record";
            cap_path = "C:\\S50SVRPlayer\\Picture";
            InitializeComponent();
        }
        /// <summary>
        /// ��ʼ��
        /// </summary>
        public void initTBShow()
        {
            this.tb_cappath.Text = cap_path;
            this.tb_recpath.Text = rec_path;
        }
        /// <summary>
        /// ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picclose_MouseEnter(object sender, EventArgs e)
        {
            this.picclose.Image = Properties.Resources.close_on;
        }
        /// <summary>
        /// ����Ƴ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picclose_MouseLeave(object sender, EventArgs e)
        {
            this.picclose.Image = Properties.Resources.close;
        }
        /// <summary>
        /// ����ر�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picclose_MouseClick(object sender, MouseEventArgs e)
        {
            this.Hide();
        }
        /// <summary>
        /// �����ʼ��
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
            dlg.Description = "��ѡ���ļ�����·��";
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
