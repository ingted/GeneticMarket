using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using GeneticMarket.Master;
using System.Threading;
using System.Runtime.Remoting;
using System.Collections;

namespace GeneticMarket.Console
{
    public partial class frmMain : Form
    {
        MasterStrategy masterStrategy = new MasterStrategy();
        private bool firstRun = true;

        public frmMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            masterStrategy.DataProviderStarted += new DataProviderEventHandler(masterStrategy_DataProviderStarted);
            masterStrategy.DataProviderStopped += new DataProviderEventHandler(masterStrategy_DataProviderStopped);
            masterStrategy.DataProviderPaused += new DataProviderEventHandler(masterStrategy_DataProviderPaused);
        }

        void masterStrategy_DataProviderPaused(int tag)
        {
            string text = lstDp.Items[tag].ToString();

            text = text.Replace("(R) ", "");
            text = text.Replace("(S) ", "");

            text = "(P) " + text;

            lstDp.Invoke(new MethodInvoker(delegate()
            {
                lstDp.Items[tag] = text;
                lblStatus.Text = "PAUSED";
            }));
        }

        void masterStrategy_DataProviderStopped(int tag)
        {
            string text = lstDp.Items[tag].ToString();

            text = text.Replace("(R) ", "");
            text = text.Replace("(P) ", "");

            text = "(S) " + text;

            lstDp.Invoke(new MethodInvoker(delegate()
                {
                    lstDp.Items[tag] = text;
                    lblStatus.Text = "STOPPED";
                }));
        }

        void masterStrategy_DataProviderStarted(int tag)
        {
            

            string text = lstDp.Items[tag].ToString();
            text = text.Replace("(S) ", "");
            text = text.Replace("(P) ", "");

            text = "(R) " + text;

            lstDp.Invoke(new MethodInvoker(delegate()
            {
                lstDp.Items[tag] = text;
                lblStatus.Text = "RUNNING";
            }));
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            masterStrategy.Stop();

            Thread.Sleep(100);
        }

        private void cmdProceed_Click(object sender, EventArgs e)
        {
            if (lblStatus.Text == "RUNNING")
            {
                MessageBox.Show("Invalid Action");
                return;
            }

            if (firstRun)
            {
                masterStrategy.Init(lblBuySignal, lblSellSignal, lblBestSignal, cboInstruments);

                //do not re-run above code after pause and run 
                firstRun = false;
            }

            if (!masterStrategy.Start())
            {
                MessageBox.Show("Error running");
            }
        }

        private void cmdSetup_Click(object sender, EventArgs e)
        {
            frmSetup frm = new frmSetup();

            frm.masterStrategy = masterStrategy;

            frm.ShowDialog();
        }

        private void cmdDp_Click(object sender, EventArgs e)
        {
            frmDataProvider frm = new frmDataProvider();

            if (frm.ShowDialog() != DialogResult.Cancel)
            {
                int counter = 0;

                foreach (ArrayList arr in frm.dpParams)
                {
                    string metaInfo = frm.metaInfo[counter++];
                    int index = lstDp.Items.Count;
                    int totalTicks = masterStrategy.AddDataProvider(frm.dpType, frm.instrument, index, arr.ToArray());

                    lstDp.Items.Add(string.Format("{3}({0},{1}) {2} ticks", frm.dpType, frm.instrument, totalTicks, metaInfo));
                }
            }
        }

        private void cmdPause_Click(object sender, EventArgs e)
        {
            if (lblStatus.Text == "PAUSED")
            {
                MessageBox.Show("Invalid Action");
                return;
            }

            masterStrategy.Pause();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //masterStrategy.SetSentiment(double.Parse(txtMin.Text), double.Parse(txtMax.Text), rbBase.Checked);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool registered = masterStrategy.RegisterNode(txtNodeAddress.Text);

            if (!registered)
            {
                MessageBox.Show("Could not load object. Make sure data providers are added first");
            }
            else
            {
                MessageBox.Show("Registered Successfully!");
            }
        }

        private void chkValidator_CheckedChanged(object sender, EventArgs e)
        {
            if (chkValidator.Checked)
            {
                masterStrategy.RegisterValidator(this.tabControl1);
            }
            else
            {
                masterStrategy.UnRegisterValidator();
            }
        }

        private void chkBestValidator_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBestValidator.Checked)
            {
                masterStrategy.RegisterBestValidator(this.tabControl1);
            }
            else
            {
                masterStrategy.UnRegisterBestValidator();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            masterStrategy.ResetValidator();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            masterStrategy.ResetBestValidator();
        }
    }
}
