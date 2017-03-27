using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

                //HIER ERSTELLUNG DER PRIMER EINFÜGEN
                progressBar1.Value= progressBar1.Value+1;
                i++;
            }
        Ende:;
        }
    }
}
