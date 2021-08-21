using System;
using System.Drawing;
using System.Windows.Forms;
using WMPLib;

namespace toAES
{
    public partial class Form1 : Form
    {
        WindowsMediaPlayer _mediaPlayer = new WindowsMediaPlayer();
        static int time = 21600;
        public Form1()
        {
            InitializeComponent();
            _mediaPlayer.settings.setMode("loop", true);
            _mediaPlayer.URL = @"jihou.mp3";// mp3も使用可能
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (time == 0)
            {
                this.label3.ForeColor = Color.Red;
                this.label3.Text = "TIME OUT";
                _mediaPlayer.settings.setMode("loop", false);
                _mediaPlayer.controls.stop();
                Console.Beep(5000, 3000);
            }
            else
            {
                this.label3.Text = "残り" + time.ToString() + "秒";
                time--;
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
        }
    }
}
