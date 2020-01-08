﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class PlayerManager
{
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
