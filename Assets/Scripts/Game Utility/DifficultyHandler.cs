using UnityEngine;

namespace Game_Utility
{
    public class DifficultyHandler : MonoBehaviour
    {
        public int Level { get; private set; }
        public float ProgressBar { get; private set; }

        private const int ProgressValue = 2;
        private const float IncreaseValue = 0.015f;
        private float _threshold = 0.34f;
        private float _passedTime;
        private bool _isGameStarted;

        private void Start()
        {
            GameEvents.OnStartGame += StartGame;
            GameEvents.OnCutTheLog += MakeProgress;

            ProgressBar = 50;
            IncreaseDifficulty();
        }

        private void OnDestroy()
        {
            GameEvents.OnStartGame -= StartGame;
            GameEvents.OnCutTheLog -= MakeProgress;
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
            if (!_isGameStarted) return;

            ProgressBar += ProgressValue;
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
    }
}
