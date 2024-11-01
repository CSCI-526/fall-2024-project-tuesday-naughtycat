// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Actions : MonoBehaviour
// {
//     public GameObject food;
//     public GameObject throw_circle;
//     public Transform position;
//     public float shrink_rate = 0.1f;
//     PlayerEat player_eat;
//     ObjectGenerator generator;

//     //create black circle(throw)
//     //increase the players speed and decrease the size of the player
//     public void PlayerThrow()
//     {
//         if (transform.localScale.x < 1)
//         {
//             return;
//         }
//         Vector2 Direction = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
//         float Z_Rotation = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg + 90f;
//         transform.rotation = Quaternion.Euler(0, 0, Z_Rotation);
//         GameObject b = Instantiate(throw_circle, position.position, Quaternion.identity);
//         b.GetComponent<ObjectForce>().ApplyForce = true;
//         transform.localScale -= new Vector3(0.05f, 0.05f, 0.05f);
//     }

//     // Start is called before the first frame update
//     void Start()
//     {
//         player_eat = GetComponent<PlayerEat>();
//         generator = ObjectGenerator.ins;
//     }

//     // Update is called once per frame
//     void LateUpdate()
//     {
//         if (transform.localScale.x <= 1)
//         {
//             return;
//         }
//         //decrease the size of the player after each frame
//         transform.localScale -= new Vector3(shrink_rate, shrink_rate, shrink_rate) * Time.deltaTime;
//         FindObjectOfType<PlayerEat>().UpdateHealthUI();
//     }
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actions : MonoBehaviour
{
    public GameObject food;
    public GameObject throw_circle;
    public Transform position;
    public float shrink_rate = 0.1f;
    private PlayerEat player_eat;
    private ObjectGenerator generator;
    private float minScale = 0.5f; // Minimum scale threshold

    // Create black circle (throw), increase player speed, and decrease player size
    public void PlayerThrow()
    {
        if (transform.localScale.x < 1)
        {
            return;
        }

        Vector2 direction = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float zRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
        transform.rotation = Quaternion.Euler(0, 0, zRotation);

        GameObject b = Instantiate(throw_circle, position.position, Quaternion.identity);
        b.GetComponent<ObjectForce>().ApplyForce = true;

        // Decrease player size when throwing
        transform.localScale -= new Vector3(0.05f, 0.05f, 0.05f);
        CheckPlayerSize(); // Check if size is below threshold
    }

    // Start is called before the first frame update
    void Start()
    {
        player_eat = GetComponent<PlayerEat>();
        generator = ObjectGenerator.ins;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (transform.localScale.x <= 1)
        {
            return;
        }

        // Decrease the size of the player each frame
        transform.localScale -= new Vector3(shrink_rate, shrink_rate, shrink_rate) * Time.deltaTime;
        FindObjectOfType<PlayerEat>().UpdateHealthUI();

        CheckPlayerSize(); // Check size in each frame to ensure player dies if size falls below minScale
    }

    // Check if player size is below the minimum threshold
    private void CheckPlayerSize()
    {
        if (transform.localScale.x <= minScale)
        {
            player_eat.GameOver(); // Trigger GameOver if size is too small
        }
    }
}
