using UnityEngine;

public class Config : MonoBehaviour
{
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        Debug.Log("Config " + Application.persistentDataPath);
        var ini = new INIParser();
        ini.Open(Application.persistentDataPath + "config.txt");

        LoadPlayer(ini);

        ini.Close();
        Debug.Log("Config done");
    }

    private void LoadPlayer(INIParser ini)
    {
        PlayerConfig.maxHealth = ini.ReadValue("Player", "MaxHealth", 100);
        PlayerConfig.respawnTime = ini.ReadValue("Player", "RespawnTime", 5);
        PlayerConfig.jumpForce = ini.ReadValue("Player", "JumpForce", 10);
        PlayerConfig.movementSmoothing = (float)ini.ReadValue("Player", "MovementSmoothing", 0.2f);
        PlayerConfig.airControl = ini.ReadValue("Player", "AirControl", true);
        PlayerConfig.minLightIntensity = ini.ReadValue("Player", "MinLightIntensity", 0);
        PlayerConfig.maxLightIntensity = (float)ini.ReadValue("Player", "MaxLightIntensity", 0.8);
        PlayerConfig.minLightRadius = ini.ReadValue("Player", "MinLightRadius", 1);
        PlayerConfig.maxLightRadius = ini.ReadValue("Player", "MaxLightRadius", 4);
    }
}
