using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoogleFormManager : MonoBehaviour
{
    private SendToGoogle _googleFormTracker;

    private int totalEnemiesDefeated = 0;
    private int enemiesShot = 0;

    private void Start()
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
        }
    }

    // Call this method when the round ends
    private void EndRound()
    {
        _googleFormTracker.SendMetrics(totalEnemiesDefeated, enemiesShot);
    }
}
