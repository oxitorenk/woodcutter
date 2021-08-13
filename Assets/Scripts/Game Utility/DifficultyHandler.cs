using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyHandler : MonoBehaviour
{
    private const int IncreaseValue = 10;
    private const float ProgressValue = 5f;

    private float _threshold = 0.5f;
    private float _progressBar;
    private float _passedTime;
    private int _difficulty;
    private int _level;
    
    private void Start()
    {
        _progressBar = 50;
        IncreaseDifficulty();
    }

    private void Update()
    {
        _passedTime += Time.deltaTime;

        if (_passedTime >= _threshold)
        {
            _progressBar -= ProgressValue;
            _passedTime -= _threshold;
        }

        CheckProgress();
    }

    public void MakeProgress()
    {
        _progressBar += IncreaseValue;
    }

    private void IncreaseDifficulty()
    {
        _level++;
        _difficulty += IncreaseValue;
        
        GameEvents.IncreaseDifficultyMethod(IncreaseValue);
    }

    private void DecreaseDifficulty()
    {
        _level--;

        if (_level < 1)
        {
            GameEvents.GameOverMethod();
        }
        
        _difficulty -= IncreaseValue;
        
        GameEvents.DecreaseDifficultyMethod(IncreaseValue);
    }

    private void CheckProgress()
    {
        if (_progressBar >= 100)
        {
            IncreaseDifficulty();
            _progressBar = 10;
        }
        else if (_progressBar <= 0)
        {
            DecreaseDifficulty();
            _progressBar = 50;
        }
    }
}
