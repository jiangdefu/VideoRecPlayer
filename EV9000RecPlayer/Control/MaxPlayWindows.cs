using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EV9000RecPlayer.Control
{
    public partial class MaxPlayWindows : Form
    {
        S50SVRPlayer player;               //播放器对象
        public MaxPlayWindows(S50SVRPlayer appplayer)
        {
            this.player = appplayer;
            InitializeComponent();
        }
        private void MaxPlayWindows_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27)///esc 退出全屏
            {
                if (player.playHwndisMax == true)
                {
                    player.ExitFullScreen();
                }
            }
            if (e.KeyValue == 32)
            {
                if (player.isvideoplay)
                {
                    player.Pause();
                }
                else
                {
                    player.Play();
                }
            }
        }
    }
}