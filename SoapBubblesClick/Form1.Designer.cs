namespace SoapBubblesClick
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.scoreLabel = new System.Windows.Forms.Label();
            this.timerLabel = new System.Windows.Forms.Label();
            this.moveTimer = new System.Windows.Forms.Timer(this.components);
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.startButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // scoreLabel
            // 
            this.scoreLabel.AutoSize = true;
            this.scoreLabel.BackColor = System.Drawing.Color.Transparent;
            this.scoreLabel.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.scoreLabel.Location = new System.Drawing.Point(38, 25);
            this.scoreLabel.Name = "scoreLabel";
            this.scoreLabel.Size = new System.Drawing.Size(100, 30);
            this.scoreLabel.TabIndex = 6;
            this.scoreLabel.Text = "Score: 0";
            // 
            // timerLabel
            // 
            this.timerLabel.AutoSize = true;
            this.timerLabel.BackColor = System.Drawing.Color.Transparent;
            this.timerLabel.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.timerLabel.Location = new System.Drawing.Point(577, 25);
            this.timerLabel.Name = "timerLabel";
            this.timerLabel.Size = new System.Drawing.Size(108, 30);
            this.timerLabel.TabIndex = 8;
            this.timerLabel.Text = "Time: 30";
            // 
            // moveTimer
            // 
            this.moveTimer.Interval = 1000;
            this.moveTimer.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // gameTimer
            // 
            this.gameTimer.Interval = 1000;
            this.gameTimer.Tick += new System.EventHandler(this.GameTimer_Tick);
            // 
            // startButton
            // 
            this.startButton.Font = new System.Drawing.Font("メイリオ", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.startButton.Location = new System.Drawing.Point(296, 197);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(176, 44);
            this.startButton.TabIndex = 9;
            this.startButton.Text = "Game Stert";
            this.startButton.UseVisualStyleBackColor = true;
            // 
            // CloseButton
            // 
            this.CloseButton.Font = new System.Drawing.Font("メイリオ", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CloseButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.CloseButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CloseButton.Location = new System.Drawing.Point(296, 274);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(176, 44);
            this.CloseButton.TabIndex = 10;
            this.CloseButton.Text = "終了";
            this.CloseButton.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.timerLabel);
            this.Controls.Add(this.scoreLabel);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label scoreLabel;
        private System.Windows.Forms.Label timerLabel;
        private System.Windows.Forms.Timer moveTimer;
        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button CloseButton;
    }
}

