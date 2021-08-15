public static class GameEvents
{
    public delegate void IncreaseDifficulty();
    public static event IncreaseDifficulty OnIncreaseDifficulty;
    
    public static void IncreaseDifficultyMethod()
    {
        OnIncreaseDifficulty?.Invoke();
    }

    public delegate void DecreaseDifficulty();
    public static event DecreaseDifficulty OnDecreaseDifficulty;

    public static void DecreaseDifficultyMethod()
    {
        OnDecreaseDifficulty?.Invoke();
    }

    public delegate void CutTheLog();
    public static event CutTheLog OnCutTheLog;

    public static void CutTheLogMethod()
    {
        OnCutTheLog?.Invoke();
    }

    public delegate void GameOver();
    public static event GameOver OnGameOver;

    public static void GameOverMethod()
    {
        OnGameOver?.Invoke();
    }
}
