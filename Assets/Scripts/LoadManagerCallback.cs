using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadManagerCallback : MonoBehaviour
{
    private bool isFirstUpdate = true;
    
    private void Update()
    {
        if (isFirstUpdate)
        {
            isFirstUpdate = false;
            LoadManager.LoadManagerCallback();
        }
    }
}
