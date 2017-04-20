using System;
using System.Collections.Generic;
using System.Text;

namespace EV9000Player.Util
{
    [Serializable]
    public class VideoFile
    {
        public string filename;         //�ļ�����
        public string filepath;         //�ļ�·��
        public bool isPlaying;          //�Ƿ����ڲ���
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
