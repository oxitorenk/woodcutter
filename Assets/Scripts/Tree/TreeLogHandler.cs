using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TreeLogHandler : MonoBehaviour
{
    [SerializeField] private Transform truckTransform;
    [SerializeField] private int treeLogCount;
    
    private PlayerHandler _playerHandler;
    private LogSpawner _logSpawner;
    
    private List<GameObject> _treeLogs;

    private void Start()
    {
        _playerHandler = gameObject.GetComponent<PlayerHandler>();
        _logSpawner = gameObject.GetComponent<LogSpawner>();
        
        GameEvents.OnCutTheLog += CutTheLog;

        InitializeFirstTime();
    }

    private void OnDestroy()
    {
        GameEvents.OnCutTheLog -= CutTheLog;
    }

    private void InitializeFirstTime()
    {
        _treeLogs = new List<GameObject>();

        for (var logCount = 0; logCount < treeLogCount; logCount++)
        {
            SpawnNewLog();
        }
    }

    private void SpawnNewLog()
    {
        _treeLogs.Add(Instantiate(_logSpawner.GetRandomLog()));
        
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
        if (_treeLogs == null) return;
        
        var bottomLog = _treeLogs[0];
        
        CheckIsPlayerCollide(bottomLog);
        
        var bottomLogPosition = bottomLog.transform.position;

        _treeLogs.Remove(bottomLog);
        Destroy(bottomLog);
        
        ShiftRemainingLogs(bottomLogPosition);
        SpawnNewLog();
    }

    private void CheckIsPlayerCollide(GameObject bottomLog)
    {
        switch (_playerHandler.PlayerPosition)
        {
            case PlayerHandler.Position.Left when !bottomLog.CompareTag("Left"):
            case PlayerHandler.Position.Right when !bottomLog.CompareTag("Right"):
                return;
            default:
                GameEvents.GameOverMethod();
                break;
        }
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
}
