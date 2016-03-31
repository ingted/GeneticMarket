using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GeneticMarket.Base.Helper;
using GeneticMarket.BackEnd.Helper;
using GeneticMarket.Core;
using System.IO;
using System.Reflection;

namespace GeneticMarket.NodeManager
{
    public partial class frmRefiner : Form
    {
        public ClientContext clientContext = null;

        private IStrategyRefiner compiledObject = null;
        private string hiddenPrefix = "";
        private string hiddenSuffix = "";
        public frmRefiner()
        {
            InitializeComponent();
        }

        private void frmRefiner_Load(object sender, EventArgs e)
        {
            hiddenPrefix = @"using System;
using System.Collections;
using System.Reflection;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Text;
using Microsoft.CSharp;
using System.Resources;
using System.IO;
using GeneticMarket.Base.Helper;

public class MyRefiner : IStrategyRefiner
{";
            lblCodePrefix.Text = @"public bool CheckRefine(int winningPositionCount, int loosingPositionCount,
        int totalLoss, int totalWin, int largestLoss,
        int largestWin, int closedPositionCount, int openPositionCount,
        double reputation)
{";

            lblCodeSuffix.Text = @"}";
            hiddenSuffix = "}";

            txtCode.Text = @"if (totalLoss > 10)
{
    return true;
}
else
{
    return false;
}";
        }

        private void txtCode_TextChanged(object sender, EventArgs e)
        {
            compiledObject = null;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool doCompile()
        {
            if (compiledObject != null)
            {
                //no need to re-compile
                return true;
            }

            string code = hiddenPrefix + lblCodePrefix.Text + txtCode.Text + lblCodeSuffix.Text + hiddenSuffix;
            List<string> references = new List<string>();

            string appPath = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(appPath);
            string path = Uri.UnescapeDataString(uri.Path);
            path = Path.GetDirectoryName(path);

            references.Add(Path.Combine(path,"GeneticMarket.Base.dll"));
            references.Add(Path.Combine(path, "GeneticMarket.Common.dll"));
            references.Add(Path.Combine(path, "GeneticMarket.Logic.dll"));
            references.Add(Path.Combine(path, "GeneticMarket.BackEnd.dll"));
            references.Add(Path.Combine(path, "GeneticMarket.Core.dll"));

            

            RuntimeResult rr = RuntimeHelper.CompileCode(code, references);

            if (rr.Success == false)
            {
                MessageBox.Show(rr.ErrorMessage);
                return false;
            }

            if (rr.CreatedObject == null)
            {
                MessageBox.Show("Error creating object");
                return false;
            }

            compiledObject = rr.CreatedObject as IStrategyRefiner;

            return true;
        }

        private void cmdTest_Click(object sender, EventArgs e)
        {
            if (!doCompile()) return;

            int result = clientContext.PerformanceLogic.TestRefine(compiledObject);

            if (result == -1)
            {
                MessageBox.Show("Runtime Error");
                return;
            }

            MessageBox.Show(string.Format("Done! {0} match(s) found.", result));
        }

        private void cmdRefine_Click(object sender, EventArgs e)
        {
            if (!doCompile()) return;

            int result = clientContext.PerformanceLogic.DoRefine(compiledObject);

            if (result == -1)
            {
                MessageBox.Show("Runtime Error");
                return;
            }

            MessageBox.Show(string.Format("Done! {0} strategies are deleted.", result));
        }
    }
}
