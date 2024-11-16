using UnityEngine;

public class AnalyticsManager : MonoBehaviour
{
    private int totalEnemiesDefeated = 0;
    private int enemiesShot = 0;
    private int enemiesSwallow = 0;
    private int escape = 0;
    float survivalTime
    bool isWin

    private SendToGoogle googleFormTracker;

    void Start()
    {
        googleFormTracker = FindObjectOfType<SendToGoogle>();
    }

    // Called when an enemy is shot for the first time
    public void EnemyShot()
    {
        enemiesShot++;
        //Debug.Log("Enemy shot! Total enemies shot: " + enemiesShot);
    }

    public void EnemySwallow()
    {
        enemiesSwallow++;
        //Debug.Log("Enemy shot! Total enemies shot: " + enemiesShot);
    }

    // Called when an enemy is defeated
    public void EnemyDefeated()
    {
        totalEnemiesDefeated++;
        //Debug.Log("Enemy defeated! Total enemies defeated: " + totalEnemiesDefeated);
    }

    public void Escape()
    {
        escape++;
    }

    // Called when the round ends
    public void EndRound()
    {
        //Debug.Log("Round ended. Total enemies defeated: " + totalEnemiesDefeated + ", Total enemies shot: " + enemiesShot);

        // Send the collected data
        googleFormTracker.SendMetrics(totalEnemiesDefeated, enemiesShot, enemiesSwallow, escape, GameManager.instance, survivalTime, isWin);
    }
}
