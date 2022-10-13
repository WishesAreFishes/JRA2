namespace TwitterStatistics
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnStart = new System.Windows.Forms.Button();
            this.lblElapsed = new System.Windows.Forms.Label();
            this.lblTotalTweets = new System.Windows.Forms.Label();
            this.lblTweetsPerSecond = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTweetsPerSecond = new System.Windows.Forms.TextBox();
            this.txtTotalTweets = new System.Windows.Forms.TextBox();
            this.txtElapsedTime = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.txtHashTags = new System.Windows.Forms.TextBox();
            this.lblQueuePerformance = new System.Windows.Forms.Label();
            this.txtQueuePerformance = new System.Windows.Forms.TextBox();
            this.btnStopCapturing = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(27, 307);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(154, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start Capturing";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lblElapsed
            // 
            this.lblElapsed.AutoSize = true;
            this.lblElapsed.Location = new System.Drawing.Point(27, 94);
            this.lblElapsed.Name = "lblElapsed";
            this.lblElapsed.Size = new System.Drawing.Size(76, 15);
            this.lblElapsed.TabIndex = 1;
            this.lblElapsed.Text = "Elapsed Time";
            // 
            // lblTotalTweets
            // 
            this.lblTotalTweets.AutoSize = true;
            this.lblTotalTweets.Location = new System.Drawing.Point(27, 140);
            this.lblTotalTweets.Name = "lblTotalTweets";
            this.lblTotalTweets.Size = new System.Drawing.Size(70, 15);
            this.lblTotalTweets.TabIndex = 2;
            this.lblTotalTweets.Text = "Total Tweets";
            // 
            // lblTweetsPerSecond
            // 
            this.lblTweetsPerSecond.AutoSize = true;
            this.lblTweetsPerSecond.Location = new System.Drawing.Point(27, 182);
            this.lblTweetsPerSecond.Name = "lblTweetsPerSecond";
            this.lblTweetsPerSecond.Size = new System.Drawing.Size(104, 15);
            this.lblTweetsPerSecond.TabIndex = 3;
            this.lblTweetsPerSecond.Text = "Tweets Per Second";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 222);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Top Ten HashTags";
            // 
            // txtTweetsPerSecond
            // 
            this.txtTweetsPerSecond.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTweetsPerSecond.Location = new System.Drawing.Point(231, 174);
            this.txtTweetsPerSecond.Name = "txtTweetsPerSecond";
            this.txtTweetsPerSecond.Size = new System.Drawing.Size(151, 23);
            this.txtTweetsPerSecond.TabIndex = 6;
            // 
            // txtTotalTweets
            // 
            this.txtTotalTweets.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotalTweets.Location = new System.Drawing.Point(231, 132);
            this.txtTotalTweets.Name = "txtTotalTweets";
            this.txtTotalTweets.Size = new System.Drawing.Size(151, 23);
            this.txtTotalTweets.TabIndex = 7;
            // 
            // txtElapsedTime
            // 
            this.txtElapsedTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtElapsedTime.Location = new System.Drawing.Point(231, 86);
            this.txtElapsedTime.Name = "txtElapsedTime";
            this.txtElapsedTime.Size = new System.Drawing.Size(151, 23);
            this.txtElapsedTime.TabIndex = 8;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // txtHashTags
            // 
            this.txtHashTags.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHashTags.Location = new System.Drawing.Point(231, 222);
            this.txtHashTags.Multiline = true;
            this.txtHashTags.Name = "txtHashTags";
            this.txtHashTags.Size = new System.Drawing.Size(151, 242);
            this.txtHashTags.TabIndex = 9;
            // 
            // lblQueuePerformance
            // 
            this.lblQueuePerformance.AutoSize = true;
            this.lblQueuePerformance.Location = new System.Drawing.Point(27, 51);
            this.lblQueuePerformance.Name = "lblQueuePerformance";
            this.lblQueuePerformance.Size = new System.Drawing.Size(113, 15);
            this.lblQueuePerformance.TabIndex = 10;
            this.lblQueuePerformance.Text = "Queue Performance";
            // 
            // txtQueuePerformance
            // 
            this.txtQueuePerformance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtQueuePerformance.Location = new System.Drawing.Point(231, 43);
            this.txtQueuePerformance.Name = "txtQueuePerformance";
            this.txtQueuePerformance.Size = new System.Drawing.Size(151, 23);
            this.txtQueuePerformance.TabIndex = 11;
            // 
            // btnStopCapturing
            // 
            this.btnStopCapturing.Enabled = false;
            this.btnStopCapturing.Location = new System.Drawing.Point(27, 352);
            this.btnStopCapturing.Name = "btnStopCapturing";
            this.btnStopCapturing.Size = new System.Drawing.Size(154, 23);
            this.btnStopCapturing.TabIndex = 12;
            this.btnStopCapturing.Text = "Stop Capturing";
            this.btnStopCapturing.UseVisualStyleBackColor = true;
            this.btnStopCapturing.Click += new System.EventHandler(this.btnStopCapturing_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 514);
            this.Controls.Add(this.btnStopCapturing);
            this.Controls.Add(this.txtQueuePerformance);
            this.Controls.Add(this.lblQueuePerformance);
            this.Controls.Add(this.txtHashTags);
            this.Controls.Add(this.txtElapsedTime);
            this.Controls.Add(this.txtTotalTweets);
            this.Controls.Add(this.txtTweetsPerSecond);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTweetsPerSecond);
            this.Controls.Add(this.lblTotalTweets);
            this.Controls.Add(this.lblElapsed);
            this.Controls.Add(this.btnStart);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btnStart;
        private Label lblElapsed;
        private Label lblTotalTweets;
        private Label lblTweetsPerSecond;
        private Label label1;
        private TextBox txtTweetsPerSecond;
        private TextBox txtTotalTweets;
        private TextBox txtElapsedTime;
        private System.Windows.Forms.Timer timer1;
        private TextBox txtHashTags;
        private Label lblQueuePerformance;
        private TextBox txtQueuePerformance;
        private Button btnStopCapturing;
    }
}