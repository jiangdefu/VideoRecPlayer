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
            //��ֹ���������˸����
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // ��ֹ��������.
            SetStyle(ControlStyles.DoubleBuffer, true); // ˫����
            /*****************************************/
        }
    }
}
