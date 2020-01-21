using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreManager
{
    public static int[] Scores = new int[] { 0, 0, 0, 0 };

    public static int PointsPerKill = 1;

    public static int SuicidePenalty = 1;

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

    private static void SetScore(int playerNumber, int score)
    {
        Scores[playerNumber - 1] = score;
    }
}
