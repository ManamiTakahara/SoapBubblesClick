using System.Collections.Generic;
using System.IO;

namespace SoapBubblesClick
{
    // スコア管理クラス
    public class ScoreManager
    {
        private const string filePath = "score.csv"; //スコア保存
        private List<(string Name, int Score)> scores = new List<(string name, int score)>(); //スコアリスト

        public ScoreManager() 
        {
            LoadScores(); //起動時にスコアを読み込む
        }

        //スコアを読み込む
        private void LoadScores()
        {
            scores.Clear();
            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length == 2 && int.TryParse(parts[1], out int score))
                        {
                            scores.Add((parts[0], score));
                        }
                    }
                }
                scores.Sort((a, b) => b.Score.CompareTo(a.Score)); // 降順ソート
            }
        }

        //スコアを追加
        public void AddScore(string name, int score)
        {
            scores.Add((name, score));
            scores.Sort((a, b) => b.Score.CompareTo(a.Score)); // 降順ソート
            if (scores.Count > 5) scores = scores.GetRange(0, 5); // 上位5つのみ保持
            SaveScores();
        }

        //スコアを保存する
        private void SaveScores()
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var entry in scores)
                {
                    writer.WriteLine($"{entry.Name},{entry.Score}");
                }
            }
        }

        //スコアリストを取得
        public List<(string Name, int Score)> GetScores()
        {
            return new List<(string Name, int Score)>(scores); // コピーを返す
        }

    }
}
