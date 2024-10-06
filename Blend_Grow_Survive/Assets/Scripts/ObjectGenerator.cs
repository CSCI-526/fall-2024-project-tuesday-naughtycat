using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public int max_food = 50;
    public float create_food_time = 0.5f;
    public float create_enemy_time = 5.0f;
    public Vector2 pos;
    public GameObject enemy;
    public int max_enemies = 10;
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
                Vector2 Position = GetRandomValidPosition();
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
                Vector2 Position = GetRandomValidPosition();
                GameObject m = Instantiate(food, Position, Quaternion.identity);
                AddObject(m, created_food);
            }
        }
    }

    // If the number of enemy less than max_food, keep creating enemies
    public IEnumerator CreateEnemy()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(create_enemy_time);

            if (created_enemies.Count < max_enemies)
            {
                Vector2 Position = GetRandomValidPosition();
                GameObject m = Instantiate(enemy, Position, Quaternion.identity);
                float randomSize = Random.Range(1.0f, 3.0f);
                m.transform.localScale = new Vector3(randomSize, randomSize, randomSize);

                AddObject(m, created_enemies);
            }
        }
    }

    // Get a random position that is not too close to the player
    public Vector2 GetRandomValidPosition()
    {
        Vector2 Position = Vector2.zero;
        bool validPosition = false;

        Vector2 playerPosition = transform.position;

        while (!validPosition)
        {
            Position = new Vector2(Random.Range(-20, 20), Random.Range(-20, 20)) / 2;

            if (Vector2.Distance(Position, playerPosition) > 2f)
            {
                validPosition = true;
            }
        }
        return Position;
            }

    public void CreateBullet()
    {
        players[0].GetComponent<PlayerEat>().AddBullet();
        }

    public void DestroyPlayerBullet()
    {
        players[0].GetComponent<PlayerEat>().RemoveBullet();
    }

    // add the gameobject to created_objects
    public void AddObject(GameObject m, List<GameObject> created_objects)
    {
        if (created_objects.Contains(m) == false)
        {
            created_objects.Add(m);
            for (int i = 0; i < players.Count; i++)
            {
                PlayerEat pp = players[i].GetComponent<PlayerEat>();
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
            for (int i = 0; i < players.Count; i++)
            {
                PlayerEat pp = players[i].GetComponent<PlayerEat>();
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