using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace SoapBubblesClick
{
    //Bubbleのクラス
    internal class Bubble : PictureBox
    {
        //コンストラクタで初期設定
        public Bubble()
        {
            //ここで画像やそのほかの初期設定を行います
            BackColor = System.Drawing.Color.AliceBlue;
        }

        //動きの処理
        public void Move()
        {
            //PicthreBoxの座標（Topプロパティ）を更新
            Top += 10;
        }

        //イベントハンドラとして使用するためのラッパーメソッド
        internal void MoveEvent(object sender, MouseEventArgs e)
        {
            Move();
        }
    }
}
