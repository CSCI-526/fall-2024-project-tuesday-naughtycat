using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyticsManager : MonoBehaviour
{
    private SendToGoogle _googleFormTracker;

    private int totalEnemiesDefeated = 0;
    private int enemiesShot = 0;

    public void Start()
    {
        _googleFormTracker = FindObjectOfType<SendToGoogle>();
    }

    // Call this method when an enemy is defeated
    public void EnemyDefeated(bool wasShot)
    {
        totalEnemiesDefeated++;

        if (wasShot)
        {
            enemiesShot++;
            Debug.Log("Enemy shot! Total enemies shot: " + enemiesShot);
        }

        Debug.Log("Total enemies defeated: " + totalEnemiesDefeated);
    }

    // Call this method when the round ends
    public void EndRound()
    {
        _googleFormTracker.SendMetrics(totalEnemiesDefeated, enemiesShot);
    }
}

