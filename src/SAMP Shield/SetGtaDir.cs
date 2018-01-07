using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAMP_Shield
{
    public partial class SetGtaDir : Form
    {
        public SetGtaDir()
        {
            InitializeComponent();
        }

        // Click in "Search..." button
        private void button1_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    textBox1.Text = fbd.SelectedPath.ToString();
                    
                }
            }
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if(!File.Exists(textBox1.Text + "gta_sa.exe"))
            {
                button2.Enabled = false;
            }else
            {
                button2.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.gta_directory = textBox1.Text;
            Properties.Settings.Default.Save();
            Close();
        }

        private void SetGtaDir_Load(object sender, EventArgs e)
        {
            this.BringToFront();
            this.Activate();
        }
    }
}
