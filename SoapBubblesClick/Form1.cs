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
        private Counter counter;
        public Form1()
        {
            InitializeComponent();
            counter = new Counter();
            gameTimer.Tick += GameTimer_Tick;
            //変数の宣言
           

            //タイマーの設定
            //counter = new Counter();

            int score;
            int rimainTime;

            //概要のコメント
            //シャボン玉の動き
            //得点が加算される
            //時間が減る
            
            //ゲームオーバーの判定

        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            int UpSpeed = 3;
            //シャボン玉を上に移動
            bubblePicture1.Top += UpSpeed;

            //画面上に到達した場合、再度下部ランダム位置に配置
            if (bubblePicture1.Top > this.PreferredSize.Height)
            {
                ResetUpObject();
            }
            
        }

        private void ResetUpObject()
        {
            bubblePicture1.Top = bubblePicture1.Height;
            Random rnd = new Random();
            bubblePicture1.Left =rnd.Next(0,bubblePicture1.Width + bubblePicture1.Width);
        }

        private void Form_Load(object sender, EventArgs e)
        {
            gameTimer.Interval = 50;//Tickイベントの発生
            gameTimer.Start();
            ResetUpObject();
        }

        private void BubblePicture_Clik(object sender, EventArgs e)
        {
            
            ResetUpObject();
            counter.Intcrement();
            scoreLabel.Text = counter.Value.ToString(); 
        }

       
    }
}
