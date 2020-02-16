using UnityEngine;

public class Config : MonoBehaviour
{
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        Debug.Log("Loading config..." + Application.dataPath + "/config.ini");
        var ini = new INIParser();
        ini.Open(Application.dataPath + "/config.ini");

        LoadGame(ini);
        LoadPlayer(ini);
        LoadWeapons(ini);

        ini.Close();
        Debug.Log("Done.");
    }

    private void LoadGame(INIParser ini)
    {
        GameConfig.scoreLimit = ini.ReadValue("Game", "ScoreLimit", 5);
        ScoreManager.ScoreLimit = GameConfig.scoreLimit;
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

    private void LoadWeapons(INIParser ini)
    {
        PistolConfig.fireDelay = (float)ini.ReadValue("Weapons", "Pistol_FireDelay", 0.6f);
        PistolConfig.damage = ini.ReadValue("Weapons", "Pistol_Damage", 10);
        PistolConfig.speed = ini.ReadValue("Weapons", "Pistol_Speed", 20);
        PistolConfig.knockbackModifier= ini.ReadValue("Weapons", "Pistol_KnockbackModifier", 7);

        SniperRifleConfig.fireDelay = ini.ReadValue("Weapons", "SniperRifle_FireDelay", 2);
        SniperRifleConfig.damage = ini.ReadValue("Weapons", "SniperRifle_Damage", 25);
        SniperRifleConfig.speed = ini.ReadValue("Weapons", "SniperRifle_Speed", 50);
        SniperRifleConfig.knockbackModifier = ini.ReadValue("Weapons", "SniperRifle_KnockbackModifier", 15);

        RocketLauncherConfig.fireDelay = ini.ReadValue("Weapons", "RocketLauncher_FireDelay", 1);
        RocketLauncherConfig.damage = ini.ReadValue("Weapons", "RocketLauncher_Damage", 30);
        RocketLauncherConfig.speed = ini.ReadValue("Weapons", "RocketLauncher_Speed", 15);
        RocketLauncherConfig.knockbackModifier = ini.ReadValue("Weapons", "RocketLauncher_KnockbackModifier", 750);
        RocketLauncherConfig.explosionRadius = ini.ReadValue("Weapons", "RocketLauncher_ExplosionRadius", 3);
        RocketLauncherConfig.explosionDuration = (float)ini.ReadValue("Weapons", "RocketLauncher_ExplosionDuration", 0.1f);

        SpikesConfig.canDamageRate = (float)ini.ReadValue("Weapons", "Spikes_CanDamageRate", 0.5f);
        SpikesConfig.damage = ini.ReadValue("Weapons", "Spikes_Damage", 15);
        SpikesConfig.knockbackModifier = ini.ReadValue("Weapons", "Spikes_KnockbackModifier", 150);
    }
}
