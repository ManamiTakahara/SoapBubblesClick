using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            moveTimer.Tick += GameTimer_Tick;
            moveTimer.Start();
            gameTimer2.Start();
            //変数の宣言

            //概要のコメント

        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            CreateBubble();
        }

        // タイマーの設定
        private void GameTimer2_Tick(object sender, EventArgs e)
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
            bubble.Tag = (size, moveTimer); // 後で停止できるようにタイマーを保存

            bubble.Click += Bubble_Click;
            Controls.Add(bubble);
        }

        private void Bubble_Click(object sender, EventArgs e)
        {
            if (isGameOver) return; // ゲーム終了時はクリック不可

            PictureBox bubble = sender as PictureBox;
            if (bubble != null)
            {
                Controls.Remove(bubble);
                bubble.Dispose();
                score++;
                scoreLabel.Text = "Score: " + score;
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

        private void Form_Load(object sender, EventArgs e)
        {
            moveTimer.Interval = 50;//Tickイベントの発生
            moveTimer.Start();

        }

        //ゲームオーバーの判定
        private void GameOver()
        {
            gameTimer2.Stop();
            moveTimer.Stop();
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

