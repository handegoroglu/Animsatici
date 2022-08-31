using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace animsatici
{
    public partial class frmMessage : Form
    {
        public frmMessage()
        {
            InitializeComponent();
        }

        public string mesaj = " ";
        private void label5_Click(object sender, EventArgs e)
        {
            


        }
        SoundPlayer player;
        private void frmMessage_Load(object sender, EventArgs e)
        {

            string sarkiyolu = Application.StartupPath + "/ref/alarm.wav";
            player = new SoundPlayer(sarkiyolu);
            player.Play();

            label5.Text = mesaj;    

        }

        private void frmMessage_FormClosed(object sender, FormClosedEventArgs e)
        {
            player.Stop();
        }
    }
}
