
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    private float radius;

    void Update()
    {

        radius = GetComponent<Renderer>().bounds.extents.x;

        if (transform.position.y >= 25 - radius)
        {
            transform.position = new Vector3(transform.position.x, 25 - radius, 0);
        }
        else if (transform.position.y <= -25 + radius)
        {
            transform.position = new Vector3(transform.position.x, -25 + radius, 0);
        }

        if (transform.position.x >= 25 - radius)
        {
            transform.position = new Vector3(25 - radius, transform.position.y, 0);
        }
        else if (transform.position.x <= -25 + radius)
        {
            transform.position = new Vector3(-25 + radius, transform.position.y, 0);
        }
    }
}

