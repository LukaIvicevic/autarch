using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class PlayerManager
{
    // Player colors
    public static Color PlayerColor1 = new Color(255, 226, 230, 255);
    public static Color PlayerColor2 = new Color(255, 108, 17, 255);
    public static Color PlayerColor3 = new Color(249, 200, 14, 255);
    public static Color PlayerColor4 = new Color(246, 1, 157, 255);


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
