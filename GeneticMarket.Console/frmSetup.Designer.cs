namespace GeneticMarket.Console
{
    partial class frmSetup
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.chkEnableReal = new System.Windows.Forms.CheckBox();
            this.chkEnableVirtual = new System.Windows.Forms.CheckBox();
            this.txtInitPop = new System.Windows.Forms.TextBox();
            this.cmdOk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chkEnableReal
            // 
            this.chkEnableReal.AutoSize = true;
            this.chkEnableReal.Checked = true;
            this.chkEnableReal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnableReal.Location = new System.Drawing.Point(21, 73);
            this.chkEnableReal.Name = "chkEnableReal";
            this.chkEnableReal.Size = new System.Drawing.Size(120, 17);
            this.chkEnableReal.TabIndex = 16;
            this.chkEnableReal.Text = "Enable Real Market";
            this.chkEnableReal.UseVisualStyleBackColor = true;
            // 
            // chkEnableVirtual
            // 
            this.chkEnableVirtual.AutoSize = true;
            this.chkEnableVirtual.Checked = true;
            this.chkEnableVirtual.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnableVirtual.Location = new System.Drawing.Point(21, 26);
            this.chkEnableVirtual.Name = "chkEnableVirtual";
            this.chkEnableVirtual.Size = new System.Drawing.Size(127, 17);
            this.chkEnableVirtual.TabIndex = 15;
            this.chkEnableVirtual.Text = "Enable Virtual Market";
            this.chkEnableVirtual.UseVisualStyleBackColor = true;
            // 
            // txtInitPop
            // 
            this.txtInitPop.Location = new System.Drawing.Point(137, 113);
            this.txtInitPop.Name = "txtInitPop";
            this.txtInitPop.Size = new System.Drawing.Size(96, 20);
            this.txtInitPop.TabIndex = 14;
            this.txtInitPop.Text = "10";
            // 
            // cmdOk
            // 
            this.cmdOk.Location = new System.Drawing.Point(76, 221);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(75, 23);
            this.cmdOk.TabIndex = 13;
            this.cmdOk.Text = "Generate";
            this.cmdOk.UseVisualStyleBackColor = true;
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Initial Population Size:";
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(221, 221);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 13;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // frmSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 272);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkEnableReal);
            this.Controls.Add(this.chkEnableVirtual);
            this.Controls.Add(this.txtInitPop);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOk);
            this.Name = "frmSetup";
            this.Text = "Setup";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkEnableReal;
        private System.Windows.Forms.CheckBox chkEnableVirtual;
        private System.Windows.Forms.TextBox txtInitPop;
        private System.Windows.Forms.Button cmdOk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdCancel;

    }
}