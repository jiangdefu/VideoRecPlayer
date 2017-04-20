using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using EV9000RecPlayer.Event;
using EV9000RecPlayer.EventHander;
using System.Runtime.InteropServices;
using System.IO;

namespace EV9000RecPlayer.Control
{
    public partial class EV9000PlayList : UserControl
    {           
        public List<EV9000List> filelist;           //播放文件列表
        public bool IsshowScrollBar;                //是否显示列表
        public bool isCtrlIsDown;                   //ctrl按键是否按下
        public event KeyEVENTHander KeyPressDown;   //键盘按键事件
        public S50SVRPlayer iev9000player;

        public bool isLeft = false;
        public POINT curpoint;

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

        public EV9000PlayList(S50SVRPlayer obj)
        {
            filelist = new List<EV9000List>();
            IsshowScrollBar = false;
            isCtrlIsDown = false;
            KeyPressDown = null;
            iev9000player = obj;
            InitializeComponent();
        }
        /// <summary>
        /// 初始化窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EV9000PlayList_Load(object sender, EventArgs e)
        {
            initComSizeAndLocation();
        }
        /// <summary>
        /// 初始化控件位置和大小
        /// </summary>
        public void initComSizeAndLocation()
        {
            this.pic_up.Location = new Point(this.Size.Width-this.pic_up.Size.Width, 0);
            this.pic_down.Location = new Point(this.Size.Width - this.pic_down.Size.Height, this.Size.Height -  this.pic_down.Size.Height-this.toolop_panel.Size.Height);
            this.scroll_panel.Size = new Size(this.pic_up.Size.Width, this.Size.Height -this.pic_up.Size.Height- this.pic_down.Size.Height - this.toolop_panel.Size.Height);
            this.scroll_panel.Location = new Point(this.Size.Width - this.scroll_panel.Size.Width, this.pic_up.Size.Height);
            this.toolop_panel.Size = new Size(this.Size.Width, this.toolop_panel.Size.Height);
            this.toolop_panel.Location = new Point(0, this.Size.Height - this.toolop_panel.Size.Height);
            this.pic_add.Location = new Point(0, (int)(this.toolop_panel.Size.Height-this.pic_add.Size.Height)/2);
            this.pic_remove.Location = new Point(this.pic_add.Size.Width+10, (int)(this.toolop_panel.Size.Height - this.pic_remove.Size.Height) / 2);
            this.pic_clear.Location = new Point(this.toolop_panel.Size.Width-this.pic_clear.Size.Width, (int)(this.toolop_panel.Size.Height - this.pic_remove.Size.Height) / 2);
        }
        /// <summary>
        /// 滚动条是否显示
        /// </summary>
        public void IsShowScrollBar()
        {
           if (filelist.Count * 50 > this.Size.Height - this.toolop_panel.Size.Height)
            {
                IsshowScrollBar = true;
                for(int i=0;i<filelist.Count-1;i++)
                {
                    filelist[i].Size = new Size(this.Size.Width - this.scroll_panel.Size.Width, filelist[i].Size.Height);
                }
                this.pic_up.Visible = true;
                this.pic_down.Visible = true;
                this.scroll_panel.Visible = true;
                //int height = this.Size.Height - this.toolop_panel.Size.Height - this.pic_up.Size.Height * 2 - (filelist.Count * 50 - this.Size.Height - this.toolop_panel.Size.Height);
                //int height = 2 * this.Size.Height - this.pic_up.Size.Height * 2 - filelist.Count * 50;//滚动条的高度

                int height = this.scroll_panel.Size.Height - (int)((this.filelist.Count * 50 - this.scroll_panel.Size.Height) / 10);
                //Console.WriteLine(height);
                if (height<=0)
                {
                    this.scroll.BackgroundImage = Properties.Resources.splider;
                    this.scroll.Size = new Size(this.scroll.Size.Width, 16);
                }
                else
                {
                    this.scroll.BackgroundImage = Properties.Resources.listscrollbar;
                    this.scroll.Size = new Size(this.scroll.Size.Width, height);
                }
            }
            else
            {
                for (int i = 0; i < filelist.Count; i++)
                {
                    filelist[i].Size = new Size(this.Size.Width, filelist[i].Size.Height);
                }
                IsshowScrollBar = false;
                this.pic_up.Visible = false;
                this.pic_down.Visible = false;
                this.scroll_panel.Visible = false;
            }
        }
        /// <summary>
        /// 窗口大小改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EV9000PlayList_SizeChanged(object sender, EventArgs e)
        {
            initComSizeAndLocation();
            IsShowScrollBar();
            foreach (EV9000List list in filelist)
            {
                if (IsshowScrollBar)
                {
                    list.Size= new Size(this.Size.Width - this.scroll_panel.Size.Width,list.Size.Height);
                }
                else
                {
                    list.Size = new Size(this.Size.Width,list.Size.Height) ;
                }
            }
        }
        /// <summary>
        /// 鼠标进入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pic_add_MouseEnter(object sender, EventArgs e)
        {
            if (sender == this.pic_add)
            {
                this.pic_add.Image = Properties.Resources.add_on;
            }
            else if(sender==this.pic_remove)
            {
                this.pic_remove.Image = Properties.Resources.remove_on;
            }
            else if (sender == this.pic_clear)
            {
                this.pic_clear.Image = Properties.Resources.clear_on;
            }
            else if (sender == this.pic_up)
            {
                this.pic_up.Image = Properties.Resources.arrow_up_on;
            }
            else if (sender == this.pic_down)
            {
                this.pic_down.Image = Properties.Resources.arrow_down_on;
            }
        }
        /// <summary>
        /// 鼠标离开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pic_add_MouseLeave(object sender, EventArgs e)
        {
            if (sender == this.pic_add)
            {
                this.pic_add.Image = Properties.Resources.add;
            }
            else if (sender == this.pic_remove)
            {
                this.pic_remove.Image = Properties.Resources.remove;
            }
            else if (sender == this.pic_clear)
            {
                this.pic_clear.Image = Properties.Resources.clear;
            }
            else if (sender == this.pic_up)
            {
                this.pic_up.Image = Properties.Resources.arrow_up;
            }
            else if (sender == this.pic_down)
            {
                this.pic_down.Image = Properties.Resources.arrow_down;
            }
        }
        /// <summary>
        /// 添加文件到播放列表
        /// </summary>
        /// <param name="filepath"></param>
        public void AddFileToPlayList(String filepath)
        {
            try
            {
                bool isExist = false;
                foreach (EV9000List list in filelist)
                {
                    if (list.Tag.Equals(filepath))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (!isExist)
                {
                    EV9000List file = new EV9000List();
                    file.Select += new EVENTHander(this.Select);
                    file.DBClick += new EVENTHander(this.DBClick);
                    file.MouseScroll += new MouseEventHandler(EV9000PlayList_MouseWheel);
                    file.KeyPressDown += new KeyEVENTHander(this.EV9000PlayList_KeyDown);
                    file.Tag = filepath;
                    file.filepath = filepath;
                    this.filelist.Add(file);
                    file.setFilePath(filepath);
                    IsShowScrollBar();
                    if (IsshowScrollBar)
                    {
                        file.Size = new Size(this.Size.Width - this.pic_up.Size.Width - 1, 48);
                    }
                    else
                    {
                        file.Size = new Size(this.Size.Width - 1, 48);
                    }
                    file.Location = new Point(1, (filelist.Count - 1) * 50);
                    this.Controls.Add(file);
                }
            }
            catch (Exception ew)
            {
                iev9000player.m_log.writeRunErrorMsg("在调用函数[AddFileToPlayList(" + filepath + ")]发生异常，异常原因：" + ew.Message); 
            }
        }
        /// <summary>
        /// 通过文件路径播放文件
        /// </summary>
        /// <param name="filepath"></param>
        public void PlayVideoByFilePath(String filepath,String fileType)
        {
            if (!filepath.Equals(""))
            {
                try
                {
                    iev9000player.PlayFileByFilePath(filepath,fileType);
                    SetCurrentPlayFile(filepath);
                    iev9000player.StartPlayTimer();
                }
                catch (Exception ew)
                {
                    iev9000player.m_log.writeRunErrorMsg("在调用函数[PlayVideoByFilePath(" + filepath + ")]发生异常，异常原因：" + ew.Message);
                }
            }
        }
        /// <summary>
        /// 设置当前正在播放的
        /// </summary>
        /// <param name="filepath"></param>
        public void SetCurrentPlayFile(String filepath)
        {
            EV9000List list = new EV9000List();
            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (this.Controls[i].GetType().FullName.Equals(list.GetType().FullName))
                {
                    EV9000List lctr = (EV9000List)this.Controls[i];
                    if (lctr.Tag.Equals(filepath))
                    {
                        lctr.SetCurrentPlay();
                    }
                    else
                    {
                        lctr.SetNotSelect();
                    }
                }
            }
        }
        /// <summary>
        /// 选中事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Select(object sender, EV9000Event e)
        {
            //Console.WriteLine(e.filepath);
            foreach (EV9000List list in filelist)
            {
                if (sender != list && !list.IsCurrentPlay)
                {
                    if (!isCtrlIsDown)
                    {
                        list.SetNotSelect();
                    }
                }
            }
        }
        /// <summary>
        /// 双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DBClick(object sender, EV9000Event e)
        {
            foreach (EV9000List list in filelist)
            {
                if (sender != list)
                {
                    list.SetNotSelect();
                }
            }
            if (!e.filepath.Equals(""))
            {
                iev9000player.PlayFileByFilePath(e.filepath.Trim(), e.filepath.Substring(e.filepath.LastIndexOf(".")+1).ToLower());
                iev9000player.StartPlayTimer();
            }
        }
        /// <summary>
        /// 鼠标滚轮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void EV9000PlayList_MouseWheel(object sender, MouseEventArgs e)
        {
            double listmoveheight = (double)(this.scroll_panel.Size.Height - this.scroll.Size.Height) / (this.filelist.Count * 50 - (this.Size.Height - this.toolop_panel.Size.Height));
            double sw = listmoveheight * (0 - filelist[0].Location.Y);
            if (IsshowScrollBar)
            {
                int space = (filelist.Count * 50 + filelist[0].Location.Y) - (this.Size.Height - this.toolop_panel.Size.Height);
                if (filelist[0].Location.Y<=0 &&  space>=0)
                {
                 
                    if (filelist[0].Location.Y +(int)(e.Delta / 10) >= 0)
                    {
                        MouseWheelPlayList(0);
                    }
                    else
                    {
                        int wid =(filelist.Count * 50 + filelist[0].Location.Y + (int)(e.Delta / 10)) - (this.Size.Height - this.toolop_panel.Size.Height);
                        if (wid <= 0)
                        {
                            filelist[0].Location = new Point(1,(this.Size.Height-this.toolop_panel.Size.Height)-filelist.Count*50);
                        }
                        else
                        {
                            MouseWheelPlayList((int)(e.Delta / 10));
                        }
                    }
                    Console.WriteLine("scorll:"+sw);
                    if(sw!=0)
                    {
                        this.scroll.Location = new Point(0, (int)sw);
                    }
                }
            }
        }
        /// <summary>
        /// 清空播放列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pic_clear_Click(object sender, EventArgs e)
        {
            EV9000List list  = new EV9000List();
            List<EV9000List> EVlist = new List<EV9000List>();
            for (int i = 0; i < this.Controls.Count;i++)
            {
                if (this.Controls[i].GetType().FullName.Equals(list.GetType().FullName))
                {
                    Console.WriteLine(this.Controls[i].GetType().FullName);
                    EVlist.Add((EV9000List)this.Controls[i]);
                } 
            }
            foreach (EV9000List lctr in EVlist)
            {
                this.Controls.Remove(lctr);
            }
            this.filelist.Clear();
        }
        /// <summary>
        /// 删除播放列表文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pic_remove_Click(object sender, EventArgs e)
        {
            EV9000List list = new EV9000List();
            List<EV9000List> EVlist = new List<EV9000List>();
            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (this.Controls[i].GetType().FullName.Equals(list.GetType().FullName))
                {
                    EV9000List lctr = (EV9000List)this.Controls[i];
                    if (lctr.IsSelect&&!lctr.IsCurrentPlay)
                    {
                        EVlist.Add(lctr);
                    }
                }
            }
            foreach (EV9000List lctr in EVlist)
            {
                this.Controls.Remove(lctr);
                this.filelist.Remove(lctr);
            }
            PlayListRefresh(0);
        }
        /// <summary>
        /// 键盘按下事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EV9000PlayList_KeyDown(object sender, KeyEventArgs e)
        {
            if(KeyPressDown!=null)
            {
                KeyPressDown(sender,e);
            }
        }

        /// <summary>
        /// 刷新列表
        /// </summary>
        /// <param name="location">初始位置</param>
        public void PlayListRefresh(int location)
        {
            if (location == 0)
            {
                IsShowScrollBar();
                for (int i = 0; i < filelist.Count;i++ )
                {
                    filelist[i].Location = new Point(1, i * 50);
                }
            }
           
        }
        /// <summary>
        /// 更新列表
        /// </summary>
        /// <param name="location"></param>
        public void RePaintList(int location)
        {
            if (location != 0)
            {
                Console.WriteLine(this.scroll.Location.Y);
                if (this.scroll.Location.Y == 1)
                {
                    filelist[0].Location = new Point(1, 0);
                }
                else
                {
                    filelist[0].Location = new Point(1, 0 - location);
                }
                for (int i = 1; i < filelist.Count; i++)
                {
                    filelist[i].Location = new Point(1, filelist[i-1].Location.Y+50);
                } 
            }
        }
        /// <summary>
        /// 滚轮滚动重绘播放列表
        /// </summary>
        /// <param name="location"></param>
        public void MouseWheelPlayList(int location)
        {
            if (location != 0)
            {
                filelist[0].Location = new Point(1, filelist[0].Location.Y+location);
                for (int i = 1; i < filelist.Count; i++)
                {
                    filelist[i].Location = new Point(1, filelist[i - 1].Location.Y + 50);
                }
            }
            else
            {
                for (int i = 0; i < filelist.Count; i++)
                {
                    filelist[i].Location = new Point(1, i * 50);
                }
            }
        }

        /// <summary>
        /// 鼠标按下选中滚动条
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scroll_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            { 
                isLeft = true;
                GetCursorPos(out curpoint);
            }
        }
        /// <summary>
        /// 鼠标停止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scroll_MouseUp(object sender, MouseEventArgs e)
        {
            isLeft = false;
        }
        /// <summary>
        /// 鼠标移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EV9000PlayList_MouseMove(object sender, MouseEventArgs e)
        {
            //Console.WriteLine(e.Y);
            if (isLeft && IsshowScrollBar)
            {
                POINT p;
                GetCursorPos(out p);
                int offset = p.Y - curpoint.Y;
                //移动百分比
                double listmoveheight = (double)(this.filelist.Count*50-(this.Size.Height-this.toolop_panel.Size.Height))/(this.scroll_panel.Size.Height-this.scroll.Size.Height);
                //Console.WriteLine(listmoveheight);
                if (this.scroll.Location.Y >= 0 && this.scroll.Location.Y + offset + this.scroll.Size.Height <= this.scroll_panel.Size.Height)
                {
                    if (this.scroll.Location.Y + offset <0)
                    {
                        this.scroll.Location = new Point(0, 0);
                    }
                    else
                    {
                        this.scroll.Location = new Point(0, this.scroll.Location.Y + offset);
                    }
                    double move =(double)(listmoveheight * this.scroll.Location.Y);
                    this.RePaintList((int)move);
                    curpoint = p;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pic_add_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Multiselect = true;
                dlg.SupportMultiDottedExtensions = true;
                dlg.Filter = "视频文件(*.wvr)|*.wvr|视频文件(*.avi)|*.avi|视频文件(*.h264)|*.h264|所有文件(*.*)|*.*";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    foreach (string s in dlg.FileNames)
                    {
                        iev9000player.playList.AddFileToPlayList(s);             //添加文件到播放列表
                    }
                }
            }
            catch (Exception ew)
            {
                iev9000player.m_log.writeRunErrorMsg("添加文件失败:" + ew.Message);
            }
        }

        /// <summary>
        /// 获取当前播放的索引
        /// </summary>
        /// <returns></returns>
        public int GetCurrentIndex()
        {
            int i = 0;
            foreach (EV9000List list in filelist)
            {
                if (list.IsCurrentPlay)
                {
                    break;
                }
                i++;
            }
            return i;
        }
    }
}
