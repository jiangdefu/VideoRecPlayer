using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using EV9000RecPlayer.Event;
using EV9000RecPlayer.EventHander;
namespace EV9000RecPlayer.Control
{
    
    public partial class ScrollBar : UserControl
    {
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

       
        public int value = 0;                                           //��������ǰֵ

        public long maxValue ;                                           //���ֵ

        public event EVENTHander Scroll;                                //�����¼�

        public event MouseEventHandler ScrollBarMouseDown;              //�϶���갴���¼�

        public event MouseEventHandler ScrollBarMouseUp;                //�϶�����ͷ��¼�

        public bool isleftMouseDown = false;                            //�������Ƿ���

        public float sizePercent;                                       //�����С����

        public ScrollBar()
        {
            /*****************************************/
            //��ֹ���������˸����
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // ��ֹ��������.
            SetStyle(ControlStyles.DoubleBuffer, true); // ˫����
            /*****************************************/
            maxValue = 0;
            InitializeComponent();
        }

     
        /// <summary>
        /// ��ʼ��λ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScrollBar_Load(object sender, EventArgs e)
        {
            this.scrollmove_panel.Size = new Size(0,20);
            this.scrollmove_panel.Location = new Point(0, 0);
            this.move_panel.Size = new Size(20, 20);
            this.move_panel.Location = new Point(this.scrollmove_panel.Size.Width, 0);
            sizePercent = this.scrollmove_panel.Size.Width / (this.Size.Width - this.move_panel.Size.Width);
        }

        public POINT currentpoint;                                      //��갴��λ�õ�����
        /// <summary>
        /// ��갴���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void move_panel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isleftMouseDown = true;
                GetCursorPos(out currentpoint);
                if (ScrollBarMouseDown!=null)
                {
                    ScrollBarMouseDown(sender, e);
                }
            }
        }
        /// <summary>
        /// ��굯���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void move_panel_MouseUp(object sender, MouseEventArgs e)
        {
            isleftMouseDown = false;
            if (ScrollBarMouseUp!=null)
            {
                ScrollBarMouseUp(sender,e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScrollBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (isleftMouseDown)
            {
                POINT p;
                GetCursorPos(out p);
                int offset = p.X - currentpoint.X;
                //Console.WriteLine("offset:"+offset);
                if (this.move_panel.Location.X >= 0 && this.scrollmove_panel.Size.Width + offset + this.move_panel.Size.Width < this.Size.Width)
                {
                    this.scrollmove_panel.Size = new Size(this.scrollmove_panel.Size.Width + offset, this.scrollmove_panel.Size.Height);
                    //Console.WriteLine("��ǰ���Ź��Ĵ�С:[X=" + this.scrollmove_panel.Size.Width + " Y=" + this.scrollmove_panel.Size.Height + "]");
                    this.move_panel.Location = new Point(this.scrollmove_panel.Size.Width, 0);
                    //Console.WriteLine("��ǰ�����λ��:[X=" + this.move_panel.Location.X + " Y=" + this.move_panel.Location.Y + "]");
                    sizePercent = (float)this.scrollmove_panel.Size.Width / (this.Size.Width - this.move_panel.Size.Width);
                    this.Invalidate();
                    currentpoint = p;
                }
                this.value = this.scrollmove_panel.Size.Width;
                if (Scroll!=null)
                {
                    Scroll(this, new EV9000Event(this.value,""));
                }
            }
        }
        /// <summary>
        /// ���ù�������ǰֵ
        /// </summary>
        /// <param name="value"></param>
        public void SetPos(long value)
        {
            if (value == 0)
            {
                this.scrollmove_panel.Size = new Size(0, this.scrollmove_panel.Size.Height);
                this.move_panel.Location = new Point(0, 0);
            }
            else
            {
                if (maxValue != 0)
                {
                    int moviebarsize = this.Size.Width - this.move_panel.Size.Width;
                    long movesize = moviebarsize * value;
                    //Console.WriteLine("��������С��" + moviebarsize + "  value:" + value + "  maxValue:" + maxValue + "  movesize:" + movesize);
                    double wid = movesize/maxValue;
                    //Console.WriteLine(wid);
                    this.scrollmove_panel.Size = new Size((int)wid, this.scrollmove_panel.Size.Height);
                    this.move_panel.Location = new Point((int)wid, 0);
                }
            }
        }
        public void SetValue(long value)
        {
            maxValue = value;
        }
        /// <summary>
        /// ��ȡValue���ֵ
        /// </summary>
        public int getMaxValue()
        {
            return this.Size.Width - this.move_panel.Size.Width;
        }
        /// <summary>
        /// �����С�����仯
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScrollBar_SizeChanged(object sender, EventArgs e)
        {
            int width = (int)(this.sizePercent * this.Size.Width);
            if (width + this.move_panel.Size.Width < this.Size.Width)
            {
                this.scrollmove_panel.Size = new Size(width, this.scrollmove_panel.Size.Height);
                this.move_panel.Location = new Point(this.scrollmove_panel.Size.Width, 0);
            }
            else
            {
                this.scrollmove_panel.Size = new Size(this.Size.Width-this.move_panel.Size.Width, this.scrollmove_panel.Size.Height);
                this.move_panel.Location = new Point(this.scrollmove_panel.Size.Width, 0);
            }
            this.value = this.scrollmove_panel.Size.Width;
        }
    }
}
