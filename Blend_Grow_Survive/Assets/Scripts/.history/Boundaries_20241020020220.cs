// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Boundaries : MonoBehaviour
// {
//     // Update is called once per frame
//     void Update()
//     {
//         // control the player position if it is out of bounds
//         if (transform.position.y >= 25)
//         {
//             transform.position = new Vector3(transform.position.x, 25, 0);
//         }
//         else if (transform.position.y <= -25)
//         {
//             transform.position = new Vector3(transform.position.x, -25, 0);
//         }

//         if (transform.position.x >= 25)
//         {
//             transform.position = new Vector3(25, transform.position.y, 0);
//         }
//         else if (transform.position.x <= -25)
//         {
//             transform.position = new Vector3(-25, transform.position.y, 0);
//         }
//     }
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    private float radius;

    // Update is called once per frame
    void Update()
    {
        // Get the current radius of the circle dynamically
        radius = GetComponent<Renderer>().bounds.extents.x; // Assuming a uniform scale for the circle

        // control the player position if it is out of bounds, considering the dynamic radius
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

