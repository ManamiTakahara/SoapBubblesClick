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
                SizeMode = PictureBoxSizeMode.StretchImage,
                Tag = size
            };

            // シャボン玉用のタイマーを作成
            Timer moveTimer = new Timer();
            moveTimer.Interval = 50; // 0.05秒ごとに移動
            moveTimer.Tick += (s, e) => MoveBubble(bubble, moveTimer);

            moveTimer.Start(); // タイマー開始
            // シャボン玉作成時にタプルとして保存
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
            if (bubble.Tag is ValueTuple<int, Timer> bubbleData)
            {
                int size = bubbleData.Item1; // タプルの1番目の値（サイズ）
                // ※または、C# 7.0以降なら、以下のように名前付きタプルも使えます：
                // var (size, timer) = ((int Size, Timer MoveTimer))bubble.Tag;

                // 4. 得点計算: 小さいシャボン玉ほど高得点になるように計算
                // 例として、60をサイズで割って得点を決定しています
                int points = 1500 / size;
                score += points;

                // 5. スコア表示の更新
                scoreLabel.Text = "Score: " + score;

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

            MessageBox.Show("Time's up! Game Over!\nScore: " + score, "Game Over");
        }
    }
}

