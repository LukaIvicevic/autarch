
public static class ScoreManager
{
    public static int[] Scores;

    public static int PointsPerKill = 1;

    public static int SuicidePenalty = 1;

    public static int ScoreLimit = 1;

    public static bool ScoreLimitReached = false;

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
        if (!ScoreLimitReached)
        {
            var score = GetScore(playerNumber) + PointsPerKill;
            SetScore(playerNumber, score);
        }
    }

    public static void Suicide(int playerNumber)
    {
        if (!ScoreLimitReached)
        {
            var score = GetScore(playerNumber) - SuicidePenalty;
            SetScore(playerNumber, score);
        }
    }

    public static void Reset()
    {
        for (int i = 0; i < PlayerManager.PlayerCount; i++)
        {
            SetScore(i + 1, 0);
        }
        ScoreLimitReached = false;
    }

    public static void Initialize()
    {
        ScoreLimitReached = false;
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
            ScoreLimitReached = true;
        }
    }
}
