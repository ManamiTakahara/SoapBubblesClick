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
        public Form1()
        {
            InitializeComponent();
            //変数の宣言
            //Bubbleインスタンスの生成
            Bubble bubble = new Bubble();
            //Bubbleの親コンテナはフォームに設定
            bubble.Parent = this;

            //タイマーの設定
            Timer timer = new Timer();
            timer.Interval = 1000;
            //timer.Tick += bubble.MoveEvent;
            timer.Start();

            int score;
            int rimainTime;

            //概要のコメント
            //シャボン玉の動き
            //得点が加算される
            //時間が減る
            //ゲームオーバーの判定

        }
    }
}
