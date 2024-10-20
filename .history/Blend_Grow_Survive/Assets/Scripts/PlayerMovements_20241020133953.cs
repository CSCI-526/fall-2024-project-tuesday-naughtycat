// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class PlayerMovements : MonoBehaviour
// {

//     Actions actions;
//     ObjectGenerator generator;
//     public float speed = 5f;
//     public GameObject bullet;

//     // Start is called before the first frame update
//     void Start()
//     {
//         actions = GetComponent<Actions>();
//         generator = ObjectGenerator.ins;
//         generator.players.Add(gameObject);
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         // Player move with WSAD keys
//         float Speed = speed / transform.localScale.x;
//         Vector2 moveDirection = Vector2.zero;

//         if (Input.GetKey(KeyCode.W))
//         {
//             moveDirection.y += 1; // Move up
//         }
//         if (Input.GetKey(KeyCode.S))
//         {
//             moveDirection.y -= 1; // Move down
//         }
//         if (Input.GetKey(KeyCode.A))
//         {
//             moveDirection.x -= 1; // Move left
//         }
//         if (Input.GetKey(KeyCode.D))
//         {
//             moveDirection.x += 1; // Move right
//         }

//         // Normalize the movement direction to maintain consistent speed when moving diagonally
//         moveDirection = moveDirection.normalized;

//         // Apply movement based on input
//         transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + moveDirection, Speed * Time.deltaTime);

//         if (Input.GetKey(KeyCode.Space))
//         {
//             actions.PlayerThrow();
//         }



//     }

// }


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    Actions actions;
    ObjectGenerator generator;
    public float speed = 5f;
    public GameObject bullet;
    public float radius = 0.5f; // Radius for collision detection (adjust based on player size)

    // Start is called before the first frame update
    void Start()
    {
        actions = GetComponent<Actions>();
        generator = ObjectGenerator.ins;
        generator.players.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        // Player move with WSAD keys
        float Speed = speed / transform.localScale.x;
        Vector2 moveDirection = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            moveDirection.y += 1; // Move up
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection.y -= 1; // Move down
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection.x -= 1; // Move left
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection.x += 1; // Move right
        }

        // Normalize the movement direction to maintain consistent speed when moving diagonally
        moveDirection = moveDirection.normalized;

        // Calculate new position
        Vector2 newPosition = (Vector2)transform.position + moveDirection * Speed * Time.deltaTime;

        // Check for collisions at the new position
        if (!IsColliding(newPosition))
        {
            // Apply movement if no collision
            transform.position = newPosition;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            actions.PlayerThrow();
        }
    }

    // Function to check for collisions at a given position
    bool IsColliding(Vector2 targetPosition)
    {
        // Use OverlapCircle to check for collisions with obstacles
        Collider2D hit = Physics2D.OverlapCircle(targetPosition, radius, obstacleLayer);
        return hit != null; // Return true if there is a collision
    }
}
