using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Slider = UnityEngine.UI.Slider;
using DG.Tweening;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private Canvas gameOverCanvas;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private Slider progressSlider;

    private ScoreHandler _scoreHandler;
    private DifficultyHandler _difficultyHandler;

    private const float FadeDuration = 2f;
    private const float MoveDuration = 1f;
    private const float MoveDistance = 0.9f;
    
    private Vector3 _levelTextOriginalPos;
    private Vector3 _levelTextShowUpPos;

    private void Start()
    {
        GameEvents.OnCutTheLog += UpdateScoreText;
        GameEvents.OnGameOver += GameOver;
        GameEvents.OnIncreaseDifficulty += UpdateLevelText;
        GameEvents.OnDecreaseDifficulty += UpdateLevelText;
        
        _scoreHandler = gameObject.GetComponent<ScoreHandler>();
        _difficultyHandler = gameObject.GetComponent<DifficultyHandler>();

        _levelTextOriginalPos = levelText.transform.position;
        _levelTextShowUpPos = new Vector3(_levelTextOriginalPos.x, _levelTextOriginalPos.y - MoveDistance, _levelTextOriginalPos.z);

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

        StartCoroutine(PlayLevelUpAnimation());

    }

    private IEnumerator PlayLevelUpAnimation()
    {

        levelText.rectTransform.DOMove(_levelTextShowUpPos, MoveDuration);
        levelText.DOFade(1, FadeDuration);
        yield return new WaitForSeconds(FadeDuration);

        levelText.DOFade(0, FadeDuration);
        yield return new WaitForSeconds(FadeDuration / 2);
        
        levelText.rectTransform.DOMove(_levelTextOriginalPos, MoveDuration);
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
