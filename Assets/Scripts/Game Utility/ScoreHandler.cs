using System;
using System.IO;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    private static string _dataPath;

    public int CurrentScore { get; private set; }

    private void Start()
    {
        _dataPath = Application.persistentDataPath + "ScoreData.json";
        
        GameEvents.OnCutTheLog += IncreaseScore;
        GameEvents.OnGameOver += SaveScore;

        if (!File.Exists(_dataPath))
        {
            InitializeFirstTime();
        }
    }
    
    private void IncreaseScore()
    {
        CurrentScore++;
    }
    
    private void SaveScore()
    {
        var highScore = GetHighScore();

        if (CurrentScore < highScore) return;

        var scoreData = new Score {score = CurrentScore};
        var scoreJson = JsonUtility.ToJson(scoreData);
        
        File.WriteAllText(_dataPath, scoreJson);
    }
    
    public static int GetHighScore()
    {
        var scoreJson = File.ReadAllText(_dataPath);
        var scoreData = JsonUtility.FromJson<Score>(scoreJson);

        return scoreData.score;
    }

    private static void InitializeFirstTime()
    {
        var scoreData = new Score {score = 0};
        var scoreJson = JsonUtility.ToJson(scoreData);
        
        File.WriteAllText(_dataPath, scoreJson);
    }

    [Serializable]
    public struct Score
    {
        public int score;
    }
}
