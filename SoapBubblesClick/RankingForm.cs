using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SoapBubblesClick
{
    public partial class RankingForm : Form
    {
        public RankingForm(List<(string Name, int Score)> scores)
        {
            InitializeComponent();
            LoadScores(scores);
        }

        private void LoadScores(List<(string Name, int Score)> scores)
        {
            listBoxRnking.Items.Clear();
            int rank = 1;
            foreach (var (name, score) in scores)
            {
                listBoxRnking.Items.Add($"{rank}位： {name} - {score}点");
                rank++;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close(); //ランキング画面を閉じる
        }
    }
}
