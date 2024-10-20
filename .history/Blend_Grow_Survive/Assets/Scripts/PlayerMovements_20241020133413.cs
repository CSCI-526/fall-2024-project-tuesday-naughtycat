using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{

    Actions actions;
    ObjectGenerator generator;
    public float speed = 5f;
    public GameObject bullet;

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

        // Apply movement based on input
        transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + moveDirection, Speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.Space))
        {
            actions.PlayerThrow();
        }



    }

}
