using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Media;

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
        //画像ファイル
        private string imageFolderPath = @"Images\"; // フォルダーのパス
        private int timar;
        private bool isGameOver;
        //スコア
        private int score;
        private ScoreManager scoreManager = new ScoreManager(); //スコア管理クラスの呼び出し
        //音楽
        private SoundPlayer player = new SoundPlayer();


        public Form1()
        {
            InitializeComponent();
            IntalizeStartScreen(); //スタート画面のセットアップ
            PlayLoopSound(); //BGMの再生
        }

        private void PlayLoopSound()
        {
            //実行ファイルのあるフォルダーにあるwavファイルを参照
            string soundFilePath = Path.Combine(Application.StartupPath, "natukasiki-omoide.wav");
            if (File.Exists(soundFilePath))
            {
                player.SoundLocation = soundFilePath;
                player.PlayLooping();
            }
        }

        private void IntalizeStartScreen()
        {
            //スタート画面用のパネル
            startPanel = new Panel()
            {
                Size = ClientSize,
                BackColor = Color.LightBlue
            };

            // スタートボタン
            startButton = new Button
            {
                Text = "Game Start",
                Font = new Font("Arial", 16),
                Size = new Size(200, 50),
                Location = new Point((ClientSize.Width - 200) / 2, (ClientSize.Height - 50) / 2)
            };
            startButton.Click += StartButton_Click;

            // 終了ボタン
            closeButton = new Button
            {
                Text = "終了",
                Font = new Font("Arial", 16),
                Size = new Size(200, 50),
                Location = new Point((ClientSize.Width - 200) / 2, (ClientSize.Height - 100))
            };
            closeButton.Click += CloseButton_Click;

            //パネルにボタンを追加
            startPanel.Controls.Add(startButton);
            startPanel.Controls.Add(closeButton);
            Controls.Add(startPanel);
        }

        //スタート画面を表示するメソッド
        private void ShowStartScreen()
        {
            startPanel.Visible = true; //スタート画面を表示
            score = 0;
            timar = 30;
            scoreLabel.Text = "Score: 0";
            timerLabel.Text = "Time: 30";
            isGameOver = false;
        }

        //スタートボタンの処理
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

        //終了ボタンの処理
        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
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

        //シャボン玉の作成
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

        //シャボン玉をクリックしたときの処理
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

        //シャボン玉が消えるときのアニメーション
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

            // 全てのシャボン玉の移動を停止
            foreach (Control control in Controls)
            {
                if (control is PictureBox bubble && bubble.Tag is Timer moveTimer)
                {
                    moveTimer.Stop();
                    moveTimer.Dispose();
                    bubble.Enabled = false; // クリック無効
                }
            }

            // プレイヤー名の入力
            string playerName = Prompt.ShowDialog("名前を入力してください", "Game Over");
            if (!string.IsNullOrWhiteSpace(playerName))
            {
                scoreManager.AddScore(playerName); //スコア保存
            }
            MessageBox.Show("Time's up! \nScore: " + score, "Game Over", MessageBoxButtons.OK);
            ShowStartScreen(); //スタート画面に戻る
        }
    }
}

