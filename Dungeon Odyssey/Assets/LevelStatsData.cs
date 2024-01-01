using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelStatsData 
{
    public int enemiesKilled, xpGained, levelsAquired, totalDamage, damageTaken, currentXP, currentLevel, currentSkillPoints;

    public LevelStatsData ()
    {
        if (StatsHandler.Instance != null)
        {
            enemiesKilled = StatsHandler.Instance.enemiesKilled;
            xpGained = StatsHandler.Instance.xpGained;
            levelsAquired = StatsHandler.Instance.levelsAquired;
            totalDamage = StatsHandler.Instance.totalDamage;
            damageTaken = StatsHandler.Instance.damageTaken;
            currentXP = StatsHandler.Instance.currentXP;
            currentLevel = StatsHandler.Instance.currentPlayerLevel;
            currentSkillPoints = StatsHandler.Instance.skillPoints;
        }
    }

    public LevelStatsData (int currentXP, int currentLevel, int currentSkillPoints)
    {
        if (StatsHandler.Instance != null)
        {

            StatsHandler.Instance.currentXP = currentXP;
            StatsHandler.Instance.currentPlayerLevel = currentLevel;
            StatsHandler.Instance.skillPoints = currentSkillPoints;
        }
    }
}
