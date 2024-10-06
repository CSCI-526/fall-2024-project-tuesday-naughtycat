using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEnemyDir : MonoBehaviour
{
    public float enemy_speed;
    private Vector3 target_position;
    // Start is called before the first frame update
    void Start()
    {
        SetRandomTargetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        // move the enemy to the target position, if it reaches the target position, set the next target position
        transform.position = Vector3.MoveTowards(transform.position, target_position, enemy_speed* Time.deltaTime);
        if (Vector3.Distance(transform.position, target_position) < 0.1f)
        {
            SetRandomTargetPosition();
        }
    }
    // randomly generate the target position for the enemy
    void SetRandomTargetPosition()
    {
        float randomX = Random.Range(-10, 10);
        float randomY = Random.Range(-10, 10);

        target_position = new Vector3(randomX, randomY, transform.position.z); 
    }
}
