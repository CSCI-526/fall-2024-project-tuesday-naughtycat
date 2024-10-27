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
    }
    #endregion

    public GameObject food;
    public List<GameObject> players = new List<GameObject>();
    public List<GameObject> created_food = new List<GameObject>();
    public int max_food = 1000;
    public float create_food_time = 0.05f;
    public float create_enemy_time = 3.0f;
    public Vector2 pos;
    public GameObject enemy;
    public GameObject healthBarPrefab;
    public int max_enemies = 50;
    public List<GameObject> created_enemies = new List<GameObject>();
    public Vector2 enemy_size_range;
    public float enemy_speed;

    public List<GameObject> created_ammos = new List<GameObject>();
    public List<GameObject> created_bullet = new List<GameObject>();
    public GameObject bullet;
    public float bullet_offset = 1f;
    private void Start()
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
        StartCoroutine(CreateFood());
        StartCoroutine(CreateEnemy());
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
        if (Random.value < 0.85f)  // 80% chance to be within 15x15
        {
            return new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
        }
        else  // 20% chance to be outside 15x15 within 50x50
        {
            Vector2 pos = new Vector2(Random.Range(-25f, 25f), Random.Range(-25f, 25f));
            while (Mathf.Abs(pos.x) <= 5f && Mathf.Abs(pos.y) <= 5f)
            {
                pos = new Vector2(Random.Range(-25f, 25f), Random.Range(-25f, 25f));
            }
            return pos;
        }
    }

    private Vector2 GetRandomValidPositionForEnemy()
    {
        return new Vector2(Random.Range(-50f, 50f), Random.Range(-50f, 50f));
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