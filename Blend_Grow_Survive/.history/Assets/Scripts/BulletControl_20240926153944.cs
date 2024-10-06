using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    [Range(1,10)]
    [SerializeFielf] private float speed = 10f;
    [Range(1,10)]
    [SerialField] private float lifetime = 3f;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    private void FixedUpdate() {
        rb.velocity = transform.up * speed;
    }
}
