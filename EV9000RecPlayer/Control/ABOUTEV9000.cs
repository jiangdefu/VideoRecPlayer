using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace EV9000RecPlayer.Control
{
    public partial class ABOUUTEV9000 : UserControl
    {
        //�����϶�����
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        public ABOUUTEV9000()
        {
            //��ֹ���������˸����
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // ��ֹ��������.
            SetStyle(ControlStyles.DoubleBuffer, true); // ˫����
            /*****************************************/
            InitializeComponent();
        }
        /// <summary>
        /// �򿪹���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label1_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.wisvision.com.cn/");
        }
        /// <summary>
        /// �رմ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void close_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        /// <summary>
        /// �����϶�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AboutEV9000_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void close_MouseEnter(object sender, EventArgs e)
        {
            this.close.Image = Properties.Resources.close_on;
        }
        /// <summary>
        /// ����Ƴ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void close_MouseLeave(object sender, EventArgs e)
        {
            this.close.Image = Properties.Resources.close;
        }

      
       
    }
}
