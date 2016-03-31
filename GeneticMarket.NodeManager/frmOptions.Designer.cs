namespace GeneticMarket.NodeManager
{
    partial class frmOptions
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
            this.chkAdd = new System.Windows.Forms.CheckBox();
            this.chkMutation = new System.Windows.Forms.CheckBox();
            this.chkCrossOver = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtEvalPeriod = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMaxStrategy = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            this.chkDelete = new System.Windows.Forms.CheckBox();
            this.chkOpenPos = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // chkAdd
            // 
            this.chkAdd.AutoSize = true;
            this.chkAdd.Location = new System.Drawing.Point(12, 12);
            this.chkAdd.Name = "chkAdd";
            this.chkAdd.Size = new System.Drawing.Size(120, 17);
            this.chkAdd.TabIndex = 0;
            this.chkAdd.Text = "Enable add strategy";
            this.chkAdd.UseVisualStyleBackColor = true;
            // 
            // chkMutation
            // 
            this.chkMutation.AutoSize = true;
            this.chkMutation.Location = new System.Drawing.Point(12, 41);
            this.chkMutation.Name = "chkMutation";
            this.chkMutation.Size = new System.Drawing.Size(142, 17);
            this.chkMutation.TabIndex = 0;
            this.chkMutation.Text = "Enable strategy mutation";
            this.chkMutation.UseVisualStyleBackColor = true;
            // 
            // chkCrossOver
            // 
            this.chkCrossOver.AutoSize = true;
            this.chkCrossOver.Location = new System.Drawing.Point(12, 70);
            this.chkCrossOver.Name = "chkCrossOver";
            this.chkCrossOver.Size = new System.Drawing.Size(151, 17);
            this.chkCrossOver.TabIndex = 0;
            this.chkCrossOver.Text = "Enable strategy cross-over";
            this.chkCrossOver.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 159);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Genetic evaluation period in ticks:";
            // 
            // txtEvalPeriod
            // 
            this.txtEvalPeriod.Location = new System.Drawing.Point(181, 155);
            this.txtEvalPeriod.Name = "txtEvalPeriod";
            this.txtEvalPeriod.Size = new System.Drawing.Size(100, 20);
            this.txtEvalPeriod.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 186);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Maximum allowed strategy:";
            // 
            // txtMaxStrategy
            // 
            this.txtMaxStrategy.Location = new System.Drawing.Point(151, 182);
            this.txtMaxStrategy.Name = "txtMaxStrategy";
            this.txtMaxStrategy.Size = new System.Drawing.Size(130, 20);
            this.txtMaxStrategy.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(165, 231);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 36);
            this.button1.TabIndex = 3;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(13, 231);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(115, 36);
            this.cmdOK.TabIndex = 3;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // chkDelete
            // 
            this.chkDelete.AutoSize = true;
            this.chkDelete.Location = new System.Drawing.Point(12, 99);
            this.chkDelete.Name = "chkDelete";
            this.chkDelete.Size = new System.Drawing.Size(139, 17);
            this.chkDelete.TabIndex = 0;
            this.chkDelete.Text = "Enable strategy deletion";
            this.chkDelete.UseVisualStyleBackColor = true;
            // 
            // chkOpenPos
            // 
            this.chkOpenPos.AutoSize = true;
            this.chkOpenPos.Location = new System.Drawing.Point(12, 128);
            this.chkOpenPos.Name = "chkOpenPos";
            this.chkOpenPos.Size = new System.Drawing.Size(139, 17);
            this.chkOpenPos.TabIndex = 0;
            this.chkOpenPos.Text = "Enable position opening";
            this.chkOpenPos.UseVisualStyleBackColor = true;
            // 
            // frmOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 279);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtMaxStrategy);
            this.Controls.Add(this.txtEvalPeriod);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkOpenPos);
            this.Controls.Add(this.chkDelete);
            this.Controls.Add(this.chkCrossOver);
            this.Controls.Add(this.chkMutation);
            this.Controls.Add(this.chkAdd);
            this.Name = "frmOptions";
            this.Text = "Options";
            this.Load += new System.EventHandler(this.frmOptions_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkAdd;
        private System.Windows.Forms.CheckBox chkMutation;
        private System.Windows.Forms.CheckBox chkCrossOver;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtEvalPeriod;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMaxStrategy;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.CheckBox chkDelete;
        private System.Windows.Forms.CheckBox chkOpenPos;
    }
}