using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SoapBubblesClick
{
    // シャボン玉の管理クラス
    public class BubbleManager
    {
        private readonly Form1 form;
        private readonly Random random = new Random();
        private readonly string imageFolderPath = @"Image\";

        public int Score { get; private set; }

        public BubbleManager(Form1 form)
        {
            this.form = form;
        }

        // シャボン玉を作成するメソッド
        public void CreateBubble()
        {
            int size = random.Next(30, 60);
            int x = random.Next(form.ClientSize.Width - size);
            int y = form.ClientSize.Height - size;

            PictureBox bubble = new PictureBox
            {
                Size = new Size(size, size),
                Location = new Point(x, y),
                BackColor = Color.Transparent,
                SizeMode = PictureBoxSizeMode.Zoom
            };

            string imagePath = Path.Combine(imageFolderPath, "bubble.png");
            if (File.Exists(imagePath))
            {
                bubble.Image = Image.FromFile(imagePath);
            }
            else
            {
                bubble.Image = CreateBubbleImage(size);
            }

            Timer moveTimer = new Timer { Interval = 10 };
            moveTimer.Tick += (s, e) => MoveBubble(bubble, moveTimer);
            moveTimer.Start();

            bubble.Tag = (Size: size, MoveTimer: moveTimer);
            bubble.Click += Bubble_Click;

            form.Controls.Add(bubble);
        }

        // シャボン玉の画像を作成するメソッド
        private Bitmap CreateBubbleImage(int size)
        {
            Bitmap bmp = new Bitmap(size, size);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);

                using (Brush brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                    new Rectangle(0, 0, size, size),
                    Color.LightBlue, Color.White, 45))
                {
                    g.FillEllipse(brush, 0, 0, size - 1, size - 1);
                }

                g.DrawEllipse(Pens.Blue, 0, 0, size - 1, size - 1);
            }
            return bmp;
        }

        // シャボン玉がクリックされたときの処理
        private void Bubble_Click(object sender, EventArgs e)
        {
            if (!form.IsGameRunning) return;

            if (sender is PictureBox bubble && bubble.Tag is ValueTuple<int, Timer> bubbleData)
            {
                int size = bubbleData.Item1;
                int points = 1800 / size;
                Score += points;
                form.UpdateScoreLabel(Score);

                bubbleData.Item2.Stop();
                bubbleData.Item2.Dispose();

                PlayPopAnimation(bubble);
            }
        }

        // シャボン玉のポップアニメーションを再生するメソッド
        private void PlayPopAnimation(PictureBox bubble)
        {
            int frame = 0;
            Timer animTimer = new Timer { Interval = 25 };

            animTimer.Tick += (s, e) =>
            {
                frame++;
                string path = Path.Combine(imageFolderPath, $"pop{frame}.png");
                if (File.Exists(path))
                {
                    bubble.Image = Image.FromFile(path);
                }

                if (frame >= 3)
                {
                    animTimer.Stop();
                    animTimer.Dispose();
                    form.Controls.Remove(bubble);
                    bubble.Dispose();
                }
            };
            animTimer.Start();
        }

        private void MoveBubble(PictureBox bubble, Timer moveTimer)
        {
            if (!form.IsGameRunning || bubble == null) return;

            bubble.Top -= 3;

            if (bubble.Top + bubble.Height < 0)
            {
                moveTimer.Stop();
                moveTimer.Dispose();
                form.Controls.Remove(bubble);
                bubble.Dispose();
            }
        }

        public void StopAllBubbles()
        {
            foreach (Control control in form.Controls)
            {
                if (control is PictureBox bubble && bubble.Tag is ValueTuple<int, Timer> data)
                {
                    data.Item2.Stop();
                    data.Item2.Dispose();
                    bubble.Enabled = false;
                }
            }
        }

        public void ResetScore()
        {
            Score = 0;
        }
    }
}
