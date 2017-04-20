using System;
using System.Collections.Generic;
using System.Text;

namespace EV9000Player.Util
{
    [Serializable]
    public class VideoFile
    {
        public string filename;         //文件名称
        public string filepath;         //文件路径
        public bool isPlaying;          //是否正在播放
        public string Filename
        {
            get { return filename; }
            set { filename = value; }
        }
        public string Filepath
        {
            get { return filepath; }
            set { filepath = value; }
        }
        public bool IsPlaying
        {
            get { return isPlaying; }
            set { isPlaying = value; }
        }
    }
}
