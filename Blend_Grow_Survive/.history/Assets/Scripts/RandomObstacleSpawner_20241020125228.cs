using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;  // Reference to the obstacle prefab
    public int numberOfObstacles = 5;  // Number of obstacles to spawn
    public Vector2 obstacleSize = new Vector2(3, 5);  // Size of the obstacles (3x5)
    public Vector2 mapBounds = new Vector2(25, 25);  // Map size bounds for random positions

    private List<GameObject> spawnedObstacles = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        SpawnObstacles();
    }

    void SpawnObstacles()
    {
        for (int i = 0; i < numberOfObstacles; i++)
        {
            // Randomly generate position within the map bounds
            float randomX = Random.Range(-mapBounds.x + obstacleSize.x / 2, mapBounds.x - obstacleSize.x / 2);
            float randomY = Random.Range(-mapBounds.y + obstacleSize.y / 2, mapBounds.y - obstacleSize.y / 2);

            // Create the position for the obstacle
            Vector3 randomPosition = new Vector3(randomX, randomY, 0);

            // Instantiate the obstacle at the random position
            GameObject newObstacle = Instantiate(obstaclePrefab, randomPosition, Quaternion.identity);

            // Set the obstacle size to 3x5
            newObstacle.transform.localScale = new Vector3(obstacleSize.x, obstacleSize.y, 1);

            // Add the obstacle to the list
            spawnedObstacles.Add(newObstacle);
        }
    }

    // Optionally, you can add a function to clear the obstacles if needed
    void ClearObstacles()
    {
        foreach (GameObject obstacle in spawnedObstacles)
        {
            Destroy(obstacle);
        }
        spawnedObstacles.Clear();
    }
}
