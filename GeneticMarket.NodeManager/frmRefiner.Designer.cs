namespace GeneticMarket.NodeManager
{
    partial class frmRefiner
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
            this.cmdRefine = new System.Windows.Forms.Button();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdTest = new System.Windows.Forms.Button();
            this.lblCodePrefix = new System.Windows.Forms.Label();
            this.lblCodeSuffix = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmdRefine
            // 
            this.cmdRefine.Location = new System.Drawing.Point(217, 361);
            this.cmdRefine.Name = "cmdRefine";
            this.cmdRefine.Size = new System.Drawing.Size(115, 36);
            this.cmdRefine.TabIndex = 0;
            this.cmdRefine.Text = "Refine";
            this.cmdRefine.UseVisualStyleBackColor = true;
            this.cmdRefine.Click += new System.EventHandler(this.cmdRefine_Click);
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(28, 78);
            this.txtCode.Multiline = true;
            this.txtCode.Name = "txtCode";
            this.txtCode.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtCode.Size = new System.Drawing.Size(497, 224);
            this.txtCode.TabIndex = 2;
            this.txtCode.TextChanged += new System.EventHandler(this.txtCode_TextChanged);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(396, 361);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(115, 36);
            this.cmdCancel.TabIndex = 0;
            this.cmdCancel.Text = "Close";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdTest
            // 
            this.cmdTest.Location = new System.Drawing.Point(38, 361);
            this.cmdTest.Name = "cmdTest";
            this.cmdTest.Size = new System.Drawing.Size(115, 36);
            this.cmdTest.TabIndex = 0;
            this.cmdTest.Text = "Test";
            this.cmdTest.UseVisualStyleBackColor = true;
            this.cmdTest.Click += new System.EventHandler(this.cmdTest_Click);
            // 
            // lblCodePrefix
            // 
            this.lblCodePrefix.AutoSize = true;
            this.lblCodePrefix.Location = new System.Drawing.Point(12, 9);
            this.lblCodePrefix.Name = "lblCodePrefix";
            this.lblCodePrefix.Size = new System.Drawing.Size(35, 13);
            this.lblCodePrefix.TabIndex = 3;
            this.lblCodePrefix.Text = "label1";
            // 
            // lblCodeSuffix
            // 
            this.lblCodeSuffix.AutoSize = true;
            this.lblCodeSuffix.Location = new System.Drawing.Point(12, 305);
            this.lblCodeSuffix.Name = "lblCodeSuffix";
            this.lblCodeSuffix.Size = new System.Drawing.Size(35, 13);
            this.lblCodeSuffix.TabIndex = 3;
            this.lblCodeSuffix.Text = "label1";
            // 
            // frmRefiner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 412);
            this.Controls.Add(this.lblCodeSuffix);
            this.Controls.Add(this.lblCodePrefix);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.cmdTest);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdRefine);
            this.Name = "frmRefiner";
            this.Text = "frmRefiner";
            this.Load += new System.EventHandler(this.frmRefiner_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdRefine;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdTest;
        private System.Windows.Forms.Label lblCodePrefix;
        private System.Windows.Forms.Label lblCodeSuffix;
    }
}