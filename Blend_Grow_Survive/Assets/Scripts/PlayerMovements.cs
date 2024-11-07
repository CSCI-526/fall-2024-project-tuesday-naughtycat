using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    Actions actions;
    ObjectGenerator generator;
    public float speed = 1f;
    public GameObject bullet;


    private Vector2 lastMoveDirection;  // Track the last movement direction
    private bool canMove = true;        // Flag to control movement based on collision

    // Start is called before the first frame update
    void Start()
    {
        actions = GetComponent<Actions>();
        generator = ObjectGenerator.ins;
        generator.players.Add(gameObject);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (canMove)  // Only allow movement if not colliding with a wall
        {
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

            // Apply movement based on input
            GetComponent<Rigidbody2D>().position = Vector2.MoveTowards(GetComponent<Rigidbody2D>().position, (Vector2)GetComponent<Rigidbody2D>().position + moveDirection, Speed * Time.deltaTime);

            // Store the last valid movement direction
            lastMoveDirection = moveDirection;
        }

        if (Input.GetKey(KeyCode.Space))
        {

            actions.PlayerThrow();  // Handle player throw when space is presse
            
        }

    }

    // Public method to stop player movement upon wall collision
    public void StopMovement()
    {
        canMove = false;
    }

    // Public method to resume player movement after leaving the wall
    public void ResumeMovement()
    {
        canMove = true;
    }
}