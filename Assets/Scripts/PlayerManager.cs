using System.Linq;
using UnityEngine;

public static class PlayerManager
{
    // Player colors
    public static Color32 PlayerColor1 = new Color32(45, 226, 230, 255);
    public static Color32 PlayerColor2 = new Color32(255, 108, 17, 255);
    public static Color32 PlayerColor3 = new Color32(249, 200, 14, 255);
    public static Color32 PlayerColor4 = new Color32(246, 1, 157, 255);

    public static bool CanControl = false;

    public static bool debug = false;

    public static int PlayerCount { 
        get
        { 
            return Players.Count();
        }
    }

    public static bool[] Players = new bool[] { false, false, false, false };

    public static void AddPlayer(int playerNumber)
    {
        Players[playerNumber - 1] = true;
    }

    public static void RemovePlayer(int playerNumber)
    {
        Players[playerNumber - 1] = false;
    }
}
