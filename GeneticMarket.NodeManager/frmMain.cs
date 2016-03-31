using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GeneticMarket.Core;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using GeneticMarket.Master;
using GeneticMarket.Base.Config;
using System.Threading;
using System.Collections;
using System.Runtime.Serialization.Formatters;
using GeneticMarket.Base.Helper;
using System.IO;

namespace GeneticMarket.NodeManager
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private ClientContext clientContext = null;
        private ClientContextProvider contextProvider = null;
        private NodeStats nodeStats = null;
        private DateTime connectTime = DateTime.MinValue;
        
        private void button1_Click(object sender, EventArgs e)
        {            
            contextProvider = new ClientContextProvider(clientContext, int.Parse(textBox1.Text), txtServer.Text);

            textBox1.Enabled = false;
            txtServer.Enabled = false;
            cmdStart.Enabled = false;

            //when GeneticMarket initializes my context, inform me so I can initialize my stats and UI
            contextProvider.ConsoleConnect += new EventHandler(contextProvider_ConsoleConnect);
            contextProvider.ConsoleDisconnect += new EventHandler(contextProvider_ConsoleDisconnect);

            lblStatus.Text = "READY";
        }

        void contextProvider_ConsoleDisconnect(object sender, EventArgs e)
        {
            lblStatus.Invoke(new MethodInvoker(delegate()
                {
                    lblStatus.Text = "NOT STARTED";
                    textBox1.Enabled = true;
                    txtServer.Enabled = true;
                    cmdStart.Enabled = false;
                }));
        }

        void contextProvider_ConsoleConnect(object sender, EventArgs e)
        {
            nodeStats = new NodeStats();
            nodeStats.Register(clientContext, pnlMarket, panel4);
            connectTime = DateTime.Now;

            lblStatus.Invoke(new MethodInvoker(delegate()
            {
                lblStatus.Text = "CONNECTED";
                upTimer.Enabled = true;
            }));
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (contextProvider != null)
            {
                //inform server and wait so my entry is removed from server data
                contextProvider.Stop();
            }
            if (nodeStats != null)
            {
                nodeStats.UnRegister();
            }
        }

        private void cmdReport_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV Files(*.csv)|*.csv|Compressed Files(*.zip)|*.zip";
            if (sfd.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            InputBoxResult ibr = InputBox.Show("Please enter some comment if you like");

            if (ibr.ReturnCode == DialogResult.Cancel)
            {
                return;
            }

            string fileName = sfd.FileName;
            clientContext.PersistenceLogic.SaveReport(fileName,ibr.Text);

            MessageBox.Show("Report is saved to " + fileName);
        }

        private void upTimer_Tick(object sender, EventArgs e)
        {
            TimeSpan timeLength = DateTime.Now.Subtract(connectTime);

            int seconds = (int)timeLength.TotalSeconds;
            int minutes = seconds / 60;
            int hours = minutes / 60;

            minutes %= 60;
            seconds %= 60;

            lblTimer.Text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);

            contextProvider.EnableAutoSave = chkAutoSave.Checked;
        }

        private void cmdLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "CSV Files(*.csv)|*.csv|Compressed Files(*.zip)|*.zip";

            if (ofd.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            int count = clientContext.PersistenceLogic.LoadReport(ofd.FileName);

            lblStrategyCount.Text = count.ToString();
            lblSignalCount.Text = clientContext.IndicatorRepository.IndicatorCount.ToString();

            MessageBox.Show(count.ToString()+" strategies were loaded from " + ofd.FileName);
        }

        private void cmdStrategyView_Click(object sender, EventArgs e)
        {
            frmStrategyView frm = new frmStrategyView();
            frm.context = clientContext;
            frm.contextProvider = contextProvider;
            frm.strategyId = int.Parse(txtStrategyId.Text);

            frm.Show();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            clientContext = new ClientContext();
        }

        private void cmdRefine_Click(object sender, EventArgs e)
        {
            frmRefiner frm = new frmRefiner();
            frm.clientContext = clientContext;

            frm.ShowDialog();
        }

        private void cmdOptions_Click(object sender, EventArgs e)
        {
            frmOptions frm = new frmOptions();

            frm.context = clientContext;
            frm.ShowDialog();
        }
    }
}
