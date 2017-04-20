using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace EV9000RecPlayer
{
    public partial class Menu : UserControl
    {
        public S50SVRPlayer player;
        public Menu(S50SVRPlayer iplayer)
        {
            player = iplayer;
            InitializeComponent();
        }
        /// <summary>
        /// 鼠标进入事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openfile_MouseEnter(object sender, EventArgs e)
        {
            if (sender == this.openfile)
            {
                this.openfile.ForeColor = Color.White;
            }
            else if (sender == this.addfile)
            {
                this.addfile.ForeColor = Color.White;
            }
            else if (sender == this.opendir)
            {
                this.opendir.ForeColor = Color.White;
            }
            else if (sender == this.about)
            {
                this.about.ForeColor = Color.White;
            }
            else if (sender == this.winhelp)
            {
                this.winhelp.ForeColor = Color.White;
            }
            else if (sender == this.clip)
            {
                this.clip.ForeColor = Color.White;
            }
        }
        /// <summary>
        /// 鼠标移出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openfile_MouseLeave(object sender, EventArgs e)
        {
            if (sender == this.openfile)
            {
                this.openfile.ForeColor = Color.Gainsboro;
            }
            else if (sender == this.addfile)
            {
                this.addfile.ForeColor = Color.Gainsboro;
            }
            else if (sender == this.opendir)
            {
                this.opendir.ForeColor = Color.Gainsboro;
            }
            else if (sender == this.about)
            {
                this.about.ForeColor = Color.Gainsboro;
            }
            else if (sender == this.winhelp)
            {
                this.winhelp.ForeColor = Color.Gainsboro;
            }
            else if (sender == this.clip)
            {
                this.clip.ForeColor = Color.Gainsboro;
            }
        }
        /// <summary>
        /// 打开播放的文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openfile_Click(object sender, EventArgs e)
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
                        player.playList.AddFileToPlayList(s);               //添加文件到播放列表
                    }
                    player.playList.PlayVideoByFilePath(dlg.FileNames[0], dlg.FileNames[0].Substring(dlg.FileNames[0].LastIndexOf(".")+1).ToLower());
                }
            }
            catch (Exception ew)
            {
                player.m_log.writeRunErrorMsg("打开文件发生异常，异常原因:" + ew.Message);
            }
            this.Visible = false;
        }
        /// <summary>
        /// 添加播放的文件到列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addfile_Click(object sender, EventArgs e)
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
                        player.playList.AddFileToPlayList(s);             //添加文件到播放列表
                    }
                }
            }
            catch (Exception ew)
            {
                player.m_log.writeRunErrorMsg("打开文件失败:" + ew.Message);
            }
            this.Visible = false;
        }
        /// <summary>
        /// 添加文件夹到列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void opendir_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog dlg = new FolderBrowserDialog();
                dlg.Description = "选择要添加的视频文件夹";
                dlg.ShowNewFolderButton = false;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    if (dlg.SelectedPath != null && !dlg.SelectedPath.Equals(""))
                    {
                        DirectoryInfo directory = new DirectoryInfo(dlg.SelectedPath);
                        FileInfo[] files = directory.GetFiles();
                        foreach (FileInfo file in files)
                        {
                            if (file.Name.Substring(file.Name.Length - 4).ToLower().Equals(".wvr") || file.Name.Substring(file.Name.Length - 4).ToLower().Equals(".avi") || file.Name.Substring(file.Name.Length - 5).ToLower().Equals(".h264"))
                            {
                                player.playList.AddFileToPlayList(file.FullName);
                            }
                        }
                    }
                }
            }
            catch (Exception ew)
            {
                player.m_log.writeRunErrorMsg("添加文件夹到播放列表发生异常，异常原因："+ew.Message); 
            }
            this.Visible = false;
        }
        /// <summary>
        /// 帮助菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void winhelp_Click(object sender, EventArgs e)
        {
            //Process.Start(Application.StartupPath + "\\EV9000Player.chm");
            this.Visible = false;
        }
        /// <summary>
        /// 关于菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void about_Click(object sender, EventArgs e)
        {
            if (player.about.Visible == true)
            {
                player.ShowAbout(false);
            }
            else
            {
                player.ShowAbout(true);
            }
            this.Visible = false;
        }
        /// <summary>
        /// 打开视频剪辑面板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public bool isShow = false;
        private void clip_Click(object sender, EventArgs e)
        {
            if (isShow)
            {
                isShow = false;
                player.initClipVedio(isShow);
            }
            else
            {
                isShow = true;
                player.initClipVedio(isShow);
                player.clipVedio.SetProgressBarValueAndFile(player.currentFilepath);
            }
            this.Visible = false;
        }

    }
}
