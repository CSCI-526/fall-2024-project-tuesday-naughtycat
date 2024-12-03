// using System.Collections.Generic;
// using UnityEngine;

// public class EnemyDistanceChecker : MonoBehaviour
// {
//     public float detectionRange = 15f;
//     public int detectionThreshold = 2;
//     public GameObject wasdImage;
//     public GameObject spacebarImage;

//     private void Update()
//     {
//         DetectEnemies();
//     }

//     public void OnBounceComplete()
//     {
//         Debug.Log("Bounce animation completed!");
//     }


//     void DetectEnemies()
//     {
//         GameObject[] regularEnemies = GameObject.FindGameObjectsWithTag("Enemy");
//         GameObject[] archerEnemies = GameObject.FindGameObjectsWithTag("EnemyArcher");

//         List<GameObject> allEnemies = new List<GameObject>();
//         allEnemies.AddRange(regularEnemies);
//         allEnemies.AddRange(archerEnemies);

//         int closeEnemiesCount = 0;

//         foreach (GameObject enemy in allEnemies)
//         {
//             float distance = Vector3.Distance(transform.position, enemy.transform.position);
//             if (distance <= detectionRange)
//             {
//                 closeEnemiesCount++;
//             }
//         }

//         float playerSize = transform.localScale.x;
//         if (closeEnemiesCount >= detectionThreshold && playerSize >= 1.1f)
//         {
//             wasdImage.SetActive(true);
//             spacebarImage.SetActive(true);
//         }
//         else
//         {
//             wasdImage.SetActive(false);
//             spacebarImage.SetActive(false);
//         }
//     }
// }


using System.Collections.Generic;
using UnityEngine;

public class EnemyDistanceChecker : MonoBehaviour
{
    public float detectionRange = 15f;
    public int detectionThreshold = 2;
    public GameObject wasdImage;
    public GameObject spacebarImage;

    private void Update()
    {
        DetectEnemies();
    }

    public void OnBounceComplete()
    {
        Debug.Log("Bounce animation completed!");
    }

    void DetectEnemies()
    {
        GameObject[] regularEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] archerEnemies = GameObject.FindGameObjectsWithTag("EnemyArcher");

        List<GameObject> allEnemies = new List<GameObject>();
        allEnemies.AddRange(regularEnemies);
        allEnemies.AddRange(archerEnemies);

        int largerEnemiesCount = 0;

        float playerSize = transform.localScale.x;

        foreach (GameObject enemy in allEnemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            float enemySize = enemy.transform.localScale.x;

            // Check if the enemy is within detection range and is larger than the player
            if (distance <= detectionRange && enemySize > playerSize)
            {
                largerEnemiesCount++;
            }
        }

        // Display spacebar only if larger enemies count meets the threshold
        if (largerEnemiesCount >= detectionThreshold)
        {
            spacebarImage.SetActive(true);
        }
        else
        {
            spacebarImage.SetActive(false);
        }

        // WASD image behavior remains unchanged
        wasdImage.SetActive(largerEnemiesCount > 0);
    }
}
