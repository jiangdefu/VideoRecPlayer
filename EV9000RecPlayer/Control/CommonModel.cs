using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace EV9000RecPlayer.Control
{
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;
        public POINT(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
