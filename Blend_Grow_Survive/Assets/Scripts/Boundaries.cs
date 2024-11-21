using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    public int isTutorial;
    // Update is called once per frame
    void Update()
    {
        if (isTutorial == 1)
        {
            if (transform.position.y >= 7)
            {
                transform.position = new Vector3(transform.position.x, 7, 0);
            }
            else if (transform.position.y <= -7)
            {
                transform.position = new Vector3(transform.position.x, -7, 0);
            }

            if (transform.position.x >= 7)
            {
                transform.position = new Vector3(7, transform.position.y, 0);
            }
            else if (transform.position.x <= -7)
            {
                transform.position = new Vector3(-7, transform.position.y, 0);
            }

        }
        else
        {
            if (transform.position.y >= 49)
            {
                transform.position = new Vector3(transform.position.x, 49, 0);
            }
            else if (transform.position.y <= -49)
            {
                transform.position = new Vector3(transform.position.x, -49, 0);
            }

            if (transform.position.x >= 49)
            {
                transform.position = new Vector3(49, transform.position.y, 0);
            }
            else if (transform.position.x <= -49)
            {
                transform.position = new Vector3(-49, transform.position.y, 0);
            }

        }
            // control the player position if it is out of bounds
        
    }
}
