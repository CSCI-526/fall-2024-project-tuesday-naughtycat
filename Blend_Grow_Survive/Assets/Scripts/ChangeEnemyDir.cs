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
           
            Vector3 direction = (player.position - transform.position).normalized;

            
            transform.position += direction * enemy_speed * Time.deltaTime;

            
            float followDistance = 100f; 
            if (Vector3.Distance(transform.position, player.position) > followDistance)
            {
              
                SetRandomTargetPosition();
            }
        }
        else
        {
            Debug.LogWarning("Player reference is null in MoveTowardPlayer.");
        }
    }
}
