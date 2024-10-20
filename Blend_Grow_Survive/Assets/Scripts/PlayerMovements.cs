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
        //player move with the mouse pointer
        // If press the W key, throw something
        float Speed_ = speed / transform.localScale.x;
        Vector2 Direction = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = Vector2.MoveTowards(transform.position, Direction, Speed_ * Time.deltaTime);
        if (Input.GetKey(KeyCode.W))
        {
            actions.PlayerThrow();
        }
    }
}