using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace EV9000RecPlayer.Control
{
    public class EV9000Panel:Panel
    {
        public EV9000Panel():base()
        {
            /*****************************************/
            //防止界面出现闪烁现象
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
            /*****************************************/
        }
    }
}
