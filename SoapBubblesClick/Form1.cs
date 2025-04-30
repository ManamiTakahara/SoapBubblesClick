using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Media;

namespace SoapBubblesClick
{
    public partial class Form1 : Form
    {
        // フォームの初期化
        private Panel startPanel;
        private Button startButton;
        private Button rankingButton;
        private Button closeButton;

        private string imageFolderPath = @"Image\";
        private int timar;

        // スコア管理クラスのインスタンス
        private ScoreManager scoreManager = new ScoreManager();
        private SoundPlayer player = new SoundPlayer();
        private BubbleManager bubbleManager;

        // ゲームの状態を管理する変数
        private bool isGameOver;
        public bool IsGameRunning => !isGameOver;

        public Form1()
        {
            InitializeComponent();
            scoreLabel.Visible = false;   // ← ここで非表示にする
            timerLabel.Visible = false;   // ← ここで非表示にする
            IntalizeStartScreen();
            PlayLoopSound();
            this.DoubleBuffered = true;
            bubbleManager = new BubbleManager(this);
        }

        // サウンドをループ再生するメソッド
        private void PlayLoopSound()
        {
            string soundFilePath = Path.Combine(Application.StartupPath, "natukasiki-omoide.wav");
            if (File.Exists(soundFilePath))
            {
                player.SoundLocation = soundFilePath;
                player.PlayLooping();
            }
        }

        // スタート画面の初期化
        private void IntalizeStartScreen()
        {
            startPanel = new Panel()
            {
                Size = ClientSize,
                BackColor = Color.LightBlue
            };

            startButton = new Button
            {
                Text = "Game Start",
                Font = new Font("Arial", 16),
                Size = new Size(200, 50),
                Location = new Point((ClientSize.Width - 200) / 2, (ClientSize.Height - 250))
            };
            startButton.Click += StartButton_Click;

            rankingButton = new Button
            {
                Text = "Ranking",
                Font = new Font("Arial", 16),
                Size = new Size(200, 50),
                Location = new Point((ClientSize.Width - 200) / 2, (ClientSize.Height - 175))
            };
            rankingButton.Click += RadioButton_Click;

            closeButton = new Button
            {
                Text = "終了",
                Font = new Font("Arial", 16),
                Size = new Size(200, 50),
                Location = new Point((ClientSize.Width - 200) / 2, (ClientSize.Height - 100))
            };
            closeButton.Click += CloseButton_Click;

            startPanel.Controls.Add(startButton);
            startPanel.Controls.Add(rankingButton);
            startPanel.Controls.Add(closeButton);
            Controls.Add(startPanel);
        }

        private void ShowStartScreen()
        {
            startPanel.Visible = true;

            // ↓ ラベル非表示
            scoreLabel.Visible = false;
            timerLabel.Visible = false;

            timar = 30;
            UpdateScoreLabel(0);
            timerLabel.Text = "Time: 30";
            isGameOver = false;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            startPanel.Visible = false;

            // ゲーム開始時にラベルを表示
            scoreLabel.Visible = true;
            timerLabel.Visible = true;

            timar = 30;
            isGameOver = false;
            bubbleManager.ResetScore();
            UpdateScoreLabel(0);
            timerLabel.Text = "Time: 30";

            timer1.Tick += Timer1_Tick;
            timer1.Start();
            gameTimer.Start();
        }

        private void RadioButton_Click(object sender, EventArgs e)
        {
            ShowRanking();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            bubbleManager.CreateBubble();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (timar > 0)
            {
                timar--;
                timerLabel.Text = "Time: " + timar;
            }
            else
            {
                GameOver();
            }
        }

        // ゲームオーバー処理
        private void GameOver()
        {
            gameTimer.Stop();
            timer1.Stop();
            isGameOver = true;

            bubbleManager.StopAllBubbles();

            string playerName = Prompt.ShowDialog("名前を入力してください", "Game Over");
            if (!string.IsNullOrWhiteSpace(playerName))
            {
                scoreManager.AddScore(playerName, bubbleManager.Score);
            }

            MessageBox.Show("Time's up! \nScore: " + bubbleManager.Score, "Game Over");

            ShowRanking();
            ShowStartScreen();
        }

        // ランキング画面を表示するメソッド
        private void ShowRanking()
        {
            var scores = scoreManager.GetScores();
            RankingForm rankingForm = new RankingForm(scores);
            rankingForm.ShowDialog();
        }

        public void UpdateScoreLabel(int newScore)
        {
            scoreLabel.Text = "Score: " + newScore;
        }
    }
}

