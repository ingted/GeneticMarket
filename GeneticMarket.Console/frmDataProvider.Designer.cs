namespace GeneticMarket.Console
{
    partial class frmDataProvider
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
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.txtInstrument = new System.Windows.Forms.TextBox();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.rbMT4 = new System.Windows.Forms.RadioButton();
            this.rbOHLC = new System.Windows.Forms.RadioButton();
            this.rbGain = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 101);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 41);
            this.button1.TabIndex = 8;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Input File:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Instrument:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(291, 9);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(24, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = ".";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtInstrument
            // 
            this.txtInstrument.Location = new System.Drawing.Point(73, 40);
            this.txtInstrument.Name = "txtInstrument";
            this.txtInstrument.Size = new System.Drawing.Size(100, 20);
            this.txtInstrument.TabIndex = 10;
            this.txtInstrument.Text = "EURUSD";
            // 
            // txtFile
            // 
            this.txtFile.Location = new System.Drawing.Point(73, 11);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(212, 20);
            this.txtFile.TabIndex = 9;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(215, 101);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(100, 41);
            this.cmdCancel.TabIndex = 8;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // rbMT4
            // 
            this.rbMT4.AutoSize = true;
            this.rbMT4.Location = new System.Drawing.Point(16, 71);
            this.rbMT4.Name = "rbMT4";
            this.rbMT4.Size = new System.Drawing.Size(47, 17);
            this.rbMT4.TabIndex = 0;
            this.rbMT4.Text = "MT4";
            this.rbMT4.UseVisualStyleBackColor = true;
            this.rbMT4.CheckedChanged += new System.EventHandler(this.rbMT4_CheckedChanged);
            // 
            // rbOHLC
            // 
            this.rbOHLC.AutoSize = true;
            this.rbOHLC.Checked = true;
            this.rbOHLC.Location = new System.Drawing.Point(16, 25);
            this.rbOHLC.Name = "rbOHLC";
            this.rbOHLC.Size = new System.Drawing.Size(97, 17);
            this.rbOHLC.TabIndex = 0;
            this.rbOHLC.TabStop = true;
            this.rbOHLC.Text = "OHLC CSV File";
            this.rbOHLC.UseVisualStyleBackColor = true;
            this.rbOHLC.CheckedChanged += new System.EventHandler(this.rbFile_CheckedChanged);
            // 
            // rbGain
            // 
            this.rbGain.AutoSize = true;
            this.rbGain.Location = new System.Drawing.Point(16, 48);
            this.rbGain.Name = "rbGain";
            this.rbGain.Size = new System.Drawing.Size(122, 17);
            this.rbGain.TabIndex = 0;
            this.rbGain.Text = "GainCapital CSV File";
            this.rbGain.UseVisualStyleBackColor = true;
            this.rbGain.CheckedChanged += new System.EventHandler(this.rbMT4_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbGain);
            this.groupBox1.Controls.Add(this.rbOHLC);
            this.groupBox1.Controls.Add(this.rbMT4);
            this.groupBox1.Location = new System.Drawing.Point(321, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(149, 100);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Type";
            // 
            // frmDataProvider
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 155);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtInstrument);
            this.Controls.Add(this.txtFile);
            this.Name = "frmDataProvider";
            this.Text = "Data Provider";
            this.Load += new System.EventHandler(this.frmDataProvider_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtInstrument;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.RadioButton rbMT4;
        private System.Windows.Forms.RadioButton rbOHLC;
        private System.Windows.Forms.RadioButton rbGain;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}