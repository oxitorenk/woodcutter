using System;
using UnityEngine;

public class DifficultyHandler : MonoBehaviour
{
    private const int IncreaseValue = 10;
    private const float ProgressValue = 5f;

    private float _threshold = 0.5f;
    public float ProgressBar { get; private set; }
    private float _passedTime;
    private int _difficulty;
    public int Level { get; private set; }

    private void Start()
    {
        GameEvents.OnCutTheLog += MakeProgress;
        
        ProgressBar = 50;
        IncreaseDifficulty();
    }

    private void OnDestroy()
    {
        GameEvents.OnCutTheLog -= MakeProgress;
    }

    private void Update()
    {
        CheckProgress();
    }

    private void MakeProgress()
    {
        ProgressBar += IncreaseValue;
    }

    private void IncreaseDifficulty()
    {
        Level++;
        _difficulty += IncreaseValue;
        
        GameEvents.IncreaseDifficultyMethod(IncreaseValue);
    }

    private void DecreaseDifficulty()
    {
        Level--;

        if (Level < 1)
        {
            GameEvents.GameOverMethod();
        }
        
        _difficulty -= IncreaseValue;
        
        GameEvents.DecreaseDifficultyMethod(IncreaseValue);
    }

    private void CheckProgress()
    {
        _passedTime += Time.deltaTime;

        if (_passedTime >= _threshold)
        {
            ProgressBar -= ProgressValue;
            _passedTime -= _threshold;
        }
        
        if (ProgressBar >= 100)
        {
            IncreaseDifficulty();
            ProgressBar = 10;
        }
        else if (ProgressBar <= 0)
        {
            DecreaseDifficulty();
            ProgressBar = 50;
        }
    }
}
