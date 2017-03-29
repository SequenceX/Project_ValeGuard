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



namespace PrimerDesignerGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.listBox1.DragDrop += new
           System.Windows.Forms.DragEventHandler(this.listBox1_DragDrop);
            this.listBox1.DragEnter += new
           System.Windows.Forms.DragEventHandler(this.listBox1_DragEnter);
            this.listBox2.DragDrop += new
           System.Windows.Forms.DragEventHandler(this.listBox2_DragDrop);
            this.listBox2.DragEnter += new
           System.Windows.Forms.DragEventHandler(this.listBox2_DragEnter);
            readInSettings();
        }
        private void listBox1_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }
        private void listBox1_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            string[] s2 = new string[s.Length];
            int i;
            for (i = 0; i < s.Length; i++)
            {
                try
                {
                    s2[i] = s[i].Substring(s[i].LastIndexOf(@"\") + 1, s[i].LastIndexOf(@"-") - s[i].LastIndexOf(@"\") - 1);
                }
                catch (Exception)
                {
                    s2[i] = Microsoft.VisualBasic.Interaction.InputBox( "Genname wurde nicht automatisch erkannt", "Genname fehlt", "Genname");
                }
                listBox2.Items.Add(s2[i]);
                listBox1.Items.Add(s[i]);
            }
        }
        private void listBox2_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }
        private void listBox2_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            string[] s2 = new string[s.Length];
            int i;
            for (i = 0; i < s.Length; i++)
            {
                try
                {
                    s2[i] = s[i].Substring(s[i].LastIndexOf(@"\") + 1, s[i].LastIndexOf(@"-") - s[i].LastIndexOf(@"\") - 1);
                }
                catch (Exception)
                {
                    s2[i] = Microsoft.VisualBasic.Interaction.InputBox("Genname wurde nicht automatisch erkannt", "Genname fehlt", "Genname");
                }
                listBox2.Items.Add(s2[i]);
                listBox1.Items.Add(s[i]);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int i=0;
            var path = new List<string>();
            var geneName = new List<string>();
            foreach (object inlistbox in listBox1.Items)
            {
                //MessageBox.Show(inlistbox.ToString());
                path.Add(inlistbox.ToString());
            }
            foreach (object inlistbox in listBox2.Items)
            {
                //MessageBox.Show(inlistbox.ToString());
                geneName.Add(inlistbox.ToString());
            }
            var pathArray = path.ToArray();
            var geneNameArray = geneName.ToArray();
            if (geneNameArray.Length==0)
            {
                MessageBox.Show("Es wurden keine Sequenzen ausgewählt.");
                goto Ende;
            } 
            progressBar1.Maximum = pathArray.Length;
            progressBar1.Value = 0;
            while (i < pathArray.Length)
            {


                //Prüfung bei eingabe der Settingswerte ob diese Zahlen sind etc

                //HIER ERSTELLUNG DER PRIMER EINFÜGEN




                progressBar1.Value= progressBar1.Value+1;
                i++;
            }
        Ende:;
        }
        private void button3_Click(object sender, EventArgs e) 
        {//Delete selecte Items selecte in Listbox2
            int x=0;
            try
            {
                x = listBox2.SelectedIndex;
                listBox1.Items.RemoveAt(x);
                listBox2.Items.RemoveAt(x);
            }
            catch (Exception)
            {
                MessageBox.Show("Es wurde nichts zum löschen ausgewält.");
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {//Delete selecte Items selecte in Listbox1
            int x = 0;
            try
            {
                x = listBox1.SelectedIndex;
                listBox1.Items.RemoveAt(x);
                listBox2.Items.RemoveAt(x);
            }
            catch (Exception)
            {
                MessageBox.Show("Es wurde nichts zum löschen ausgewält.");
            }
        }




        private void readInSettings()
        {
            string documentsFolderPath;
            string desktopFolderPath;
            string StrandsPrimerMakerINI = @"StrandsPrimerMaker.ini";
            documentsFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string iniPath = documentsFolderPath + @"\" + StrandsPrimerMakerINI;
            desktopFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            if (!File.Exists(iniPath))
            {
                //create start INI
                IniFile ini = new IniFile(iniPath);
                ini.IniWriteValue("Settings", "PrimerConcentration", richTextBox1.Text);
                ini.IniWriteValue("Settings", "SaltConcentration", richTextBox2.Text);
                ini.IniWriteValue("Settings", "DistanceBetweenPrimer", richTextBox4.Text);
                ini.IniWriteValue("Settings", "SearchArea", richTextBox3.Text);
                ini.IniWriteValue("Settings", "MinSeqLength", richTextBox5.Text);
                ini.IniWriteValue("Settings", "MaxSeqLength", richTextBox6.Text);
                ini.IniWriteValue("Settings", "OutputDirectory", desktopFolderPath);
                richTextBox7.Text = desktopFolderPath;
            }
            else
            {
                //Read in settings
                IniFile ini = new IniFile(iniPath);
                richTextBox1.Text = ini.IniReadValue("Settings", "PrimerConcentration");
                richTextBox2.Text = ini.IniReadValue("Settings", "SaltConcentration");
                richTextBox4.Text = ini.IniReadValue("Settings", "DistanceBetweenPrimer");
                richTextBox3.Text = ini.IniReadValue("Settings", "SearchArea");
                richTextBox5.Text = ini.IniReadValue("Settings", "MinSeqLength");
                richTextBox6.Text = ini.IniReadValue("Settings", "MaxSeqLength");
                richTextBox7.Text = ini.IniReadValue("Settings", "OutputDirectory");
            }
        }




        private void button2_Click(object sender, EventArgs e)
        {


     
            

        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Default Settings
            richTextBox1.Text = "50";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //Default Settings
            richTextBox2.Text = "50";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //Default Settings
            richTextBox4.Text = "90";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //Default Settings
            richTextBox3.Text = "30";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //Default Settings
            richTextBox5.Text = "160";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //Default Settings
            richTextBox6.Text = "1000";
        }
    }
}
