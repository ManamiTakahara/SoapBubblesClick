using System;
using System.Drawing;
using System.Windows.Forms;

namespace SoapBubblesClick
{
    public partial class Form1 : Form
    {
        // シャボン玉
        private Random random = new Random();
        // スコア
        private int score = 0;
        //タイマー
        private int timar = 30; //残り時間（秒）
        private bool isGameOver = false; //ゲーム終了判定
        public Form1()
        {
            InitializeComponent();
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
                Image = CreateBubbleImage(size),
                SizeMode = PictureBoxSizeMode.StretchImage
            };

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
                Controls.Remove(bubble);
                bubble.Dispose();
            }
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


        private Bitmap CreateBubbleImage(int size)
        {
            Bitmap bmp = new Bitmap(size, size);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (Brush brush = new System.Drawing.Drawing2D.LinearGradientBrush
                (new Rectangle(0, 0, size, size),
                Color.LightBlue, Color.Wheat, 45))
                {
                    g.FillRectangle(brush, 0, 0, size, size);
                }
                g.DrawEllipse(Pens.Blue, 0, 0, size - 1, size - 1);
            }
            return bmp;
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

