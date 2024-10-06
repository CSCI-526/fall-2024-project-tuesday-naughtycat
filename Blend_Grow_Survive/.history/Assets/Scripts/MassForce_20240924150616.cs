using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassForce : MonoBehaviour
{

    public bool ApplyForce = false;

    public float Speed = 20f;
    public float LoseSpeed = 100f;

    public float RandomRotation = 10f;
    public float RandomeForce = 3f;

    // Boundary limits
    private float boundaryX = 10f;
    private float boundaryY = 10f;

    // Start is called before the first frame update
    void Start()
    {
        if (ApplyForce == false)
        {
            enabled = false;
            return;
        }

        // calculate and apply rotation
        Vector2 Direction = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float zr = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg + 90f;

        zr += Random.Range(-RandomRotation, RandomRotation);

        transform.rotation = Quaternion.Euler(0, 0, zr);


        Speed += Random.Range(-RandomeForce, RandomeForce);

    }

    // Update is called once per frame
    void Update()
    {
        // Move the mass
        transform.Translate(Vector2.up * Speed * Time.deltaTime);
        Speed -= LoseSpeed * Time.deltaTime;

        // Clamp the position within the 20x20 boundary (-10 to 10 on both axes)
        float clampedX = Mathf.Clamp(transform.position.x, -boundaryX, boundaryX);
        float clampedY = Mathf.Clamp(transform.position.y, -boundaryY, boundaryY);
        transform.position = new Vector2(clampedX, clampedY);

        // Check if the mass has hit the boundary
        if (Mathf.Abs(transform.position.x) >= boundaryX || Mathf.Abs(transform.position.y) >= boundaryY)
        {
            Speed = 0f; // Stop the mass movement
            enabled = false; // Disable further updates
        }

        if (Speed <= 0f)
        {
            enabled = false;
        }
    }
}