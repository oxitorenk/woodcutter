using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class LogSpawner : MonoBehaviour
{
    [SerializeField] private GameObject normalLogPrefab;
    [SerializeField] private GameObject leftBranchLogPrefab;
    [SerializeField] private GameObject rightBranchLogPrefab;

    public string LeftBranchTag { get; private set; }
    public string RightBranchTag { get; private set; }
    
    private const int MaxBranchChance = 80;
    private const int MinBranchChance = 40;
    private const int BranchChanceIncreaseValue = 10;
    private int _branchChance;

    private void Start()
    {
        GameEvents.OnIncreaseDifficulty += IncreaseBranchChance;
        GameEvents.OnDecreaseDifficulty += DecreaseBranchChange;

        LeftBranchTag = leftBranchLogPrefab.tag;
        RightBranchTag = rightBranchLogPrefab.tag;

        _branchChance = MinBranchChance;
    }

    private void OnDestroy()
    {
        GameEvents.OnIncreaseDifficulty -= IncreaseBranchChance;
        GameEvents.OnDecreaseDifficulty -= DecreaseBranchChange;
    }

    public GameObject GetNormalLog()
    {
        return normalLogPrefab;
    }
    public GameObject GetRandomLog(string logTag)
    {
        var randomValue = Random.Range(1, 101);

        if (randomValue <= _branchChance)
        {
            var randomBranch = Random.Range(0, 2);
            
            return logTag switch
            {
                "Left" => randomBranch == 0 ? leftBranchLogPrefab : normalLogPrefab,
                "Right" => randomBranch == 0 ? rightBranchLogPrefab : normalLogPrefab,
                _ => randomBranch == 0 ? leftBranchLogPrefab : rightBranchLogPrefab
            };
        }

        return normalLogPrefab;
    }
    
    private void IncreaseBranchChance()
    {
        if (_branchChance < MaxBranchChance) 
            _branchChance += BranchChanceIncreaseValue;
    }

    private void DecreaseBranchChange()
    {
        if (_branchChance > MinBranchChance)
            _branchChance -= BranchChanceIncreaseValue;
    }
}
