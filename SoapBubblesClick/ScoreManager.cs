using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SoapBubblesClick
{
    public class ScoreManager
    {
        private const string SocoreFilePath = "score.txt"; //スコア保存
        private List<(string Name, int Score)> scores = new List<(string, int)>(); //スコアリスト

        public ScoreManager() 
        {
            LoadScores(); //起動時にスコアを読み込む
        }

        //スコアを読み込む
        private void LoadScores()
        {
            scores.Clear();
            if (File.Exists(SocoreFilePath))
            {
                string[] lines = File.ReadAllLines(SocoreFilePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int score))
                    {
                        scores.Add((parts[0], score));
                    }
                }
                scores = scores.OrderByDescending(s => s.Score).Take(5).ToList(); //上位5位のみ保存
            }
        }

        //スコアを追加
        private void AddScore(string name, int score)
        {
            scores.Add((name, score));
            scores = scores.OrderByDescending(s => s.Score).Take(5).ToList(); //上位5位のみ保存
            SaveScores();
        }

        //スコアを保存する
        private void SaveScores()
        {
            List<string> lines =scores.Select(s => s.Name).ToList();
            File.WriteAllLines(SocoreFilePath, lines);
        }

        //スコアリストを取得
        public List<(string Name, int Score)> GetScores()
        {
            return scores;
        }
    }
}
