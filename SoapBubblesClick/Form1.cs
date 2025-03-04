using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace SoapBubblesClick
{
    public partial class Form1 : Form
    {
        // スタート画面のパネル
        private Panel startPanel;
        // スタートボタン
        private Button startButton;
        // 終了ボタン
        private Button closeButton;

        // シャボン玉
        private Random random = new Random();

        private string imageFolderPath = @"Images\"; // フォルダーのパス
        private int timar;
        private bool isGameOver;
        private int score;

        public Form1()
        {
            InitializeComponent();
            IntalizeStartScreen(); //スタート画面のセットアップ
        }

        private void IntalizeStartScreen()
        {
            startPanel = new Panel()
            {
                BackColor = Color.LightBlue
            };

            // スタートボタン
            startButton = new Button
            {
                Text = "Game Start",
                Font = new Font("Arial", 16),
                Size = new Size(200, 50),
                Location = new Point((ClientSize.Width - 200) / 2, (ClientSize.Height - 50) / 2),
            };
            startButton.Click += StartButton_Click;

            //パネルにボタンを追加
            startPanel.Controls.Add(startButton);
            Controls.Add(startPanel);
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            //スタート画面を非表示
            startPanel.Visible = false;

            //ゲーム開始
            timar = 30; //残り時間（秒）
            isGameOver = false; //ゲーム終了判定
            score = 0;
            timer1.Tick += Timer1_Tick;
            timer1.Start();
            gameTimer.Start();
            
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            CreateBubble();
        }

        // タイマーの設定
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

        private void CreateBubble()
        {
            int size = random.Next(30, 60);
            int x = random.Next(ClientSize.Width - size);
            int y = ClientSize.Height - size; // 下から発生

            PictureBox bubble = new PictureBox
            {
                Size = new Size(size, size),
                Location = new Point(x, y),
                BackColor = Color.Transparent,
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            //画像を読み込む
            string bubbleImagePath = Path.Combine(imageFolderPath, "bubble.png");
            if (File.Exists(bubbleImagePath))
            {
                bubble.Image = Image.FromFile(bubbleImagePath);
            }

            // シャボン玉用の移動タイマーを作成
            Timer moveTimer = new Timer();
            moveTimer.Interval = 50; // 50ミリ秒ごとに移動
            moveTimer.Tick += (s, e) => MoveBubble(bubble, moveTimer);
            moveTimer.Start();

            // タプルとしてサイズとタイマーの両方をTagに保存
            bubble.Tag = (Size: size, MoveTimer: moveTimer);
            bubble.Click += Bubble_Click;

            Controls.Add(bubble);
        }

        private void Bubble_Click(object sender, EventArgs e)
        {
            // 1. ゲーム終了状態の場合は処理を中断
            if (isGameOver) return;

            // 2. クリックされたオブジェクトを PictureBox として取得
            PictureBox bubble = sender as PictureBox;
            if (bubble != null)
            {
                // タプルからサイズを取得
                if (bubble.Tag is ValueTuple<int, Timer> bubbleData)
                {
                    int size = bubbleData.Item1;  // タプルの1番目がサイズ
                    int points = 1500 / size;
                    score += points;
                    scoreLabel.Text = "Score: " + score;
                }

                //　シャボン玉の移動タイマーも停止
                if (bubble.Tag is ValueTuple<int, Timer> date)
                {
                    date.Item2.Stop();
                    date.Item2.Dispose();
                }

                // 6. シャボン玉を画面から削除し、リソースを解放
                PlayPopAnimation(bubble);
            }
        }

        private void PlayPopAnimation(PictureBox bubble)
        {
            int frame = 0;
            Timer animTimer = new Timer { Interval = 50 };

            animTimer.Tick += (s, e) =>
            {
                frame++;
                string popImegePath = Path.Combine(imageFolderPath, $"pop{frame}.png");

                if (File.Exists(popImegePath))
                {
                    bubble.Image = Image.FromFile(popImegePath);
                }

                if (frame >= 3) //3フレームで終了
                {
                    animTimer.Stop();
                    animTimer.Dispose();
                    Controls.Remove(bubble);
                    bubble.Dispose();
                }
            };
            animTimer.Start();
        }

        private void MoveBubble(PictureBox bubble, Timer moveTimer)
        {
            if (bubble == null || isGameOver) return;

            // 上へ移動
            bubble.Top -= 5;

            // 画面上端に到達したら削除
            if (bubble.Top + bubble.Height < 0)
            {
                moveTimer.Stop();
                moveTimer.Dispose();
                Controls.Remove(bubble);
                bubble.Dispose();
            }
        }

        //ゲームオーバーの判定
        private void GameOver()
        {
            gameTimer.Stop();
            timer1.Stop();
            isGameOver = true;

            foreach (Control control in Controls)
            {
                if (control is PictureBox bubble && bubble.Tag is Timer moveTimer)
                {
                    moveTimer.Stop();
                    moveTimer.Dispose();
                    bubble.Enabled = false; // クリック無効
                }
            }

            MessageBox.Show("Time's up! \nScore: " + score, "Game Over");
        }
    }
}

