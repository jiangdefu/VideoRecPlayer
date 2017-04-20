using System;
using System.Collections.Generic;
using System.Text;

namespace EV9000RecPlayer.Event
{
    public class EV9000Event:EventArgs
    {
        public int value;
        public string filepath;
        public EV9000Event(int value, string filepath)
        {
            this.value = value;
            this.filepath = filepath;
        }
    }
}
