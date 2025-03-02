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
        private Counter counter;
        //タイマー
        private int timar = 30; //残り時間（秒）
        private bool isGameOver = false; //ゲーム終了判定
        public Form1()
        {
            InitializeComponent();
            counter = new Counter();
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();
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
            // シャボン玉のサイズ
            int size = random.Next(30, 60);
            int x = random.Next(ClientSize.Width - size);
            int y = random.Next(ClientSize.Height - size);
            PictureBox bubble = new PictureBox
            {
                Size = new Size(size, size),
                Location = new Point(x, y),
                BackColor = Color.Transparent,
                Image = CreateBubbulImage(size),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Tag = "bubble"
            };

            bubble.Click += Bubble_Click;
            Controls.Add(bubble);
        }

        // シャボン玉をクリックしたときの処理
        private void Bubble_Click(object sender, EventArgs e)
        {
            PictureBox bubble = sender as PictureBox;
            if (bubble != null)
            {
                Controls.Remove(bubble);
                bubble.Dispose();
                counter.Intcrement();
                scoreLabel.Text = counter.Value.ToString();
            }
        }

        private Bitmap CreateBubbulImage(int size)
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
            gameTimer.Interval = 50;//Tickイベントの発生
            gameTimer.Start();

        }

        // シャボン玉をクリックすると加算する
        private void BubblePicture_Clik(object sender, EventArgs e)
        {

        }

        //ゲームオーバーの判定
        private void GameOver()
        {
            gameTimer2.Stop(); //シャボン玉の生成を停止
            gameTimer.Stop(); //ゲームの終了
            isGameOver = true;

            //全てのシャボン玉をクリックできないようにする
            foreach (Control control in Controls)
            {
                if (control is PictureBox && control.Tag?.ToString() == "bubble")
                {
                    control.Enabled = false; //無効化
                }
            }
            MessageBox.Show("Time's Up! \nScore: " + counter.Value, "Game Over");
        }
    }
}

