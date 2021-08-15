using System;
using UnityEngine;

public class DifficultyHandler : MonoBehaviour
{
    public int Level { get; private set; }
    public float ProgressBar { get; private set; }
    
    private const int ProgressValue = 2;
    private const float Threshold = 0.25f;
    private float _passedTime;
    
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
        ProgressBar += ProgressValue;
    }

    private void IncreaseDifficulty()
    {
        Level++;

        GameEvents.IncreaseDifficultyMethod();
    }

    private void DecreaseDifficulty()
    {
        Level--;

        if (Level < 1)
        {
            GameEvents.GameOverMethod();
        }

        GameEvents.DecreaseDifficultyMethod();
    }

    private void CheckProgress()
    {
        _passedTime += Time.deltaTime;

        if (_passedTime >= Threshold)
        {
            ProgressBar -= ProgressValue;
            _passedTime -= Threshold;
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
