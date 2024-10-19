using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // control the player position if it is out of bounds
        if (transform.position.y >= 48)
        {
            transform.position = new Vector3(transform.position.x, 48, 0);
        }
        else if(transform.position.y <= -48)
        {
            transform.position = new Vector3(transform.position.x,-48, 0);
        }

        if (transform.position.x >= 48)
        {
            transform.position = new Vector3(48, transform.position.y,0);
        }
        else if(transform.position.x <= -48)
        {
            transform.position = new Vector3(-48, transform.position.y,0);
        }
    }
}
