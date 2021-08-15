using System;
using UnityEngine;

public class DifficultyHandler : MonoBehaviour
{
    public int Level { get; private set; }
    public float ProgressBar { get; private set; }
    
    private const int ProgressValue = 2;
    private const float IncreaseValue = 0.015f;
    private float _threshold = 0.37f;
    private float _passedTime;
    private bool _isGameStarted;
    
    private void Start()
    {
        GameEvents.OnCutTheLog += MakeProgress;
        GameEvents.OnStartGame += StartGame;
        
        ProgressBar = 50;
        IncreaseDifficulty();
    }

    private void OnDestroy()
    {
        GameEvents.OnCutTheLog -= MakeProgress;
        GameEvents.OnStartGame -= StartGame;
    }

    private void Update()
    {
        CheckProgress();
    }

    private void StartGame()
    {
        _isGameStarted = true;
    }

    private void MakeProgress()
    {
        if (!_isGameStarted)
            GameEvents.StartGameMethod();
        
        ProgressBar += ProgressValue;
    }

    private void IncreaseDifficulty()
    {
        Level++;
        _threshold -= IncreaseValue;

        GameEvents.IncreaseDifficultyMethod();
    }

    private void DecreaseDifficulty()
    {
        Level--;
        _threshold += IncreaseValue;

        if (Level < 1)
        {
            GameEvents.GameOverMethod();
        }

        GameEvents.DecreaseDifficultyMethod();
    }

    private void CheckProgress()
    {
        if (_isGameStarted) 
            _passedTime += Time.deltaTime;

        if (_passedTime >= _threshold)
        {
            ProgressBar -= ProgressValue;
            _passedTime -= _threshold;
        }
        
        if (ProgressBar >= 100)
        {
            IncreaseDifficulty();
            ProgressBar = 30;
        }
        else if (ProgressBar <= 0)
        {
            DecreaseDifficulty();
            ProgressBar = 70;
        }
    }
}
