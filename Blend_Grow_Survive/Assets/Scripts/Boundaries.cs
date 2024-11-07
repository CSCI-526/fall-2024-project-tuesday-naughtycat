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
            if (transform.position.y >= 9.5)
            {
                transform.position = new Vector3(transform.position.x, (float)9.5, 0);
            }
            else if (transform.position.y <= -9.5)
            {
                transform.position = new Vector3(transform.position.x, -(float)9.5, 0);
            }

            if (transform.position.x >= 9.5)
            {
                transform.position = new Vector3((float)9.5, transform.position.y, 0);
            }
            else if (transform.position.x <= -9.5)
            {
                transform.position = new Vector3(-(float)9.5, transform.position.y, 0);
            }

        }
        else
        {
            if (transform.position.y >= 48)
            {
                transform.position = new Vector3(transform.position.x, 48, 0);
            }
            else if (transform.position.y <= -48)
            {
                transform.position = new Vector3(transform.position.x, -48, 0);
            }

            if (transform.position.x >= 48)
            {
                transform.position = new Vector3(48, transform.position.y, 0);
            }
            else if (transform.position.x <= -48)
            {
                transform.position = new Vector3(-48, transform.position.y, 0);
            }

        }
            // control the player position if it is out of bounds
        
    }
}
