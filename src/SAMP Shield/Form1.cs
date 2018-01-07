using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAMP_Shield
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /*
        
            ALLOWED MODS MD5 HASH LIST: 
             ALLOWED MODS MD5 HASH LIST: 
              ALLOWED MODS MD5 HASH LIST: 
               ALLOWED MODS MD5 HASH LIST: 
                ALLOWED MODS MD5 HASH LIST: 
             
        */
        List<string> OkFiles = new List<string>(new string[] 
        {
            "961a5c939e835575b347a9980f38b5e2", //CLEO .ASI
            "a149eab00a219b03d34e6c5270558a70", //NOSHADOWS MOD .ASI
            "47fc1f23d62be8573986ab200c178e3c", //CRASHESMOD .ASI
            "228ae63e3f138385a49cfb8ecd5102ee", //MIX SETS .CS
            "aa570c713a4af30cf9258ab9925a5688", //MODLOADER .ASI

            "fdb6450b3934d96f712bf2849c6e99b9", //MEMORY FIX 1GB .CS
            "07dd7a442ca6b3afa0e27919d9bfe4aa", //MEMORY FIX 2GB .CS
            "612c5c3f15c85b7b3f39e91e2d77c90e", //MEMORY FIX 512mb .CS

            "ce0cd3c2b7988644214af78dc8e6ccd8", //GTA IV HUD .ASI
            "bdadbdf8046a39730ed5083e4988c1bd", //HOOKS .ASI
            "817f985d55da741dd91fc591291e6eec", //SAMP RESTORE GRAPHICS .ASI
            "88af72e7ea4455e3af7fcda59add1083", //SAMPGRASS .ASI
            "27313c8bf5d434fcf754422c379da399", //SaGRADING GRAPHIC MOD .ASI
            "cfa2d51d5c84e3b34959d1c426c182df", //RADARRECT .ASI
            "4bd7cd58b3c3f472188e354be6ea82dd", //OUTFIT .ASI
            "795df56453ab16ed321a3937efa33d82", //OUTFITFIX .ASI
            "d8b9ea2487e5c417e6192b810c7eee59", //MOONLOADER .ASI
            "fb409578e697573bd0a8eb243a480710", //IMFX .ASI
            "b1f05747f3f2c601a348508c9a48cc89", //ANTICRASHER037 .CS
        });


      


        private void Form1_Load(object sender, EventArgs e)
        {
            // CHECK NEW VERSIONS
            WebClient wc = new WebClient();
            string lst = wc.DownloadString("https://raw.githubusercontent.com/ViniciusBrokeh/SAMP-Shield/master/version");
            if(lst.ToString().Contains(Application.ProductVersion.ToString()) == false)
            {
                MessageBox.Show("New version available: " + lst);
            }
            MessageBox.Show("https://github.com/ViniciusBrokeh/");
            
            
            
            
            // CHECK IF GTADIR EXISTS IN SETTINGS
            if (Properties.Settings.Default.gta_directory.Length < 1)
            {
                SetGtaDir formSelectPathGTA = new SetGtaDir();

                
                    formSelectPathGTA.Show();
             
            }
            lbl_version.Text = Application.ProductVersion.ToString();
            
        }
        //Function to get MD5 Hash of file
        static string CalculateMD5(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }
        string gtadir = Properties.Settings.Default.gta_directory;
        //Scan
        private void ScanThread_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
            
                   // LIST ALL .ASI AND .CS FILES THAT EXIST IN THE GAME DIRECTORY
                List<string> asiFiles = Directory.GetFiles(gtadir, "*.asi", SearchOption.AllDirectories).ToList();
                List<string> cleoFiles = Directory.GetFiles(gtadir, "*.cs", SearchOption.AllDirectories).ToList();
                foreach (string asiFile in asiFiles)
                {
                  //CHECK IF IS NOT ALLOWED MOD
                    if (OkFiles.Contains(CalculateMD5(asiFile)) != true)
                    {

                        BeginInvoke((MethodInvoker)delegate
                        {
                            if (cheat_list.Items.Contains("Suspect file: " + Path.GetFileName(asiFile)) != true)
                            {
                            // IF DONT ALREDY EXIST IN LIST.. ADD
                                cheat_list.Items.Add("Suspect file: " + Path.GetFileName(asiFile));
                            }

                        });


                    }
                }
                foreach (string cleoFile in cleoFiles)
                {
                    //CHECK IF IS NOT ALLOWED MOD .CS
                    if (OkFiles.Contains(CalculateMD5(cleoFile)) != true)
                    {

                        BeginInvoke((MethodInvoker)delegate
                        {
                            if (cheat_list.Items.Contains("Suspect file: " + Path.GetFileName(cleoFile)) != true)
                            {
                            // IF DONT ALREDY EXIST IN LIST.. ADD
                                cheat_list.Items.Add("Suspect file: " + Path.GetFileName(cleoFile));
                            }

                        });

                    }
                }
            }
            catch
            {

            }


              
          
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
        //IF GTADIR IS OK... START SCAN
            if (Properties.Settings.Default.gta_directory.Length > 1)
            {
                timer1.Stop();
                ScanThread.RunWorkerAsync();
            }
        }

        private void ScanThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        //CREATE LOOP
            ScanThread.RunWorkerAsync();
        }


        
        private void closeGTA_Tick(object sender, EventArgs e)
        {
        // if found cheats close the game
            if (cheat_list.Items.Count > 0) {
                Process[] processes = Process.GetProcessesByName("gta_sa");
                if (processes.Length > 0)
                {
                    if found cheats close the game
                    processes[0].Kill();
                    MessageBox.Show("GTA can not be opened because there are suspicious files in the game folder.");
                }
         }
        }


    //MODPACK ALLOWED
        private void label2_Click(object sender, EventArgs e)
        {
            Process.Start("https://drive.google.com/file/d/1Lni0NpjQ36Nk0VrvrNOZ4etp_9bwiyQN/view?usp=sharing");
        }

    //EXIT BUTTON
        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    //MINIMIZE BUTTON
        private void label5_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
