using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

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
    }
    #endregion

    public GameObject food;
    public List<GameObject> players = new List<GameObject>();
    public List<GameObject> created_food = new List<GameObject>();
    public int max_food = 200;
    public int max_ammo = 200;
    public float create_food_time = 0.03f;
    public float create_enemy_time = 3.0f;
    public float create_ammo_time = 0.03f;
    public Vector2 pos;
    public GameObject enemy;
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
            Vector2 Position = GetRandomValidPositionForEnemy();
            GameObject m = Instantiate(enemy, Position, Quaternion.identity);
            m.transform.localScale = new Vector3((float)0.9,(float) 0.9, 1);
            m.gameObject.SetActive(false);
            AddObject(m, created_enemies);

            Vector2 Position2 = new Vector2(12, 16);
            GameObject m2 = Instantiate(enemy, Position2, Quaternion.identity);
            m2.transform.localScale = new Vector3(2, 2, 1);
            m2.gameObject.SetActive(false);
            AddObject(m2, created_enemies);

            Vector2 Position3 = new Vector2(-11, -13);
            GameObject m3 = Instantiate(enemy, Position3, Quaternion.identity);
            m3.transform.localScale = new Vector3((float)0.9, (float)0.9, 1);
            m3.gameObject.SetActive(false);
            AddObject(m3, created_enemies);


        }
        else
        {
            // Create 5 enemies with random position and size at the beginning
            for (int i = 0; i < 5; i++)
            {
                if (created_enemies.Count < max_enemies)
                {
                    Vector2 Position = GetRandomValidPositionForEnemy();
                    GameObject m = Instantiate(enemy, Position, Quaternion.identity);
                    float randomSize = Random.Range(1.0f, 3.0f);
                    m.transform.localScale = new Vector3(randomSize, randomSize, randomSize);

                    AddObject(m, created_enemies);
                }
            }
        }
        StartCoroutine(CreateFood());
        StartCoroutine(CreateEnemy());
        StartCoroutine(CreateAmmo());
    }

    // If the number of food less than max_food, keep creating food
    public IEnumerator CreateFood()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(create_food_time);
            if (created_food.Count < max_food)
            {
                Vector2 Position = GetRandomFoodPosition();
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
                GameObject m = Instantiate(ammo, Position, Quaternion.identity);
                
            }
        }
    }

    // If the number of enemy less than max_food, keep creating enemies
    public IEnumerator CreateEnemy()
    {
        Physics2D.SyncTransforms();
        while (true)
        {
            yield return new WaitForSecondsRealtime(create_enemy_time);

            if (created_enemies.Count < max_enemies)
            {
                
                Vector2 Position = GetRandomValidPositionForEnemy();
                GameObject m = Instantiate(enemy, Position, Quaternion.identity);
                float randomSize = Random.Range(1.0f, 4.0f);
                

                m.transform.localScale = new Vector3(randomSize, randomSize, randomSize);

                AddObject(m, created_enemies);
            }
        }
    }

    private Vector2 GetRandomFoodPosition()
    {
        if (Random.value < 0.7f)  // 80% chance to be within 15x15
        {
            return new Vector2(Random.Range(-25f, 25f), Random.Range(-25f, 25f));
        }
        else  // 20% chance to be outside 15x15 within 50x50
        {
            Vector2 pos = new Vector2(Random.Range(-48f, 48f), Random.Range(-48f, 48f));
            while (Mathf.Abs(pos.x) <= 25f || Mathf.Abs(pos.y) <= 25f)
            {
                pos = new Vector2(Random.Range(-48f, 48f), Random.Range(-48f, 48f));
            }
            return pos;
        }
    }

    



    private Vector2 GetRandomValidPositionForEnemy()
    {
        Vector2 playerPosition = transform.position; // Get the player's current position
        float minimumDistanceFromPlayer = 10f; // Set minimum distance from player to avoid overlap

        Vector2 spawnPosition;

        if (isTutorial == 1)
        {
            spawnPosition = new Vector2(10, 10);
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

    /*
        public void CreateBullet()
        {
            players[0].GetComponent<PlayerEat>().AddBullet();
        }

        public void DestroyPlayerBullet()
        {
            players[0].GetComponent<PlayerEat>().RemoveBullet();
        }
    */
    // add the gameobject to created_objects
    public void AddObject(GameObject m, List<GameObject> created_objects)
    {
        if (created_objects.Contains(m) == false)
        {
            created_objects.Add(m);
            if (players.Count > 0)
            {
                PlayerEat pp = players[0].GetComponent<PlayerEat>();
                pp.AddObject(m);
            }
        }
    }

    // remove the gameobject in created_objects
    public void RemoveObject(GameObject m, List<GameObject> created_objects)
    {
        if (created_objects.Contains(m) == true)
        {
            created_objects.Remove(m);
            if (players.Count > 0)
            {
                PlayerEat pp = players[0].GetComponent<PlayerEat>();
                pp.RemoveObject(m);
            }
        }
    }

    public void StopGenerating()
    {
        StopAllCoroutines();
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position, pos);
    }
}