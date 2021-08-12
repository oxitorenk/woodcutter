using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyHandler : MonoBehaviour
{
    [SerializeField] private BodyHandler bodyHandler;
    
    private const int IncreaseValue = 10;
    private int _difficulty;
    private void Start()
    {
        IncreaseDifficulty();
    }

    private void IncreaseDifficulty()
    {
        _difficulty += IncreaseValue;
        bodyHandler.BranchChance += _difficulty;
    }
}
