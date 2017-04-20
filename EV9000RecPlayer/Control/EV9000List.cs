using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using EV9000RecPlayer.Event;
using EV9000RecPlayer.EventHander;

namespace EV9000RecPlayer.Control
{
    public partial class EV9000List : UserControl
    {
        public string filepath;                                         //文件路径

        public bool IsSelect;                                           //是否选中

        public bool IsCurrentPlay;                                      //是否正在播放
        public event EVENTHander Select;                                //选中事件
        public event EVENTHander DBClick;                               //双击事件 
        public event KeyEVENTHander KeyPressDown;

        public event MouseEventHandler MouseScroll;                     //鼠标滚轮事件
        
        public EV9000List()
        {
            /*****************************************/
            //防止界面出现闪烁现象
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
            /*****************************************/
            IsCurrentPlay = false;
            IsSelect = false;
            Select = null;
            DBClick = null;
            MouseScroll = null;
            InitializeComponent();
        }
        /// <summary>
        /// 窗口加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EV9000List_Load(object sender, EventArgs e)
        {
            initFilename();
        }
        /// <summary>
        /// 初始化 显示
        /// </summary>
        public void initFilename()
        {
            this.carmera.Location = new Point(7, 9);
            this.filename.Location = new Point(this.carmera.Location.X+this.carmera.Size.Width + 5, 8);
            this.filename.Size = new Size(this.Size.Width-this.carmera.Size.Width-11,16);
            this.filename2.Location = new Point(3, this.filename.Location.Y + this.filename.Size.Height + 2);
            this.filename2.Size = new Size(this.Size.Width - 6, 16);
            
        }
        /// <summary>
        /// 设置文件目录
        /// </summary>
        /// <param name="filepath"></param>
        public void setFilePath(string file)
        {
            this.filepath = file;
            if (file.LastIndexOf('\\') > 0)
            {
                file = file.Substring(file.LastIndexOf('\\') + 1);
            }
            if (file.Length <= 25)
            {
                this.filename.Text = file.Substring(0, file.LastIndexOf('.'));
            }
            else
            {
                this.filename.Text = file.Substring(0, 19);
                this.filename2.Text = file.Substring(19, file.LastIndexOf('.') - 19);
            }
            this.ttip.SetToolTip(this.filename, file);
            this.ttip.SetToolTip(this.filename2, file);
        }
        /// <summary>
        /// 单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void filename_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = Properties.Resources.listbgfocus;
            this.carmera.Image = Properties.Resources.carmera_on;
            this.filename.ForeColor = Color.White;
            this.filename2.ForeColor = Color.White;
            IsSelect = true;
            if (Select != null)
            {
                Select(this,new EV9000Event(0,filepath));
            }
        }
        /// <summary>
        /// 灰色其他
        /// </summary>
        public void SetNotSelect()
        {
            this.BackgroundImage = Properties.Resources.listbg;
            this.carmera.Image = Properties.Resources.carmera;
            this.filename.ForeColor = Color.Gainsboro;
            this.filename2.ForeColor = Color.Gainsboro;
            IsSelect = false;
            IsCurrentPlay = false;
        }
        /// <summary>
        /// 双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EV9000List_DoubleClick(object sender, EventArgs e)
        {
            this.BackgroundImage = Properties.Resources.listbg;
            this.carmera.Image = Properties.Resources.carmera;
            IsSelect = false;
            this.filename.ForeColor =Color.FromArgb(0, 100, 0);
            this.filename2.ForeColor = Color.FromArgb(0, 100, 0);
            IsCurrentPlay = true;
            if (DBClick != null)
            {
                DBClick(this, new EV9000Event(0, filepath));
            }
        }
        /// <summary>
        /// 设置当前正在播放的状态
        /// </summary>
        public void SetCurrentPlay()
        {
            this.BackgroundImage = Properties.Resources.listbg;
            this.carmera.Image = Properties.Resources.carmera;
            IsSelect = false;
            this.filename.ForeColor = Color.FromArgb(0, 100, 0);
            this.filename2.ForeColor = Color.FromArgb(0, 100, 0);
            IsCurrentPlay = true;
        }
        /// <summary>
        /// 鼠标滚轮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void EV9000List_MouseWheel(object sender, MouseEventArgs e)
        {
            if (this.MouseScroll!=null)
            {
                MouseScroll(sender, e);
            }
        }
        /// <summary>
        /// 键盘按下操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EV9000List_KeyDown(object sender, KeyEventArgs e)
        {
            if (KeyPressDown != null)
            {
                KeyPressDown(sender,e);
            }
        }

        private void EV9000List_Resize(object sender, EventArgs e)
        {
            initFilename();
        }
    }
}
