using System.Collections.Generic;
using UnityEngine;

public class BodyHandler : MonoBehaviour
{
    [SerializeField] private Transform truckTransform;
    [SerializeField] private GameObject treeLogPrefab;
    [SerializeField] private GameObject leftBranchPrefab;
    [SerializeField] private GameObject rightBranchPrefab;

    [SerializeField] private int treeLogCount;

    private List<GameObject> _treeLogs;

    public int BranchChance { get; set; }

    private void Start()
    {
        BranchChance = 20;
        
        _treeLogs = new List<GameObject>();

        for (var logCount = 0; logCount < treeLogCount; logCount++)
        {
            SpawnNewLog();
        }
    }

    private void Update()
    {
        if (_treeLogs != null && Input.GetKeyDown(KeyCode.Mouse0))
        {
            CutTheLog();
        }
    }

    private void SpawnNewLog()
    {
        _treeLogs.Add(Instantiate(PickRandomLog()));
        var spawnedLog = _treeLogs[_treeLogs.Count - 1];

        spawnedLog.transform.position = SetSpawnPosition(spawnedLog);
    }

    private Vector3 SetSpawnPosition(GameObject treeLog)
    {
        var treeLogIndex = _treeLogs.IndexOf(treeLog);
        
        var spawnPositionX = treeLog.transform.position.x;
        var spawnPositionY = 0f;

        if (treeLogIndex == 0)
            spawnPositionY = truckTransform.position.y + 1;
        else
            spawnPositionY = _treeLogs[treeLogIndex - 1].transform.position.y + 1;

        return new Vector3(spawnPositionX, spawnPositionY);
    }

    private void CutTheLog()
    {
        var bottomLog = _treeLogs[0];
        var bottomLogPosition = bottomLog.transform.position;

        _treeLogs.Remove(bottomLog);
        Destroy(bottomLog);
        
        ShiftRemainingLogs(bottomLogPosition);
        SpawnNewLog();
    }

    private void ShiftRemainingLogs(Vector3 firstPosition)
    {
        var nextLogPosition = firstPosition;

        foreach (var currentLog in _treeLogs)
        {
            var positionForCurrentLog = nextLogPosition;

            nextLogPosition = currentLog.transform.position;
            currentLog.transform.position = positionForCurrentLog;
        }
    }

    private GameObject PickRandomLog()
    {
        var randomLogNumber = Random.Range(0, 100);

        if (randomLogNumber <= BranchChance)
            return Random.Range(0, 2) == 0 ? leftBranchPrefab : rightBranchPrefab;

        return treeLogPrefab;
    }
}
