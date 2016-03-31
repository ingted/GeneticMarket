using System;

namespace GeneticMarket.NodeManager
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private global::System.ComponentModel.IContainer components = null;

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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdStart = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label17 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblCurrentTick = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.lblTickQueueSize = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblSignalCount = new System.Windows.Forms.Label();
            this.lblStrategyDeleteCount = new System.Windows.Forms.Label();
            this.lblStrategyCount = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblOpenPositionCount = new System.Windows.Forms.Label();
            this.lblClosedPositionCount = new System.Windows.Forms.Label();
            this.pnlMarket = new System.Windows.Forms.Panel();
            this.cboTimeFrames = new System.Windows.Forms.ComboBox();
            this.cboInstruments = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblMarketQuoteSentiment = new System.Windows.Forms.Label();
            this.lblMarketTrend = new System.Windows.Forms.Label();
            this.lblMarketBaseSentiment = new System.Windows.Forms.Label();
            this.lblMarketVolatility = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmdReport = new System.Windows.Forms.Button();
            this.cmdLoad = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.lblTimer = new System.Windows.Forms.Label();
            this.upTimer = new System.Windows.Forms.Timer(this.components);
            this.txtStrategyId = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.cmdStrategyView = new System.Windows.Forms.Button();
            this.chkAutoSave = new System.Windows.Forms.CheckBox();
            this.cmdRefine = new System.Windows.Forms.Button();
            this.cmdOptions = new System.Windows.Forms.Button();
            this.panel4.SuspendLayout();
            this.pnlMarket.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Port #:";
            // 
            // cmdStart
            // 
            this.cmdStart.Location = new System.Drawing.Point(56, 84);
            this.cmdStart.Name = "cmdStart";
            this.cmdStart.Size = new System.Drawing.Size(106, 38);
            this.cmdStart.TabIndex = 1;
            this.cmdStart.Text = "Start";
            this.cmdStart.UseVisualStyleBackColor = true;
            this.cmdStart.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(57, 48);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(121, 20);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "8085";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label17);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.lblCurrentTick);
            this.panel4.Controls.Add(this.label20);
            this.panel4.Controls.Add(this.lblTickQueueSize);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.lblSignalCount);
            this.panel4.Controls.Add(this.lblStrategyDeleteCount);
            this.panel4.Controls.Add(this.lblStrategyCount);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Controls.Add(this.lblOpenPositionCount);
            this.panel4.Controls.Add(this.lblClosedPositionCount);
            this.panel4.Location = new System.Drawing.Point(12, 159);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(244, 255);
            this.panel4.TabIndex = 12;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(10, 229);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(89, 13);
            this.label17.TabIndex = 1;
            this.label17.Text = "Tick Queue Size:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 193);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Indicator Count:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Current Tick:";
            // 
            // lblCurrentTick
            // 
            this.lblCurrentTick.AutoSize = true;
            this.lblCurrentTick.Location = new System.Drawing.Point(79, 13);
            this.lblCurrentTick.Name = "lblCurrentTick";
            this.lblCurrentTick.Size = new System.Drawing.Size(35, 13);
            this.lblCurrentTick.TabIndex = 1;
            this.lblCurrentTick.Text = "label1";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(10, 85);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(114, 13);
            this.label20.TabIndex = 1;
            this.label20.Text = "Strategy Delete Count:";
            // 
            // lblTickQueueSize
            // 
            this.lblTickQueueSize.AutoSize = true;
            this.lblTickQueueSize.Location = new System.Drawing.Point(101, 229);
            this.lblTickQueueSize.Name = "lblTickQueueSize";
            this.lblTickQueueSize.Size = new System.Drawing.Size(13, 13);
            this.lblTickQueueSize.TabIndex = 1;
            this.lblTickQueueSize.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Strategy Count:";
            // 
            // lblSignalCount
            // 
            this.lblSignalCount.AutoSize = true;
            this.lblSignalCount.Location = new System.Drawing.Point(96, 193);
            this.lblSignalCount.Name = "lblSignalCount";
            this.lblSignalCount.Size = new System.Drawing.Size(13, 13);
            this.lblSignalCount.TabIndex = 1;
            this.lblSignalCount.Text = "0";
            // 
            // lblStrategyDeleteCount
            // 
            this.lblStrategyDeleteCount.AutoSize = true;
            this.lblStrategyDeleteCount.Location = new System.Drawing.Point(127, 85);
            this.lblStrategyDeleteCount.Name = "lblStrategyDeleteCount";
            this.lblStrategyDeleteCount.Size = new System.Drawing.Size(13, 13);
            this.lblStrategyDeleteCount.TabIndex = 1;
            this.lblStrategyDeleteCount.Text = "0";
            // 
            // lblStrategyCount
            // 
            this.lblStrategyCount.AutoSize = true;
            this.lblStrategyCount.Location = new System.Drawing.Point(96, 49);
            this.lblStrategyCount.Name = "lblStrategyCount";
            this.lblStrategyCount.Size = new System.Drawing.Size(13, 13);
            this.lblStrategyCount.TabIndex = 1;
            this.lblStrategyCount.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 121);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Open Position Count:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 157);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(113, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Closed Position Count:";
            // 
            // lblOpenPositionCount
            // 
            this.lblOpenPositionCount.AutoSize = true;
            this.lblOpenPositionCount.Location = new System.Drawing.Point(123, 121);
            this.lblOpenPositionCount.Name = "lblOpenPositionCount";
            this.lblOpenPositionCount.Size = new System.Drawing.Size(13, 13);
            this.lblOpenPositionCount.TabIndex = 1;
            this.lblOpenPositionCount.Text = "0";
            // 
            // lblClosedPositionCount
            // 
            this.lblClosedPositionCount.AutoSize = true;
            this.lblClosedPositionCount.Location = new System.Drawing.Point(123, 157);
            this.lblClosedPositionCount.Name = "lblClosedPositionCount";
            this.lblClosedPositionCount.Size = new System.Drawing.Size(13, 13);
            this.lblClosedPositionCount.TabIndex = 1;
            this.lblClosedPositionCount.Text = "0";
            // 
            // pnlMarket
            // 
            this.pnlMarket.Controls.Add(this.cboTimeFrames);
            this.pnlMarket.Controls.Add(this.cboInstruments);
            this.pnlMarket.Controls.Add(this.label12);
            this.pnlMarket.Controls.Add(this.label8);
            this.pnlMarket.Controls.Add(this.label9);
            this.pnlMarket.Controls.Add(this.label10);
            this.pnlMarket.Controls.Add(this.label13);
            this.pnlMarket.Controls.Add(this.lblMarketQuoteSentiment);
            this.pnlMarket.Controls.Add(this.lblMarketTrend);
            this.pnlMarket.Controls.Add(this.lblMarketBaseSentiment);
            this.pnlMarket.Controls.Add(this.lblMarketVolatility);
            this.pnlMarket.Controls.Add(this.label11);
            this.pnlMarket.Location = new System.Drawing.Point(282, 162);
            this.pnlMarket.Name = "pnlMarket";
            this.pnlMarket.Size = new System.Drawing.Size(210, 255);
            this.pnlMarket.TabIndex = 13;
            // 
            // cboTimeFrames
            // 
            this.cboTimeFrames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTimeFrames.FormattingEnabled = true;
            this.cboTimeFrames.Location = new System.Drawing.Point(110, 55);
            this.cboTimeFrames.Name = "cboTimeFrames";
            this.cboTimeFrames.Size = new System.Drawing.Size(88, 21);
            this.cboTimeFrames.TabIndex = 2;
            // 
            // cboInstruments
            // 
            this.cboInstruments.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboInstruments.FormattingEnabled = true;
            this.cboInstruments.Location = new System.Drawing.Point(112, 15);
            this.cboInstruments.Name = "cboInstruments";
            this.cboInstruments.Size = new System.Drawing.Size(86, 21);
            this.cboInstruments.TabIndex = 2;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(14, 59);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(95, 13);
            this.label12.TabIndex = 1;
            this.label12.Text = "Active TimeFrame:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(92, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Active Instrument:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 99);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Market Trend:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(14, 181);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(125, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "Market Quote Sentiment:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(14, 139);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(120, 13);
            this.label13.TabIndex = 1;
            this.label13.Text = "Market Base Sentiment:";
            // 
            // lblMarketQuoteSentiment
            // 
            this.lblMarketQuoteSentiment.AutoSize = true;
            this.lblMarketQuoteSentiment.Location = new System.Drawing.Point(145, 181);
            this.lblMarketQuoteSentiment.Name = "lblMarketQuoteSentiment";
            this.lblMarketQuoteSentiment.Size = new System.Drawing.Size(35, 13);
            this.lblMarketQuoteSentiment.TabIndex = 1;
            this.lblMarketQuoteSentiment.Text = "label1";
            // 
            // lblMarketTrend
            // 
            this.lblMarketTrend.AutoSize = true;
            this.lblMarketTrend.Location = new System.Drawing.Point(87, 99);
            this.lblMarketTrend.Name = "lblMarketTrend";
            this.lblMarketTrend.Size = new System.Drawing.Size(13, 13);
            this.lblMarketTrend.TabIndex = 1;
            this.lblMarketTrend.Text = "0";
            // 
            // lblMarketBaseSentiment
            // 
            this.lblMarketBaseSentiment.AutoSize = true;
            this.lblMarketBaseSentiment.Location = new System.Drawing.Point(132, 139);
            this.lblMarketBaseSentiment.Name = "lblMarketBaseSentiment";
            this.lblMarketBaseSentiment.Size = new System.Drawing.Size(35, 13);
            this.lblMarketBaseSentiment.TabIndex = 1;
            this.lblMarketBaseSentiment.Text = "label1";
            // 
            // lblMarketVolatility
            // 
            this.lblMarketVolatility.AutoSize = true;
            this.lblMarketVolatility.Location = new System.Drawing.Point(107, 219);
            this.lblMarketVolatility.Name = "lblMarketVolatility";
            this.lblMarketVolatility.Size = new System.Drawing.Size(35, 13);
            this.lblMarketVolatility.TabIndex = 1;
            this.lblMarketVolatility.Text = "label1";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(16, 219);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(84, 13);
            this.label11.TabIndex = 1;
            this.label11.Text = "Market Volatility:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmdStart);
            this.groupBox1.Controls.Add(this.txtServer);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 135);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(11, 21);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(54, 13);
            this.label15.TabIndex = 0;
            this.label15.Text = "Server IP:";
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(71, 18);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(106, 20);
            this.txtServer.TabIndex = 2;
            this.txtServer.Text = "localhost";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(218, 20);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(84, 13);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "NOT STARTED";
            // 
            // cmdReport
            // 
            this.cmdReport.Location = new System.Drawing.Point(386, 20);
            this.cmdReport.Name = "cmdReport";
            this.cmdReport.Size = new System.Drawing.Size(106, 38);
            this.cmdReport.TabIndex = 1;
            this.cmdReport.Text = "Save State";
            this.cmdReport.UseVisualStyleBackColor = true;
            this.cmdReport.Click += new System.EventHandler(this.cmdReport_Click);
            // 
            // cmdLoad
            // 
            this.cmdLoad.Location = new System.Drawing.Point(386, 64);
            this.cmdLoad.Name = "cmdLoad";
            this.cmdLoad.Size = new System.Drawing.Size(106, 38);
            this.cmdLoad.TabIndex = 1;
            this.cmdLoad.Text = "Load State";
            this.cmdLoad.UseVisualStyleBackColor = true;
            this.cmdLoad.Click += new System.EventHandler(this.cmdLoad_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(218, 46);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(50, 13);
            this.label14.TabIndex = 1;
            this.label14.Text = "Up Time:";
            // 
            // lblTimer
            // 
            this.lblTimer.AutoSize = true;
            this.lblTimer.Location = new System.Drawing.Point(274, 46);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(49, 13);
            this.lblTimer.TabIndex = 1;
            this.lblTimer.Text = "00:00:00";
            // 
            // upTimer
            // 
            this.upTimer.Interval = 1000;
            this.upTimer.Tick += new System.EventHandler(this.upTimer_Tick);
            // 
            // txtStrategyId
            // 
            this.txtStrategyId.Location = new System.Drawing.Point(76, 430);
            this.txtStrategyId.Name = "txtStrategyId";
            this.txtStrategyId.Size = new System.Drawing.Size(114, 20);
            this.txtStrategyId.TabIndex = 2;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(9, 434);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(61, 13);
            this.label16.TabIndex = 0;
            this.label16.Text = "Strategy Id:";
            // 
            // cmdStrategyView
            // 
            this.cmdStrategyView.Location = new System.Drawing.Point(196, 429);
            this.cmdStrategyView.Name = "cmdStrategyView";
            this.cmdStrategyView.Size = new System.Drawing.Size(60, 22);
            this.cmdStrategyView.TabIndex = 1;
            this.cmdStrategyView.Text = "View";
            this.cmdStrategyView.UseVisualStyleBackColor = true;
            this.cmdStrategyView.Click += new System.EventHandler(this.cmdStrategyView_Click);
            // 
            // chkAutoSave
            // 
            this.chkAutoSave.AutoSize = true;
            this.chkAutoSave.Location = new System.Drawing.Point(299, 432);
            this.chkAutoSave.Name = "chkAutoSave";
            this.chkAutoSave.Size = new System.Drawing.Size(100, 17);
            this.chkAutoSave.TabIndex = 15;
            this.chkAutoSave.Text = "Auto save state";
            this.chkAutoSave.UseVisualStyleBackColor = true;
            // 
            // cmdRefine
            // 
            this.cmdRefine.Location = new System.Drawing.Point(386, 109);
            this.cmdRefine.Name = "cmdRefine";
            this.cmdRefine.Size = new System.Drawing.Size(106, 38);
            this.cmdRefine.TabIndex = 1;
            this.cmdRefine.Text = "Refine Strategies";
            this.cmdRefine.UseVisualStyleBackColor = true;
            this.cmdRefine.Click += new System.EventHandler(this.cmdRefine_Click);
            // 
            // cmdOptions
            // 
            this.cmdOptions.Location = new System.Drawing.Point(221, 109);
            this.cmdOptions.Name = "cmdOptions";
            this.cmdOptions.Size = new System.Drawing.Size(106, 38);
            this.cmdOptions.TabIndex = 1;
            this.cmdOptions.Text = "Options";
            this.cmdOptions.UseVisualStyleBackColor = true;
            this.cmdOptions.Click += new System.EventHandler(this.cmdOptions_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 475);
            this.Controls.Add(this.chkAutoSave);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.cmdStrategyView);
            this.Controls.Add(this.cmdRefine);
            this.Controls.Add(this.cmdLoad);
            this.Controls.Add(this.cmdOptions);
            this.Controls.Add(this.cmdReport);
            this.Controls.Add(this.pnlMarket);
            this.Controls.Add(this.txtStrategyId);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.lblTimer);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.lblStatus);
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Text = "Genetic Market Node Manager";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.pnlMarket.ResumeLayout(false);
            this.pnlMarket.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private global::System.Windows.Forms.Label label1;
        private global::System.Windows.Forms.Button cmdStart;
        private global::System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblCurrentTick;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblSignalCount;
        private System.Windows.Forms.Label lblStrategyDeleteCount;
        private System.Windows.Forms.Label lblStrategyCount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblOpenPositionCount;
        private System.Windows.Forms.Label lblClosedPositionCount;
        private System.Windows.Forms.Panel pnlMarket;
        private System.Windows.Forms.ComboBox cboTimeFrames;
        private System.Windows.Forms.ComboBox cboInstruments;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblMarketQuoteSentiment;
        private System.Windows.Forms.Label lblMarketTrend;
        private System.Windows.Forms.Label lblMarketBaseSentiment;
        private System.Windows.Forms.Label lblMarketVolatility;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button cmdReport;
        private System.Windows.Forms.Button cmdLoad;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblTimer;
        private System.Windows.Forms.Timer upTimer;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Label lblTickQueueSize;
        private System.Windows.Forms.TextBox txtStrategyId;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button cmdStrategyView;
        private System.Windows.Forms.CheckBox chkAutoSave;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button cmdRefine;
        private System.Windows.Forms.Button cmdOptions;
    }
}

