using System;
using System.Collections.Generic;
using System.Text;
using EV9000RecPlayer.Event;
using System.Windows.Forms;

namespace EV9000RecPlayer.EventHander
{
    public delegate void EVENTHander(object sender, EV9000Event e); //¶¨ÒåÎ¯ÍÐ
    public delegate void KeyEVENTHander(object sender,KeyEventArgs e);
}
