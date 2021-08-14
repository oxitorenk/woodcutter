public static class GameEvents
{
    public delegate void IncreaseDifficulty(int increaseValue);
    public static event IncreaseDifficulty OnIncreaseDifficulty;
    
    public static void IncreaseDifficultyMethod(int increaseValue)
    {
        OnIncreaseDifficulty?.Invoke(increaseValue);
    }

    public delegate void DecreaseDifficulty(int decreaseValue);
    public static event DecreaseDifficulty OnDecreaseDifficulty;

    public static void DecreaseDifficultyMethod(int decreaseValue)
    {
        OnDecreaseDifficulty?.Invoke(decreaseValue);
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
