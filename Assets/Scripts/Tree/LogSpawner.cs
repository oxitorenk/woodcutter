using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogSpawner : MonoBehaviour
{
    [SerializeField] private GameObject normalLogPrefab;
    [SerializeField] private GameObject leftBranchLogPrefab;
    [SerializeField] private GameObject rightBranchLogPrefab;

    private int _branchChance = 20;

    private void Start()
    {
        GameEvents.OnIncreaseDifficulty += IncreaseBranchChance;
        GameEvents.OnDecreaseDifficulty += DecreaseBranchChange;
    }

    public GameObject GetRandomLog()
    {
        var randomValue = Random.Range(1, 101);

        if (randomValue <= _branchChance)
            return Random.Range(0, 2) == 0 ? leftBranchLogPrefab : rightBranchLogPrefab;

        return normalLogPrefab;
    }


    private void IncreaseBranchChance(int increaseValue)
    {
        _branchChance += increaseValue;
    }

    private void DecreaseBranchChange(int decreaseValue)
    {
        _branchChance -= decreaseValue;
    }
}
