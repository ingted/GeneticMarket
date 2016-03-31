using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GeneticMarket.Core;

namespace GeneticMarket.NodeManager
{
    public partial class frmOptions : Form
    {
        public ClientContext context = null;

        public frmOptions()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmOptions_Load(object sender, EventArgs e)
        {
            chkAdd.Checked = context.EnvParams.AddEnabled;
            chkMutation.Checked = context.EnvParams.MutationEnabled;
            chkCrossOver.Checked = context.EnvParams.CrossOverEnabled;
            chkDelete.Checked = context.EnvParams.DeleteEnabled;
            chkOpenPos.Checked = context.EnvParams.OpenPositionEnabled;

            txtMaxStrategy.Text = context.EnvParams.MaxStrategyInRepository.ToString();
            txtEvalPeriod.Text = context.EnvParams.EvaluateTickPeriod.ToString();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            context.EnvParams.AddEnabled = chkAdd.Checked;
            context.EnvParams.MutationEnabled = chkMutation.Checked;
            context.EnvParams.CrossOverEnabled = chkCrossOver.Checked;
            context.EnvParams.DeleteEnabled = chkDelete.Checked;
            context.EnvParams.OpenPositionEnabled = chkOpenPos.Checked;

            context.EnvParams.MaxStrategyInRepository = int.Parse(txtMaxStrategy.Text);
            context.EnvParams.EvaluateTickPeriod = int.Parse(txtEvalPeriod.Text);

            Close();
        }
    }
}
