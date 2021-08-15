using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Slider = UnityEngine.UI.Slider;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private Canvas gameOverCanvas;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private Slider progressSlider;

    private ScoreHandler _scoreHandler;
    private DifficultyHandler _difficultyHandler;

    private void Start()
    {
        GameEvents.OnCutTheLog += UpdateScoreText;
        GameEvents.OnGameOver += GameOver;
        GameEvents.OnIncreaseDifficulty += UpdateLevelText;
        GameEvents.OnDecreaseDifficulty += UpdateLevelText;
        
        _scoreHandler = gameObject.GetComponent<ScoreHandler>();
        _difficultyHandler = gameObject.GetComponent<DifficultyHandler>();

        scoreText.text = _scoreHandler.CurrentScore.ToString();
    }

    private void OnDestroy()
    {
        GameEvents.OnCutTheLog -= UpdateScoreText;
        GameEvents.OnGameOver -= GameOver;
        GameEvents.OnIncreaseDifficulty -= UpdateLevelText;
        GameEvents.OnDecreaseDifficulty -= UpdateLevelText;
    }

    private void Update()
    {
        progressSlider.value = _difficultyHandler.ProgressBar;
    }

    private void UpdateScoreText()
    {
        var score = _scoreHandler.CurrentScore.ToString();
        scoreText.text = score;
    }

    private void UpdateLevelText()
    {
        var level = _difficultyHandler.Level.ToString();
        levelText.text = "level " + level;
    }

    private void GameOver()
    {
        var highScore = ScoreHandler.GetHighScore().ToString();
        highScoreText.text = "high score: " + highScore;
        
        gameOverCanvas.enabled = true;
    }

    public void OnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
