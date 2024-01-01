using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsWindow : MonoBehaviour
{
    public TextMeshProUGUI totalEnemies, damageDealt, healthLost, XPGained, levelsGained;
    private LevelStatsData data;

    // Start is called before the first frame update
    void Start()
    {
        data = SaveAndLoad.LoadStats();
        totalEnemies.text = ("Enemies Killed: " + data.enemiesKilled);
        damageDealt.text = ("Damage Dealt: " + data.totalDamage);
        healthLost.text = ("Health Lost: " + data.damageTaken);
        XPGained.text = ("XP Gained: " + data.xpGained);
        levelsGained.text = ("Levels Gained: " + data.levelsAquired);
    }

    

    public void SetStatsBoolFalse()
    {
        PlayerPrefs.SetInt("StatsWindowBool", 0);
    }
}
