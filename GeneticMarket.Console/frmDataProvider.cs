using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace GeneticMarket.Console
{
    public partial class frmDataProvider : Form
    {
        public frmDataProvider()
        {
            InitializeComponent();
        }

        public string dpType = null;
        public string instrument = null;
        public List<ArrayList> dpParams = new List<ArrayList>();  //maybe multiple files are selected
        public List<string> metaInfo = new List<string>();

        private void button1_Click(object sender, EventArgs e)
        {
            instrument = txtInstrument.Text;

            if (rbOHLC.Checked)
            {
                dpType = "ohlc";

                foreach (string file in txtFile.Text.Split(';'))
                {
                    if (file.Length == 0) continue;

                    ArrayList arr = new ArrayList();
                    dpParams.Add(arr);

                    arr.Add(file);
                    arr.Add(',');

                    metaInfo.Add(Path.GetFileName(file));
                }

                
            }
            else if (rbGain.Checked)
            {
                dpType = "gain";

                foreach (string file in txtFile.Text.Split(';'))
                {
                    if (file.Length == 0) continue;

                    ArrayList arr = new ArrayList();
                    dpParams.Add(arr);

                    arr.Add(txtFile.Text);

                    metaInfo.Add(Path.GetFileName(file));
                }
            }
            else
            {
                dpParams.Add(new ArrayList());
                dpType = "mt4";
                metaInfo.Add("metatrader");
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Multiselect = true;

            if (ofd.ShowDialog() != DialogResult.Cancel)
            {
                string temp = "";

                foreach (string file in ofd.FileNames)
                {
                    temp += file;
                    temp += ";";
                }

                txtFile.Text = temp;
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void rbMT4_CheckedChanged(object sender, EventArgs e)
        {
            txtFile.Enabled = !rbMT4.Checked;
        }

        private void rbFile_CheckedChanged(object sender, EventArgs e)
        {
            txtFile.Enabled = !rbMT4.Checked;
        }

        private void frmDataProvider_Load(object sender, EventArgs e)
        {

        }
    }
}
