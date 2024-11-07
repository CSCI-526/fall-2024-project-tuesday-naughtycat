using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectGenerator : MonoBehaviour
{
    #region instance 
    public static ObjectGenerator ins;

    private void Awake()
    {
        if (ins == null)
        {
            ins = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public GameObject food;
    public List<GameObject> players = new List<GameObject>();
    public List<GameObject> created_food = new List<GameObject>();
    public int max_food = 100;
    public int max_ammo = 15;
    public float create_food_time = 0.1f;
    public float create_enemy_time = 5.0f;
    public float create_ammo_time = 2f;
    public Vector2 pos;
    public GameObject enemy;
    public GameObject enemyArcherPrefab; // Assign the EnemyArcher prefab in the Inspector
    public GameObject healthBarPrefab;
    public GameObject ammo;
    public int max_enemies = 50;
    public List<GameObject> created_enemies = new List<GameObject>();
    public Vector2 enemy_size_range;
    public float enemy_speed;

    public List<GameObject> created_ammos = new List<GameObject>();
    public List<GameObject> created_bullet = new List<GameObject>();
    public GameObject bullet;
    public float bullet_offset = 1f;

    public int isTutorial;

    // Wave and level properties
    public int currentWave = 1;
    public List<WaveDefinition> waveDefinitions = new List<WaveDefinition>();
    private int currentWaveEnemies = 0;
    private int enemiesPerWave = 10;
    private bool isWaveActive = false;
    private PlayerEat bosscaller;

    public TextMeshProUGUI levelText;

    // Define enemy size ranges per level
    private Vector2 level1SizeRange = new Vector2(0.1f, 0.3f);
    private Vector2 level2SizeRange = new Vector2(0.4f, 0.7f);

    // Created to access the isWaveActive variable without allowing to change it
    public bool IsWaveActive
    {
        get { return isWaveActive; }
    }

    public List<GameObject> getEnemy()
    {
        return created_enemies;
    }

    public GameObject[] getEnemyArr()
    {
        GameObject[] aaa = new GameObject[created_enemies.Count];
        for (int i = 0; i < created_enemies.Count; i++)
        {
            aaa[i] = created_enemies[i];
        }
        return aaa;
    }

    public GameObject[] getAmmoArr()
    {
        GameObject[] aaa = new GameObject[created_ammos.Count];
        for (int i = 0; i < created_ammos.Count; i++)
        {
            aaa[i] = created_ammos[i];
        }
        return aaa;
    }

    public void changeMaxEnemy(int maxNum)
    {
        max_enemies = maxNum;
    }

    public int getMaxEnemy()
    {
        return max_enemies;
    }

    private void Start()
    {
        if (isTutorial == 1) // tutorial mode
        {
            // [Tutorial spawning logic...]
            // (Omitted for brevity)
        }
        else
        {
            // Starting the first level wave
            StartNextWave();
        }
        StartCoroutine(CreateFood());
        StartCoroutine(CreateEnemy());
        StartCoroutine(CreateAmmo());
    }

    // If the number of food is less than max_food, keep creating food
    public IEnumerator CreateFood()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(create_food_time);
            if (created_food.Count < max_food)
            {
                Vector2 Position = GetRandomFoodPosition();
                Collider2D hit = Physics2D.OverlapCircle(Position, 0.5f);
                while (hit != null)
                {
                    Position = GetRandomFoodPosition();
                    hit = Physics2D.OverlapCircle(Position, 0.5f);
                }

                GameObject m = Instantiate(food, Position, Quaternion.identity);
                AddObject(m, created_food);
            }
        }
    }

    public IEnumerator CreateAmmo()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(create_ammo_time);
            if (created_ammos.Count < max_ammo)
            {
                Vector2 Position = new Vector2(Random.Range(-30f, 30f), Random.Range(-30f, 30f));
                Collider2D hit = Physics2D.OverlapCircle(Position, 0.5f);
                while (hit != null)
                {
                    Position = new Vector2(Random.Range(-30f, 30f), Random.Range(-30f, 30f));
                    hit = Physics2D.OverlapCircle(Position, 0.5f);
                }

                GameObject c = Instantiate(ammo, Position, Quaternion.identity);
                AddObject(c, created_ammos); // If you have an AddObject method for ammo
            }
        }
    }

    // Modified CreateEnemy coroutine to handle multiple enemy types per wave
    public IEnumerator CreateEnemy()
    {
        Physics2D.SyncTransforms();
        while (true)
        {
            yield return new WaitForSecondsRealtime(create_enemy_time);

            // Only create enemies if the wave is active and has not reached its limit
            if (isWaveActive && created_enemies.Count < max_enemies)
            {
                // Get the current wave definition
                WaveDefinition currentWaveDef = waveDefinitions.Find(w => w.waveNumber == currentWave);
                if (currentWaveDef == null)
                {
                    Debug.LogError($"No WaveDefinition found for wave {currentWave}");
                    yield break;
                }

                // Determine which type of enemy to spawn
                GameObject enemyToSpawn = enemy; // Default to normal enemy

                int totalEnemiesToSpawn = currentWaveDef.normalEnemies + currentWaveDef.archerEnemies;

                if (currentWaveEnemies < totalEnemiesToSpawn)
                {
                    if (currentWaveEnemies < currentWaveDef.normalEnemies)
                    {
                        enemyToSpawn = enemy; // Normal enemy
                    }
                    else
                    {
                        enemyToSpawn = enemyArcherPrefab; // Archer enemy
                    }
                }
                else
                {
                    // All enemies for this wave have been spawned
                    isWaveActive = false;
                    Debug.Log("Reached maximum enemies for Wave " + currentWave);
                    continue;
                }

                // Spawn the enemy
                Vector2 Position = GetRandomValidPositionForEnemy();
                GameObject m = Instantiate(enemyToSpawn, Position, Quaternion.identity);

                // Set enemy size according to the wave level it currently is
                float randomSize;
                if (currentWave == 1)
                {
                    randomSize = Random.Range(level1SizeRange.x, level1SizeRange.y);
                    Debug.Log("Spawning Enemies of size range " + level1SizeRange.x + " to " + level1SizeRange.y);
                }
                else if (currentWave == 2)
                {
                    randomSize = Random.Range(level2SizeRange.x, level2SizeRange.y);
                    Debug.Log("Spawning Enemies of size range " + level2SizeRange.x + " to " + level2SizeRange.y);
                }
                else
                {
                    randomSize = Random.Range(0.7f, 0.9f); // Default if levels go beyond 2
                    Debug.Log("Now enemies will only be of size 4-7");
                }

                m.transform.localScale = new Vector3(randomSize, randomSize, 1);
                AddObject(m, created_enemies);
                currentWaveEnemies++;

                Debug.Log($"Spawned Enemy {currentWaveEnemies} ({enemyToSpawn.name}) for Wave {currentWave}");

                // Stop spawning if we reached the limit for the current wave
                if (currentWaveEnemies >= totalEnemiesToSpawn)
                {
                    isWaveActive = false;
                    Debug.Log("Reached maximum enemies for Wave " + currentWave);
                }
            }
        }
    }

    public void StartNextWave()
    {
        // Check if there's a definition for the current wave
        WaveDefinition currentWaveDef = waveDefinitions.Find(w => w.waveNumber == currentWave);
        if (currentWaveDef == null)
        {
            Debug.LogError($"No WaveDefinition found for wave {currentWave}");
            return;
        }

        currentWaveEnemies = 0;
        isWaveActive = true;
        Debug.Log("Starting Wave " + currentWave);
        levelText = GameObject.Find("Level").GetComponent<TextMeshProUGUI>();
        if (currentWave < 3)
        {
            levelText.text = "Level " + currentWave.ToString();
        }
        else
        {
            levelText.text = "Boss!";
        }
        StartCoroutine(HideLevelTextAfterDelay(1f));
    }

    IEnumerator HideLevelTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        levelText.enabled = false; // Disable the text component
        // isWaveActive is already set to true in StartNextWave
    }

    //// Called when all enemies in the wave are cleared
    //public void OnWaveCleared()
    //{
    //    Debug.Log("Wave " + currentWave + " Cleared!");

    //    // Increment the wave number
    //    currentWave++;

    //    // Check if there's a definition for the next wave
    //    WaveDefinition nextWaveDef = waveDefinitions.Find(w => w.waveNumber == currentWave);
    //    if (nextWaveDef != null)
    //    {
    //        StartNextWave(); // Start the next wave with defined enemies
    //    }
    //    else
    //    {
    //        Debug.Log("No more waves defined. Proceeding to Boss!");
    //        StartBossWave(); // Implement your boss wave logic here
    //    }
    //}
    public void OnWaveCleared()
    {
        Debug.Log("Wave " + currentWave + " Cleared!");

        // Increment the wave number
        currentWave++;

        // Check if there's a definition for the next wave
        WaveDefinition nextWaveDef = waveDefinitions.Find(w => w.waveNumber == currentWave);
        if (nextWaveDef != null)
        {
            StartNextWave(); // Start the next wave with defined enemies
        }
        else
        {
            Debug.Log("No more waves defined. Proceeding to Boss!");
            StartBossWave(); // Implement your boss wave logic here
        }
    }


    private void StartBossWave()
    {
        // Implement your boss activation logic here
        bosscaller = FindObjectOfType<PlayerEat>();
        bosscaller.ActivateBoss();
    }

    private Vector2 GetRandomFoodPosition()
    {
        if (Random.value < 0.7f)  // 70% chance to be within 25x25
        {
            return new Vector2(Random.Range(-25f, 25f), Random.Range(-25f, 25f));
        }
        else  // 30% chance to be outside 25x25 within 50x50
        {
            Vector2 pos;
            do
            {
                pos = new Vector2(Random.Range(-48f, 48f), Random.Range(-48f, 48f));
            } while (Mathf.Abs(pos.x) <= 25f && Mathf.Abs(pos.y) <= 25f);
            return pos;
        }
    }

    private Vector2 GetRandomValidPositionForEnemy()
    {
        Vector2 playerPosition = transform.position; // Get the player's current position
        float minimumDistanceFromPlayer = 30f; // Set minimum distance from player to avoid overlap

        Vector2 spawnPosition;

        if (isTutorial == 1)
        {
            spawnPosition = new Vector2(15, 15);
        }
        else
        {
            do
            {
                spawnPosition = new Vector2(Random.Range(-50f, 50f), Random.Range(-50f, 50f));
            } while (Vector2.Distance(spawnPosition, playerPosition) < minimumDistanceFromPlayer);
        }

        return spawnPosition;
    }

    // Add the gameobject to created_objects and notify the player
    public void AddObject(GameObject m, List<GameObject> created_objects)
    {
        if (!created_objects.Contains(m))
        {
            created_objects.Add(m);
            if (players.Count > 0)
            {
                PlayerEat pp = players[0].GetComponent<PlayerEat>();
                //pp.AddObject(m);
            }
        }
    }

    // Remove the gameobject from created_objects
    //public void RemoveObject(GameObject m, List<GameObject> created_objects)
    //{
    //    if (created_objects.Contains(m))
    //    {
    //        created_objects.Remove(m);
    //        if (players.Count > 0)
    //        {
    //            PlayerEat pp = players[0].GetComponent<PlayerEat>();
    //            //pp.RemoveObject(m);
    //        }

    //        // Check if the wave is cleared after an enemy is removed
    //        WaveDefinition currentWaveDef = waveDefinitions.Find(w => w.waveNumber == currentWave);
    //        if (currentWaveDef == null)
    //        {
    //            Debug.LogError($"No WaveDefinition found for wave {currentWave}");
    //            return;
    //        }

    //        if (created_enemies.Count == 0 && currentWaveEnemies >= (currentWaveDef.normalEnemies + currentWaveDef.archerEnemies))
    //        {
    //            OnWaveCleared();
    //        }
    //    }
    //}

    public void RemoveObject(GameObject m, List<GameObject> created_objects)
    {
        if (created_objects.Contains(m))
        {
            created_objects.Remove(m);
            if (players.Count > 0)
            {
                PlayerEat pp = players[0].GetComponent<PlayerEat>();
                //pp.RemoveObject(m);
            }

            // Check if the wave is cleared after an enemy is removed
            if (created_enemies.Count == 0 && !isWaveActive && currentWaveEnemies >= enemiesPerWave)
            {
                OnWaveCleared();
            }
        }
    }


    public void StopGenerating()
    {
        StopAllCoroutines();
    }

    public void StartGenerating()
    {
        StartCoroutine(CreateFood());
        StartCoroutine(CreateEnemy());
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position, pos);
    }
}
