using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private Canvas gameOverCanvas;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highScoreText;

    private ScoreHandler _scoreHandler;

    private void Start()
    {
        GameEvents.OnCutTheLog += UpdateScoreText;
        GameEvents.OnGameOver += GameOver;
        
        _scoreHandler = gameObject.GetComponent<ScoreHandler>();
        scoreText.text = _scoreHandler.CurrentScore.ToString();
    }

    private void UpdateScoreText()
    {
        var highScore = _scoreHandler.CurrentScore.ToString();
        highScoreText.text = "high score: " + highScore;
    }

    private void GameOver()
    {
        highScoreText.text = ScoreHandler.GetHighScore().ToString();
        gameOverCanvas.enabled = true;
    }
}
