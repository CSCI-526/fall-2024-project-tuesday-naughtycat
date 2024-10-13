using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEnemyDir : MonoBehaviour
{
    public float enemy_speed;
    private Vector3 target_position;
    [SerializeField]
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardPlayer();
    }
    // randomly generate the target position for the enemy
    void SetRandomTargetPosition()
    {
        float randomX = Random.Range(-10, 10);
        float randomY = Random.Range(-10, 10);

        target_position = new Vector3(randomX, randomY, transform.position.z); 
    }

    void MoveTowardPlayer()
    {
        if (player != null)
        {
            // 計算敵人到玩家的方向
            Vector3 direction = (player.position - transform.position).normalized;

            // 移動敵人朝向玩家
            transform.position += direction * enemy_speed * Time.deltaTime;

            // 限制敵人到達一定距離後才進行其他行為
            float followDistance = 100f; // 定義跟隨距離
            if (Vector3.Distance(transform.position, player.position) > followDistance)
            {
                // 在一定距離內不進行隨機移動
                SetRandomTargetPosition();
            }
        }
        else
        {
            Debug.LogWarning("Player reference is null in MoveTowardPlayer.");
        }
    }
}
