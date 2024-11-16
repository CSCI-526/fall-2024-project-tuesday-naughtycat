using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeEnemyDir : MonoBehaviour
{
    public float enemy_speed;
    private Vector3 target_position;
    [SerializeField]
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name.CompareTo("TutorialScene") == 0)
        {
            enemy_speed = 2f;
        }
        else if (FindObjectOfType<ObjectGenerator>().currentWave == 1)
        {
            enemy_speed = Random.Range(3.0f, 7.0f);
            
        }
        else if (FindObjectOfType<ObjectGenerator>().currentWave == 2)
        {
            enemy_speed = Random.Range(5.0f, 9.0f);
            
        }
        else if (FindObjectOfType<ObjectGenerator>().currentWave == 3)
        {
            enemy_speed = Random.Range(6.0f, 10.0f);
            
        }
        else
        {
            enemy_speed = 4f;
        }

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
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
           
            Vector2 direction = (player.position - transform.position).normalized;


            //transform.position += direction * enemy_speed * Time.deltaTime;

            
            GetComponent<Rigidbody2D>().position += direction * enemy_speed * Time.deltaTime;

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
