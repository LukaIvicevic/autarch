
public static class ScoreManager
{
    public static int[] Scores;

    public static int PointsPerKill = 1;

    public static int SuicidePenalty = 1;

    public static int ScoreLimit = 15;

    private static bool scoreLimitReached = false;

    static ScoreManager()
    {
        Initialize();
    }

    public static int GetScore(int playerNumber)
    {
        return Scores[playerNumber - 1];
    }

    public static void Kill(int playerNumber)
    {
        var score = GetScore(playerNumber) + PointsPerKill;
        SetScore(playerNumber, score);
    }

    public static void Suicide(int playerNumber)
    {
        var score = GetScore(playerNumber) - SuicidePenalty;
        SetScore(playerNumber, score);
    }

    public static void Reset()
    {
        for (int i = 0; i < PlayerManager.PlayerCount; i++)
        {
            SetScore(i + 1, 0);
        }
        scoreLimitReached = false;
    }

    private static void Initialize()
    {
        Scores = new int[PlayerManager.PlayerCount];
        for (int i = 0; i < PlayerManager.PlayerCount; i++)
        {
            SetScore(i + 1, 0);
        }
    }

    private static void SetScore(int playerNumber, int score)
    {
        Scores[playerNumber - 1] = score;
        if (GetScore(playerNumber) == ScoreLimit)
        {
            scoreLimitReached = true;
        }
    }
}
