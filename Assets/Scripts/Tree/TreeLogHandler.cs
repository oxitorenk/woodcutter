using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game_Utility;
using UnityEngine;

namespace Tree
{
    public class TreeLogHandler : MonoBehaviour
    {
        [SerializeField] private Transform trunkTransform;
        [SerializeField] private int treeLogCount;
    
        private PlayerHandler _playerHandler;
        private LogSpawner _logSpawner;
    
        private List<GameObject> _treeLogs;
    
        private const float AnimationDuration = 0.7f;
        private float _horizontalMove;
        private float _zRotate;
        private string _leftBranchTag;
        private string _rightBranchTag;
    
        private void Start()
        {
            _playerHandler = gameObject.GetComponent<PlayerHandler>();
            _logSpawner = gameObject.GetComponent<LogSpawner>();
        
            GameEvents.OnCutTheLog += CutTheLog;
        
            _leftBranchTag = _logSpawner.LeftBranchTag;
            _rightBranchTag = _logSpawner.RightBranchTag;

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
            if (_treeLogs.Count < 3)
            {
                _treeLogs.Add(Instantiate(_logSpawner.GetNormalLog()));
            }
            else
            {
                var previousLogTag = _treeLogs[_treeLogs.Count - 1].tag;
                _treeLogs.Add(Instantiate(_logSpawner.GetRandomLog(previousLogTag)));
            }

            var spawnedLog = _treeLogs[_treeLogs.Count - 1];
            spawnedLog.transform.position = SetSpawnPosition(spawnedLog);
        }

        private Vector3 SetSpawnPosition(GameObject treeLog)
        {
            var treeLogIndex = _treeLogs.IndexOf(treeLog);
        
            var spawnPositionX = trunkTransform.transform.position.x;
            float spawnPositionY;

            if (treeLogIndex == 0)
                spawnPositionY = trunkTransform.position.y + 1.475f;
            else
                spawnPositionY = _treeLogs[treeLogIndex - 1].transform.position.y + 1.945f;

            return new Vector3(spawnPositionX, spawnPositionY);
        }

        private void CutTheLog()
        {
            if (_treeLogs == null) return;
        
            var bottomLog = _treeLogs[0];

            CheckIsPlayerCollide(bottomLog);
        
            _treeLogs.Remove(bottomLog);

            SpawnNewLog();

            StartCoroutine(PlayCutAnimation(bottomLog));
        }

        private IEnumerator PlayCutAnimation(GameObject treeLog)
        {
            var logPosition = treeLog.transform.position;
            var logRotation = treeLog.transform.rotation;

            if (_playerHandler.PlayerPosition == PlayerHandler.Position.Left)
            {
                _horizontalMove = 10;
                _zRotate = 45;
            }
            else
            {
                _horizontalMove = -10;
                _zRotate = -45;
            }

            var move = new Vector3(logPosition.x + _horizontalMove, logPosition.y,
                logPosition.z);
            var rotate = new Vector3(logRotation.x, logRotation.y, logRotation.z + _zRotate);
        
        
            treeLog.transform.DOJump(move, 1f, 1, AnimationDuration); 
            treeLog.transform.DORotate(rotate, AnimationDuration);
            Camera.main.DOShakePosition(0.1f, 0.1f);
            yield return new WaitForSeconds(0.05f);
        
            ShiftRemainingLogs(logPosition);
            yield return new WaitForSeconds(AnimationDuration);
        
            Destroy(treeLog);
        
        }

        private void CheckIsPlayerCollide(GameObject bottomLog)
        {
            switch (_playerHandler.PlayerPosition)
            {
                case PlayerHandler.Position.Left when !bottomLog.CompareTag(_leftBranchTag):
                case PlayerHandler.Position.Right when !bottomLog.CompareTag(_rightBranchTag):
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
}
