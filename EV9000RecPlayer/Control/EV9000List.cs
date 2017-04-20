using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using EV9000RecPlayer.Event;
using EV9000RecPlayer.EventHander;

namespace EV9000RecPlayer.Control
{
    public partial class EV9000List : UserControl
    {
        public string filepath;                                         //�ļ�·��

        public bool IsSelect;                                           //�Ƿ�ѡ��

        public bool IsCurrentPlay;                                      //�Ƿ����ڲ���
        public event EVENTHander Select;                                //ѡ���¼�
        public event EVENTHander DBClick;                               //˫���¼� 
        public event KeyEVENTHander KeyPressDown;

        public event MouseEventHandler MouseScroll;                     //�������¼�
        
        public EV9000List()
        {
            /*****************************************/
            //��ֹ���������˸����
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // ��ֹ��������.
            SetStyle(ControlStyles.DoubleBuffer, true); // ˫����
            /*****************************************/
            IsCurrentPlay = false;
            IsSelect = false;
            Select = null;
            DBClick = null;
            MouseScroll = null;
            InitializeComponent();
        }
        /// <summary>
        /// ���ڼ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EV9000List_Load(object sender, EventArgs e)
        {
            initFilename();
        }
        /// <summary>
        /// ��ʼ�� ��ʾ
        /// </summary>
        public void initFilename()
        {
            this.carmera.Location = new Point(7, 9);
            this.filename.Location = new Point(this.carmera.Location.X+this.carmera.Size.Width + 5, 8);
            this.filename.Size = new Size(this.Size.Width-this.carmera.Size.Width-11,16);
            this.filename2.Location = new Point(3, this.filename.Location.Y + this.filename.Size.Height + 2);
            this.filename2.Size = new Size(this.Size.Width - 6, 16);
            
        }
        /// <summary>
        /// �����ļ�Ŀ¼
        /// </summary>
        /// <param name="filepath"></param>
        public void setFilePath(string file)
        {
            this.filepath = file;
            if (file.LastIndexOf('\\') > 0)
            {
                file = file.Substring(file.LastIndexOf('\\') + 1);
            }
            if (file.Length <= 25)
            {
                this.filename.Text = file.Substring(0, file.LastIndexOf('.'));
            }
            else
            {
                this.filename.Text = file.Substring(0, 19);
                this.filename2.Text = file.Substring(19, file.LastIndexOf('.') - 19);
            }
            this.ttip.SetToolTip(this.filename, file);
            this.ttip.SetToolTip(this.filename2, file);
        }
        /// <summary>
        /// �����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void filename_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = Properties.Resources.listbgfocus;
            this.carmera.Image = Properties.Resources.carmera_on;
            this.filename.ForeColor = Color.White;
            this.filename2.ForeColor = Color.White;
            IsSelect = true;
            if (Select != null)
            {
                Select(this,new EV9000Event(0,filepath));
            }
        }
        /// <summary>
        /// ��ɫ����
        /// </summary>
        public void SetNotSelect()
        {
            this.BackgroundImage = Properties.Resources.listbg;
            this.carmera.Image = Properties.Resources.carmera;
            this.filename.ForeColor = Color.Gainsboro;
            this.filename2.ForeColor = Color.Gainsboro;
            IsSelect = false;
            IsCurrentPlay = false;
        }
        /// <summary>
        /// ˫���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EV9000List_DoubleClick(object sender, EventArgs e)
        {
            this.BackgroundImage = Properties.Resources.listbg;
            this.carmera.Image = Properties.Resources.carmera;
            IsSelect = false;
            this.filename.ForeColor =Color.FromArgb(0, 100, 0);
            this.filename2.ForeColor = Color.FromArgb(0, 100, 0);
            IsCurrentPlay = true;
            if (DBClick != null)
            {
                DBClick(this, new EV9000Event(0, filepath));
            }
        }
        /// <summary>
        /// ���õ�ǰ���ڲ��ŵ�״̬
        /// </summary>
        public void SetCurrentPlay()
        {
            this.BackgroundImage = Properties.Resources.listbg;
            this.carmera.Image = Properties.Resources.carmera;
            IsSelect = false;
            this.filename.ForeColor = Color.FromArgb(0, 100, 0);
            this.filename2.ForeColor = Color.FromArgb(0, 100, 0);
            IsCurrentPlay = true;
        }
        /// <summary>
        /// �������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void EV9000List_MouseWheel(object sender, MouseEventArgs e)
        {
            if (this.MouseScroll!=null)
            {
                MouseScroll(sender, e);
            }
        }
        /// <summary>
        /// ���̰��²���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EV9000List_KeyDown(object sender, KeyEventArgs e)
        {
            if (KeyPressDown != null)
            {
                KeyPressDown(sender,e);
            }
        }

        private void EV9000List_Resize(object sender, EventArgs e)
        {
            initFilename();
        }
    }
}
