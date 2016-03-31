namespace GeneticMarket.NodeManager
{
    partial class frmStrategyView
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
            this.cmdClose = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lblReputation = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblWinCount = new System.Windows.Forms.Label();
            this.lblLossCount = new System.Windows.Forms.Label();
            this.lblTotalWin = new System.Windows.Forms.Label();
            this.lblTotalLoss = new System.Windows.Forms.Label();
            this.lblLargestWin = new System.Windows.Forms.Label();
            this.lblLargestLoss = new System.Windows.Forms.Label();
            this.lblClosedPositionCount = new System.Windows.Forms.Label();
            this.lblCurrentSignal = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblOpenPositionCount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblUpdateRemaining = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblOpenPL = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(329, 3);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(43, 22);
            this.cmdClose.TabIndex = 0;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(2, 131);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Reputation:";
            // 
            // lblReputation
            // 
            this.lblReputation.AutoSize = true;
            this.lblReputation.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReputation.ForeColor = System.Drawing.Color.ForestGreen;
            this.lblReputation.Location = new System.Drawing.Point(70, 131);
            this.lblReputation.Name = "lblReputation";
            this.lblReputation.Size = new System.Drawing.Size(15, 17);
            this.lblReputation.TabIndex = 1;
            this.lblReputation.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(5, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Profitable positions:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(187, 7);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Loosing positions:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(5, 31);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(103, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Total profit points:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(187, 31);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(94, 13);
            this.label11.TabIndex = 1;
            this.label11.Text = "Total loss points:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(5, 55);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(81, 13);
            this.label13.TabIndex = 1;
            this.label13.Text = "Biggest profit:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(187, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Biggest loss:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(193, 29);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(96, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Closed positions:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(239, 131);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(39, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "Signal:";
            // 
            // lblWinCount
            // 
            this.lblWinCount.AutoSize = true;
            this.lblWinCount.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWinCount.Location = new System.Drawing.Point(114, 7);
            this.lblWinCount.Name = "lblWinCount";
            this.lblWinCount.Size = new System.Drawing.Size(13, 13);
            this.lblWinCount.TabIndex = 1;
            this.lblWinCount.Text = "0";
            // 
            // lblLossCount
            // 
            this.lblLossCount.AutoSize = true;
            this.lblLossCount.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLossCount.Location = new System.Drawing.Point(287, 7);
            this.lblLossCount.Name = "lblLossCount";
            this.lblLossCount.Size = new System.Drawing.Size(13, 13);
            this.lblLossCount.TabIndex = 1;
            this.lblLossCount.Text = "0";
            // 
            // lblTotalWin
            // 
            this.lblTotalWin.AutoSize = true;
            this.lblTotalWin.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalWin.Location = new System.Drawing.Point(105, 31);
            this.lblTotalWin.Name = "lblTotalWin";
            this.lblTotalWin.Size = new System.Drawing.Size(13, 13);
            this.lblTotalWin.TabIndex = 1;
            this.lblTotalWin.Text = "0";
            // 
            // lblTotalLoss
            // 
            this.lblTotalLoss.AutoSize = true;
            this.lblTotalLoss.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalLoss.Location = new System.Drawing.Point(278, 31);
            this.lblTotalLoss.Name = "lblTotalLoss";
            this.lblTotalLoss.Size = new System.Drawing.Size(13, 13);
            this.lblTotalLoss.TabIndex = 1;
            this.lblTotalLoss.Text = "0";
            // 
            // lblLargestWin
            // 
            this.lblLargestWin.AutoSize = true;
            this.lblLargestWin.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLargestWin.Location = new System.Drawing.Point(84, 55);
            this.lblLargestWin.Name = "lblLargestWin";
            this.lblLargestWin.Size = new System.Drawing.Size(13, 13);
            this.lblLargestWin.TabIndex = 1;
            this.lblLargestWin.Text = "0";
            // 
            // lblLargestLoss
            // 
            this.lblLargestLoss.AutoSize = true;
            this.lblLargestLoss.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLargestLoss.Location = new System.Drawing.Point(257, 55);
            this.lblLargestLoss.Name = "lblLargestLoss";
            this.lblLargestLoss.Size = new System.Drawing.Size(13, 13);
            this.lblLargestLoss.TabIndex = 1;
            this.lblLargestLoss.Text = "0";
            // 
            // lblClosedPositionCount
            // 
            this.lblClosedPositionCount.AutoSize = true;
            this.lblClosedPositionCount.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClosedPositionCount.Location = new System.Drawing.Point(292, 29);
            this.lblClosedPositionCount.Name = "lblClosedPositionCount";
            this.lblClosedPositionCount.Size = new System.Drawing.Size(13, 13);
            this.lblClosedPositionCount.TabIndex = 1;
            this.lblClosedPositionCount.Text = "0";
            // 
            // lblCurrentSignal
            // 
            this.lblCurrentSignal.AutoSize = true;
            this.lblCurrentSignal.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentSignal.ForeColor = System.Drawing.Color.DarkRed;
            this.lblCurrentSignal.Location = new System.Drawing.Point(281, 131);
            this.lblCurrentSignal.Name = "lblCurrentSignal";
            this.lblCurrentSignal.Size = new System.Drawing.Size(33, 17);
            this.lblCurrentSignal.TabIndex = 1;
            this.lblCurrentSignal.Text = "N/A";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(11, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Open positions:";
            // 
            // lblOpenPositionCount
            // 
            this.lblOpenPositionCount.AutoSize = true;
            this.lblOpenPositionCount.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOpenPositionCount.Location = new System.Drawing.Point(105, 29);
            this.lblOpenPositionCount.Name = "lblOpenPositionCount";
            this.lblOpenPositionCount.Size = new System.Drawing.Size(13, 13);
            this.lblOpenPositionCount.TabIndex = 1;
            this.lblOpenPositionCount.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(193, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Updating in";
            // 
            // lblUpdateRemaining
            // 
            this.lblUpdateRemaining.AutoSize = true;
            this.lblUpdateRemaining.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUpdateRemaining.Location = new System.Drawing.Point(258, 8);
            this.lblUpdateRemaining.Name = "lblUpdateRemaining";
            this.lblUpdateRemaining.Size = new System.Drawing.Size(13, 13);
            this.lblUpdateRemaining.TabIndex = 1;
            this.lblUpdateRemaining.Text = "0";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(275, 8);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(39, 13);
            this.label14.TabIndex = 1;
            this.label14.Text = "ticks...";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.lblLargestLoss);
            this.panel1.Controls.Add(this.lblWinCount);
            this.panel1.Controls.Add(this.lblLargestWin);
            this.panel1.Controls.Add(this.lblLossCount);
            this.panel1.Controls.Add(this.lblTotalLoss);
            this.panel1.Controls.Add(this.lblTotalWin);
            this.panel1.Location = new System.Drawing.Point(5, 50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(367, 78);
            this.panel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Open profit/loss:";
            // 
            // lblOpenPL
            // 
            this.lblOpenPL.AutoSize = true;
            this.lblOpenPL.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOpenPL.Location = new System.Drawing.Point(105, 8);
            this.lblOpenPL.Name = "lblOpenPL";
            this.lblOpenPL.Size = new System.Drawing.Size(13, 13);
            this.lblOpenPL.TabIndex = 1;
            this.lblOpenPL.Text = "0";
            // 
            // frmStrategyView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 162);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblReputation);
            this.Controls.Add(this.lblCurrentSignal);
            this.Controls.Add(this.lblOpenPL);
            this.Controls.Add(this.lblOpenPositionCount);
            this.Controls.Add(this.lblClosedPositionCount);
            this.Controls.Add(this.lblUpdateRemaining);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmdClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmStrategyView";
            this.Text = "Strategy View";
            this.Load += new System.EventHandler(this.frmStrategyView_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmStrategyView_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmStrategyView_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblReputation;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblWinCount;
        private System.Windows.Forms.Label lblLossCount;
        private System.Windows.Forms.Label lblTotalWin;
        private System.Windows.Forms.Label lblTotalLoss;
        private System.Windows.Forms.Label lblLargestWin;
        private System.Windows.Forms.Label lblLargestLoss;
        private System.Windows.Forms.Label lblClosedPositionCount;
        private System.Windows.Forms.Label lblCurrentSignal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblOpenPositionCount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblUpdateRemaining;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblOpenPL;
    }
}